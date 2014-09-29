#dependencies
express = require("express")
cors = require('cors')
http = require("http")
path = require("path")
mongoose = require("mongoose")
fs = require("fs")
mkdirp = require('mkdirp')
errorHandler = require('errorhandler')
morgan = require('morgan')

app = express()
config = require("./config/config")
domainError = require("./middleware/domainError")

createUploadDirectory = ->
  mkdirp(item) for item in ['./static/upload/temp', './static/upload/gallery']

require("./bootstrap/registerModels")()

#express init
createUploadDirectory()
uploadPath = path.join(path.dirname(__dirname), 'server/static/upload/temp')
app.use cors({ exposedHeaders: "authorization" })
app.use express.bodyParser({uploadDir : uploadPath})
app.use express.methodOverride()
app.use require("./middleware/authorization")()
app.use morgan("short")
app.use domainError()
app.use errorHandler()
#application init
app.use(express["static"](path.join(__dirname, "./static")))
require("./bootstrap/registerREST")(app)

#db init
mongoose.connect config.db

#import test-data
#require("./bootstrap/test-data/init")()

#start web server
app.listen(process.env.PORT or config.port or 30000)