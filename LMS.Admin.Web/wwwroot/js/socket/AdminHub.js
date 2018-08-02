const connection = new signalR.HubConnectionBuilder()
    .withUrl("/testHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
connection.on("Report", (report) => {
    document.getElementById("report").innerHTML = UnserReport(report);
});

connection.on("Check", (report) => {
    alert(report);
});
connection.start().catch(err => console.error(err.toString()));

function send() {
    connection.invoke("SendComandToGroups", document.getElementById("comand").value, document.getElementById("message").value)
        .catch(err => console.error(err.toString()));

}
function send2() {
    connection.invoke("AdminCheck", document.getElementById("comand").value)
        .catch(err => console.error(err.toString()));
}