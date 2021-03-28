const TuyAPI = require('tuyapi');

function ThingSwitchStatus(req, res) {

    
    console.log("Changing status")
    let thingId = req.body.id;
    let thingKey = req.body.key;



    const device = new TuyAPI({
        id: thingId,
        key: thingKey
    });
    let stateHasChanged = false;

    // Find device on network
    device.find().then(() => {
        // Connect to device
        device.connect();
    });




/*
    (async () => {
        await device.find();

        await device.connect();

        let status = await device.get();
        let old= status; 

        console.log(`Current status: ${status}.`);

        await device.set({ set: !status });

        status = await device.get();

        let newStatus= status

        if (old!=newStatus){stateHasChanged=true}

        console.log(`New status: ${status}.`);
        
        var returnJson = {
            "id": thingId,
            "key": thingKey,
            "stateChanged": stateHasChanged
        };
    
    
        device.disconnect();
        console.log(returnJson)
        res.json(returnJson)

        
    })();
    */

    
// Find device on network
device.find().then(() => {
    // Connect to device
    device.connect();
  });
  
  // Add event listeners
  device.on('connected', () => {
    console.log('Connected to device!');
  });
  
  device.on('disconnected', () => {
    console.log('Disconnected from device.');
  });
  
  device.on('error', error => {
    console.log('Error!', error);
  });
  
  device.on('data', data => {
    console.log('Data from device:', data);
  
    console.log(`Boolean status of default property: ${data.dps['1']}.`);
  
    // Set default property to opposite
    if (!stateHasChanged) {
      device.set({set: !(data.dps['1'])});
  
      // Otherwise we'll be stuck in an endless
      // loop of toggling the state.
      stateHasChanged = true;
    }
  });
  
  // Disconnect after 10 seconds
 // setTimeout(() => { device.disconnect(); }, 1000);

  var returnJson = {
    "id": thingId,
    "key": thingKey,
    "stateChanged": stateHasChanged
}
res.json(returnJson)

}


function getThingId(req, res) {
    console.log("things id")
    let thingId = req.params.id;

    returnJson = {
        "id": thingId,
        "key": "key"
    };

    console.log(returnJson)
    res.json(returnJson)
}

module.exports = { ThingSwitchStatus, getThingId };
