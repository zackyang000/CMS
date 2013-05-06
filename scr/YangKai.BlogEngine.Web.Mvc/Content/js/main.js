//#region  setup

$(document).ready(function () {

    menu();
    
    //菜单lock及滑动效果
    tabbedContent.init();
    
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

//格式化json日期
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

function imglazyload(dom) {
    $(dom).lazyload({
        placeholder: "/Content/Image/grey.gif",
        effect: "fadeIn"
    });
}

//#endregion


//菜单滑动效果
function menu() {
    var d = 300;
    $('#menu a').each(function () {
        $(this).stop().animate({
            'marginTop': '-80px'
        }, d += 150);
    });
    $('#menu > li').hover(
        function () {
            $('a', $(this)).stop().animate({
                'marginTop': '-2px'
            }, 200);
        },
        function () {
            $('a', $(this)).stop().animate({
                'marginTop': '-80px'
            }, 200);
        });
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

//菜单lock及滑动效果
var tabbedContent = {
    init: function () {
        var currentParam = urlHelper.getGroupUrlOrChannelUrl();
        $("nav ul li.link").each(function (i) {
            var url = $("nav ul li.link").eq(i).find('a').prop('href');
            var linkParam = urlHelper.getGroupUrlOrChannelUrl(url);
            if (currentParam == linkParam) {
                $("nav ul li.moving_bg").css("left", $("nav ul li.link").eq(i).position()['left']);
                $("nav ul li.link").eq(i).addClass('nohover');
            }
        });

        $("nav ul li.link").click(function () {
            var o = $(this);

            //先将所有item设置为nohover,否则hover将遮挡move效果.
            o.parent().find('.link').addClass('nohover');

            var background = o.parent().find(".moving_bg");
            $(background).stop().animate({ left: o.position()['left'] }, 300, function () {
                o.siblings().removeClass("nohover");
            });
        });
    }
};

var urlHelper = {
    //获取QueryString
    //    getQuery: function(name) {
    //        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    //        var r = window.location.search.substr(1).match(reg);
    //        if (r != null)
    //            return unescape(r[2]);
    //        return "";
    //    },

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
    getChannelUrl: function(url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以(http)://开头],[经过一个"/"],[以"#"或"/"或"-"结尾,若没有,则取后面所有] 的字符串.
        var r = new RegExp("://(.*?)/(.*?)(?:#.*)?(?:/.*)?(?:-.*)?$");
        var m = url.match(r);
        return m ? m[2] : "";
    },
    getGroupUrl: function(url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以#!/开头],[以"/"或"?"结尾,若没有,则取后面所有] 的字符串.
        var r = new RegExp("#!/(.*?)(?:(/|[\?]).*)?$");
        var m = url.match(r);
        return m ? (IsNum(m[1]) ? "" : m[1])  : "";
    },
    getGroupUrlOrChannelUrl:function(url) {
        var group = urlHelper.getGroupUrl(url);
        if (group != "") {
            return group;
        }
        return urlHelper.getChannelUrl(url);
    },
    getPage: function(url) {
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
    getQuery: function(url) {
        if (url == undefined) {
            url = window.location.toString();
        }
        //匹配 [以?开头] 的字符串.
        var r = new RegExp("[\?](.*?)$");
        var m = url.match(r);
        return m ? m[0] : "";
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


