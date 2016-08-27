function HelperAce(el, readonly) {
    var _editor = undefined;

    var initEditor = function (el, readonly) {
        _editor = ace.edit(el);
        _editor.setTheme("ace/theme/twilight");
        _editor.session.setMode("ace/mode/csharp");
        if (readonly)
            _editor.setReadOnly(true);
        _editor.setFontSize(16);
        _editor.setPrintMarginColumn(false);
        _editor.session.setUseWrapMode(true);
    };

    this.insertText = function (text) {
        _editor.selectAll();
        _editor.removeLines();
        _editor.insert(text);
    };

    this.autoCopyText = function (selector) {
        _editor.on("change", function (e) {
            $(selector).val(_editor.getValue());
        });
    };

    initEditor(el, readonly);
};
