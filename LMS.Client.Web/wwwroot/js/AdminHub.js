const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:49244/testHub')
    .configureLogging(signalR.LogLevel.Information)
    .build();

//var connection = $.conection("http://localhost:49244/testHub");
//conection.start("{ withCredentials: false }");
connection.on("Report", (report) => {
    document.getElementById("report").innerHTML = UnserReport(report);
});

connection.on("Check", (report) => {
    alert(report);
});

connection.on("Report", (report) => {
    document.getElementById("report").innerHTML = UnserReport(report);
});
connection.start("{ withCredentials: true}").catch(err => console.error(err.toString()));

function send() {
    connection.invoke("SendComandToGroups", document.getElementById("comand").value, document.getElementById("message").value)
        .catch(err => console.error(err.toString()));
}

function send2() {
    connection.invoke("AdminCheck", document.getElementById("comand").value)
        .catch(err => console.error(err.toString()));
}