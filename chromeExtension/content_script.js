

document.body.style.backgroundColor="red"
debugger
var a=readability.init()
$.post("http://localhost:33333/odata/Article",
  {
    Content: "AAA",
    Description: "BBB",
    PostId: "1c01377d-394b-4274-9fcc-385c8a1cf122",
    Title: "ASFDAFSASSFD",
    Url: "AASDFSAFDAS"
  },
  function(data){
    debugger
    window.open("http://www.woshinidezhu.com")
  })


