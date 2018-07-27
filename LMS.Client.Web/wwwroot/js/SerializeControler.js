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


//test
function sertest() {
    console.log("Serializing");
    console.log("Singl " + SinglAnswerSerializer(2));
    console.log("Singl " + SinglAnswerSerializer(2.1));
    console.log("Singl " + SinglAnswerSerializer("m"));
    console.log("Multy " + MultyAnswerSerializer([1, 2, 3, 4]));
    console.log("Multy " + MultyAnswerSerializer([1, 's', 3, 4]));
    console.log("Multy " + MultyAnswerSerializer([1, 's', 's', 4]));
    console.log("Multy " + MultyAnswerSerializer([1, 's', 's','s', 4,'s','m',7]));
    console.log("Multy " + MultyAnswerSerializer([1, 2.1, 3.2, 4, 7]));
    console.log("Multy " + MultyAnswerSerializer(["1", 2.1, 3.2, 4.1, "m"]));
    console.log("Open " + OpenAnswerSerializer("Some text"));
    console.log("Deserializing");
    console.log("Singl " + Deserializer(SinglAnswerSerializer(2)));
    console.log("Multy " + Deserializer(MultyAnswerSerializer([1, 2, 3, 4])));
    console.log("Open " + Deserializer(OpenAnswerSerializer("Some text")));
}
