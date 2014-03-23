module.exports = (grunt) ->

  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  debug = grunt.option("release") isnt true

  dir = if debug then "debug" else "dist"

  jsFiles = [
    "#{dir}/vendor/jquery/*.js"
    "#{dir}/vendor/jquery-ui/*.js"
    "#{dir}/vendor/bootstrap/*.js"
    "#{dir}/vendor/bootstrap-plugin/*.js"
    "#{dir}/vendor/angular/angular.js"
    "#{dir}/vendor/angular/*.js"
    "#{dir}/vendor/moment/moment.js"
    "#{dir}/vendor/moment/*.js"
    "#{dir}/vendor/messenger/messenger.js"
    "#{dir}/vendor/messenger/*.js"

    "#{dir}/vendor/angular-translate/*.js"
    "#{dir}/vendor/angular-translate/**/*.js"

    #todo need to remove
    "#{dir}/common/directives/*.js"

    "#{dir}/vendor/**/*.js"
    "#{dir}/config/**/*.js"
    "#{dir}/common/**/*.js"
    "#{dir}/i18n/**/*.js"
    "#{dir}/app/**/*.js"

    "#{dir}/plugin/unify*/**/*.js"

    "#{dir}/plugin/select2/select2.js"
    "#{dir}/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js"
    "#{dir}/plugin/syntaxhighlighter_3.0.83/scripts/*.js"
  ]

  cssFiles = [
    "#{dir}/vendor/**/*.css"
    "#{dir}/common/**/*.css"
    "#{dir}/app/**/*.css"

    "#{dir}/plugin/font-awesome/*.css"

    "#{dir}/plugin/unify*/*.css"
    "#{dir}/plugin/unify*/theme/default/*.css"

    "#{dir}/plugin/select2/select2.css"
    "#{dir}/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css"
  ]

  adminJsFiles =[
    "#{dir}/vendor/jquery/*.js"
    "#{dir}/vendor/jquery-ui/*.js"
    "#{dir}/vendor/bootstrap/*.js"
    "#{dir}/vendor/bootstrap-plugin/*.js"
    "#{dir}/vendor/angular/angular.js"
    "#{dir}/vendor/angular/*.js"
    "#{dir}/vendor/moment/moment.js"
    "#{dir}/vendor/moment/*.js"
    "#{dir}/vendor/messenger/messenger.js"
    "#{dir}/vendor/messenger/*.js"

    "#{dir}/vendor/angular-translate/*.js"
    "#{dir}/vendor/angular-translate/**/*.js"

    #todo need to remove
    "#{dir}/common/directives/*.js"

    "#{dir}/vendor/**/*.js"
    "#{dir}/config/**/*.js"
    "#{dir}/common/**/*.js"
    "#{dir}/i18n/**/*.js"
    "#{dir}/app-admin/**/*.js"

    "#{dir}/plugin/ace*/**/*.js"

    "#{dir}/plugin/select2/select2.js"
    "#{dir}/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js"
    "#{dir}/plugin/syntaxhighlighter_3.0.83/scripts/*.js"
  ]

  adminCssFiles = [
    "#{dir}/vendor/messenger/messenger.css"
    "#{dir}/vendor/messenger/*.css"
    "#{dir}/vendor/**/*.css"
    "#{dir}/common/**/*.css"
    "#{dir}/app-admin/**/*.css"

    "#{dir}/plugin/font-awesome/*.css"

    "#{dir}/plugin/ace*/*.css"

    "#{dir}/plugin/select2/select2.css"
  ]

  LIVERELOAD_PORT = 35729

  #-----------------------------------------------------------------

  grunt.initConfig

    dir: dir

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
              connect.static(require('path').resolve(dir))
              ]

    open:
      server:
        url:'http://localhost:30000'

    watch:
      normalFile:
        files: [
          'src/**/*'
          '!src/**/*.coffee'
          '!src/**/*.less'
        ]
        tasks: [
          'newer:copy:all'
          'sails-linker'
        ]
        options:
          event: ['all']
      coffee:
        files: [
          'src/**/*.coffee'
        ]
        tasks: [
          'newer:coffee'
          'sails-linker'
        ]
        options:
          event: ['all']
      less:
        files: [
          'src/**/*.less'
        ]
        tasks: [
          'newer:less'
          'sails-linker'
        ]
        options:
          event: ['all']

      #Auto refresh browser
      livereload:
        options:
          livereload:LIVERELOAD_PORT
        files: ["#{dir}/**/*"]

    coffee:
      options:
        bare: true
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.coffee']
          dest: dir
          ext: '.js'
        ]

    less:
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.less']
          dest: dir
          ext: '.css'
        ]
        
    uglify:
      #options:
        #mangle: true #不改变变量名和方法名
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
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/index.html": if debug then jsFiles else "dist/index.js"
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/index.html": if debug then cssFiles else "dist/index.css"
      'admin-js':
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/admin-index.html": if debug then adminJsFiles else "dist/admin-index.js"
      'admin-css':
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/admin-index.html": if debug then adminCssFiles else "dist/admin-index.css"

    clean:
      all:
        src: "#{dir}/**/*"

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
          dest: dir
        ]

    #todo remove tag STYLES/SCRIPT etc.
    replace:
      title:
        src: "<%= dir %>/index.html"
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