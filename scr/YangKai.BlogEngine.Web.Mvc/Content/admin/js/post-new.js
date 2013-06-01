$(function () {
    $("input:submit").button();
    $("input:button").button();
    CKEDITOR.replace('post_content', { toolbar: 'Main' });
    CKEDITOR.replace('post_description', { toolbar: 'Basic' });
});

//#region main-box

$(function () {
    $("#post_title").bind("focusout", function () { get_post_url(); });
    $("#post_url").change(function () { set_fixed_link(); });
});

function get_post_url() {
    window.mycallback = function (response) {
        response = $.trim(response);
        response = response.toLowerCase();
        response = response.replace(/[^_a-zA-Z\d\s]/g, '');
        response = response.replace(/[\s]/g, "-");
        $("#post_url").val(response);
        $("#post-url-ajax-loading").addClass("ajax-loading");
        set_fixed_link();
    };
    $("#post-url-ajax-loading").removeClass("ajax-loading");
    var s = document.createElement("script");
    s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + $("#post_title").val();
    document.getElementsByTagName("head")[0].appendChild(s);
}

function set_fixed_link() {
    if ($("#post_url").val().length > 0) {
        $("#fixed-link").html("http://www.woshinidezhu.com/post/" + $("#post_url").val());
        $("#fixed-link-div").removeClass("fixed-link-div");
    }
    else {
        $("#fixed-link-div").addClass("fixed-link-div");
    }
}
//#endregion

//#region submit-box

$(function () {
    $("#pubdate").datepicker({
        showAnim: "drop",
        showMonthAfterYear: true,
        //changeMonth: true,
        //changeYear: true,
        buttonImageOnly: true,
        minDate: "0",
        maxDate: "+1y",
        dateFormat: "yy-mm-dd"
    });
    $("#post-status-select").hide();
    $("#timestamp-select-div").hide();
    $(".save-post-status").button();
    $(".save-timestamp").button();
    $(".validateTips").hide();
    //编辑文章状态button
    $(".post-status-action").bind("click", function () {
        $("#post-status-select").slideToggle();
    });
    //编辑发布时间button
    $(".edit-timestamp").bind("click", function () {
        $("#timestamp-select-div").slideToggle();
    });
    //提交修改文章状态
    $(".save-post-status").bind("click", function () {
        post_status_change();
    });
    //取消修改文章状态
    $(".cancel-post-status").bind("click", function () {
        $("#post_status").val("draft");
        post_status_change();
    });
    //提交修改发布时间
    $(".save-timestamp").bind("click", function () {
        post_timestamp_change();
    });
    //取消修改发布时间
    $(".cancel-timestamp").bind("click", function () {
        if ($("#isnew").val() == "True") {
            $("#timestamp").html("<b>立即</b>发布");
        }
        $("#timestamp-select-div").slideUp();
    });
    //发布
    $("#publishing-action input").bind("click", function () {
        return publish_post();
    });
    //保存草稿
    $("#save-action input").bind("click", function () {
        save_draft_pending_post();
    });
    //预览
    $("#preview-action input").bind("click", function () {
        alert("TODO:预览");
    });
    //移动到回收站
    $(".submitdelete").bind("click", function () {
        save_deleted_post();
    });
    //快速延迟
    $("#fast-later-publish a").each(function () {
        $(this).bind("click", function () {
            $("#hh").val("09");
            $("#mm").val("00");
            var date = new Date($("#pubdate").val());
            var day = $(this).html().substring(0, 1);
            date.setHours(((Number(date.getHours()) + Number(day))*24));
            $("#pubdate").val(format_date(date));
        });
    });
});

function publish_post() {
    set_category();
    if (submit_check()) {
        $("#publish").prop("disabled", true);
        $("#publishing-ajax-loading").removeClass("ajax-loading");
        if ($("#isnew").val() == "True") {
            $("#isAudit").val("true");
            $("#isDraft").val("false");
            $("#isDel").val("false");
        }
        $("form").submit();
    }
    return false;
}

function save_draft_pending_post() {
    set_category();
    if (draft_check()) {
        $("#save-action input").prop("disabled", true);
        $("#draft-ajax-loading").removeClass("ajax-loading");
        $("form").submit();
    }
}

function save_deleted_post() {
    set_category();
    if (draft_check()) {
        $("#isDel").val("true");
        $("form").submit();
    }
}

