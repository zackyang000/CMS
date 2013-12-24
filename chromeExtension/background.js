
chrome.tabs.onUpdated.addListener(function (tabId) {
  chrome.pageAction.show(tabId);
});

var data = undefined;
chrome.runtime.onMessage.addListener(function(request, sender, sendRequest){
	data = request;
});
