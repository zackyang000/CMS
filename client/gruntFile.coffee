module.exports = (grunt) ->
  
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)

  isdebug = grunt.option("release") isnt true
  launchdir = if isdebug then "debug" else "dist"

  jsfiles = [
    "#{launchdir}/vendor/**/*.js"
    "#{launchdir}/common/**/*.js"
    "#{launchdir}/app/**/*.js"

    "#{launchdir}/plugin/unify*/**/*.js"
    "#{launchdir}/plugin/select2/select2.js"

    "#{launchdir}/plugin/syntaxhighlighter_3.0.83/scripts/shCore.js",
    "#{launchdir}/plugin/syntaxhighlighter_3.0.83/scripts/*.js",
  ]

  cssfiles = [
    "#{launchdir}/vendor/**/*.css"
    "#{launchdir}/common/**/*.css"
    "#{launchdir}/app/**/*.css"
    "#{launchdir}/plugin/select2/select2.css"
    "#{launchdir}/plugin/syntaxhighlighter_3.0.83/styles/shCoreDefault.css"
  ]

  #压缩后的js
  minjs = '<%= config.dist %>/min/app.min.js'

  #压缩后的css
  mincss = '<%= config.dist %>/min/app.min.css'

  LIVERELOAD_PORT = 35729

  mountFolder = (connect, dir) ->
    connect.static(require('path').resolve(dir))

  modRewrite = require('connect-modrewrite')

  appconfig = {
    src: 'src'
    debug: 'debug'
    dist: 'dist'
  }

  #-----------------------------------------------------------------

  grunt.initConfig

    config: appconfig

    dir: launchdir

    #设置web server
    connect:
      options:
        port: 30000
        hostname: 'localhost'
      livereload:
        options:
          middleware: (connect, options) ->
            return [
              modRewrite([
                '!\\.html|\\.js|\\.css|\\.otf|\\.eot|\\.svg|\\.ttf|\\.woff|\\.jpg|\\.bmp|\\.gif|\\.png|\\.txt$ /index.html'
              ])

              require('connect-livereload')
                port:LIVERELOAD_PORT
              mountFolder(connect, launchdir)
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
          "#{launchdir}/**/*"
        ]

    coffee:
      options:
        bare: true
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.coffee']
          dest: launchdir
          ext: '.js'
        ]

    less:
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.less']
          dest: launchdir
          ext: '.css'
        ]

    uglify:
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
          appRoot: "#{launchdir}/"
        files:
          "<%= dir %>/index.html": if isdebug then jsfiles else '<%= config.dist %>/min/app.min.js'
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: "<link href='/%s' rel='stylesheet' />"
          appRoot: "#{launchdir}/"
        files:
          "<%= dir %>/index.html": if isdebug then cssfiles else '<%= config.dist %>/min/*.css'

    #静态文件防缓存
    rev:
      files:
        src: [
          minjs
          mincss
        ]

    clean:
      all:
        src: "#{launchdir}/**/*"

      redundant:
        src: [
          "<%= config.dist %>/**/*"
        ]

    #替换字符
    replace:
      dist:
        src: [
          "<%= config.dist %>/min/*.app.min.js"
        ]
        overwrite: true
        replacements: [
          from: '"use strict";'
          to: ''
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
          dest: launchdir
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
          dest: launchdir
        ]

    #打包html
    inline_angular_templates:
      dist:
        options:
          base: '<%= config.dist %>'  #(Optional) ID of the <script> tag will be relative to this folder. Default is project dir.
          prefix: '/'                 #(Optional) Prefix path to the ID. Default is empty string.
          selector: 'body'            #(Optional) CSS selector of the element to use to insert the templates. Default is `body`.
          method: 'prepend'           #(Optional) DOM insert method. Default is `prepend`.
          unescape:                   #(Optional) List of escaped characters to unescape
            '&lt;': '<'
            '&gt;': '>'
            '&apos;': '\''
            '&amp;': '&'
        files:
          '<%= config.dist %>/index.html': [
            '<%= config.dist %>/**/*.html'
            '!<%= config.dist %>/index.html'
          ]


  grunt.registerTask "build", ->
    if isdebug
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
        "rev"
        #"replace:dist"                   #去掉严格模式（uglify没有提供此option）
        "sails-linker"
        "replace:initscript"
        "replace:loadscript"
        "inline_angular_templates"  #打包html到index.html
        "clean:redundant"           #清空release目录多余的文件
      ]

  grunt.registerTask "server", ->
    grunt.task.run [
      "connect"
      "open"
      "watch"
    ]

  grunt.registerTask "test", [
    
  ]

  grunt.registerTask "default", [
    'build'
    #'server'
  ]