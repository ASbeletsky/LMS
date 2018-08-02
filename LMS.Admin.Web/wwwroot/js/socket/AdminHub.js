var connection = new signalR.HubConnectionBuilder()
    .withUrl("/testHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
connection.on("Report", function (report) {
    document.getElementById("report").innerHTML = UnserReport(report);
});

connection.start().catch(function (err) {
    console.error(err.toString());
});

function send() {
    connection.invoke("SendComandToGroups", document.getElementById("comand").value, document.getElementById("message").value)
        .catch(function (err) {
            console.error(err.toString());
        });
}