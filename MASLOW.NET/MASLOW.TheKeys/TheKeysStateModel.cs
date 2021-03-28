using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASLOW.TheKeys
{
    public class TheKeysStateModel
    {
        public int status { get; set; }
        public Data data { get; set; }
        public Message message { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string id_serrure { get; set; }
        public string code { get; set; }
        public string code_serrure { get; set; }
        public string etat { get; set; }
        public string nom { get; set; }
        public string qrcode { get; set; }
        public bool serrure_droite { get; set; }
        public bool main_libre { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
        public int radius { get; set; }
        public string timezone { get; set; }
        public int maxSpeed { get; set; }
        public int latchDelay { get; set; }
        public bool assistedActions { get; set; }
        public bool unlockOnly { get; set; }
        public object description { get; set; }
        public int version { get; set; }
        public int version_cible { get; set; }
        public int beta { get; set; }
        public Produit produit { get; set; }
        public Compte compte { get; set; }
        public Created_At created_at { get; set; }
        public Updated_At updated_at { get; set; }
        public int logSequence { get; set; }
        public Log[] logs { get; set; }
        public string public_key { get; set; }
        public string message { get; set; }
        public Accessoire[] accessoires { get; set; }
        public int battery { get; set; }
        public Battery_Date battery_date { get; set; }
        public int door { get; set; }
    }

    public class Produit
    {
        public int id { get; set; }
        public string nom { get; set; }
        public int version { get; set; }
        public int versionBeta { get; set; }
    }

    public class Compte
    {
        public int id { get; set; }
        public string nom { get; set; }
    }

    public class Created_At
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Updated_At
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Battery_Date
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Log
    {
        public int id { get; set; }
        public Action_At action_at { get; set; }
        public Created_At1 created_at { get; set; }
        public string action { get; set; }
        public Data1 data { get; set; }
        public int status { get; set; }
        public int share_id { get; set; }
        public string utilisateur { get; set; }
        public object utilisateur_id { get; set; }
        public int accessoire_id { get; set; }
    }

    public class Action_At
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Created_At1
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Data1
    {
        public int ts { get; set; }
    }

    public class Accessoire
    {
        public int id { get; set; }
        public Accessoire1 accessoire { get; set; }
        public object info { get; set; }
    }

    public class Accessoire1
    {
        public int id { get; set; }
        public string id_accessoire { get; set; }
        public string nom { get; set; }
        public int type { get; set; }
        public int version { get; set; }
        public int type_version { get; set; }
        public Info info { get; set; }
        public object[] configuration { get; set; }
    }

    public class Info
    {
        public string last_seen { get; set; }
        public string ip { get; set; }
    }

    public class Message
    {
        public object[] global { get; set; }
        public object[] form { get; set; }
    }
}
