var userConnection = new signalR.HubConnectionBuilder()
    .withUrl("/sessionHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

userConnection.On("Ban", function (sessionId) {
    sessionStorage.setItem("Baned", true);
    document.location.href = "/Home/Baned"; break;
});

userConnection.start().catch(function (err) {
    console.error(err.toString());
});

if (sessionStorage.getItem("Baned") === "true") {
    document.location.href = "/Home/Baned";
}

function sendUpdateState(currentNumber, completedCount, totalCount) {
    userConnection.invoke("UpdateState", new {
        CurrentNumber = currentNumber,
        CompletedCount = completedCount,
        TotalCount = totalCount
    }).catch(function (err) {
        console.error(err.toString());
    });
}

function sendCompleted(completedCount, totalCount) {
    userConnection.invoke("Completed", new {
        CompletedCount = completedCount,
        TotalCount = totalCount
    }).catch(function (err) {
        console.error(err.toString());
    });
}