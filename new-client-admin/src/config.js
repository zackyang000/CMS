require('babel-polyfill');

const environment = {
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
  site: {
    title: 'CMS',
  },
  port: 43200,
  html5Mode: true
}, environment);
