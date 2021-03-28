var express = require('express'),
  app = express(),
  port = process.env.PORT || 3000;
  
  let bodyParser = require('body-parser');
  let client = require('./api/routes/thingRoute');


  // parse requests of content-type - application/json
app.use(bodyParser.json());

// parse requests of content-type - application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }));

// Pour accepter les connexions cross-domain (CORS)
app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    res.header("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    next();
});

// Pour les formulaires
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// les routes
const prefix = '/api';

app.route(prefix + '/things/switch')
    .post(client.ThingSwitchStatus)

app.route(prefix + '/things/id/:id')
    .get(client.getThingId)

app.listen(port, "0.0.0.0");

const options = {
    useNewUrlParser: true,
    useUnifiedTopology: true,
    useFindAndModify: false
};

console.log('Iot control RESTful API server started on: ' + port);
module.exports = app;