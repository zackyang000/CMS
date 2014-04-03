module.exports = (grunt) ->

  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  debug = grunt.option("release") isnt true

  jsFiles = [
    "dist/public/vendor/jquery/*.js"
    "dist/public/vendor/jquery-ui/*.js"
    "dist/public/vendor/bootstrap/*.js"
    "dist/public/vendor/bootstrap-plugin/*.js"
    "dist/public/vendor/angular/angular.js"
    "dist/public/vendor/angular/*.js"
    "dist/public/vendor/moment/moment.js"
    "dist/public/vendor/moment/*.js"
    "dist/public/vendor/messenger/messenger.js"
    "dist/public/vendor/messenger/*.js"

    #order by folder level.
    "dist/public/vendor/*.js"
    "dist/public/vendor/*/*.js"
    "dist/public/vendor/*/*/*.js"
    "dist/public/vendor/*/*/*/*.js"
    "dist/public/vendor/**/*.js"

    "dist/public/config/**/*.js"
    "dist/public/common/**/*.js"
    "dist/public/i18n/**/*.js"
    "dist/public/app/**/*.js"

    "dist/public/plugin/unify*/**/*.js"

    "dist/public/plugin/select2/select2.js"
    "dist/public/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js"
    "dist/public/plugin/syntaxhighlighter_3.0.83/scripts/*.js"
  ]

  cssFiles = [
    "dist/public/vendor/**/*.css"
    "dist/public/common/**/*.css"
    "dist/public/app/**/*.css"
    "dist/public/plugin/font-awesome/*.css"
    "dist/public/plugin/unify*/*.css"
    "dist/public/plugin/unify*/theme/default/*.css"
    "dist/public/plugin/select2/select2.css"
    "dist/public/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css"
  ]

  adminJsFiles =[
    "dist/public/vendor/jquery/*.js"
    "dist/public/vendor/jquery-ui/*.js"
    "dist/public/vendor/bootstrap/*.js"
    "dist/public/vendor/bootstrap-plugin/*.js"
    "dist/public/vendor/angular/angular.js"
    "dist/public/vendor/angular/*.js"
    "dist/public/vendor/moment/moment.js"
    "dist/public/vendor/moment/*.js"
    "dist/public/vendor/messenger/messenger.js"
    "dist/public/vendor/messenger/*.js"

    #order by folder level.
    "dist/public/vendor/*.js"
    "dist/public/vendor/*/*.js"
    "dist/public/vendor/*/*/*.js"
    "dist/public/vendor/*/*/*/*.js"
    "dist/public/vendor/**/*.js"

    "dist/public/config/**/*.js"
    "dist/public/common/**/*.js"
    "dist/public/i18n/**/*.js"
    "dist/public/app-admin/**/*.js"

    "dist/public/plugin/ace*/**/*.js"

    "dist/public/plugin/select2/select2.js"
    "dist/public/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js"
    "dist/public/plugin/syntaxhighlighter_3.0.83/scripts/*.js"
  ]

  adminCssFiles = [
    "dist/public/vendor/messenger/messenger.css"
    "dist/public/vendor/messenger/*.css"
    "dist/public/vendor/**/*.css"
    "dist/public/common/**/*.css"
    "dist/public/app-admin/**/*.css"
    "dist/public/plugin/font-awesome/*.css"
    "dist/public/plugin/ace*/*.css"
    "dist/public/plugin/select2/select2.css"
  ]

  LIVERELOAD_PORT = 35730

  #-----------------------------------------------------------------

  grunt.initConfig
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

    connect:
      options:
        port: 30000
        hostname: 'localhost'
      livereload:
        options:
          middleware: (connect, options) ->
            return [
              require('connect-modrewrite')([
                '^/admin$ /admin-index.html'
                '^/admin[/](.*)$ /admin-index.html'
                '!\\.html|\\.js|\\.css|\\.otf|\\.eot|\\.svg|\\.ttf|\\.woff|\\.jpg|\\.bmp|\\.gif|\\.png|\\.txt$ /index.html'
              ])

              require('connect-livereload')
                port:LIVERELOAD_PORT
              connect.static(require('path').resolve('dist'))
            ]

    open:
      server:
        url:'http://localhost:30000'

    watch:
      options:
        livereload:LIVERELOAD_PORT
      normalFile:
        files: ['client/**/*','!client/**/*.coffee','!client/**/*.less']
        tasks: ['newer:copy:all']
      coffee:
        files: ['client/**/*.coffee']
        tasks: ['newer:coffee']
      less:
        files: ['client/**/*.less']
        tasks: ['newer:less']
      linker:
        files: ['client/**/*.js','client/**/*.css','client/**/*.coffee','client/**/*.less']
        tasks: ['sails-linker']
        options:
          event: ['added', 'deleted']

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
    #mangle: false #改变变量名和方法名
    #beautify: true #不压缩
      my_target:
        files:
          'dist/public/index.js': jsFiles
          'dist/public/admin-index.js': adminJsFiles

    cssmin:
      combine:
        files:
          'dist/public/index.css': cssFiles
          'dist/public/admin-index.css': adminCssFiles

    'sails-linker':
      js:
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "dist/public/"
        files:
          "dist/public/index.html": if debug then jsFiles else "dist/public/index.js"
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "dist/public/"
        files:
          "dist/public/index.html": if debug then cssFiles else "dist/public/index.css"
      'admin-js':
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "dist/public/"
        files:
          "dist/public/admin-index.html": if debug then adminJsFiles else "dist/public/admin-index.js"
      'admin-css':
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "dist/public/"
        files:
          "dist/public/admin-index.html": if debug then adminCssFiles else "dist/public/admin-index.css"

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
          base: 'dist'
          prefix: '/'
          selector: 'body'
          method: 'append'
          unescape:
            '&lt;': '<'
            '&gt;': '>'
            '&apos;': '\''
            '&amp;': '&'
        files:
          'dist/index.html': [
            'dist/app/**/*.html'
            '!dist/index.html'
          ]
          'dist/admin-index.html': [
            'dist/app-admin/**/*.html'
            '!dist/admin-index.html'
          ]

  grunt.registerTask "build", ->
    if debug
      grunt.task.run [
        "clean:all"
        "copy"
        "coffee"
        "less"
        "sails-linker"
      ]
    else
      grunt.task.run [
        "clean:all"
        "copy:all"
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
    'nodemon'
    'watch'
    #'start-web-server'
  ]

