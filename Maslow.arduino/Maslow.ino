#include <WiFi.h>
#include <PubSubClient.h>
#include <ArduinoJson.h> 
#include <Wire.h>
#include "OneWire.h"
#include "DallasTemperature.h"
#include "net_misc.h"

/*============= thing ids ======================*/
#define ID_RELAY_02  "3b5708b079f247938f4096ee057fbc01"
#define ID_RELAY_04  "6e72fcb15903465fafa3f28c9e5d0d1c"
#define ID_PHOTORESISTOR  "6b13a7131fe54e1096a7aac9ea77bb9d"
#define ID_TEMPERATURE "75fcc4da231942739d04dd56874f3bd9"


/*============= GPIO ======================*/
const int ledPin = 22; // LED Pin
const int photo_resistor_pin =A7; //Photoresistor 6b13a7131fe54e1096a7aac9ea77bb9d
#define PIN_RELAY_02 A5 //thing 3b5708b079f247938f4096ee057fbc01
#define PIN_RELAY_04 A4 //thing 6e72fcb15903465fafa3f28c9e5d0d1c


OneWire oneWire(23);
DallasTemperature tempSensor(&oneWire);  // 75fcc4da231942739d04dd56874f3bd9

WiFiClient espClient; // Wifi
PubSubClient client(espClient) ; // MQTT client

String whoamiTemp; // Identification de CET ESP au sein de la flotte
String whoamiLight; // Identification de CET ESP au sein de la flotte
String id_relay_02;  
String id_relay_04;


//StaticJsonBuffer<200> jsonBuffer;

/*===== MQTT broker/server and TOPICS ========*/
//const char* mqtt_server = "192.168.1.100";
const char* mqtt_server = "broker.hivemq.com";

#define TOPIC_TEMP "maslow/sensors/75fcc4da231942739d04dd56874f3bd9"
#define TOPIC_LED "maslow/sensors/led"
#define TOPIC_LIGHT "maslow/sensors/6b13a7131fe54e1096a7aac9ea77bb9d"
#define TOPIC_CALLBACK "callback/maslow"

#define TOPIC_RELAY_02 "maslow/thing/3b5708b079f247938f4096ee057fbc01"
#define TOPIC_RELAY_04 "maslow/thing/6e72fcb15903465fafa3f28c9e5d0d1c"

/*=============== SETUP =====================*/

void setup () {
  // Gpio
  //pinMode (ledPin , OUTPUT);
  pinMode (PIN_RELAY_02 , OUTPUT);
  pinMode (PIN_RELAY_04 , OUTPUT);


  // Serial
  Serial.begin (9600);
  
  /* Wifi */
  connect_wifi();
  
  /*  L'ESP est un client du mqtt_server */
  client.setServer(mqtt_server, 1883);
  // set callback when publishes arrive for the subscribed topic
  // methode a effet local => on n'a pas a initier/verifier la connection.
  client.setCallback(mqtt_pubcallback);


  /* Choix d'une identification pour cet ESP ---*/
 
   whoamiTemp = "75fcc4da231942739d04dd56874f3bd9"; 
   whoamiLight="6b13a7131fe54e1096a7aac9ea77bb9d";
   id_relay_02= "3b5708b079f247938f4096ee057fbc01";
   id_relay_04="6e72fcb15903465fafa3f28c9e5d0d1c";
  //whoami =  String(WiFi.macAddress());
}

/*============== MQTT CALLBACK ===================*/

