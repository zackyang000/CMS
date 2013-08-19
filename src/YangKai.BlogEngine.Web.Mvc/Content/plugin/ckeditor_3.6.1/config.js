/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    config.toolbar_Main =
    [
    ['Source'],
    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    ['NumberedList', 'BulletedList'],
    ['JustifyLeft', 'JustifyCenter', 'JustifyRight','Link', 'Unlink'],
    ['Code', 'Image',  'HorizontalRule'],
    ['Format'],
    ['TextColor', 'BGColor'],
    ['Maximize']
    ];

    config.toolbar_Basic =
    [
    ['Source'],
    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    ['Link', 'Unlink', 'Anchor'],
    ];

    config.EnterMode = 'p'; // p | div | br   表示回车换行用<p>标签
    config.ShiftEnterMode = 'br'; // p | div | br 表示shifit + 回车换行用<br>标签

    //在 CKEditor 中集成 CKFinder，注意 ckfinder 的路径选择要正确。
    var ckfinderPath = "/Content/plugin/ckfinder_aspnet_2.0.1"; //ckfinder路径
    config.filebrowserBrowseUrl = ckfinderPath + '/ckfinder.html';
    config.filebrowserImageBrowseUrl = ckfinderPath + '/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = ckfinderPath + '/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = ckfinderPath + '/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = ckfinderPath + '/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = ckfinderPath + '/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    config.extraPlugins = 'syntaxhighlight';
};