function set_category() {
    var categorylist = "";
    $("#categorychecklist li").each(function () {
        if ($(this).children("label").children("input").prop("checked")) {
            categorylist += ",";
            categorylist += $(this).children("label").children("input").val();
        }
    });
    $("#category").val(categorylist.substring(1));
}

function post_status_change() {
    $("#post-status-display").html($("#post_status option:selected").html());
    var state = $("#post_status option:selected").val();
    if (state == "draft") {
        $("#save-action input").val("保存草稿");
        $("#isAudit").val("true");
        $("#isDraft").val("true");
        $("#isDel").val("false");
    }
    else if (state == "pending") {
        $("#save-action input").val("保存为待审");
        $("#isAudit").val("false");
        $("#isDraft").val("false");
        $("#isDel").val("false");
    }
    else if (state == "publish") {
        $("#save-action input").hide();
        $("#isAudit").val("true");
        $("#isDraft").val("false");
        $("#isDel").val("false");
    }
    $("#post-status-select").slideUp();
}

function post_timestamp_change() {
    var date = $("#pubdate").val();
    var time = $("#hh").val() + "：" + $("#mm").val();
    $("#timestamp").html("发布：" + date + " @ " + time);
    $("#timestamp-select-div").slideUp();
}

function submit_check() {
    var postUrl = $("#post_url").val();
    var postTitle = $("#post_title").val();
    var categroy = $("#category").val();
    var postContent = CKEDITOR.instances.post_content.getData();
    var postDescription = CKEDITOR.instances.post_description.getData();
    var info = "";
    var index = 1;
    if (postTitle.length == 0) {
        info += "<br />" + index + ".\"<b>标题</b>\"不能为空\n";
        index++;
    }
    if (postTitle.length > 50) {
        info += "<br />" + index + ".\"<b>标题</b>\"最大允许50个字符,该提交超过系统限制\n";
        index++;
    }
    if (postUrl.length == 0) {
        info += "<br />" + index + ".\"<b>URL</b>\"不能为空";
        index++;
    }
    if (postUrl.length > 150) {
        info += "<br />" + index + ".\"<b>URL</b>\"最大允许150个字符,该提交超过系统限制\n";
        index++;
    }
    if (postContent.length == 0) {
        info += "<br />" + index + ".\"<b>内容</b>\"不能为空\n";
        index++;
    }
    if (postDescription.length == 0) {
        info += "<br />" + index + ".\"<b>摘要</b>\"不能为空\n";
        index++;
    }
    if (categroy.length == 0) {
        info += "<br />" + index + ".\"<b>分类目录</b>\"至少选择一项\n";
        index++;
    }
    updateTips(info.substring(6));
    if (info != "")
        return false;
    return true;
}

function draft_check() {
    var postUrl = $("#post_url").val();
    var postTitle = $("#post_title").val();
    var categroy = $("#category").val();
    var info = "";
    var index = 1;
    if (postTitle.length == 0) {
        info += "<br />" + index + ".\"<b>标题</b>\"不能为空\n";
        index++;
    }
    if (postTitle.length > 50) {
        info += "<br />" + index + ".\"<b>标题</b>\"最大允许50个字符,该提交超过系统限制\n";
        index++;
    }
    if (postUrl.length == 0) {
        info += "<br />" + index + ".\"<b>URL</b>\"不能为空";
        index++;
    }
    if (postUrl.length > 150) {
        info += "<br />" + index + ".\"<b>URL</b>\"最大允许150个字符,该提交超过系统限制\n";
        index++;
    }
    if (categroy.length == 0) {
        info += "<br />" + index + ".\"<b>分类目录</b>\"至少选择一项\n";
        index++;
    }
    updateTips(info.substring(6));
    if (info != "")
        return false;
    return true;
}

function updateTips(t) {
    if (t.length > 0)
        $("#submit-check-tip").html(t).addClass("ui-state-highlight").show();
    else
        $("#submit-check-tip").html(t).removeClass("ui-state-highlight").hide();
}

//#endregion

//#region thumbnail-box

$(function () {
    $("#renew-div").hide();
    $("#del-thumbnail").bind("click", function () { delete_thumbnail(); });
    $("#renew-thumbnail").bind("click", function () { renew_thumbnail(); });
});

function delete_thumbnail() {
    $("#exist-thumbnail").val("false");
    $("#thumbnail-img").slideUp();
    $("#renew-div").slideDown();
}

function renew_thumbnail() {
    $("#exist-thumbnail").val("true");
    $("#thumbnail-img").slideDown();
    $("#renew-div").slideUp();
}

//#endregion

//#region channel-box

