﻿var testTemplate = (function () {
    this.refreshByIdTimers = [];

    this.maxScoreChanged = function() {
        var totalMaxScore = $(".level input[name$='.MaxScore']")
            .map(function () { return parseInt($(this).val()) })
            .toArray()
            .reduce(function (a, b) { return a + b; }, 0);

        if (totalMaxScore != 100) {
            $("#maxScoreInvalid").text("Total max score must be equal to 100");
        }
        else {
            $("#maxScoreInvalid").text("");
        }
    }

    this.updateScorePerTask = function(index) {
        maxScoreChanged();

        var maxScore = $("input[name='Levels[" + index + "].MaxScore']").val();
        var count = $("input[name='Levels[" + index + "].Count']").val();
        $("#levelMaxScoreLabel" + index).text(maxScore);
        $("#levelCountLabel" + index).text(count);
        var scorePerTask = (maxScore / count).toFixed(2);
        $("input[name='Levels[" + index + "].ScorePerTask']").val(scorePerTask);
    }

    this.complexityChanged = function(index) {
        refreshLevel(index);

        var complexity = $("input[name='Levels[" + index + "].Filter.ComplexityRange']").val().replace(',', '-');
        $("#levelComplexityLabel" + index).text(complexity);
    }

    this.refreshLevel = function(index) {
        if (refreshByIdTimers[index]) {
            clearTimeout(refreshByIdTimers[index]);
        }
        refreshByIdTimers[index] = setTimeout(function () {
            var complexityRange = $("input[name='Levels[" + index + "].Filter.ComplexityRange']").attr("value");
            var selectedCategories = $("select[name='Levels[" + index + "].Filter.CategoryIds']")
                .val()
                .map(function (o) { return parseInt(o); });
            var selectedTasks = $("select[name='Levels[" + index + "].Filter.TaskTypeIds']")
                .val()
                .map(function (o) { return parseInt(o); });

            $.ajax({
                type: 'POST',
                url: '/task/filter',
                traditional: true,
                data: {
                    ComplexityRange: complexityRange,
                    CategoryIds: selectedCategories,
                    TaskTypeIds: selectedTasks
                },
                contentType: "application/x-www-form-urlencoded",
                success: function (data) {
                    $("input[name='Levels[" + index + "].ValidTaskCount']").val(data["count"]);
                }
            });
        },
            500);
    }

    this.removeLevel = function(level) {
        var index = $(level).attr("data-index");
        level.remove();
        while (true) {
            var level = $(".level[data-index=" + (++index) + "]");
            if (level.length == 0)
                break;
            var newIndex = index - 1;
            level.find(".card-header a").text("Level #" + (newIndex + 1));
            level.attr("data-index", newIndex);
            $("[name^='Levels[" + index + "]'")
                .each(function () {
                    $(this).attr("name", $(this).attr("name").replace("Levels[" + index + "]", "Levels[" + newIndex + "]"))
                });
        }
    }

    this.maxScoreChanged();

    return this;
})();