var adminConnection = new signalR.HubConnectionBuilder()
    .withUrl("/sessionHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

adminConnection.start().catch(function (err) {
    console.error(err.toString());
});

adminConnection.on("UpdateState", function (state) {
    console.log("UpdateState", state);
});

adminConnection.on("Complete", function (state) {
    console.log("Complete", state);
});

adminConnection.on("Users", function (users) {
    console.log("Users", users);
});

adminConnection.on("UserConnected", function (user) {
    console.log("UserConnected", user);
});

adminConnection.on("UserDisconnected", function (user) {
    console.log("UserDisconnected", user);
});

function banUser(sessionId, userId) {
    adminConnection.invoke("Ban", sessionId, userId)
        .catch(function (err) {
            console.error(err.toString());
        });
}