#dependencies
odata = require 'node-odata'
cors = require 'cors'
path = require "path"
errorHandler = require 'errorhandler'
morgan = require 'morgan'
domainError = require './middleware/domainError'

server = odata('mongodb://localhost/cms')

# save resouces for actions and functions.
odata.resources = server.resources

# odata config
server.use cors exposedHeaders: "authorization"
# server.use odata._express.bodyParser({uploadDir : path.join(path.dirname(__dirname), 'server/static/upload/temp')})
# server.use odata._express["static"](path.join(__dirname, "./static"))
server.use require("./middleware/authorization")
server.use morgan("short")
server.use domainError()
server.use errorHandler()

# init resources
server.use require('./resources/article/article')
server.use require('./resources/article/category')
server.use require('./resources/article/comment')
server.use require('./resources/gallery/gallery')
server.use require('./resources/board/board')
server.use require('./resources/system/user')

# init functions
server.use require('./functions/login')
server.use require('./functions/upload')

# start web server
server.listen(process.env.PORT or 40002, ->
  # import data
  require('./bootstrap/init-data').import()
  console.log("services has be started at localhost:40002")
)
