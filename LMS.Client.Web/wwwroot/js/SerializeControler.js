function SingleAnswerSerializer(id) {
    if (!isInteger(id)) {
        return undefined;
    }
    var SingleAnswerModel =
        {
            AnswerOptionId: id
        }
    return JSON.stringify(SingleAnswerModel);
}
function OpenAnswerSerializer(open) {
    var OpenAnswerModel =
        {
            Content: open
        }
    return JSON.stringify(OpenAnswerModel);
}

function isInteger(number) {
    var res = parseInt(number);
    if (typeof number !== "number" || !isFinite(number) || res !== number) {
        return false;
    }
    return true;
}

function Deserializer(str) {
    var answer = JSON.parse(str);
    for (var key in answer) {
        return answer[key];
    }
}
