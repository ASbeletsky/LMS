const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:49244/testHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("Task", (comand, message) => {
    switch (comand) {
        case "setTimer":
            var time = TimeToArray(message);
            console.log(time);
            SetTime(time[0], time[1], time[2]); break;
        case "Banne":
            sessionStorage.setItem("Baned", true);
            document.location.href = "/Home/Baned"; break;
        case "stopTimer":
            TimerM.pause.call(TimerM); break;
        case "contTimer":
            TimerM.continue.call(TimerM); break;
        case "report":
            SendReport();
            break;
    }
});

connection.start().catch(err => console.error(err.toString()));

if (sessionStorage.getItem("Baned") == "true") {
    document.location.href = "/Home/Baned";
}

function SendReport() {
    var report = Report(TimerM.StartTime, TimeToString(TimerM.EndTime.diff(moment())));
    connection.invoke("SendReportToGroups", report)
        .catch(err => console.error(err.toString()));
}

function SendAnswer(number,answer) {
    connection.invoke("SendAnswer", answer)
        .catch(err => console.error(err.toString()));
}
