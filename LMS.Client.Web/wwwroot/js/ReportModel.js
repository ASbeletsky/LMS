function Report(startT,leftT) {
    var rep = {
        StartTime: startT,
        TimeLeft: leftT
    }
    return JSON.stringify(rep);
}
function UnserReport(report) {
    var repObj = JSON.parse(report);
    var str = "Start time " + repObj.StartTime + ", time left " + repObj.TimeLeft;
    return str;
}