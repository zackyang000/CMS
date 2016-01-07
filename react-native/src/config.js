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

module.exports = { 
  platform: 'mobile'
};
