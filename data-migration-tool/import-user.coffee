request = require('request')
mongoose = require('mongoose')
User = mongoose.model("User")

module.exports = (host) ->
  url = "#{host}/odata/User"

  request.get {url: url, json: true},  (e, r, data) ->
    userArr = []
    for item, i in data.value
      unless item.UserName in userArr
        userArr.push item.UserName
        user = new User
          name: item.UserName
          loginName: item.LoginName
          password: "123"
          email: item.Email
          token: item.Token
          disabled: item.IsDeleted
        user.save()
        console.log "[USER] [#{i}] '#{item.UserName}' import is complate."
      else
        console.log "[USER] [#{i}] '#{item.UserName}' exist, skip."
