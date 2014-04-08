
#Module dependencies.
express = require("express")
http = require("http")
path = require("path")
app = express()
config = require("config/config")

#All environments
app.set "port", process.env.PORT or config.port or 33000
app.use express.favicon(path.join(__dirname, 'public/img/favicon.ico'))
#app.use express.logger('dev')  #'default', 'short', 'tiny', 'dev'
app.use express.bodyParser()
app.use express.methodOverride()
app.use express.static(path.join(__dirname, "public"))
app.use app.router

quotes = [
  { author : 'Audrey Hepburn', text : "Nothing is impossible, the word itself says 'I'm possible'!"},
  { author : 'Walt Disney', text : "You may not realize it when it happens, but a kick in the teeth may be the best thing in the world for you"},
  { author : 'Unknown', text : "Even the greatest was once a beginner. Don't be afraid to take that first step."},
  { author : 'Neale Donald Walsch', text : "You are afraid to die, and you're afraid to live. What a way to exist."}
];

app.get '/api/user', (req, res) ->
  res.json(quotes)

app.get('/admin*', (req, res) ->
  res.sendfile('public/admin-index.html');
)

app.get('*', (req, res) ->
  res.sendfile('public/index.html');
)

http.createServer(app).listen app.get("port"), ->
  console.log("Express server listening on port " + app.get("port"))

