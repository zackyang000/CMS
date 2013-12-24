
var kittenGenerator = {

  requestKittens: function() {
    chrome.extension.onRequest.addListener(onRequest);
    chrome.pageAction.show();
  }
};

chrome.pageAction.onClicked.addListener(function () {
  debugger
  kittenGenerator.requestKittens();
});
chrome.extension.onRequest.addListener(function(request, sender, sendResponse){
  debugger
  chrome.pageAction.show(sender.tab.id);
  sendResponse({});
})