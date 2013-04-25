//#region  setup

$(document).ready(function () {
    //ajax禁用IE缓存
    $.ajaxSetup({ cache: false });

    //table hover高亮
    ko.bindingHandlers.hoverToggle = {
        update: function (element, valueAccessor) {
            var css = valueAccessor();

            ko.utils.registerEventHandler(element, "mouseover", function () {
                var value = ko.utils.unwrapObservable(css);
                $(element).addClass(value);
            });

            ko.utils.registerEventHandler(element, "mouseout", function () {
                var value = ko.utils.unwrapObservable(css);
                $(element).removeClass(value);
            });
        }
    };
});

//fix Array indexOf() in JavaScript for IE browsers
if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length >>> 0;
        var from = Number(arguments[1]) || 0;
        from = (from < 0) ? Math.ceil(from) : Math.floor(from);
        if (from < 0) from += len;

        for (; from < len; from++) {
            if (from in this && this[from] === elt) return from;
        }
        return -1;
    };
}

String.prototype.Format = function () {
    var fmt = "yyyy年MM月dd日 hh:mm";
    var myDate = ConvertJsonDate(this);
    var o = {
        "M+": myDate.getMonth() + 1,
        "d+": myDate.getDate(),
        "h+": myDate.getHours(),
        "m+": myDate.getMinutes(),
        "s+": myDate.getSeconds(),
        "q+": Math.floor((myDate.getMonth() + 3) / 3),
        "S": myDate.getMilliseconds()
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (myDate.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};

function ConvertJsonDate(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    } else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }
    return new Date(parseInt(jsondate, 10));
}

//#endregion

//导航lock状态
function navlock() {
    //判断全部lock
    if (document.URL.split('/')[3] == "" || $("#navigation td a").length == 1) {
        $("#navigation td a").eq(0).parent().removeClass("link");
        $("#navigation td a").eq(0).parent().addClass("lock");
    }
    //判断各版块lock
    $("#navigation td a").each(function (i) {
        if (this.href.split('/')[3] == document.URL.split('/')[3].split('?')[0]) {
            $("#navigation td a").eq(i).parent().removeClass("link");
            $("#navigation td a").eq(i).parent().addClass("lock");
        }
    });
    $("#navigation td a").each(function (i) {
        if (this.href.split('/')[3] == document.URL.split('/')[3].split('-')[0] && i > 0) {
            $("#navigation td a").eq(i).parent().removeClass("link");
            $("#navigation td a").eq(i).parent().addClass("lock");
        }
    });
}

//#region common

//html转义
function HtmlEncode(text) {
    return text.replace(/&/g, '&amp').replace(/\"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

//判断url合法性
String.prototype.isUrl = function () {
    var argvalue = this;
    if (argvalue.indexOf(" ") != -1)
        return false;
    else if (argvalue == "http://")
        return false;
    else if (argvalue.indexOf("http://") > 0)
        return false;
    argvalue = argvalue.substring(7, argvalue.length);
    if (argvalue.indexOf(".") == -1)
        return false;
    else if (argvalue.indexOf(".") == 0)
        return false;
    else if (argvalue.charAt(argvalue.length - 1) == ".")
        return false;
    if (argvalue.indexOf("/") != -1) {
        argvalue = argvalue.substring(0, argvalue.indexOf("/"));
        if (argvalue.charAt(argvalue.length - 1) == ".")
            return false;
    }
    if (argvalue.indexOf(":") != -1) {
        if (argvalue.indexOf(":") == (argvalue.length - 1))
            return false;
        else if (argvalue.charAt(argvalue.indexOf(":") + 1) == ".")
            return false;
        argvalue = argvalue.substring(0, argvalue.indexOf(":"));
        if (argvalue.charAt(argvalue.length - 1) == ".")
            return false;
    }
    return true;
};

//格式化json日期
String.prototype.Format = function () {
    var fmt = "yyyy年MM月dd日";
    var myDate = getjsondate(this);
    var o = {
        "M+": myDate.getMonth() + 1,
        "d+": myDate.getDate(),
        "h+": myDate.getHours(),
        "m+": myDate.getMinutes(),
        "s+": myDate.getSeconds(),
        "q+": Math.floor((myDate.getMonth() + 3) / 3),
        "S": myDate.getMilliseconds()
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (myDate.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};
function getjsondate(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    } else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }
    return new Date(parseInt(jsondate, 10));
}

//C# Dictionary类型转换为javascript Dictionary
function mapDictionaryToArray(dictionary) {
    var result = [];
    for (var key in dictionary) {
        if (dictionary.hasOwnProperty(key)) {
            result.push({ key: key, value: dictionary[key] });
        }
    }
    return result;
}

//获取QueryString
function getQuery(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return "";
}

message = {
    success: function (msg) {
        Messenger().post({ message: msg, type: 'success' });
    },
    error: function (msg) {
        Messenger().post({ message: 'Error,reason:' + msg, type: 'error' });
    }
};

//#endregion

//#region ko common

function elementFadeIn(elem) { if (elem.nodeType === 1) $(elem).hide().fadeIn(500); }

function itemRefresh(list, entity) {
    var i = list().indexOf(entity);
    list.remove(entity);
    list.splice(i, 0, entity);
}

//#endregion


