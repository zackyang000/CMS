#dependencies
odata = require 'node-odata'
cors = require 'cors'
path = require "path"
fs = require 'fs'
mkdirp = require 'mkdirp'
errorHandler = require 'errorhandler'
morgan = require 'morgan'
domainError = require './middleware/domainError'

server = odata('mongodb://localhost/cms')

createUploadDirectory = ->
  mkdirp(item) for item in [
    './static/upload/temp'
    './static/upload/gallery'
  ]

# odata init
createUploadDirectory()
server.use cors exposedHeaders: "authorization"
server.use odata.express.bodyParser({uploadDir : path.join(path.dirname(__dirname), 'server/static/upload/temp')})
server.use require("./middleware/authorization")
server.use morgan("short")
server.use domainError()
server.use errorHandler()

#application init
server.use(odata.express["static"](path.join(__dirname, "./static")))

require('./odata').setup(server)

#import test-data
require("./bootstrap/test-data/init")()

#start web server
server.listen(process.env.PORT or 30002)
