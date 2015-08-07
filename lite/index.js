var express = require('express');
var cons = require('consolidate');
var request = require('request');

var app = express();

app.engine('html', cons.handlebars);

app.set('view engine', 'html');
app.set('views', __dirname + '/views');

app.get('/', function(req, res){
  request({ url: 'http://localhost:40002/article?$top=10&$select=title,date', json: true }, function(error, response, body) {
    res.render('index', { articles: body.value });
  });
});

app.listen(3000);
