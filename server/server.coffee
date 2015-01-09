#dependencies
cors = require('cors')
http = require("http")
path = require("path")
fs = require("fs")
mkdirp = require('mkdirp')
errorHandler = require('errorhandler')
morgan = require('morgan')

odata = require("./odata")
app = require("node-odata")._app
express = require("node-odata")._express

domainError = require("./middleware/domainError")

createUploadDirectory = ->
  mkdirp(item) for item in ['./static/upload/temp', './static/upload/gallery']

#express init
createUploadDirectory()
app.use cors({ exposedHeaders: "authorization" })
app.use express.bodyParser({uploadDir : path.join(path.dirname(__dirname), 'server/static/upload/temp')})
app.use require("./middleware/authorization")
app.use morgan("short")
app.use domainError()
app.use errorHandler()

#application init
app.use(express["static"](path.join(__dirname, "./static")))

odata.setup('mongodb://localhost/cms-dev')

#import test-data
require("./bootstrap/test-data/init")()

#start web server
app.listen(process.env.PORT or 30002)
