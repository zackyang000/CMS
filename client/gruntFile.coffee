module.exports = (grunt) ->

  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  debug = grunt.option("release") isnt true

  dir = if debug then "debug" else "dist"

  jsfiles = [
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
    "#{dir}/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js",
    "#{dir}/plugin/syntaxhighlighter_3.0.83/scripts/*.js",
  ]

  cssfiles = [
    "#{dir}/vendor/**/*.css"
    "#{dir}/common/**/*.css"
    "#{dir}/app/**/*.css"

    "#{dir}/plugin/font-awesome/*.css"

    "#{dir}/plugin/unify*/*.css"
    "#{dir}/plugin/unify*/theme/default/*.css"

    "#{dir}/plugin/select2/select2.css"
    "#{dir}/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css"
  ]

  #压缩后的js
  minjs = 'dist/index.js'

  #压缩后的css
  mincss = 'dist/index.css'

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
          'newer:copy:normalFile'
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
        files: [
          "#{dir}/**/*"
        ]

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
      options:
        mangle: true #不改变变量和方法名
        beautify: true #不压缩
      dist:
        src: jsfiles
        dest: minjs

    cssmin:
      dist:
        src: cssfiles
        dest: mincss

    #替换标签
    'sails-linker':
      js:
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl: "<script src='/%s\'></" + "script>"
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/index.html": if debug then jsfiles else 'dist/index.js'
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: "<link href='/%s' rel='stylesheet' />"
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/index.html": if debug then cssfiles else 'dist/index.css'
      title:
        options:
          startTag: "<!--TITLE-->"
          endTag: "<!--TITLE END-->"
          fileTmpl: "<title ng-bind=\"title + '|%s'\">%s</title>"
          appRoot: "#{dir}/"
        files:
          "<%= dir %>/index.html": 'AAA'

    #静态文件防缓存
    rev:
      files:
        src: [
          minjs
          mincss
        ]

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
      normalFile:
        files: [
          expand: true
          cwd: 'src/'
          src: [
            '**/*'
            '!**/*.coffee'
            '!**/*.less'
          ]
          dest: dir
        ]

    #替换字符
    replace:
      title:
        src: "<%= dir %>/index.html"
        overwrite: true
        replacements: [
          from: ''
          to: ''
        ]

    #打包html
    inline_angular_templates:
      dist:
        options:
          base: 'dist'
          prefix: '/'
          selector: 'body'
          method: 'prepend'
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
        "inline_angular_templates"  #打包html到index.html
        "clean:redundant"           #清空release目录多余的文件
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