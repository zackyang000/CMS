var webpack = require('webpack');
var WebpackDevServer = require('webpack-dev-server');
var config = require('./webpack.development.config');

new WebpackDevServer(webpack(config), {
  contentBase: "./src",
  publicPath: config.output.publicPath,
  hot: true,
  historyApiFallback: true,
  stats: {
    colors: true
  },
  headers: {"Access-Control-Allow-Origin": "*"}
}).listen(50000, 'localhost', function (err) {
  if (err) {
    console.log(err);
  }
  console.log('✅  Server is listening at http://localhost:50000');
});
