document.addEventListener('DOMContentLoaded', function () {
	var data = chrome.extension.getBackgroundPage().data;
  $("#message").hide();
  $("#content-title").text(data.title);
  $("#content-content").text(data.content);
  $("#content-url").text(data.url);
});
