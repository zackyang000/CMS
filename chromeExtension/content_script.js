

var data=readability.get()

if (data.description.length>200)
  data.description=data.description.substring(0,200)+"...";

$.post("http://localhost:33333/odata/Article",
  {
    Content: data.content,
    Description: data.description.substring(0,200),
    Title: data.title,
    Source: document.URL
  },
  function(data){
    debugger
    window.location.href="http://127.0.0.1:33333/admin/article('"+data.PostId+"')";
  });