void mqtt_pubcallback(char* topic, byte* message, unsigned int length) {
  /* 
   *  Callback if a message is published on this topic.
   */
   char data[80];
  String payload;
  
  // Byte list to String ... plus facile a traiter ensuite !
  // Mais sans doute pas optimal en performance => heap ?
  String messageTemp ;
  for(int i = 0 ; i < length ; i++) {
    messageTemp += (char) message[i];
  }
  
  Serial.print("Message : ");
  Serial.println(messageTemp);
  Serial.print("arrived on topic : ");
  Serial.println(topic) ;
 
  // Analyse du message et Action 
  if(String (topic) == TOPIC_CALLBACK) {
     // Par exemple : Changes the LED output state according to the message   
    Serial.print("Action : Changing output to ");
    
    if(messageTemp == "3b5708b079f247938f4096ee057fbc01/on") {
      Serial.println("on");
  
      digitalWrite(PIN_RELAY_02, LOW);

      delay(750);

          payload = "{\"thing\": \"";
  payload += id_relay_02;   
  payload += "\", \"value\": " ;
  payload += get_pin(PIN_RELAY_02); 
  payload += "}";
  
  payload.toCharArray(data, (payload.length() + 1)); // Convert String payload to a char array
  Serial.println(data);
  client.publish(TOPIC_RELAY_02, data);  // publish it 
      
     
    } else if (messageTemp == "3b5708b079f247938f4096ee057fbc01/off") {
      Serial.println("offlamp");
      digitalWrite(PIN_RELAY_02, HIGH);

       delay(750);

          payload = "{\"thing\": \"";
  payload += id_relay_02;   
  payload += "\", \"value\": " ;
  payload += get_pin(PIN_RELAY_02); 
  payload += "}";
  
  payload.toCharArray(data, (payload.length() + 1)); // Convert String payload to a char array
  Serial.println(data);
  client.publish(TOPIC_RELAY_02, data);  // publish it 
    }


     if(messageTemp == "6e72fcb15903465fafa3f28c9e5d0d1c/on") {
      Serial.println("on");
  
      digitalWrite(PIN_RELAY_04, LOW);

       delay(750);

          payload = "{\"thing\": \"";
  payload += id_relay_04;   
  payload += "\", \"value\": " ;
  payload += get_pin(PIN_RELAY_04); 
  payload += "}";
  
  payload.toCharArray(data, (payload.length() + 1)); // Convert String payload to a char array
  Serial.println(data);
  client.publish(TOPIC_RELAY_04, data);  // publish it 
     
    } else if (messageTemp == "6e72fcb15903465fafa3f28c9e5d0d1c/off") {
      Serial.println("offlamp");
      digitalWrite(PIN_RELAY_04, HIGH);

       delay(750);

          payload = "{\"thing\": \"";
  payload += id_relay_04;   
  payload += "\", \"value\": " ;
  payload += get_pin(PIN_RELAY_04); 
  payload += "}";
  
  payload.toCharArray(data, (payload.length() + 1)); // Convert String payload to a char array
  Serial.println(data);
  client.publish(TOPIC_RELAY_04, data);  // publish it 
    }

    // else if (messageTemp == "3b5708b079f247938f4096ee057fbc01") {
    //   Serial.println("change state of 3b5708b079f247938f4096ee057fbc01");
    //   state=get_pin(PIN_RELAY_02);
    //   if (state=="LOW")
    //   {
    //     Serial.println("change state of 3b5708b079f247938f4096ee057fbc01 to ON");
    //     set_pin(PIN_RELAY_02,HIGH );
    //   }
    //   else
    //   {
    //     Serial.println("change state of 3b5708b079f247938f4096ee057fbc01 to OFF");
    //     set_pin(ledPin,LOW);
    //   }
      
    // }

    else if (messageTemp == "75fcc4da231942739d04dd56874f3bd9/temp") {
      Serial.println("---get temp---");

       payload = "{\"sensor\": \"";
  payload += whoamiTemp;   
  payload += "\", \"value\": " ;
  payload += get_temperature(); 
  payload += "}";
  
  payload.toCharArray(data, (payload.length() + 1)); // Convert String payload to a char array
  Serial.println(data);
  client.publish(TOPIC_TEMP, data);  // publish it 
      
    }
    else if (messageTemp == "6b13a7131fe54e1096a7aac9ea77bb9d/light") {
      Serial.println("--get photoresistor----");

       payload = "{\"sensor\": \"" + whoamiLight + "\", \"value\": " + get_light() + "}";
  payload.toCharArray(data, (payload.length() + 1));
  Serial.println(data);
  client.publish(TOPIC_LIGHT, data);
      
    }
  }
  
}





/*============= MQTT SUBSCRIBE =====================*/

void mqtt_mysubscribe(char* topic) {
  /*
   * ESP souscrit a ce topic. Il faut qu'il soit connecte.
   */
  while(!client.connected()) { // Loop until we are reconnected
    Serial.print("Attempting MQTT connection...");
    if(client.connect("esp32", "try", "try")) { // Attempt to connect 
      Serial.println("connected");
      client.subscribe(topic); // and then Subscribe
    } else { // Connection failed
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println("try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5*1000);
    }
  }
}



/*============= ACCESSEURS ====================*/

float get_temperature() {
  float temperature;
  tempSensor.requestTemperaturesByIndex(0);
  delay (750);
  temperature = tempSensor.getTempCByIndex(0);
  return temperature;
}

float get_light(){
  return analogRead(photo_resistor_pin);
}

void set_pin(int pin, int val){
 digitalWrite(pin, val) ;
}

int get_pin(int pin){
  return digitalRead(pin);
}


/*================= LOOP ======================*/
void loop () {
  char data[80];
  String payload; // Payload : "JSON ready" 
  int32_t period = 30 * 100; // Publication period
  
  /* Subscribe to TOPIC_LED if not yet ! */
  if (!client.connected()) {
    mqtt_mysubscribe((char*) (TOPIC_CALLBACK));
  }
  
  /* Publish Temperature & Light periodically */
 

  /* char tempString[8];
     dtostrf(temperature, 1, 2, tempString);
     client.publish(TOPIC_TEMP, tempString); */



  Serial.println("waiting mqtt");
  
  client.loop();


  

  delay(period);
  client.loop(); // Process MQTT ... obligatoire une fois par loop()
}
