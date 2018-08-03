var adminConnection = new signalR.HubConnectionBuilder()
    .withUrl("/sessionHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

adminConnection.start().catch(function (err) {
    console.error(err.toString());
});

adminConnection.on("UpdateState", function (user) {
    $("#examenee-" + user.id + "[data-session='" + user.sessionId + "'] .examenee-state").text(
        "Current task: " + user.taskState.currentNumber + "<br/>" +
        "Progress: " + user.taskState.completedCount + " / " + user.taskState.totalCount);

    console.log("UpdateState", user);
});

adminConnection.on("Complete", function (user, time) {
    var exameneeElement = $("#examenee-" + user.id + "[data-session='" + user.sessionId + "']");
    if (exameneeElement.length == 0)
        return;

    var timeSpan = exameneeElement.find(".examenee-time");
    var startDate = timeSpan.data("startTime");
    var duration = moment(new Date(time)).diff(startDate);
    timeSpan.text(moment(startDate).format("HH:mm")
        + " / "
        + moment.utc(duration).format("HH:mm"));
    exameneeElement.addClass("completed");
    exameneeElement.find(".examenee-state").text("Completed " + user.taskState.completedCount + " / " + user.taskState.totalCount);

    console.log("Complete", user, time);
});

adminConnection.on("Users", function (users) {
    for (var userKey in users) {
        var user = users[userKey];
        var exameneeElement = $("#examenee-" + user.id);
        var exameneeTimeElement = exameneeElement.find(".examenee-time")
        var exameneeStateElement = exameneeElement.find(".examenee-state")
        var date = new Date(user.startTime);
        var dateString = moment(date).format("HH:mm");

        exameneeTimeElement.text(dateString);
        exameneeTimeElement.data("startTime", date);
        exameneeElement.removeClass("disconnected");
        if (user.state) {
            exameneeStateElement.find(".examenee-state").text(
                "Current task: " + state.currentNumber +
                "Progress: " + state.completedCount + " / " + state.totalCount);
        }
        else {
            exameneeStateElement.find(".examenee-state").text("Started");
        }
    }
    console.log("Users", users);
});

adminConnection.on("UserConnected", function (user) {
    var exameneeElement = $("#examenee-" + user.id);
    var exameneeTimeElement = exameneeElement.find(".examenee-time")
    var exameneeStateElement = exameneeElement.find(".examenee-state")

    exameneeElement.removeClass("disconnected");

    if (user.startTime) {
        var date = new Date(user.startTime);
        var dateString = moment(date).format("HH:mm");

        exameneeTimeElement.text(dateString + " / -");
        exameneeTimeElement.data("startTime", date);
    }
    else {
        exameneeTimeElement.text("- / -");
    }
    if (user.state) {
        exameneeStateElement.text(
            "Current task: " + state.currentNumber +
            "Progress: " + state.completedCount + " / " + state.totalCount);
    }
    else {
        exameneeStateElement.text("Connected");
    }
    console.log("UserConnected", user);
});

adminConnection.on("Start", function (user) {
    var exameneeElement = $("#examenee-" + user.id);
    var exameneeTimeElement = exameneeElement.find(".examenee-time")
    var exameneeStateElement = exameneeElement.find(".examenee-state")

    exameneeElement.removeClass("disconnected");

    if (user.startTime) {
        var date = new Date(user.startTime);
        var dateString = moment(date).format("HH:mm");

        exameneeTimeElement.text(dateString + " / -");
        exameneeTimeElement.data("startTime", date);
    }
    else {
        exameneeTimeElement.text("- / -");
    }
    if (user.state) {
        exameneeStateElement.text(
            "Current task: " + state.currentNumber +
            "Progress: " + state.completedCount + " / " + state.totalCount);
    }
    else {
        exameneeStateElement.text("Connected");
    }
    console.log("UserConnected", user);
});

adminConnection.on("UserDisconnected", function (user) {
    $("#examenee-" + user.id).addClass("disconnected");
    if (user.state) {
        exameneeElement.find(".examenee-state").text("Disconnected");
    }
    console.log("UserDisconnected", user);
});

function banUser(sessionId, userId) {
    adminConnection.invoke("Ban", sessionId, userId)
        .catch(function (err) {
            console.error(err.toString());
        });
}