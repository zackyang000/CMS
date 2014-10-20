var server = "http://api.woshinidezhu.com"
var client = "http://www.woshinidezhu.com"

try {
  var data = readability.get()
  $("body").html('<div class="backend_loadingbox">Processing...</div>')
  if(data.description.length > 200)
    data.description = data.description.substring(0, 200) + "...";

  $.post(server + "/odata/articles",
    {
      content: data.content,
      description: data.description.substring(0, 200),
      title: data.title,
      meta : {},
      date: new Date(),
      comments : []
    },
    function(data) {
      window.location.href = client + "/admin/article/" + data._id;
    });
}
catch (e) {
  alert("解析失败");
  window.location.reload();
}



