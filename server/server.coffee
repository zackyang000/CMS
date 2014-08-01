#dependencies
express = require("express")
http = require("http")
path = require("path")
app = express()
config = require("./config/config")
mongoose = require("mongoose")
fs = require("fs")
mkdirp = require('mkdirp')

generateUploadDirectory = ->
  mkdirp(item) for item in [
    '../client/upload/temp'
    '../client/upload/gallery'
  ]



#db init
mongoose.connect config.db

#express init
#app.use(express.logger('dev')); /*'default', 'short', 'tiny', 'dev'*/
generateUploadDirectory()
uploadPath = path.join(path.dirname(__dirname), 'client/upload/temp')
app.use(express.bodyParser({uploadDir : uploadPath}))
app.use(express.methodOverride())
app.use(express.favicon(path.join(__dirname, "../client/img/favicon.ico")))
app.use(express["static"](path.join(__dirname, "../client")))

#application init
require("./bootstrap/registerModels")()
require("./bootstrap/registerRewrite")(app)
require("./bootstrap/registerAPIs")(app)

#data init
require("./bootstrap/db/init")()

#start server
app.listen(process.env.PORT or config.port or 30000)