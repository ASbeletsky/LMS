var testVariant = (function () {

    this.toggleTask = function (level, id) {
        var select = $("select#tasks_" + level);
        var selected = select.find("option[value=" + id + "]:selected");
        if (selected.length > 0) {
            selected.remove();
        }
        else {
            select.append("<option selected value=" + id + "></option>");
        }
    }
    this.selectTemplate = function (select) {
        var templateId = parseInt($(select).find(":selected").attr("value"));
        if (templateId > 0) {
            location.href = buildUrlWithParam(location.href, 'templateId', templateId);
        }
    }

    return this;
})();