document.addEventListener('DOMContentLoaded', function () {
  debugger
	var data = chrome.extension.getBackgroundPage().data;
  $("#message").hide();
  $("#content-title").text(data.title);
  $("#content-content").text(data.content);
  $("#content-url").text(data.url);


});