$(function () {
    //绑定channel选项卡点击事件并选中第一项
    $("#channel-tabs li a").bind("click", function () { channel_tab_click(this); });
    $("#channel-tabs li:first a").click();
    //绑定group单选按钮点击事件并选中第一项
    $("#channelbox .group-panel li").bind("change", function () { group_radio_click(); });
    $('input[name=groupRadio]').get(0).checked = true;
    group_radio_click();
});

//channel选项卡点击事件
function channel_tab_click(a) {
    $("#channel-tabs li").removeClass("tab");
    $(a).parent().addClass("tab");
    $("#channelbox .tabs-panel").hide();
    $("#group-" + $(a).parent().prop("id").substring(12)).show();
}

//group单选按钮点击事件
function group_radio_click() {
    bind_category($("input[name='groupRadio']:checked").val());
    bind_tag($("input[name='groupRadio']:checked").val());
    set_fixed_link();
}

//#endregion

//#region category-box

$(function () {
    $("#category-tabs li a").bind("click", function () { select_category_tab(this); });
    $("#category-pop").hide();
});

function select_category_tab(link) {
    $("#category-tabs li").removeClass("tab");
    $(link).parent().addClass("tab");
    $("#category-all").hide();
    $("#category-pop").hide();
    if ($(link).html() == "全部分类目录") {
        $("#category-all").show();
    }
    else {
        $("#category-pop").show();
    }
}

function bind_category(groupUrl) {
    $("#categorychecklist").html("");
    $.ajax({
        type: "GET",
        async: "false",
        url: "/admin/postmanage/GetCategory",
        dataType: "json",
        data: "groupUrl=" + groupUrl,
        cache: false,
        success: function (result) {
            var categoryListHtml = "";
            $.each(result, function (i) {
                var categoryCheckbox = "<li><label><input type=\"checkbox\" value=\"" + result[i].Value + "\" />" + result[i].Text + "</label></li>";
                categoryListHtml += categoryCheckbox;
            });
            $("#categorychecklist").html(categoryListHtml);

            //编辑状态自动选中已有"分类目录"
            if ($("#isnew").val() == "False") {
                var categories = $("#edit-category").val().split(',');
                $("#categorychecklist li").each(function () {
                    var currentCategoryInput = $(this).children("label").children("input");
                    $.each(categories, function (i) {
                        if (currentCategoryInput.val() == categories[i]) {
                            currentCategoryInput.prop("checked", true);
                        }
                    });
                });
            }
        }
    });
}

//#endregion

//#region add-category-box

$(function () {
    $("#category-add").hide();
    $("#category-add-toggle").bind("click", function () { $("#category-add").toggle(); });
    $("#add-category-submit").bind("click", function () { add_category(); });
    $("#add-category-name").bind("focusout", function () { get_category_url(); });
    $("#add-category-tip").hide();
});
function add_category() {
    var groupUrl = $("input[name='groupRadio']:checked").val();
    var name = $("#add-category-name").val();
    var url = $("#add-category-url").val();
    if (name != "" && url != "") {
        $.ajax({
            type: "POST",
            url: "/admin/postmanage/AddCategory",
            dataType: "json",
            data: "name=" + name + "&url=" + url + "&groupUrl=" + groupUrl,
            cache: false,
            success: function (context) {
                if (context.result) {
                    bind_category($("input[name='groupRadio']:checked").val());
                    $("#add-category-name").val("");
                    $("#add-category-url").val("");
                    $("#add-category-tip").html("");
                    $("#add-category-tip").hide();
                    $("#category-add-toggle").click();
                }
                else {
                    $("#add-category-tip").show();
                    $("#add-category-tip").addClass("ui-state-highlight").html(context.reason);
                }
            }
        });
    }
    else {
        $("#add-category-tip").show();
        $("#add-category-tip").addClass("ui-state-highlight").html("请输入“名称”与“别名”。");
    }
}
function get_category_url() {
    window.mycallback = function (response) {
        response = $.trim(response);
        response = response.toLowerCase();
        response = response.replace(/[^_a-zA-Z\d\s]/g, '');
        response = response.replace(/[\s]/g, "-");
        $("#add-category-url").val(response);
        $("#category-url-ajax-loading").addClass("ajax-loading");
    };
    $("#category-url-ajax-loading").removeClass("ajax-loading");
    var s = document.createElement("script");
    s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=A4D660A48A6A97CCA791C34935E4C02BBB1BEC1C&from=zh-cn&to=en&text=" + $("#add-category-name").val();
    document.getElementsByTagName("head")[0].appendChild(s);
}

