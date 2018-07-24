function changeMode(mode) {
    document.querySelector('input[name=mode]').value = mode;
}
function changeGot(got) {
    document.querySelector('input[name=got]').value = got;
}
function showValues() {
    var fields = $(":input").serializeArray();
    console.log($(":input").serializeArray());
    $("#result").empty();
    jQuery.each(fields, function (i, field) {
        $("#result").append(field.value + " ");
    });
}