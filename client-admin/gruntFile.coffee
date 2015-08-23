module.exports = (grunt) ->
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)
  debug = !grunt.option("release")

  grunt.initConfig
    assets: grunt.file.readJSON('assets.json')

    connect:
      admin:
        options:
          port: 40001
          hostname: 'localhost'
          base:'_dist/client/admin'
          middleware:  (connect, options) ->
            middlewares = []
            middlewares.push(require('connect-modrewrite')([
              '!\\.html|\\.js|\\.css|\\.otf|\\.eot|\\.svg|\\.ttf|\\.woff|\\.jpg|\\.bmp|\\.gif|\\.png|\\.txt$ /index.html'
            ]))
            require('connect-livereload') port:40005
            options.base.forEach (base) ->
              middlewares.push(connect.static(base))
            return middlewares

    open:
      app:
        url:'http://localhost:40000'

    watch:
      options:
        livereload: 40003
      clientFile:
        files: ['client/**/*','!client/**/*.coffee','!client/**/*.less']
        tasks: ['newer:copy:client','sails-linker','replace:livereload']
      clientCoffee:
        files: ['client/**/*.coffee']
        tasks: ['newer:coffee:client','sails-linker','replace:livereload']
      clientLess:
        files: ['client/**/*.less']
        tasks: ['newer:less','sails-linker','replace:livereload']
      server:
        files: ['server/**/*']
        tasks: ['newer:copy:server']

    coffeelint:
      app: ['client/**/*.coffee', 'server/**/*.coffee']
      options:
        max_line_length:
          level: 'ignore'

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
      production:
        files:
          '_dist/client/public/index.js': ["<%= assets.public.js %>"]
          '_dist/client/admin/index.js': ["<%= assets.admin.js %>"]

    cssmin:
      production:
        files:
          '_dist/client/public/index.css': ["<%= assets.public.css %>"]
          '_dist/client/admin/index.css': ["<%= assets.admin.css %>"]

    imagemin:
      dynamic:
        files: [
          expand : true
          cwd : ''
          src : ['client/public/img/**/*.{png,jpg,gif}', 'client/admin/img/**/*.{png,jpg,gif}']
          dest : '_dist'
        ]

    'sails-linker':
      'public-js':
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "_dist/client/public/"
        files:
          '_dist/client/public/index.html': if debug then ["<%= assets.public.js %>"] else "_dist/client/public/index.js"
      'public-css':
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "_dist/client/public/"
        files:
          '_dist/client/public/index.html': if debug then ["<%= assets.public.css %>"] else "_dist/client/public/index.css"
      'admin-js':
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "_dist/client/admin/"
        files:
          '_dist/client/admin/index.html': if debug then ["<%= assets.admin.js %>"] else "_dist/client/admin/index.js"
      'admin-css':
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "_dist/client/admin/"
        files:
          '_dist/client/admin/index.html': if debug then ["<%= assets.admin.css %>"] else "_dist/client/admin/index.css"

    clean:
      all:
        src: "_dist/**/*"

      redundant:
        src: [
          "_dist/client/public/*"
          "!_dist/client/public/data"
          "!_dist/client/public/img"
          "!_dist/client/public/plugin"
          "!_dist/client/public/*.*"
          "_dist/client/admin/*"
          "!_dist/client/admin/data"
          "!_dist/client/admin/img"
          "!_dist/client/admin/plugin"
          "!_dist/client/admin/*.*"
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
          ]
          dest: '_dist/server'
        ]
      package:
        files: [
          expand: true
          cwd: ''
          src: [
            'package.json'
          ]
          dest: '_dist/server'
        ]
      'client-public-font':
        files: [
          expand: true
          cwd: '_dist/client/public/vendor/fontawesome'
          src: [
            '*'
          ]
          dest: '_dist/client/public/vendor/fonts'
        ]
      'client-admin-font':
        files: [
          expand: true
          cwd: '_dist/client/admin/vendor/fontawesome'
          src: [
            '*'
          ]
          dest: '_dist/client/admin/vendor/fonts'
        ]
      'client-public-config':
        files: [
          expand: true
          src: if debug then 'config/config.dev.js' else 'config/config.prd.js'
          dest: '_dist/client/public'
          ext: '.js'
        ]
      'client-admin-config':
        files: [
          expand: true
          src: if debug then 'config/config.dev.js' else 'config/config.prd.js'
          dest: '_dist/client/admin'
          ext: '.js'
        ]



    bower:
      public:
        dest: '_dist/client/public/vendor'
        options:
          expand: true
      admin:
        dest: '_dist/client/admin/vendor'
        options:
          expand: true

    ngtemplates:
      'public-templates':
        src: ['app/**/*.html', 'vendor/angular-utils-pagination/*.tpl.html']
        dest: '_dist/client/public/common/templates.js'
        cwd: '_dist/client/public'
        options:
          prefix: '/'
          standalone: true
          htmlmin:
            collapseBooleanAttributes:      true
            collapseWhitespace:             true
            removeAttributeQuotes:          true
            removeComments:                 false
            removeEmptyAttributes:          true
            removeRedundantAttributes:      true
            removeScriptTypeAttributes:     true
            removeStyleLinkTypeAttributes:  true
      'admin-templates':
        src: ['app/**/*.html']
        dest: '_dist/client/admin/common/templates.js'
        cwd: '_dist/client/admin'
        options:
          prefix: '/'
          standalone: true
          htmlmin:
            collapseBooleanAttributes:      true
            collapseWhitespace:             true
            removeAttributeQuotes:          true
            removeComments:                 false
            removeEmptyAttributes:          true
            removeRedundantAttributes:      true
            removeScriptTypeAttributes:     true
            removeStyleLinkTypeAttributes:  true

    replace:
      livereload:
        src: ["_dist/client/public/index.html","_dist/client/admin/index.html"]
        overwrite: true
        replacements: [
          from: '<!--LIVERELOAD-->'
          to: '<script src="//localhost:40003/livereload.js"></script>'
        ]

    concurrent:
      tasks: ['nodemon', 'watch', 'open']
      options:
        logConcurrentOutput: true


  grunt.registerTask "build", ->
    grunt.task.run [
        "coffeelint"
        "clean:all"
        "bower"
        "copy"
        "coffee"
        "less"
      ]
    if debug
      grunt.task.run [
        "sails-linker"
        "replace:livereload"
      ]
    else
      grunt.task.run [
        "ngtemplates"
        "uglify"
        "cssmin"
        "imagemin"
        "sails-linker"
        "clean:redundant"
      ]

  grunt.registerTask "default", [
    'build'
    'connect:public'
    'connect:admin'
    'concurrent'
  ]
