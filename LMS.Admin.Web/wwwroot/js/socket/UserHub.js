var userConnection = new signalR.HubConnectionBuilder()
    .withUrl("/sessionHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

userConnection.on("Ban", function (sessionId) {
    sessionStorage.setItem("Baned", true);
    document.location.href = "/Home/Baned"; 
});

userConnection.start().catch(function (err) {
    console.error(err.toString());
});

if (sessionStorage.getItem("Baned") === "true") {
    document.location.href = "/Home/Baned";
}

function sendStart() {
    userConnection.invoke("Start").catch(function (err) {
        console.error(err.toString());
    });
}

function sendUpdateState(currentNumber, completedCount, totalCount) {
    userConnection.invoke("UpdateState", {
        CurrentNumber: currentNumber,
        CompletedCount: completedCount,
        TotalCount: totalCount
    }).catch(function (err) {
        console.error(err.toString());
    });
}

function sendComplete(completedCount, totalCount) {
    userConnection.invoke("Complete", {
        CompletedCount: completedCount,
        TotalCount: totalCount
    }).catch(function (err) {
        console.error(err.toString());
    });
}