
chrome.tabs.onUpdated.addListener(function (tabId) {
  chrome.pageAction.show(tabId);
});

var data = undefined;
chrome.runtime.onMessage.addListener(function(request, sender, sendRequest){
	data = request;
});



chrome.pageAction.onClicked.addListener(function(tab) {
  chrome.tabs.executeScript(null, {file: "jquery-2.0.0.min.js"});
  chrome.tabs.executeScript(null, {file: "readability.js"});
  chrome.tabs.executeScript(null, {file: "content_script.js"});
  chrome.tabs.insertCSS(null, {file: "content_css.css"})
});