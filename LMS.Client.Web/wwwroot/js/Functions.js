function changeMode(mode) {
    var answer = document.getElementById("result").value;
    var answerSer = OpenAnswerSerializer(answer);
    var sessionId = sessionStorage.getItem("sessionId");
    var number = getElementById.getElementById("number").value;
    SendAnswer(sessionId, number, answer);

    //document.querySelector('input[name=mode]').value = mode;
}
function changeGot(got) {
    document.querySelector('input[name=got]').value = got;
}
function showValues() {
    var fields = $(":input").serializeArray();
    console.log($(":input").serializeArray());
    //$("#result").empty();
    //jQuery.each(fields, function (i, field) {
    //    $("#result").append(field.value + " ");
    //});
}
function StoreToSession(type, number, res) {
    if (type === "check" || type === "radio") {
        sessionStorage.setItem(number, SinglAnswerSerializer(Number(res)));
    }
    else if (type === "text" || type === "code") {
        sessionStorage.setItem(number, OpenAnswerSerializer(res));
    }
    showValues();
}
function DeserializeFormFromSession(type, number) {
    var res = Deserializer(sessionStorage.getItem(number));
    if (res !== undefined && res!==null) {
        if (type === "check") {
            res.forEach(function (element) {
                alert(element);
                $("input:checkbox[id='" + element + "']").prop('checked', true);
            });
        }
        else if (type === "radio") {
            $("input:radio[id='" + res + "']").prop('checked', true);
        }
        else if (type === "text") {
            $("textarea").text(res);
        }
        else if (type === "code") {
            editor.setValue(res); // задаем
        }
    }
    return res;
}
function DeserializeForm(type, res) {
    if (type === "check") {
        res.forEach(function (element) {
            alert(element);
            $("input:checkbox[id='" + element + "']").prop('checked', true);
        });
        
    }
    else if (type === "radio") {
        $("input:radio[id='" + res + "']").prop('checked', true);
    }
    else if (type === "text") {
        $("textarea").text(res);
    }
}
function update(number)
{
    var res = editor.getSession().getValue();
    document.querySelector('input[name=result]').value = res;
    sessionStorage.setItem(number, OpenAnswerSerializer(res));
    console.log($(editor).serialize());
}
function getResults() {
    var res = editor.getValue(); // -> String (получаем)
    document.querySelector('input[name=result]').value = res;
    document.querySelector('input[name=mode]').value = 'res'
}