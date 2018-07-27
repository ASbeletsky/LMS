var testSession = (function () {
    var testSession = {};

    testSession.selectTemplate = function (templateId, configureDuration) {
        $.ajax({
            type: 'GET',
            url: buildUrlWithParam('/Test/Filter', 'templateId', templateId),
            success: function (tests) {
                if (configureDuration) {
                    if (tests[0] && tests[0].testTemplate) {
                        testSession.configureDurationPicker(tests[0].testTemplate.duration);
                    }
                    else {
                        testSession.configureDurationPicker("00:00:00");
                    }
                }

                $("select#tests").html("");
                var testIds = $("select#tests").data("testIds");
                for (var testIndex in tests) {
                    var test = tests[testIndex];
                    var selected = testIds.indexOf(test.id) >= 0 ? "selected" : "";
                    $("select#tests").append(
                        "<option " + selected + " value='" + test.id + "'>" + test.title + "</option>"
                    );
                }
                $('select#tests').selectpicker('refresh');
            }
        });
    }

    testSession.configureDurationPicker = function (duration) {
        $('#durationPicker').datetimepicker("destroy");
        var date = moment(duration, "HH:mm:ss").toDate();
        $('#durationPicker').datetimepicker({
            date: date,
            format: 'HH:mm'
        });
        $("#sessionDuration").val(date.getHours() + ":" + date.getMinutes() + ":00");
        $('#durationPicker').on("change.datetimepicker", function (e) {
            var date = e.date.toDate();
            $("#sessionDuration").val(date.getHours() + ":" + date.getMinutes() + ":00");
        });
    }

    testSession.init = function (args) {
        var templateId = $("select#testTemplateSelect").val();

        var defaultDuration = $("#sessionDuration").data("default");
        testSession.configureDurationPicker(defaultDuration);

        if (templateId > 0) {
            $("select#testTemplateSelect").selectpicker('val', templateId);
            testSession.selectTemplate(templateId, args.configureDurationFromTemplate);
        }

        var defaultStartTime = new Data($("#sessionStartTime").data("default"));
        $('#startTimePicker').datetimepicker({
            date: defaultStartTime,
            format: 'DD.MM.YYYY HH:mm'
        });
        $("#sessionStartTime").val(defaultStartTime);
        $('#startTimePicker').on("change.datetimepicker", function (e) {
            $("#sessionStartTime").val(e.date.toJSON());
        });
    }

    return testSession;
})();
