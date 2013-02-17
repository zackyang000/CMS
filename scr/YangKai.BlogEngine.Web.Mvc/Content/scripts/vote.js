swfobject.embedSWF("/Content/Flash/vote.swf", "voteflash", "100", "100", "9.0.0", "expressInstall.swf", {}, { wmode: 'transparent' });

function setFlashPosition(obj) {
    var pos = getObjectPosition(obj);
    var div = $("#voteflash");
    div.css({ left: pos.left - 40 + "px", top: pos.top - 50 + "px" });
}

function showAnimation(containerId, actionValue) {
    var obj = document.getElementById(containerId);
    var flashx = document.getElementById("voteflash");
    if (!flashx) {
        return;
    }
    setFlashPosition(obj);
    try {
        if (actionValue > 0) {
            flashx.plus();
        } else {
            flashx.subtract();
        }
    } catch (e) { }
}

function hideFlash() {
    $('#voteflash').css({ left: "-1000px", top: "-1000px" });
}

function getObjectPosition(obj) {
    var result = new Object();
    result.top = obj.offsetTop;
    result.left = obj.offsetLeft;

    if (obj.offsetParent != null) {
        obj = obj.offsetParent;
        var pos = getObjectPosition(obj);

        result.top += pos.top;
        result.left += pos.left;
    }
    return result;
}

function voteup(id) {
    var posscore = parseInt($('#up' + id).text());
    showAnimation('topic' + id, 1);
    $('#up' + id).text(posscore + 1);
    $('#topic' + id + ' a').hide();
    $.post("/Article/Vote", { id: id, vote: "1" });
}

function votedown(id) {
    var negscore = parseInt($('#down' + id).text());
    showAnimation('down' + id, -1);
    $('#down' + id).text(negscore + 1);
    $('#topic' + id + ' a').hide();
    $.post("/Article/Vote", { id: id, vote: "0" });
}
