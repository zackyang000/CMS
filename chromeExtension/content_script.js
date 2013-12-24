
debugger


var msg = {
  title : $("head title").text(),
  content : "123...",
  url: document.URL
};
chrome.runtime.sendMessage(msg);

