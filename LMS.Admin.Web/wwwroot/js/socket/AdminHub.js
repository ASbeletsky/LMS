var adminConnection = new signalR.HubConnectionBuilder()
    .withUrl("/sessionHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

adminConnection.start().catch(function (err) {
    console.error(err.toString());
});

adminConnection.on("UpdateState", function (user) {
    updateUser(user);

    console.log("UpdateState", user);
});

adminConnection.on("Complete", function (user, time) {
    updateUser(user, "Completed");

    console.log("Complete", user, time);
});

adminConnection.on("Users", function (users) {
    for (var userKey in users) {
        var user = users[userKey];
        updateUser(user);
    }
    console.log("Users", users);
});

adminConnection.on("UserConnected", function (user) {
    updateUser(user, "Connected");
    console.log("UserConnected", user);
});

adminConnection.on("Start", function (user) {
    updateUser(user, "Started");

    console.log("Start", user);
});

adminConnection.on("UserDisconnected", function (user) {
    updateUser(user, "Disconnected");

    console.log("UserDisconnected", user);
});

function updateUser(user, state) {
    var exameneeElement = $("#examenee-" + user.id);
    var exameneeTimeElement = exameneeElement.find(".examenee-time");
    var exameneeDurationElement = exameneeElement.find(".examenee-duration");
    var exameneeStateElement = exameneeElement.find(".examenee-state");

    if (user.startTime) {
        var date = new Date(user.startTime);
        var dateString = moment(date).format("HH:mm");
        exameneeTimeElement.text(dateString);
    }
    else {
        exameneeTimeElement.text("-");
    }
    if (user.duration) {
        var time = moment(user.duration, "HH:mm:ss").format("HH:mm:ss");
        exameneeDurationElement.text(time);
    }
    else {
        exameneeDurationElement.text("-");
    }
    if (state === "Disconnected"
        || !user.duration) {
        exameneeStateElement.text(state);
    }
    else if (user.tasksState) {
        if (user.duration) {
            exameneeStateElement.html(
                "Completed " + user.tasksState.completedCount + " / " + user.tasksState.totalCount);
        }
        else {
            exameneeStateElement.html(
                "Current task: " + user.tasksState.currentNumber + "<br/>" +
                "Progress: " + user.tasksState.completedCount + " / " + user.tasksState.totalCount);
        }
    }
    else if (state) {
        exameneeStateElement.text(state);
    }
    else if (user.duration) {
        exameneeStateElement.text("Completed");
    }
    else if (user.startTime) {
        exameneeStateElement.text("Started");
    }
    else {
        exameneeStateElement.text("-");
    }
    exameneeElement.removeClass("alert-warning");
    exameneeElement.removeClass("alert-success");
    if (user.duration) {
        exameneeElement.addClass("alert-success");
    }
    else if (state == "Disconnected") {
        exameneeElement.addClass("alert-warning");
    }
}

function banUser(sessionId, userId) {
    adminConnection.invoke("Ban", sessionId, userId)
        .catch(function (err) {
            console.error(err.toString());
        });
}