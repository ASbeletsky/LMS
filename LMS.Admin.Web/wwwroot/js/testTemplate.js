var testTemplate = (function () {
    this.refreshByIdTimers = [];

    this.maxScoreChanged = function () {
        var inputs = $(".level input[name$='.MaxScore']");
        var totalMaxScore = inputs
            .map(function () { return parseInt($(this).val()) })
            .toArray()
            .reduce(function (a, b) { return a + b; }, 0);

        var requiredValue =  inputs.data("slider-max");

        if (totalMaxScore !== requiredValue) {
            var overflow = totalMaxScore - requiredValue;
            var message = "Total max score should be equal to " + requiredValue + "<br/>";
            if (overflow > 0)
                message += "You need to remove " + overflow + " points from score<br/>";
            else
                message += "You need to add " + (-overflow) + " points to score<br/>";

            $("#maxScoreInvalid").html(message);
        }
        else {
            $("#maxScoreInvalid").text("");
        }
    }

    this.updateScorePerTask = function (index) {
        this.maxScoreChanged();

        var maxScore = $("input[name='Levels[" + index + "].MaxScore']").val();
        var count = $("input[name='Levels[" + index + "].Count']").val();
        $("#levelMaxScoreLabel" + index).text(maxScore);
        $("#levelCountLabel" + index).text(count);
        var scorePerTask = (maxScore / count).toFixed(2);
        $("input[name='Levels[" + index + "].ScorePerTask']").val(scorePerTask);
    }

    this.complexityChanged = function (index) {
        this.refreshLevel(index);

        var complexity = $("input[name='Levels[" + index + "].Filter.ComplexityRange']").val().replace(',', '-');
        $("#levelComplexityLabel" + index).text(complexity);
    }

    this.refreshLevel = function (index) {
        if (this.refreshByIdTimers[index]) {
            clearTimeout(this.refreshByIdTimers[index]);
        }
        this.refreshByIdTimers[index] = setTimeout(function () {
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

    this.removeLevel = function (level) {
        var index = $(level).attr("data-index");
        level.remove();
        while (true) {
            var nextLevel = $(".level[data-index=" + (++index) + "]");
            if (nextLevel.length === 0)
                break;
            var newIndex = index - 1;
            nextLevel.find(".card-header a").text("Level #" + (newIndex + 1));
            nextLevel.attr("data-index", newIndex);
            $("[name^='Levels[" + index + "]'")
                .each(function () {
                    $(this).attr("name", $(this).attr("name").replace("Levels[" + index + "]", "Levels[" + newIndex + "]"))
                });
        }
        this.maxScoreChanged();

        var levels = $(".level");
        if (levels.length === 1) {
            levels.find(".level-remove").remove();
        }
    }

    this.maxScoreChanged();

    return this;
})();