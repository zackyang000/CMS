//登陆
function login() {
    var user = $("#user").val();
    var pwd = $("#pwd").val();
    var saveCookie = document.getElementById("remember").checked ? true : false;

    if (user != "" && pwd != "") {
        $.ajax({
                url: "/Admin/Account/Login",
                type: "POST",
                dataType: "json",
                data: "user=" + user + "&pwd=" + pwd + "&remember=" + saveCookie,
                success: function(result) {
                    if (result.result) {
                        window.location.href = "/Admin";
                    } else {
                        $("#login_error").fadeIn(1000);
                        setTimeout(function() {
                            $("#login_error").fadeOut(2000);
                        }, 3000);
                    }
                }
            });
    }
}

//注销
function loginoff() {
    $.ajax({
        url: "/Admin/Account/LoginOff",
        type: "POST",
        dataType: "json",
        success: function (result) { if (result.result) { window.location.reload(); } else { alert(result.reason); } },
        error: function (XMLHttpRequest, textStatus, errorThrown) { this; }
    })
}


$(function () {
    $.datepicker.regional['zh-CN'] = {
        closeText: '关闭',
        prevText: '上月',
        nextText: '下月',
        currentText: '今天',
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
          '七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['一', '二', '三', '四', '五', '六',
         '七', '八', '九', '十', '十一', '十二'],
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        weekHeader: '周',
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: true,
        yearSuffix: '年'
    };
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
});

function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};
    var j;
    for (var i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function format_date(date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1);
    if (month < 10) {
        month = "0" + month;
    }
    var day = date.getDate();
    if (day < 10) {
        day = "0" + day;
    }
    return year + "-" + month + "-" + day;
}


function translate() { //中文 - 英文
    window.mycallback = function (response) { $("#post_url").val(response); }
    var s = document.createElement("script");
    s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + $("#post_title").val();
    document.getElementsByTagName("head")[0].appendChild(s);
}
function translate2() { //英文 - 中文
    window.mycallback = function (response) { $("post_url").val(response); }
    var s = document.createElement("script");
    s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=en&to=zh-cn&text=" + $("#post_title").val();
    document.getElementsByTagName("head")[0].appendChild(s);
}