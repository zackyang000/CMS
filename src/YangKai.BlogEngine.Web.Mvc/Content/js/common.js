angular.resetForm = function (scope, formName) {
    $('form[name=' + formName + '], form[name=' + formName + '] .ng-dirty').removeClass('ng-dirty').addClass('ng-pristine');
    var form = scope[formName];
    form.$dirty = false;
    form.$pristine = true;
    for (var field in form) {
        if (form[field].$pristine === false) {
            form[field].$pristine = true;
        }
        if (form[field].$dirty === true) {
            form[field].$dirty = false;
        }
    }
};

//#region  setup

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

//#endregion

//#region common

function codeformat() {
    jQuery(function () {
        SyntaxHighlighter.defaults['gutter'] = true;
        SyntaxHighlighter.defaults['collapse'] = false;
        SyntaxHighlighter.highlight();
    });
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
        Messenger().post({ message: msg, type: 'success', showCloseButton: true });
    },
    error: function (msg) {
        if (msg == '401') {
            msg = 'Unauthorized.';
        }
        Messenger().post({ message: msg, type: 'error', showCloseButton: true, delay: 60 });
    },
    confirm: function (callback) {
        var msg = Messenger().post({
            message: "Do you want to continue?",
            id: "Only-one-message",
            showCloseButton: true,
            actions: {
                OK: {
                    label: 'OK',
                    phrase: 'Confirm',
                    delay: 60,
                    action: function () {
                        callback();
                        msg.cancel();
                    }
                },
                cancel: {
                    action: function () {
                        return msg.cancel();
                    }
                }
            }
        });
    }
};

//#endregion