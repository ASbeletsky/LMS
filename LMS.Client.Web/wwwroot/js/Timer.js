var TimerM;
var strch = sessionStorage.getItem('Started');
console.log(strch);
if (strch != undefined) {
    if (strch == "true") {
        console.log(1);
        var arrtime = TimeToArray(moment(sessionStorage.getItem("EndTime")).diff(moment()));
        TimerM = StartTimerM(sessionStorage.getItem("Element"), arrtime[0], arrtime[1], arrtime[2]);
    } else {
        console.log(2);
        var arrtime = TimeToArray(sessionStorage.getItem("duration"));
        TimerM = StartTimerM(sessionStorage.getItem("Element"), arrtime[0], arrtime[1], arrtime[2]);
    }
}
function SetTime(h, m, s) {
    TimerM.setTime(h, m, s);
}
function CreatTimer(el, h, m, s) {
    TimerM = StartTimerM(el, h, m, s);
}
function StartTimerM(element, hours, minutes, sec) {
    this.StartTime = moment();
    this.duration = moment.duration({ y: 0, M: 0, d: 0, h: hours, m: minutes, s: sec, ms: 0 });
    this.EndTime = moment().add(this.duration);
    sessionStorage.setItem("EndTime", EndTime);
    this.diference;
    this.ele = element;
    sessionStorage.setItem("Element", this.ele);
    document.getElementById(ele).innerHTML = TimeToString(duration);
    this.start = function () {
        sessionStorage.setItem("Started", true);
        return setInterval(function () {
            this.diference = this.EndTime.diff(moment());
            if (this.diference <= 0) {
                StopTimerM();
                return;
            }
            document.getElementById(ele).innerHTML = TimeToString(diference);
        }, 500);
    }
    this.interval;
    if (sessionStorage.getItem("Started") == undefined || sessionStorage.getItem("Started") == "true") {
        this.interval = this.start();
    } else {
        this.duration = sessionStorage.getItem('duration');
    }
    function StopTimerM() {
        clearInterval(interval);
    }
    this.pause = function () {
        clearInterval(interval);
        this.duration = this.EndTime.diff(moment());
        sessionStorage.setItem('duration', duration);
        sessionStorage.setItem("Started", false);
    }
    this.continue = function () {
        this.EndTime = moment().add(this.duration);
        sessionStorage.setItem('EndTime', EndTime);
        this.interval = this.start();
        sessionStorage.setItem("Started", true);
    }
    this.setTime = function (h, m, s) {
        this.duration = moment.duration({ y: 0, M: 0, d: 0, h: h, m: m, s: s, ms: 0 });
        this.EndTime = moment().add(duration);
        sessionStorage.setItem('EndTime', EndTime);
    }
    return this;
}
function TimeToString(time) {
    var h, m, s;
    var arr = TimeToArray(time);
    h = arr[0];
    m = arr[1];
    s = arr[2];
    if (h < 10) {
        h = "0" + h;
    } else {
        h = hours;
    }
    if (m < 10) {
        m = "0" + m;
    } else {
        m = m;
    } if (s < 10) {
        s = "0" + s;
    } else {
        s = s;
    }
    return h + ":" + m + ":" + s;
}
function TimeToArray(time) {
    var h, m, s;
    var seconds = time / 1000;
    s = Math.floor(seconds % 60);
    m = Math.floor(seconds / 60) % 60;
    h = Math.floor(seconds / 3600);
    return [h, m, s];
}