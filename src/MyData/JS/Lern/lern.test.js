
var lern = lern || {};

lern.test = {
    opt: {
        elContent: "",
        elEditor: "",
        elMessage: "",
        elAnswers: "",
        urlCheckAnswer: "",
        nextQuection: undefined,
    },
    helper: undefined,
    getEditor: function() {
        return $(this.opt.elContent + " " + this.opt.elEditor);
    },
    getMessage: function () {
        return $(this.opt.elContent + " " + this.opt.elMessage);
    },
    getAnswers: function () {
        return $(this.opt.elContent + " " + this.opt.elAnswers);
    },
    init: function (opt) {
        this.opt = $.extend(this.opt, opt);
        this.helper = as.ace.init({ el: this.opt.elEditor, readonly: true });
        this.initEvents();
        return this;
    },
    initEvents: function () {
        this.getAnswers().delegate(".answer", "click", function (e) {
            e.preventDefault();
            if ($(this).data("correctAnswer") == 1)
                this.next();
            else {
                var answerID = $(this).data("id");
                this.checkAnswer(answerID);
            }
        });
    },
    showQuestion: function (testData) {
        this.helper.insertText(testData.Question);
        this.getMessage().empty();
        var divAnswers = this.getAnswers();
        divAnswers.empty();
        $.each(testData.Answers, function (i, answer) {
            var awr = as.sys.htmlEncode(answer);
            var link = "<a href='#' class='answer label-info' data-id='" + i + "'><pre>" + awr + "</pre></a>";
            divAnswers.append(link);
        });
        this.getAnswers().show();
    },
    checkAnswer: function(answerID) {
        $.ajax({ type: "POST", url: this.opt.urlCheckAnswer, data: { answer: answerID },
            success: function (response) {
                console.log(response);
                if (response.IsCorrect) {
                    this.viewSuccess(response);
                } else {
                    this.viewError(response);
                }
            }
        });
    },
    viewSuccess: function(response) {
        this.getMessage().html("Верно!").css("color", "green");
        this.getAnswers().hide();
        setTimeout(this.next, 2000);
    },
    viewError: function(responce) {
        this.getMessage().html("Упс!").css("color", "red");
        this.getAnswers().filter('.answer').removeClass("label-info").addClass("label-danger");
        var awr = $(this.getAnswers().filter('.answer').get(responce.CorrectAnswer));
        awr.removeClass("label-danger").addClass("label-success");
        awr.data("correctAnswer", "1")
    },
    next: function () {
        this.opt.nextQuection();
    }
};