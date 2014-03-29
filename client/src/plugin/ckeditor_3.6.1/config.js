/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    config.toolbar_Main =
    [
    ['Source'],
    ['Bold', 'Italic'],
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

    config.extraPlugins = 'syntaxhighlight';
};
