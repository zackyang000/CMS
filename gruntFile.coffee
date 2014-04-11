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
          ignore: ["public/**"]
          ext: "js,html"
          nodeArgs: ["--debug"]
          delayTime: 1
          env:
            PORT: 30000
          cwd: 'dist'

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
          dest: 'dist/public/'
          ext: '.js'
        ]
      server:
        files: [
          expand: true
          cwd: 'server/'
          src: ['**/*.coffee']
          dest: 'dist/'
          ext: '.js'
        ]

    less:
      client:
        files: [
          expand: true
          cwd: 'client/'
          src: ['**/*.less']
          dest: 'dist/public'
          ext: '.css'
        ]

    uglify:
      #options:
        #mangle: false
        #beautify: true
      production:
        files:
          'dist/public/index.js': ["<%= assets.js %>", "<%= assets.commonJs %>"]
          'dist/public/admin-index.js': ["<%= assets.adminJs %>", "<%= assets.commonJs %>"]

    cssmin:
      production:
        files:
          'dist/public/index.css': ["<%= assets.css %>", "<%= assets.commonCss %>"]
          'dist/public/admin-index.css': ["<%= assets.adminCss %>", "<%= assets.commonCss %>"]

    'sails-linker':
      js:
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "dist/public/"
        files:
          'dist/public/index.html': if debug then ["<%= assets.js %>", "<%= assets.commonJs %>"] else "dist/public/index.js"
          'dist/public/admin-index.html': if debug then ["<%= assets.adminJs %>", "<%= assets.commonJs %>"] else "dist/public/admin-index.js"
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "dist/public/"
        files:
          'dist/public/index.html': if debug then ["<%= assets.css %>", "<%= assets.commonCss %>"] else "dist/public/index.css"
          'dist/public/admin-index.html': if debug then ["<%= assets.adminCss %>", "<%= assets.commonCss %>"] else "dist/public/admin-index.css"

    clean:
      all:
        src: "dist/**/*"

      redundant:
        src: [
          "dist/public/*"
          "!dist/public/data"
          "!dist/public/img"
          "!dist/public/plugin"
          "!dist/public/*.*"
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
          dest: 'dist/public'
        ]
      server:
        files: [
          expand: true
          cwd: 'server/'
          src: [
            '**/*'
            '!**/*.coffee'
          ]
          dest: 'dist'
        ]

    inline_angular_templates:
      dist:
        options:
          base: 'dist/public'
          prefix: '/'
          selector: 'body'
          method: 'append'
          unescape:
            '&lt;': '<'
            '&gt;': '>'
            '&apos;': '\''
            '&amp;': '&'
        files:
          'dist/public/index.html': [
            'dist/public/app/**/*.html'
          ]
          'dist/public/admin-index.html': [
            'dist/public/app-admin/**/*.html'
          ]

    replace:
      livereload:
        src: ["dist/public/index.html","dist/public/admin-index.html"]
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