
var as = as || {};

as.ace = {
    editor: "",
    options: {
        el: "",
        readonly: false,
    },
    init: function (options) {
        as.ace.options = $.extend(as.ace.options, options);
        as.ace.editor = as.ace.initEditor();
        return this;
    },
    initEditor: function () {
        var editor = ace.edit(as.ace.options.el);
        editor.setTheme("ace/theme/twilight");
        editor.session.setMode("ace/mode/csharp");
        if (as.ace.readonly)
            editor.setReadOnly(true);
        editor.setFontSize(16);
        editor.setPrintMarginColumn(false);
        editor.session.setUseWrapMode(true);
        return editor;
    },
    insertText: function (text) {
        var editor = ace.edit(as.ace.options.el);
        editor.selectAll();
        editor.removeLines();
        editor.insert(text);
    },
    autoCopyText: function (selector) {
        as.ace.editor.on("change", function (e) {
            $(selector).val(as.ace.editor.getValue());
        });
    }
};