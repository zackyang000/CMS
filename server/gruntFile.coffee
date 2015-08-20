module.exports = (grunt) ->
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  debug = !grunt.option("release")
  config = if debug then '../config/config.dev.json' else '../config/config.prd.json'

  grunt.initConfig
    nodemon:
      server:
        script: "index.js"
        options:
          watch: '.'
          args: []
          ext: "js,json,html"
          cwd: '_dist'

    clean:
      all:
        src: "_dist/**/*"

    copy:
      server:
        files: [
          expand: true
          cwd: 'src/'
          src: [
            '**/*'
          ]
          dest: '_dist'
        ]
      packageFile:
        files: [
          expand: true
          cwd: ''
          src: 'package.json'
          dest: '_dist'
        ]
      configFile:
        files: [
          expand: true
          cwd: ''
          src: if debug then '../config.dev.json' else '../config.prd.json'
          dest: '_dist/_dist'
          ext: '.json'
        ]

    watch:
      server:
        files: ['src/**/*']
        tasks: ['newer:copy:server']

    concurrent:
      tasks: ['nodemon', 'watch']
      options:
        logConcurrentOutput: true

  grunt.registerTask "default", [
    "clean"
    "copy"
    'concurrent'
  ]
