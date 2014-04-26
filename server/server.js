//dependencies
var express = require("express"),
  http = require("http"),
  path = require("path"),
  app = express(),
  config = require("./config/config"),
  mongoose = require('mongoose');

//db init
mongoose.connect(config.db);

//express init
app.use(express.logger('dev')); /*'default', 'short', 'tiny', 'dev'*/
app.use(express.bodyParser());
app.use(express.methodOverride());
app.use(express.favicon(path.join(__dirname, '../client/img/favicon.ico')));
app.use(express["static"](path.join(__dirname, "../client")));

//application init
require('./bootstrap/registerModels')();
require('./bootstrap/registerRewrite')(app);
require('./bootstrap/registerAPIs')(app);

app.listen(process.env.PORT || config.port || 30000);

