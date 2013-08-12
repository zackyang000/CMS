$(function () {
    $("#post-new-button").button();
});

function post_delete(id) {
    $.ajax({
        url: "/admin/postmanage/delete",
        type: "POST",
        dataType: "json",
        data: "id=" + id,
        success: function (context) {
            if (context.result) {
                $("#post-" + id).animate({
                    opacity: 'toggle',
                    backgroundColor: '#ff8888'
                }, 500);
                setTimeout(function () {
                    var tdLength = $("#post-" + id).children("td").length;
                    var title = $("#post-" + id + " td strong a").html();
                    $("#post-" + id).after("<tr id=\"post-deleted-" + id + "\" style=\"display: none;background:#f4f4f4;\"><td style=\"padding-left:50px; padding-bottom:5px;\" colspan=\"" + tdLength + "\">《<strong>" + title + "</strong>》已移至回收站。 <a href=\"JavaScript:void(0)\" onclick=\"post_deleted_revoke('" + id + "')\">撤销</a></td></tr>");
                    $("#post-deleted-" + id).animate({
                        opacity: 'toggle'
                    }, 500);
                }, 500);
                update_status_summary();
            }
            else { alert(context.reason); }
        }
    });
}

function post_deleted_revoke(id) {
    $.ajax({
        url: "/admin/postmanage/renew",
        type: "POST",
        dataType: "json",
        data: "id=" + id,
        success: function (context) {
            if (context.result) {
                $("#post-" + id).css("background", "#fff");
                $("#post-deleted-" + id).animate({
                    opacity: 'toggle',
                    backgroundColor: '#88ff88'
                }, 500);
                setTimeout(function () {
                    $("#post-" + id).animate({
                        opacity: 'toggle'
                    }, 500);
                    $("#post-deleted-" + id).remove();
                }, 500);
                update_status_summary();
            }
            else { alert(context.reason); }
        }
    });
}

function post_renew(id) {
    $.ajax({
            url: "/admin/postmanage/renew",
            type: "POST",
            dataType: "json",
            data: "id=" + id,
            success: function(context) {
                if (context.result) {
                    $("#post-" + id).animate({
                            opacity: 'toggle',
                            backgroundColor: '#88ff88'
                        }, 500);
                    setTimeout(function() {
                        var tdLength = $("#post-" + id).children("td").length;
                        var title = $("#post-" + id + " td strong a").html();
                        $("#post-" + id).after("<tr id=\"post-deleted-" + id + "\" style=\"display: none;\"><td style=\"padding-left:50px; padding-bottom:5px;\" colspan=\"" + tdLength + "\">《<strong>" + title + "</strong>》已从回收站中恢复。 </td></tr>");
                        $("#post-deleted-" + id).animate({
                                opacity: 'toggle'
                            }, 500);
                    }, 500);
                    update_status_summary();
                }
                else { alert(context.reason); }
            }
        });
}

function update_status_summary() {
    var status = request("status");
    $("#status-summary").load("/admin/postmanage/status-summary?status=" + status + " #status-summary ul");
}