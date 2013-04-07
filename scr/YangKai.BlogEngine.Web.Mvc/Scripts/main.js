//展开文章
function show_detail(id) {
    $("#showdetail" + id).animate({
        opacity: 'toggle'
    }, 300);
    setTimeout(function () {
        $("#hidedetail" + id).animate({
            opacity: 'toggle'
        }, 300);
    }, 2000);
    
    $("#post-body-detail-" + id).html("<img src='/Content/Image/ajax-loader2.gif'' alt='' />加载中...");
    $("#post-body-desc-" + id).animate({
            opacity: 'toggle'
        }, 300);
        setTimeout(function () {
            $("#post-body-detail-" + id).animate({
                opacity: 'toggle'
            }, 300);
        }, 300);
    
    setTimeout(function() {
        $.ajax({
                type: "POST",
                url: "/Article/FastDetail",
                dataType: "json",
                data: "postId=" + id,
                success: function(context) {
                    $("#post-body-detail-" + id).hide();
                    if (context.success) {
                        $("#post-body-detail-" + id).html(context.content);
                    }
                    else {
                        $("#post-body-detail-" + id).html("<div style='color:red'>" + context.reason + "</div>");
                    }
                    $("#post-body-detail-" + id).animate({
                            opacity: 'toggle'
                        }, 300);
                },
                complete: function() {

                }
            });
        }, 600);
}

//收缩文章
function hide_detail(id) {
    $("#hidedetail" + id).animate({
        opacity: 'toggle'
    }, 300);
    setTimeout(function() {
        $("#showdetail" + id).animate({
            opacity: 'toggle'
        }, 300);
    }, 2000);

    $("#post-body-detail-" + id).animate({
        opacity: 'toggle'
    }, 300);
    setTimeout(function () {
        $("#post-body-desc-" + id).animate({
            opacity: 'toggle'
        }, 300);
    }, 300);
}

//删除文章
function post_delete(id) {
    $.ajax({
        url: "/admin/postmanage/delete",
        type: "POST",
        dataType: "json",
        data: "id=" + id,
        success: function (context) {
            if (context.result) {
                $("#list" + id).animate({
                    opacity: 'toggle',
                    backgroundColor: '#ff8888'
                }, 500);
                setTimeout(function () {
                    var title = $("#list" + id + " h2 strong").html();
                    $("#list" + id).after("<li id=\"post-deleted-" + id + "\" style=\"display: none;\"><td style=\"padding-left:50px; padding-bottom:5px;\">《<strong>" + title + "</strong>》已移至回收站。 <a href=\"JavaScript:void(0)\" onclick=\"post_deleted_revoke('" + id + "')\">撤销</a></td></tr>");
                    $("#post-deleted-" + id).animate({
                        opacity: 'toggle'
                    }, 500);
                }, 500);
            }
            else { alert(context.reason); }
        }
    });
}

//撤销删除文章
function post_deleted_revoke(id) {
    $.ajax({
        url: "/admin/postmanage/renew",
        type: "POST",
        dataType: "json",
        data: "id=" + id,
        success: function (context) {
            if (context.result) {
                $("#post-deleted-" + id).animate({
                    opacity: 'toggle',
                    backgroundColor: '#88ff88'
                }, 500);
                setTimeout(function () {
                    $("#list" + id).animate({
                        opacity: 'toggle'
                    }, 500);
                    $("#post-deleted-" + id).remove();
                }, 500);
            }
            else { alert(context.reason); }
        }
    });
}

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

function page_begin() {
    window.location.href = "#content";
    $("#posts-and-pager").html("<img src=\"/Content/Image/ajax-loader2.gif\" alt=\"\" />");
}

//#region common

//html转义
function HtmlEncode(text) {
    return text.replace(/&/g, '&amp').replace(/\"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

//转换日期格式yyyy.MM.dd HH:mm(兼容FF,chrome)
function GetDateFormat(date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1);
    if (month < 10) {
        month = "0" + month;
    }
    var day = date.getDate();
    if (day < 10) {
        day = "0" + day;
    }
    var hour = date.getHours();
    if (hour < 10) {
        hour = "0" + hour;
    }
    var min = date.getMinutes();
    if (min < 10) {
        min = "0" + min;
    }
    return year + "." + month + "." + day + " " + hour + ":" + min;
}

//得到radiobuttonlist的值
function getRadioValue(name) {
    var radios = document.getElementsByName(name);
    var len = radios.length;
    var selectInt = 0;
    for (var i = 0; i < len; i++) {
        if (radios[i].checked == true) {
            selectInt = radios[i].value;
        }
    }
    return selectInt;
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

//#endregion

//#region ko common

function elementFadeIn(elem) { if (elem.nodeType === 1) $(elem).hide().fadeIn(1000); }

//#endregion