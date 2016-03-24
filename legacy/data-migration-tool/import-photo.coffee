request = require('request')
mongoose = require('mongoose')
Gallery = mongoose.model("Gallery")

module.exports = (host) ->
  url = "#{host}/odata/Gallery?$expand=Photos&$filter=IsDeleted+eq+false&$orderby=CreateDate+desc"

  request.get {url: url, json: true},  (e, r, data) ->
    for item, i in data.value
      gallery = new Gallery
        name: item.Name
        description: item.Description
        cover: item.Cover
        hidden: item.IsHidden || false
        date: item.CreateDate
        photos : []
      for photo in item.Photos
        gallery.photos.push
          name: photo.Name
          description: photo.Description
          url: photo.Path
          thumbnail: photo.Thumbnail
        gallery.save()
      console.log "[PHOTO] [#{i}] '#{item.Name}' import is complate."