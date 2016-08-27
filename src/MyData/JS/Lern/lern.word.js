
var lern = lern || {};

lern.word = {
    options: {
        elEditor: "",
        elMessage: "",
        elAnswer: "",
        urlCheckAnswer: "",
        nextQuection: undefined,
    },
    helper: undefined,
    init: function (options) {
        this.options = $.extend(this.options, options);
        this.helper = as.ace.init({ el: this.options.elEditor, readonly: true });
        lern.word.initEvents();
        return this;
    },
    initEvents: function() {
    },
    showQuestion: function (testData) {
        this.helper.insertText(testData.Question);
        $(lern.word.options.elMessage).empty();
        $(lern.word.options.elAnswer).empty();
    },
    checkAnswer: function(answerID) {
        $.ajax({ type: "POST", url: lern.word.options.urlCheckAnswer, data: { answer: answerID },
            success: function (response) {
                console.log(response);
                if (response.IsCorrect) {
                    lern.word.viewSuccess(response);
                } else {
                    lern.word.viewError(response);
                }
            }
        });
    },
    viewSuccess: function(response) {
        $(lern.word.options.elMessage).html("Верно!").css("color", "green");
        $(lern.word.options.elAnswer).hide();
        setTimeout(lern.word.next, 2000);
    },
    viewError: function(responce) {
        $(lern.word.options.elMessage).html("Упс!").css("color", "red");
    },
    next: function () {
        lern.word.options.nextQuection();
    }
};