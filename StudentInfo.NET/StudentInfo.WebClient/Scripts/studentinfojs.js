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

function updateDepartments() {
    $("#departmentSelector").empty();
    getDepartments($('#facultySelector').val());
}

function getDepartments(facultyId) {
    var url = '/Course/GetDepartments';
    $.ajax({
        type: 'GET',
        url: url,
        data: { facultyId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $('#departmentSelector').append('<option value = "' + data[i].value + '">' + data[i].text + '</option>');
            }
        }
    });
}

function enrollCourse(semesterCourseId) {
    $.ajax({
        type: 'POST',
        url: '/Course/Enroll',
        data: { semesterCourseId },
        success: function (data) {
            var alertDivId = "alertDiv" + semesterCourseId;
            var alertDiv = document.createElement("div");
            alertDiv.setAttribute("id", alertDivId);
            var alertContent = document.createTextNode(data.message);

            if (data.success == true) {
                alertDiv.className = "alert alert-success";
            }
            else {
                alertDiv.className = "alert alert-danger";
            }

            alertDiv.appendChild(alertContent);
            document.getElementById("alertsContainer").appendChild(alertDiv);

            $("#" + alertDivId).delay(5000).fadeOut();
        }
    })
}