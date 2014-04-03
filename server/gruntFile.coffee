module.exports = (grunt) ->

  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  #-----------------------------------------------------------------

  grunt.initConfig
    watch:
      js:
        files: [
          "gruntfile.js"
          "server.js"
          "server/**/*.js"
          "public/js/**"
          "test/**/*.js"
        ]
        tasks: ["jshint"]
        options:
          livereload: true
      html:
        files: [
          "public/views/**"
          "server/views/**"
        ]
        options:
          livereload: true
      css:
        files: ["public/css/**"]
        tasks: ["csslint"]
        options:
          livereload: true

    nodemon:
      dev:
        script: "server.js"
        options:
          args: []
          ignore: ["public/**"]
          ext: "js,html"
          nodeArgs: ["--debug"]
          delayTime: 1
          env:
            PORT: 3000
          cwd: __dirname

  grunt.registerTask('default', ['nodemon','watch']);