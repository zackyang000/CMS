module.exports = (grunt) ->
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)
  debug = grunt.option("release") isnt true

  grunt.initConfig
    assets: grunt.file.readJSON('assets.json')

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
                port:30001
              connect.static(require('path').resolve('dist'))
            ]

    open:
      server:
        url:'http://localhost:30000'

    watch:
      options:
        livereload: 30001
      general:
        files: ['src/**/*','!src/**/*.coffee','!src/**/*.less']
        tasks: ['newer:copy','sails-linker']
      coffee:
        files: ['src/**/*.coffee']
        tasks: ['newer:coffee','sails-linker']
      less:
        files: ['src/**/*.less']
        tasks: ['newer:less','sails-linker']

    coffee:
      options:
        bare: true
      compile:
        files: [
          expand: true
          cwd: 'src/'
          src: ['**/*.coffee']
          dest: 'dist/'
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
        #mangle: false
        #beautify: true
      production:
        files:
          'dist/index.js': ["<%= assets.commonJs %>", "<%= assets.js %>"]
          'dist/admin-index.js': ["<%= assets.commonJs %>", "<%= assets.adminJs %>"]

    cssmin:
      production:
        files:
          'dist/index.css': ["<%= assets.commonCss %>", "<%= assets.css %>"]
          'dist/admin-index.css': ["<%= assets.commonCss %>", "<%= assets.adminCss %>"]

    'sails-linker':
      js:
        options:
          startTag: "<!--SCRIPTS-->"
          endTag: "<!--SCRIPTS END-->"
          fileTmpl:  if debug then "<script src='/%s\'><\/script>" else "<script src='/%s?v=#{+new Date()}\'><\/script>"
          appRoot: "dist/"
        files:
          'dist/index.html': if debug then ["<%= assets.commonJs %>", "<%= assets.js %>"] else "dist/index.js"
          'dist/admin-index.html': if debug then ["<%= assets.commonJs %>", "<%= assets.adminJs %>"] else "dist/admin-index.js"
      css:
        options:
          startTag: "<!--STYLES-->"
          endTag: "<!--STYLES END-->"
          fileTmpl: if debug then "<link href='/%s' rel='stylesheet' />" else "<link href='/%s?v=#{+new Date()}' rel='stylesheet' />"
          appRoot: "dist/"
        files:
          'dist/index.html': if debug then ["<%= assets.commonCss %>", "<%= assets.css %>"] else "dist/index.css"
          'dist/admin-index.html': if debug then ["<%= assets.commonCss %>", "<%= assets.adminCss %>"] else "dist/admin-index.css"

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
      general:
        files: [
          expand: true
          cwd: 'src/'
          src: [
            '**/*'
            '!**/*.coffee'
            '!**/*.less'
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
          ]
          'dist/admin-index.html': [
            'dist/app-admin/**/*.html'
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
    "connect"
    "open"
    "watch"
  ]