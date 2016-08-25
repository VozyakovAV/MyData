
var lern = lern || {};

lern.test = {
    options: {
        elEditor: "",
        elMessage: "",
        elAnswers: "",
        urlCheckAnswer: "",
        ttt: undefined,
    },
    helper: undefined,
    init: function (options) {
        lern.test.options = $.extend(lern.test.options, options);
        helper = as.ace.init({ el: lern.test.options.elEditor, readonly: true });
        lern.test.initEvents();
        return this;
    },
    initEvents: function() {
        $(lern.test.options.elAnswers).delegate(".answer", "click", function (e) {
            e.preventDefault();
            if ($(this).data("correctAnswer") == 1)
                lern.test.next();
            else {
                var answerID = $(this).data("id");
                lern.test.checkAnswer(answerID);
            }
        });
    },
    showQuestion: function (testData) {
        helper.insertText(testData.Question);
        $(lern.test.options.elMessage).empty();
        $(lern.test.options.elAnswers).empty();
        $.each(testData.Answers, function (i, answer) {
            var awr = as.sys.htmlEncode(answer);
            var link = "<a href='#' class='answer label-info' data-id='" + i + "'><pre>" + awr + "</pre></a>";
            $(lern.test.options.elAnswers).append(link);
        });
        $(lern.test.options.elAnswers).show();
        $(lern.test.options.elMessage).empty();
    },
    checkAnswer: function(answerID) {
        $.ajax({ type: "POST", url: lern.test.options.urlCheckAnswer, data: { answer: answerID },
            success: function (response) {
                console.log(response);
                if (response.IsCorrect) {
                    lern.test.viewSuccess(response);
                } else {
                    lern.test.viewError(response);
                }
            }
        });
    },
    viewSuccess: function(response) {
        $(lern.test.options.elMessage).html("Верно!").css("color", "green");
        $(lern.test.options.elAnswers).hide();
        setTimeout(lern.test.next, 2000);
    },
    viewError: function(responce) {
        $(lern.test.options.elMessage).html("Упс!").css("color", "red");
        $(lern.test.options.elAnswers + ' .answer').removeClass("label-info").addClass("label-danger");
        var awr = $($(lern.test.options.elAnswers + ' .answer').get(responce.CorrectAnswer));
        awr.removeClass("label-danger").addClass("label-success");
        awr.data("correctAnswer", "1")
    },
    next: function () {
        lern.test.options.ttt();
    }
};