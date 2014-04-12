
#Module dependencies.
express = require("express")
http = require("http")
path = require("path")
app = express()
config = require("./config/config")
mongoose = require('mongoose')

#db init
mongoose.connect(config.db)

#express init
app.set "port", process.env.PORT or config.port or 33000
app.use express.favicon(path.join(__dirname, '../client/img/favicon.ico'))
app.use express.logger('dev')  #'default', 'short', 'tiny', 'dev'
app.use express.bodyParser()
app.use express.methodOverride()

#support static files.
app.use express.static(path.join(__dirname, "../client"))

app.get('/admin*', (req, res) ->
  res.sendfile(path.join(__dirname, '../client/admin-index.html'));
)

app.get(/(?!api).+/, (req, res) ->
  res.sendfile(path.join(__dirname, '../client/index.html'));
)

#route init
require('./bootstrap/registerModels')()
require('./bootstrap/registerAPIs')(app)

http.createServer(app).listen app.get("port"), ->
  console.log("Server listening on port " + app.get("port"))

