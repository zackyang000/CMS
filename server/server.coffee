#dependencies
express = require("express")
http = require("http")
path = require("path")
app = express()
config = require("./config/config")
mongoose = require('mongoose')

#db init
mongoose.connect(config.db)

#express init
app.set "port", process.env.PORT or config.port or 30000
app.use express.logger('dev')  #'default', 'short', 'tiny', 'dev'
app.use express.bodyParser()
app.use express.methodOverride()

#application init
require('./bootstrap/registerModels')()
require('./bootstrap/registerStaticFiles')(app)
require('./bootstrap/registerAPIs')(app, config.apiPrefix)


http.createServer(app).listen app.get("port"), ->
  console.log("Server listening on port " + app.get("port"))

