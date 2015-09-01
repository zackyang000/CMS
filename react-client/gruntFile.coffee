module.exports = (grunt) ->
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks)
  debug = !grunt.option("deploy")

  grunt.initConfig
    execute:
      debug:
        src: ['server.js']

    uglify:
      production:
        files:
          '_dist/client/admin/index.js': ["<%= assets.admin.js %>"]

    imagemin:
      dynamic:
        files: [
          expand : true
          cwd : ''
          src : ['client/public/img/**/*.{png,jpg,gif}', 'client/admin/img/**/*.{png,jpg,gif}']
          dest : '_dist'
        ]

    clean:
      all:
        src: "dist/**/*"

    copy:
      config:
        files: [
          expand: true
          src: if debug then 'config.dev.js' else 'config.prd.js'
          dest: 'src/'
          ext: '.js'
        ]
      vendor:
        files: [
          expand: true
          cwd: 'bower_components'
          src: [
            'font-awesome/css/font-awesome.min.css'
            'font-awesome/fonts/*'
            'bootstrap/dist/css/bootstrap.min.css'
          ]
          dest: 'src/vendor'
        ]
      static:
        files: [
          expand: true
          cwd: 'src'
          src: [
            'vendor/**/*'
            'images/**/*'
          ]
          dest: 'build'
        ]

  grunt.registerTask "default", ->
    grunt.task.run [
        "copy:config"
        "copy:vendor"
      ]
    if debug
      grunt.task.run [
        "execute:debug"
      ]
    else
      grunt.task.run [
        "copy:static"
        #"uglify"
        #"imagemin"
      ]
