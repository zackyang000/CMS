request = require('request')
mongoose = require('mongoose')
Gallery = mongoose.model("Gallery")

module.exports = (host, db) ->
  url = "#{host}/odata/Gallery?$expand=Photos&$filter=IsDeleted+eq+false&$orderby=CreateDate+desc"
  mongoose.connect db

  request.get {url: url, json: true},  (e, r, data) ->
    for item in data.value
      gallery = new Gallery
        name: item.Name
        description: item.Description
        cover: item.Cover
        hidden: item.IsHidden || false
        date: item.CreateDate
        photos : []
      for photo, i in item.Photos
        gallery.photos.push
          name: photo.Name
          description: photo.Description
          url: photo.Path
          thumbnail: photo.Thumbnail
        gallery.save()
        console.log "[GALLERY] [#{i}] '#{item.Name}' import is complate."