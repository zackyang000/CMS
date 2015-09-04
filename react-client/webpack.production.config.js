var path = require('path');
var webpack = require('webpack');

module.exports = {
  entry: {
    app: path.resolve(__dirname, 'src/index.js'),
    vendors: [
      'babel-core/polyfill',
      'react', 
      'react-mixin', 
      'react-bootstrap',
      'react-redux', 
      'redux-thunk', 
      'redux-logger', 
      'redux-diff-logger',
      'redux-devtools',
      'isomorphic-fetch'
    ]
  },
  output: {
    path: path.join(__dirname, 'build'),
    filename: 'index.js',
    publicPath: '/'
  },
  plugins: [
    new webpack.optimize.CommonsChunkPlugin('vendors', 'vendors.js')
  ],
  module: {
    loaders: [
      {
        test: /\.js$/,
        loaders: ['babel'],
        exclude: /node_modules/,
        include: __dirname
      },
      {
        test: /\.scss$/,
        loader: 'style!css!sass'
      }
    ]
  }
};

