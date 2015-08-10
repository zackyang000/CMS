require('coffee-script/register');
require("./bootstrap/registerModels")();
mongoose = require('mongoose')

host = "http://www.woshinidezhu.com:80";

mongoose.connect("mongodb://127.0.0.1/cms-dev");
console.log("Loading data...");
require('./import-article')(host);
require('./import-board')(host);
require('./import-photo')(host);
require('./import-user')(host);