//#endregion

//#region tag-box

$(function () {
    $(".choose_before").bind("click", function () { toggle_before_tag(); });
});

function bind_tag(groupUrl) {
    $("#tag_list").html("");
    $.ajax({
        type: "GET",
        async: "false",
        url: "/admin/postmanage/GetTag",
        dataType: "json",
        data: "groupUrl=" + groupUrl,
        cache: false,
        success: function (result) {
            var tagListHtml = "";
            $.each(result, function (i) {
                var tagLink = ",<a href=\"JavaScript:void(0);\" title=\"" + result[i].Text + " 个话题\">" + result[i].Value + "</a>";
                tagListHtml += tagLink;
            });
            $("#tag_list").html(tagListHtml.substring(1));
            $("#tag_list a").bind("click", function () { add_tag(this); });
        }
    });
}

function toggle_before_tag() {
    $("#tag_list").toggle();
}

function add_tag(tag) {
    if ($("#tag").val().length > 0) $("#tag").val($("#tag").val() + ",");
    $("#tag").val($("#tag").val() + $(tag).html());
}

//#endregion

//#region source-box

$(function () {
    $("#sourceurl").bind("focusout", function () { get_source_title(); });
    $("#source-title-tip").hide();
});

function get_source_title() {
    $("#source-ajax-loading").removeClass("ajax-loading");
    $("#source-title-tip").hide();
    $.ajax({
        type: "GET",
        url: "/admin/postmanage/GetSourceTitle",
        dataType: "json",
        data: "url=" + $("#sourceurl").val(),
        cache: false,
        success: function (context) {
            if (context.result) {
                $("#sourcetitle").val(context.title);
                $("#sourcebox .tip").html("");
            }
            else {
                $("#sourcetitle").val("");
                $("#source-title-tip").show();
                $("#source-title-tip").addClass("ui-state-highlight").html(context.reason);
            }
            $("#source-ajax-loading").addClass("ajax-loading");
            $("#sourceurl").bind("focusout", function () { get_source_title(); });
        }
    });
}

//#endregion

//#region new or edit

$(function () {
    if ($("#isnew").val() == "False") {
        post_timestamp_change();
        $("#channel-tab-" + $("#edit-channel").val() + " a").click();
        $("input[name='groupRadio'][value='" + $("#edit-group").val() + "']").prop("checked", true);
        group_radio_click();

        var state = $("#edit-state").val();
        switch (state) {
            case "publish":
                $("#post_status").prepend($("<option></option>").val("publish").html("已发布"));
                $("#post_status").val("publish");
                post_status_change();
                $("#publish").val("更新");
                break;
            case "future":
                break;
            case "pending":
                $("#post_status").val("pending");
                post_status_change();
                break;
            case "draft":
                break;
            case "trash":
                break;
            default:
        }
    }
    else {
//        CKEDITOR.instances.post_content.on('blur', function () {
//            var content = CKEDITOR.instances.post_content.getData();
//            content = Generate_Brief(content, 50);
//            CKEDITOR.instances.post_description.setData(content);
//        });
    }
});

function Generate_Brief(text, length) {
    if (text.length < length) return text;
    var foremost = text.substr(0, length);

    var re = /<(\/?)(BODY|SCRIPT|P|DIV|H1|H2|H3|H4|H5|H6|ADDRESS|PRE|TABLE|TR|TD|TH|INPUT|SELECT|TEXTAREA|OBJECT|A|UL|OL|LI|BASE|META|LINK|HR|BR|PARAM|IMG|AREA|INPUT|SPAN)[^>]*(>?)/ig;

    var singlable = /BASE|META|LINK|HR|BR|PARAM|IMG|AREA|INPUT/i;
    var stack = new Array(), posStack = new Array();
    while (true) {
        var newone = re.exec(foremost);
        if (newone == null) break;

        if (newone[1] == "") {
            var elem = newone[2];
            if (elem.match(singlable) && newone[3] != "") {
                continue;
            }
            stack.push(newone[2].toUpperCase());
            posStack.push(newone.index);

            if (newone[3] == "") break;
        } else {
            var stackTop = stack[stack.length - 1];
            var end = newone[2].toUpperCase();
            if (stackTop == end) {
                stack.pop();
                posStack.pop();
                if (newone[3] == "") {
                    foremost = foremost + ">";
                }
            }

        };
    }
    var cutpos = posStack.shift();
    foremost = foremost.substring(0, cutpos);

    return foremost;
}

//#endregion