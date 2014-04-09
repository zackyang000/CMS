
#Module dependencies.
express = require("express")
http = require("http")
path = require("path")
app = express()
config = require("./config/config")
mongoose = require('mongoose')

#All environments
mongoose.connect(config.db)
app.set "port", process.env.PORT or config.port or 33000
app.use express.favicon(path.join(__dirname, 'public/img/favicon.ico'))
app.use express.logger('dev')  #'default', 'short', 'tiny', 'dev'
app.use express.bodyParser()
app.use express.methodOverride()
app.use express.static(path.join(__dirname, "public"))
app.use app.router

require("./models/user")
require("./routes/users")(app)

app.get('/admin*', (req, res) ->
  res.sendfile('public/admin-index.html');
)

app.get('*', (req, res) ->
  res.sendfile('public/index.html');
)

http.createServer(app).listen app.get("port"), ->
  console.log("Express server listening on port " + app.get("port"))

