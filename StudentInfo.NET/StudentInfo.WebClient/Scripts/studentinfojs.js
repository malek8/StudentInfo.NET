function loadCourseDetails(courseId, allowEdit) {
    $.get('/Course/Details?id=' + courseId + '&allowEdit=' + allowEdit, function (data) {
        $('#courseDetailsModalBody').html(data);
        $('#courseDetailsModal').modal('show');
    })
}

function showValidationIssues(isShown) {
    if (isShown) {
        $("#errorContainer").show();
    }
    else {
        $("#errorContainer").hide();
    }
}

function fillValidationErrors(errors) {
    $("#errors").html("");

    var errorsList = document.createElement('ul');

    for (var i = 0; i < errors.length; i++) {
        var item = document.createElement('li');
        item.appendChild(document.createTextNode(errors[i]));
        errorsList.appendChild(item);
    }

    $("#errors").html(errorsList);
}

function processResult(data) {
    if (data.success) {
        showValidationIssues(false);
        $('#courseDetailsModal').modal('hide');
    }
    else {
        fillValidationErrors(data);
        showValidationIssues(true);
    }
}