var test = (function () {
    test = {};
    test.toggleTask = function (level, id) {
        var select = $("select#tasks_" + level);
        var selected = select.find("option[value=" + id + "]:selected");
        if (selected.length > 0) {
            selected.remove();
        }
        else {
            select.append("<option selected value=" + id + "></option>");
        }
        test.checkTasksCount(level);
    }
    test.checkTasksCount = function (level) {
        var select = $("select#tasks_" + level);
        var totalCountOnLevel = select.find("option:selected").length;
        var requiredCount = select.data("requiredCount");
        var overflow = totalCountOnLevel - requiredCount;
        if (overflow != 0) {
            var message = "Level tasks count should be equal to " + requiredCount + "<br/>";
            if (overflow > 0) {
                message += "You need to remove " + overflow + " tasks from level.";
            }
            else {
                message += "You need to add " + (-overflow) + " tasks to level.";
            }
            $("#taskCountDanger_" + level).html(message);
        }
        else {
            $("#taskCountDanger_" + level).html("");
        }
    }
    test.selectTemplate = function (select) {
        var templateId = parseInt($(select).find(":selected").attr("value"));
        if (templateId > 0) {
            location.href = buildUrlWithParam(location.href, 'templateId', templateId);
        }
    }
    $(".level[data-index]").each(function () { test.checkTasksCount($(this).data("index")) });
    return test;
})();