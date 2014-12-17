var server = "http://api.zackyang.com"
var client = "http://www.zackyang.com"
var admin =  "http://admin.zackyang.com"

try {
  token = '';
  //读取token
  try{ token = chrome.storage.local.get('token') } catch(e) {}


  //设置token
  if(!token){
    var token = prompt("please input token.","");
    chrome.storage.local.set({'token': token})
  }

  $.ajaxSetup({
    beforeSend: function (xhr)
    {
       xhr.setRequestHeader("authorization", token);
    }
  });


  var data = readability.get()
  $("body").html('<div class="backend_loadingbox">Processing...</div>')

  if(data.description.length > 200)
    data.description = data.description.substring(0, 200) + "...";

  $.post(server + "/odata/articles",
    {
      content: data.content,
      description: data.description,
      title: data.title,
      meta : {},
      date: new Date(),
      comments : []
    },
    function(data) {
      window.location.href = admin + "/article/" + data._id;
    });
}
catch (e) {
  alert("解析失败");
  window.location.reload();
}


