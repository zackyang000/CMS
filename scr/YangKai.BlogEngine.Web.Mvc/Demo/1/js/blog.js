/*
Author: mg12
Update: 2008/11/21
Author URI: http://www.neoease.com/
*/
(function() {

function $(id) {
	return document.getElementById(id);
}

function setStyleDisplay(id, status) {
	$(id).style.display = status;
}

function goTop(acceleration, time) {
	acceleration = acceleration || 0.1;
	time = time || 16;

	var dx = 0;
	var dy = 0;
	var bx = 0;
	var by = 0;
	var wx = 0;
	var wy = 0;

	if (document.documentElement) {
		dx = document.documentElement.scrollLeft || 0;
		dy = document.documentElement.scrollTop || 0;
	}
	if (document.body) {
		bx = document.body.scrollLeft || 0;
		by = document.body.scrollTop || 0;
	}
	var wx = window.scrollX || 0;
	var wy = window.scrollY || 0;

	var x = Math.max(wx, Math.max(bx, dx));
	var y = Math.max(wy, Math.max(by, dy));

	var speed = 1 + acceleration;
	window.scrollTo(Math.floor(x / speed), Math.floor(y / speed));
	if(x > 0 || y > 0) {
		var invokeFunction = "MGJS.goTop(" + acceleration + ", " + time + ")"
		window.setTimeout(invokeFunction, time);
	}
}

function switchTab(showPanels, hidePanels, activeTab, activeClass, fadeTab, fadeClass) {
	$(activeTab).className = activeClass;
	$(fadeTab).className = fadeClass;

	var panel, panelList;
	panelList = showPanels.split(',');
	for (var i = 0; i < panelList.length; i++) {
		var panel = panelList[i];
		if ($(panel)) {
			setStyleDisplay(panel, 'block');
		}
	}
	panelList = hidePanels.split(',');
	for (var i = 0; i < panelList.length; i++) {
		panel = panelList[i];
		if ($(panel)) {
			setStyleDisplay(panel, 'none');
		}
	}
}

window['MGJS'] = {};
window['MGJS']['$'] = $;
window['MGJS']['setStyleDisplay'] = setStyleDisplay;
window['MGJS']['goTop'] = goTop;
window['MGJS']['switchTab'] = switchTab;

})();

function switchImage(imageId, imageUrl, linkId, linkUrl, preview, title, alt) {
	if(imageId && imageUrl) {
		var image = $(imageId);
		image.src = imageUrl;

		if(title) {
			image.title = title;
		}
		if(alt) {
			image.alt = alt;
		}
	}

	if(linkId && linkUrl) {
		var link = $(linkId);
		link.href = linkUrl;
	}
}