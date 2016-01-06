require('babel/polyfill');

var environment = {
  development: {
    apiRoot: 'http://local.xxx.alibaba-inc.com',
  },
  testing: {
    apiRoot: 'http://test.xxx.alibaba-inc.com',
  },
  production: {
    apiRoot: 'http://xxx.alibaba-inc.com',
  }
}[process.env.NODE_ENV || 'development'];

module.exports = Object.assign({
  port: 5000,
  //publicPath: 'http://xxx/{name}/{version}',
  theme: { cuz: '1.3.5' },
  html5Mode: true
}, environment);
