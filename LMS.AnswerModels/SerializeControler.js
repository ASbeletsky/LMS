function SinglAnswerSerializer(id) {
    if (!isInteger(id)) {
        return undefined;
    }
    var SinglAnswerModel =
        {
            Answer: id
        }
    return JSON.stringify(SinglAnswerModel);
}
function MultyAnswerSerializer(ListId) {
    var toDelete=[];
    ListId.forEach(function (item, i, arr) {
        if (!isInteger(item)) {
            toDelete.push(i);
        }
    });
    toDelete.forEach(function (item, i, arr) {
        ListId.splice(item - i, 1);
    });
    if (ListId.length < 1) {
        return undefined;
    }
    var MultyAnswerModel =
        {
            Answer: ListId
        }
    return JSON.stringify(MultyAnswerModel);
}
function OpenAnswerSerializer(open) {
    var OpenAnswerModel =
        {
            Answer: open
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
    return answer["Answer"];
}
