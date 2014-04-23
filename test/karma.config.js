// Karma configuration
// Generated on Tue Apr 15 2014 13:45:54 GMT+0800 (China Standard Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '../',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
      "dist/vendor/jquery/*.js",
      "dist/vendor/jquery-ui/*.js",
      "dist/vendor/jquery-plugin/*.js",
      "dist/vendor/bootstrap/*.js",
      "dist/vendor/bootstrap-plugin/*.js",
      "dist/vendor/angular/angular.js",
      "dist/vendor/angular/*.js",
      "dist/vendor/moment/moment.js",
      "dist/vendor/moment/*.js",
      "dist/vendor/messenger/messenger.js",
      "dist/vendor/messenger/*.js",
      "dist/vendor/*.js",
      "dist/vendor/*/*.js",
      "dist/vendor/*/*/*.js",
      "dist/vendor/*/*/*/*.js",
      "dist/vendor/**/*.js",
      "dist/config/**/*.js",
      "dist/common/**/*.js",
      "dist/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js",
      "dist/plugin/syntaxhighlighter_3.0.83/scripts/*.js",

      "dist/app/**/*.js",
      "dist/plugin/unify*/**/*.js",

      'test/angular-mocks.js',
      'test/unit/**/*.js'
    ],


    // list of files to exclude
    exclude: [
      
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
    
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['Chrome'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false
  });
};
