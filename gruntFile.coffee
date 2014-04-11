module.exports = (grunt) ->
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)
  debug = grunt.option("release") isnt true

  grunt.initConfig
    assets: grunt.file.readJSON('server/config/assets.json')

    nodemon:
      dev:
        script: "server.js"
        options:
          args: []
          ext: "js,html"
          nodeArgs: ["--debug"]
          delayTime: 1
          env:
            PORT: 30000
          cwd: '_dist/server'

    open:
      server:
        url:'http://localhost:30000'

    watch:
      options:
        livereload: 30001
      clientFile:
        files: ['client/**/*','!client/**/*.coffee','!client/**/*.less']
        tasks: ['newer:copy:client','sails-linker','replace:livereload']
      clientCoffee:
        files: ['client/**/*.coffee']
        tasks: ['newer:coffee:client','sails-linker','replace:livereload']
      clientLess:
        files: ['client/**/*.less']
        tasks: ['newer:less','sails-linker','replace:livereload']
      serverFile:
        files: ['server/**/*','!server/**/*.coffee']
        tasks: ['newer:copy:server']
      serverCoffee:
        files: ['server/**/*.coffee']
        tasks: ['newer:coffee:server']

    coffee:
      options:
        bare: true
      client:
        files: [
          expand: true
          cwd: 'client/'
          src: ['**/*.coffee']
          dest: '_dist/client/'
          ext: '.js'
        ]
      server:
        files: [
          expand: true
          cwd: 'server/'
          src: ['**/*.coffee']
          dest: '_dist/server/'
          ext: '.js'
        ]

    less:
      client:
        files: [
          expand: true
          cwd: 'client/'
          src: ['**/*.less']
          dest: '_dist/client'
          ext: '.css'
        ]

    uglify:
      #options:
        #mangle: false
        #beautify: true
      production:
        files:
          '_dist/client/index.js': ["<%= assets.js %>", "<%= assets.commonJs %>"]
          '_dist/client/admin-index.js': ["<%= assets.adminJs %>", "<%= assets.commonJs %>"]

    cssmin:
      production:
        files:
          '_dist/client/index.css': ["<%= assets.css %>", "<%= assets.commonCss %>"]
          '_dist/client/admin-index.css': ["<%= assets.adminCss %>", "<%= assets.commonCss %>"]

    'sails-linker':
      js:
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "_dist/client/"
        files:
          '_dist/client/index.html': if debug then ["<%= assets.js %>", "<%= assets.commonJs %>"] else "_dist/client/index.js"
          '_dist/client/admin-index.html': if debug then ["<%= assets.adminJs %>", "<%= assets.commonJs %>"] else "_dist/client/admin-index.js"
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "_dist/client/"
        files:
          '_dist/client/index.html': if debug then ["<%= assets.css %>", "<%= assets.commonCss %>"] else "_dist/client/index.css"
          '_dist/client/admin-index.html': if debug then ["<%= assets.adminCss %>", "<%= assets.commonCss %>"] else "_dist/client/admin-index.css"

    clean:
      all:
        src: "_dist/**/*"

      redundant:
        src: [
          "_dist/client/*"
          "!_dist/client/data"
          "!_dist/client/img"
          "!_dist/client/plugin"
          "!_dist/client/*.*"
        ]

    copy:
      client:
        files: [
          expand: true
          cwd: 'client/'
          src: [
            '**/*'
            '!**/*.coffee'
            '!**/*.less'
          ]
          dest: '_dist/client'
        ]
      server:
        files: [
          expand: true
          cwd: 'server/'
          src: [
            '**/*'
            '!**/*.coffee'
          ]
          dest: '_dist/server'
        ]

    inline_angular_templates:
      _dist:
        options:
          base: '_dist/client'
          prefix: '/'
          selector: 'body'
          method: 'append'
          unescape:
            '&lt;': '<'
            '&gt;': '>'
            '&apos;': '\''
            '&amp;': '&'
        files:
          '_dist/client/index.html': [
            '_dist/client/app/**/*.html'
          ]
          '_dist/client/admin-index.html': [
            '_dist/client/app-admin/**/*.html'
          ]

    replace:
      livereload:
        src: ["_dist/client/index.html","_dist/client/admin-index.html"]
        overwrite: true
        replacements: [
          from: '<!--LIVERELOAD-->'
          to: '<script src="//localhost:30001/livereload.js"></script>'
        ]

    concurrent:
      tasks: ['nodemon', 'watch', 'open']
      options:
        logConcurrentOutput: true


  grunt.registerTask "build", ->
    if debug
      grunt.task.run [
        "clean:all"
        "copy"
        "coffee"
        "less"
        "sails-linker"
        "replace:livereload"
      ]
    else
      grunt.task.run [
        "clean:all"
        "copy"
        "coffee"
        "less"
        "uglify"
        "cssmin"
        "sails-linker"
        "inline_angular_templates"
        "clean:redundant"
      ]

  grunt.registerTask "default", [
    'build'
    'concurrent'
  ]