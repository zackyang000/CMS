module.exports = (grunt) ->

  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  debug = grunt.option("release") isnt true

  jsFiles = [
    "dist/vendor/jquery/*.js"
    "dist/vendor/jquery-ui/*.js"
    "dist/vendor/bootstrap/*.js"
    "dist/vendor/bootstrap-plugin/*.js"
    "dist/vendor/angular/angular.js"
    "dist/vendor/angular/*.js"
    "dist/vendor/moment/moment.js"
    "dist/vendor/moment/*.js"
    "dist/vendor/messenger/messenger.js"
    "dist/vendor/messenger/*.js"

    #order by folder level.
    "dist/vendor/*.js"
    "dist/vendor/*/*.js"
    "dist/vendor/*/*/*.js"
    "dist/vendor/*/*/*/*.js"
    "dist/vendor/**/*.js"

    "dist/config/**/*.js"
    "dist/common/**/*.js"
    "dist/i18n/**/*.js"
    "dist/app/**/*.js"

    "dist/plugin/unify*/**/*.js"

    "dist/plugin/select2/select2.js"
    "dist/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js"
    "dist/plugin/syntaxhighlighter_3.0.83/scripts/*.js"
  ]

  cssFiles = [
    "dist/vendor/**/*.css"
    "dist/common/**/*.css"
    "dist/app/**/*.css"
    "dist/plugin/font-awesome/*.css"
    "dist/plugin/unify*/*.css"
    "dist/plugin/unify*/theme/default/*.css"
    "dist/plugin/select2/select2.css"
    "dist/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css"
  ]

  adminJsFiles =[
    "dist/vendor/jquery/*.js"
    "dist/vendor/jquery-ui/*.js"
    "dist/vendor/bootstrap/*.js"
    "dist/vendor/bootstrap-plugin/*.js"
    "dist/vendor/angular/angular.js"
    "dist/vendor/angular/*.js"
    "dist/vendor/moment/moment.js"
    "dist/vendor/moment/*.js"
    "dist/vendor/messenger/messenger.js"
    "dist/vendor/messenger/*.js"

    #order by folder level.
    "dist/vendor/*.js"
    "dist/vendor/*/*.js"
    "dist/vendor/*/*/*.js"
    "dist/vendor/*/*/*/*.js"
    "dist/vendor/**/*.js"

    "dist/config/**/*.js"
    "dist/common/**/*.js"
    "dist/i18n/**/*.js"
    "dist/app-admin/**/*.js"

    "dist/plugin/ace*/**/*.js"

    "dist/plugin/select2/select2.js"
    "dist/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js"
    "dist/plugin/syntaxhighlighter_3.0.83/scripts/*.js"
  ]

  adminCssFiles = [
    "dist/vendor/messenger/messenger.css"
    "dist/vendor/messenger/*.css"
    "dist/vendor/**/*.css"
    "dist/common/**/*.css"
    "dist/app-admin/**/*.css"
    "dist/plugin/font-awesome/*.css"
    "dist/plugin/ace*/*.css"
    "dist/plugin/select2/select2.css"
  ]

  LIVERELOAD_PORT = 35730

  #-----------------------------------------------------------------

  grunt.initConfig
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
        files: ['src/**/*','!src/**/*.coffee','!src/**/*.less']
        tasks: ['newer:copy:all']
      coffee:
        files: ['src/**/*.coffee']
        tasks: ['newer:coffee']
      less:
        files: ['src/**/*.less']
        tasks: ['newer:less']
      linker:
        files: ['src/**/*.js','src/**/*.css','src/**/*.coffee','src/**/*.less']
        tasks: ['sails-linker']
        options:
          event: ['added', 'deleted']

    coffee:
      options:
        bare: true
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.coffee']
          dest: 'dist'
          ext: '.js'
        ]

    less:
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.less']
          dest: 'dist'
          ext: '.css'
        ]
        
    uglify:
      #options:
        #mangle: false #改变变量名和方法名
        #beautify: true #不压缩
      my_target:
        files:
          'dist/index.js': jsFiles
          'dist/admin-index.js': adminJsFiles

    cssmin:
      combine:
        files:
          'dist/index.css': cssFiles
          'dist/admin-index.css': adminCssFiles

    'sails-linker':
      js:
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "dist/"
        files:
          "dist/index.html": if debug then jsFiles else "dist/index.js"
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "dist/"
        files:
          "dist/index.html": if debug then cssFiles else "dist/index.css"
      'admin-js':
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "dist/"
        files:
          "dist/admin-index.html": if debug then adminJsFiles else "dist/admin-index.js"
      'admin-css':
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "dist/"
        files:
          "dist/admin-index.html": if debug then adminCssFiles else "dist/admin-index.css"

    clean:
      all:
        src: "dist/**/*"

      redundant:
        src: [
          "dist/*"
          "!dist/data"
          "!dist/img"
          "!dist/plugin"
          "!dist/*.*"
        ]


    copy:
      all:
        files: [
          expand: true
          cwd: 'src/'
          src: [
            '**/*'
            '!**/*.coffee'
            '!**/*.less'
            '!**/*.min.js'
            '!**/*.min.css'
          ]
          dest: 'dist'
        ]

    #todo remove tag STYLES/SCRIPT etc.
    replace:
      title:
        src: "dist/index.html"
        overwrite: true
        replacements: [
          from: ''
          to: ''
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
        "copy:all"
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

  grunt.registerTask "start-web-server", [
      "connect"
      "open"
      "watch"
  ]

  grunt.registerTask "test", [

  ]

  grunt.registerTask "default", [
    'build'
    'start-web-server'
  ]