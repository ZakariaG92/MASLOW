const TuyAPI = require('tuyapi');

async function ThingSwitchStatus(req, res) {

    try{

      console.log("Changing status")
      let thingId = req.body.id;
      let thingKey = req.body.key;
      let dps = req.body.dps;
      let value = req.body.value;
  
  
  
      const device = new TuyAPI({
          id: thingId,
          key: thingKey,
          issueGetOnConnect: false
      });

      await device.find();

      await device.connect();

      await device.set({dps: dps, set: value});

      let status = await device.get({dps: dps});

      device.disconnect();

    var returnJson = {
      "id": thingId,
      "key": thingKey,
      "status": status
    };

    console.log(returnJson);
    return res.json(returnJson);

  }catch(e){
      console.log(e);
      return res.status(400).json({
        error: e.stack
      })
  }

}

async function getThingId(req, res) {
  try{

    console.log("Get status")
    let thingId = req.params.id;
    let thingKey = req.params.key;
    let dps = req.params.dps;

    const device = new TuyAPI({
        id: thingId,
        key: thingKey,
        issueGetOnConnect: false
    });

    await device.find();

    await device.connect();

    let status = await device.get({dps: dps});

    device.disconnect();

    var returnJson = {
      "id": thingId,
      "key": thingKey,
      "status": status
    };

    console.log(returnJson);
    return res.json(returnJson);

  }catch(e){
      console.log(e);
      return res.status(400).json({
        error: e.stack
      })
  }
}

module.exports = { ThingSwitchStatus, getThingId };
