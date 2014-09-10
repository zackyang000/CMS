require('coffee-script/register');
require("./bootstrap/registerModels")();

//host = "http://www.woshinidezhu.com:80";
host = "http://192.168.192.174:8088";
db = "mongodb://127.0.0.1/cms-dev";

//require('./import-article')(host, db);
//require('./import-board')(host, db);
require('./import-photo')(host, db);