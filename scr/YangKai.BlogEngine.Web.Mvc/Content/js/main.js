//#region  setup

$(document).ready(function () {

    //ajax禁用IE缓存
    $.ajaxSetup({ cache: false });

    infuser.defaults.templateUrl = "/content/ko/template";
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

//格式化json日期
String.prototype.Format = function (fmt) {
    if (fmt==null) fmt = "yyyy-MM-dd";
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

function imglazyload(dom) {
    $(dom+" img").lazyload({
        placeholder: "/Content/Image/grey.gif",
        effect: "fadeIn"
    });
}

function codeformat() {
    jQuery(function () {
        SyntaxHighlighter.defaults['gutter'] = true;
        SyntaxHighlighter.defaults['collapse'] = false;
        SyntaxHighlighter.highlight();
    });
}

//#endregion

//sider搜索
function SearchkeyDown(txt) {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        var channel = urlHelper.getChannelUrl();
        var group = urlHelper.getGroupUrl();
        location.href = '/' + channel + '#!/' + group + '/search/' + txt;
    }
}

//#region common

//判断是否为数字
function IsNum(s) {
    if (s != null && s != "") {
        return !isNaN(s);
    }
    return false;
}

//json日期转换为Date对象
function ConvertJsonDate(jsondate) {
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

//alert方法
var message = {
    success: function (msg) {
        Messenger().post({ message: msg, type: 'success' });
    },
    error: function (msg) {
        Messenger().post({ message: 'Error,reason:' + msg, type: 'error' });
    }
};

//#endregion

//#region ko common

//淡入
function elementFadeIn(elem) { if (elem.nodeType === 1) $(elem).hide().fadeIn(500); }

//刷新list中的item
function itemRefresh(list, entity) {
    var i = list().indexOf(entity);
    list.remove(entity);
    list.splice(i, 0, entity);
}

//#endregion



var urlHelper = {
    //format: 
    //*{groupUrl}
    //*{groupUrl}/{page}
    //*{groupUrl}?{query}
    //*{groupUrl}/{page}?{query}
    //http://www.woshinidezhu.com/{ChannelUrl}#xxx
    //default value:
    //groupUrl:由route定义
    //page:1
    //query:""
    getChannelUrl: function (url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以(http)://开头],[经过一个"/"],[以"#"或"/"或"-"结尾,若没有,则取后面所有] 的字符串.
        var r = new RegExp("://(.*?)/(.*?)(?:#.*)?(?:/.*)?(?:-.*)?$");
        var m = url.match(r);
        return m ? m[2] : "";
    },
    getGroupUrl: function (url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以#!/开头],[以"/"或"?"结尾,若没有,则取后面所有] 的字符串.
        var r = new RegExp("#!/(.*?)(?:(/|[\?]).*)?$");
        var m = url.match(r);
        if (m) return (IsNum(m[1]) ? "" : m[1]);

        r = new RegExp("//(.*)/(.*)-(.*?)?$");
        m = url.match(r);
        return m ? m[2] : "";
    },
    getGroupUrlOrChannelUrl: function (url) {
        var group = urlHelper.getGroupUrl(url);
        if (group != "") {
            return group;
        }
        return urlHelper.getChannelUrl(url);
    },
    getPage: function (url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以#!/开头],[经过一个"/"],[以"?"结尾,若没有,则取后面所有] 的数字.
        var r = new RegExp("#!/(.*?)/(.*?)(?:[\?].*)?$");
        var m = url.match(r);
        if (m && (IsNum(m[2]))) return m[2];
        //没有group的情况
        r = new RegExp("#!/(.*?)(?:[\?].*)?$");
        m = url.match(r);
        return m ? (IsNum(m[1]) ? m[1] : 1) : 1;
    },
    getQuery: function (url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以?开头] 的字符串.
        var r = new RegExp("[\?](.*?)$");
        var m = url.match(r);
        return m ? m[0] : "";
    }
};


