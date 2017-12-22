function publishClassrooms(actionName) {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState == XMLHttpRequest.DONE) {
            var data = JSON.parse(request.responseText);
            alert(data.message);
        }
    };
    request.open("GET", "/Admin/" + actionName, true);
    request.send(null);
}
function publishData(dataType) {
    var message;
    switch (dataType) {
        case "classrooms":
            publishClassrooms("PublishClassrooms");
            break;
        case "courses":
            publishClassrooms("PublishCourses");
            break;
    }
}
//# sourceMappingURL=AdminFunctions.js.map