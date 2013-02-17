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

//增加留言
function add_message() {
    $("#info").css({ color: "", display: "block", background: "url(/Content/Image/ajax-loader.gif) 0px 3px no-repeat" });
    $("#info").html("正在提交");
    $("#submit").attr({ "disabled": "disabled" });
    var name = $("#Author").val();
    var email = $("#Email").val();
    var url = $("#Url").val();
    var content = $("#Content").val();
    name = $.trim(name);
    email = $.trim(email);
    url = $.trim(url);
    if (!url.isUrl()) {
        url = "";
    }
    $.ajax({
        type: "POST",
        url: "/Board/Add",
        dataType: "json",
        data: "Content=" + content + "&Author=" + name + "&Email=" + email + "&Url=" + url,
        cache: false,
        success: function (result) {
            if (result.result) {
                content = content.replace(/(\n)/g, "<br />");
                var date = new Date();
                var datetimeFormat = date.getYear() + '.' + (date.getMonth() + 1) + '.' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes();
                var newMessage = "<li><div class=\"gray\">#" + result.index;
                newMessage += "&nbsp;" + name + "&nbsp;" + datetimeFormat;
                newMessage += "</div>";
                newMessage += "<div class=\"msg\">" + content + "</div></li>";
                $("#new").html(newMessage + $("#new").html());
                $("#Content").val("");
                $("#info").html("");
            }
            else {
                $("#info").css({ color: "red" });
                $("#info").html(result.reason);
            }
        },
        complete: function () {
            $("#info").css({ background: "none" });
            $("#submit").removeAttr("disabled");
        }
    });
}

//删除留言
function delete_message(id) {
    $.ajax({
        url: "/board/delete",
        type: "POST",
        dataType: "json",
        data: "id=" + id,
        success: function (result) {
            if (result.result) {
                $("#" + id).hide();
            }
            else { alert(result.reason); }
        }
    });
}

//添加评论
function add_comment(postId, pic) {
    $("#info").css({ color: "", display: "block", background: "url(/Content/Image/ajax-loader.gif) 0px 3px no-repeat" });
    $("#info").html("正在提交");
    $("#submit").attr({ "disabled": "disabled" });
    var name = $("#Author").val();
    var email = $("#Email").val();
    var url = $("#Url").val();
    var content = $("#Content").val();
    var picurl = pic == "" ? "/Content/Image/avatar.png" : pic;

    name = $.trim(name);
    email = $.trim(email);
    url = $.trim(url);
    if (!url.isUrl()) { url = ""; }
    $.ajax({
        type: "POST",
        url: "/Comment/Add",
        dataType: "json",
        data: "Content=" + content + "&Author=" + name + "&Email=" + email + "&Url=" + url + "&PostId=" + postId,
        cache: false,
        success: function (result) {
            if (result.result) {
                content = HtmlEncode(content);
                content = content.replace(/(\n)/g, "<br />");
                var date = GetDateFormat(new Date());
                var li = "<li id=\"list" + result.index + "\" style=\"display:none;\"><div class=\"author\"><div class=\"pic\"><img class=\"avatar\" width=\"32\" height=\"32\" alt=\"avatar\" src=\"" + picurl + "\" complete=\"complete\" /></div><div class=\"name\">" + name + "</div></div><div class=\"info\"><span class=\"date\">" + date + " | #" + result.index + "</span><div class=\"act\"></div><div class=\"clear\"></div><div class=\"content\">" + content + "</div></div><div class=\"clear\"></div></li>";
                if (result.index > 1) {
                    $("#thecomments").append(li);
                    $("#list" + result.index).animate({
                        opacity: 'toggle'
                    }, 1000);
                }
                else {
                    $("#thecomments").animate({
                        opacity: 'toggle'
                    }, 1000);
                    setTimeout(function () {
                        $("#thecomments").html(li);
                        $("#list" + result.index).show();
                        $("#thecomments").animate({
                            opacity: 'toggle'
                        }, 1000);
                    }, 1000);
                }
                $("#Content").val("");
                $("#info").html("");
                $("#hide_author_info").hide();
                $("#show_author_info").show();
                $("#author_info").hide();
            }
            else {
                $("#info").css({ color: "red" });
                $("#info").html(result.reason);
            }
        },
        complete: function () {
            $("#info").css({ background: "none" });
            $("#submit").removeAttr("disabled");
        }
    });
}

//删除评论
function delete_comment(index, id) {
    $.ajax({
            url: "/comment/delete",
            type: "POST",
            dataType: "json",
            data: "id=" + id,
            success: function(result) {
                if (result.result) {
                    $("#date" + index).append("<span id=\"warningbox-" + index + "\" class=\"warningbox\" style=\"display:none;\">该评论已删除,仅管理员可见.</span>");
                    $("#function-" + index).fadeOut(1000);
                    $("#warningbox-" + index).fadeIn(1000);
                }
                else { alert(result.reason); }
            }
        });
}

//恢复评论
function renew_comment(index, id) {
    $.ajax({
        url: "/comment/renew",
        type: "POST",
        dataType: "json",
        data: "id=" + id,
        success: function (result) {
            if (result.result) {
                $("#function-" + index).fadeOut(1000);
                $("#warningbox-" + id).fadeOut(1000, function () {
                    $("#date" + index).append("<span id=\"successbox-" + index
                        + "\" class=\"successbox\" style=\"display:none;\">该评论已恢复.</span>");
                    $("#successbox-" + index).fadeIn(1000);
                });
            }
            else { alert(result.reason); }
        }
    });
}

//游客注销
function guest_loginoff() {
    $.ajax({
        url: "/comment/login-off",
        type: "POST",
        dataType: "json",
        success: function (result) { if (result.result) { alert("注销成功!"); window.location.reload(); } else { alert(result.reason); } }
    });
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

function page_complete() {
   
}

function page_error() {
   
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
//#endregion