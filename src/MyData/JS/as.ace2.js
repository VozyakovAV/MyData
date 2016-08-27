
var as = as || {};

as.ace = {
    editor: "",
    options: {
        el: "",
        readonly: false,
    },
    init: function (options) {
        this.options = $.extend(this.options, options);
        this.editor = this.initEditor();
        return this;
    },
    initEditor: function () {
        var editor = ace.edit(this.options.el);
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
        var editor = ace.edit(this.options.el);
        editor.selectAll();
        editor.removeLines();
        editor.insert(text);
    },
    autoCopyText: function (selector) {
        this.editor.on("change", function (e) {
            $(selector).val(this.editor.getValue());
        });
    }
};