//var targetSite="http://www.woshinidezhu.com"
//var targetSite="http://localhost:33333"

var server="http://10.16.75.10:18002"
var client="http://10.16.75.10:8002"

try
{
  var data=readability.get()
  $("body").html('<div class="backend_loadingbox">Processing...</div>')
  if (data.description.length>200)
    data.description=data.description.substring(0,200)+"...";

  $.post(server+"/odata/Article",
    {
      Content: data.content,
      Description: data.description.substring(0,200),
      Title: data.title,
      Source: document.URL
    },
    function(data){
      window.location.href=client+"/admin/article('"+data.PostId+"')";
    });

}
catch(e)
{
  alert("解析失败");
  window.location.reload();
}



