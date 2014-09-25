#dependencies
express = require("express")
cors = require('cors')
http = require("http")
path = require("path")
mongoose = require("mongoose")
fs = require("fs")
mkdirp = require('mkdirp')

app = express()
config = require("./config/config")

createUploadDirectory = ->
  mkdirp(item) for item in ['../client/upload/temp', '../client/upload/gallery']


require("./bootstrap/registerModels")()

#express init
#app.use(express.logger('dev')); #'default', 'short', 'tiny', 'dev'
createUploadDirectory()
uploadPath = path.join(path.dirname(__dirname), 'client/upload/temp')
app.use(cors({ exposedHeaders: "authorization" }))
app.use(express.bodyParser({uploadDir : uploadPath}))
app.use(express.methodOverride())
app.use(require("./middleware/authorization")())
#app.use(express.favicon(path.join(__dirname, "../client/img/favicon.ico")))
#app.use(express["static"](path.join(__dirname, "../client")))

#application init
require("./bootstrap/registerAPIs")(app)
require("./bootstrap/registerREST")(app)

#db init
mongoose.connect config.db

#import test-data
#require("./bootstrap/test-data/init")()

#start web server
app.listen(process.env.PORT or config.port or 30000)