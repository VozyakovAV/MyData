function LernTest(elContent, elEditor, elMessage, elAnswers, urlCheckAnswer, nextQuestion) {
    var _elContent = elContent;
    var _elEditor = elEditor;
    var _elMessage = elMessage;
    var _elAnswers = elAnswers;
    var _urlCheckAnswer = urlCheckAnswer;
    var _nextQuestion = nextQuestion;
    var _helper = undefined;

    var getEditor = function () {
        return $(_elContent + " " + _elEditor);
    };
    var getMessage = function () {
        return $(_elContent + " " + _elMessage);
    };
    var getAnswers = function () {
        return $(_elContent + " " + _elAnswers);
    };
    var init = function() {
        _helper = new HelperAce(_elEditor, true);
        initEvents();
    };
    var initEvents = function () {
        getAnswers().delegate(".answer", "click", function (e) {
            e.preventDefault();
            if ($(this).data("correctAnswer") == 1)
                next();
            else {
                var answerID = $(this).data("id");
                checkAnswer(answerID);
            }
        });
    };
    this.showQuestion = function(testData) {
        _helper.insertText(testData.Question);
        getMessage().empty();
        var divAnswers = getAnswers();
        divAnswers.empty();
        $.each(testData.Answers, function (i, answer) {
            var awr = as.sys.htmlEncode(answer);
            var link = "<a href='#' class='answer label-info' data-id='" + i + "'><pre>" + awr + "</pre></a>";
            divAnswers.append(link);
        });
        getAnswers().show();
    };
    var checkAnswer = function(answerID) {
        $.ajax({ type: "POST", url: _urlCheckAnswer, data: { answer: answerID },
            success: function (response) {
                console.log(response);
                if (response.IsCorrect) {
                    viewSuccess(response);
                } else {
                    viewError(response);
                }
            }
        });
    };
    var viewSuccess = function (response) {
        getMessage().html("Верно!").css("color", "green");
        getAnswers().hide();
        setTimeout(next, 2000);
    };
    var viewError = function (responce) {
        getMessage().html("Упс!").css("color", "red");
        getAnswers().children('.answer').removeClass("label-info").addClass("label-danger");
        var awr = $(getAnswers().children('.answer').get(responce.CorrectAnswer));
        awr.removeClass("label-danger").addClass("label-success");
        awr.data("correctAnswer", "1")
    };
    var next = function () {
        _nextQuestion();
    };

    init();
};
