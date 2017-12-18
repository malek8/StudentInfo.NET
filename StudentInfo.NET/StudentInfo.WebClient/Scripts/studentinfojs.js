function loadCourseDetails(courseId, allowEdit) {
    $.get("/Course/Details?id=" + courseId + "&allowEdit=" + allowEdit, function (data) {
        $("#courseDetailsModalBody").html(data);
        $("#courseDetailsModal").modal("show");
    });
}

function loadSemesterCourseDetails(courseId) {
    $.get("/Course/SemesterCourseDetails?id=" + courseId, function (data) {
        $("#courseDetailsModalBody").html(data);
        $("#courseDetailsModal").modal("show");
    });
}

function loadStudentCourseDetails(studentCourseId) {
    $.get("/Course/StudentCourse?studentCourseId=" + studentCourseId, function (data) {
        $("#studentCourseDetailsModalBody").html(data);
        $("#studentCourseDetailsModal").modal("show");
    })
}

function loadTeachersModal(semesterCourseId) {
    $.get("/Course/GetInstructor?semesterCourseId=" + semesterCourseId, function (data) {
        $("#courseInstructorModalBody").html(data);
        $("#courseInstructorModal").modal("show");
    });
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

    var errorsList = document.createElement("ul");

    for (var i = 0; i < errors.length; i++) {
        var item = document.createElement("li");
        item.appendChild(document.createTextNode(errors[i]));
        errorsList.appendChild(item);
    }

    $("#errors").html(errorsList);
}

function fillPasswordChangeValidationErrors(errors) {
    $("#passwordChangeErrors").html("");

    var errorsList = document.createElement("ul");

    for (var i = 0; i < errors.length; i++) {
        var item = document.createElement("li");
        item.appendChild(document.createTextNode(errors[i]));
        errorsList.appendChild(item);
    }

    $("#passwordChangeErrors").html(errorsList);
}

function processResult(data) {
    if (data.success) {
        showValidationIssues(false);
        $("#courseDetailsModal").modal("hide");
    }
    else {
        fillValidationErrors(data);
        showValidationIssues(true);
    }
}

function processEmailChangeResult(data) {
    if (data.success) {
        showElement(false, "#emailChangeErrorContainer");
        $("#emailChangeModal").on("hide.bs.modal", function (e) {
            window.location = "/Account/Login/";
        });

        setTimeout(function () {
            $("#emailChangeModal").modal("hide");
        }, 500);
    }
    else {
        fillValidationErrorsGeneric(data, "#emailChangeErrors");
        showElement(true, "#emailChangeErrorContainer");
    }
}

function processPasswordChangeResult(data) {
    if (data.success) {
        showElement(false, "#passwordChangeErrorContainer");
        $("#passwordChangeModal").on("hide.bs.modal", function (e) {
            window.location = "/Account/Login/";
        });

        setTimeout(function () {
            $("#passwordChangeModal").modal("hide");
        }, 500);
    }
    else {
        fillValidationErrorsGeneric(data, "#passwordChangeErrors");
        showElement(true, "#passwordChangeErrorContainer");
    }
}

function processPayBalanceResult(data) {
    if (data.success === true) {
        showElement(false, "#payBalanceErrorContainer");
        $("#payBalanceModal").on("hide.bs.modal", function (e) {
            setTimeout(function () {
                window.location = "/ManageAccount/";
            }, 500);
        })
        $("#payBalanceModal").modal("hide");
        displayAlert(data.messages[0], true);
    }
    else {
        fillValidationErrorsGeneric(data.messages, "#payBalanceErrors");
        showElement(true, "#payBalanceErrorContainer");
    }
}

function processEditUserResults(data) {
    if (data.success === true) {
        displayAlert(false, "Updated successfully");
        $("#userDetailsModal").modal("hide");
    }
    else {
        displayAlert(data.message, false);
    }
}

function processNewCourseResults(data) {
    if (data.success === true) {
        document.getElementById("createCourseForm").reset();
        displayAlert("Course created successfully", true);
    }
    else {
        displayAlert("Failed to create new course, check inputs", false);
    }
}

function processEditCourseResults(data) {
    if (data.success === true) {
        displayAlert("Course updated successfully", true);
    }
    else {
        displayAlert("Failed to update course", false);
    }
}

function updateDepartments() {
    $("#departmentSelector").empty();
    getDepartments($("#facultySelector").val());
}

function getDepartments(facultyId) {
    var url = "/Course/GetDepartments";
    $.ajax({
        type: "GET",
        url: url,
        data: { facultyId },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#departmentSelector").append('<option value = "' + data[i].value + '">' + data[i].text + '</option>');
            }
        }
    });
}

function enrollCourse(semesterCourseId) {
    $.ajax({
        type: "POST",
        url: "/Course/Enroll",
        data: { semesterCourseId },
        success: function (data) {
            displayAlert(data.message, data.success);
        }
    });
}

function dropCourse(studentCourseId) {
    $.ajax({
        type: "POST",
        url: "/Course/Drop",
        data: { studentCourseId },
        success: function (data) {
            if (data.success === true) {
                $("#tr_item" + studentCourseId).remove();
            }
            displayAlert(data.message, data.success);
        }
    });
}

function displayAlert(messageContent, state) {
    var className = "";
    var alertDivId = Date.now();
    if (state === true) {
        className = "alert alert-success";
    }
    else {
        className = "alert alert-danger";
    }

    var alertDiv = document.createElement("div");
    alertDiv.setAttribute("id", alertDivId);
    var alertContent = document.createTextNode(messageContent);

    alertDiv.className = className;
    alertDiv.appendChild(alertContent);
    document.getElementById("alertsContainer").appendChild(alertDiv);

    $("#" + alertDivId).delay(5000).fadeOut();
}

function assignInstructor(semesterCourseId) {
    var userId = $("#instructorsDropdown").val();
    $.ajax({
        type: "POST",
        url: "/Course/AssignInstructor",
        data: { userId, semesterCourseId },
        success: function (data) {
            displayAlert(data.message, data.success);
        }
    });
}

function loadGradesModal(semesterCourseId) {
    $.get("/Course/GetCourseGrades?semesterCourseId=" + semesterCourseId, function (data) {
        $("#courseGradesModalBody").html(data);
        $("#courseGradesModal").modal("show");
    })
}

function captureGrade(_this) {
    var grade = $(_this).val();
    var studentId = _this.id;

    addStudentGrade(studentId, grade);
}

function addStudentGrade(studentId, grade) {
    var gradeItem = { id: studentId, grade: grade };
    var existingItemIndex = searchGrades(studentId);

    if (existingItemIndex >= 0) {
        studentGradesArray[existingItemIndex].grade = grade;
    }
    else {
        studentGradesArray.push(gradeItem);
    }
}

function searchGrades(studentId) {
    for (var i= 0; i < studentGradesArray.length;i++){
        if (studentGradesArray[i].id === studentId) {
            return i;
        }
    }
}

function loadChangeEmailModal() {
    $("#emailChangeModal").modal("show");
}

function loadPasswordChangeModal() {
    $("#passwordChangeModal").modal("show");
}

function loadPayBalanceModal(paymentId) {
    $.get("/ManageAccount/BalanceToPay?paymentId=" + paymentId, function (data) {
        $("#payBalanceModalBody").html(data);
        $("#payBalanceModal").modal("show");
    });
}

function loadTransactionsModal(paymentId) {
    $.get("/ManageAccount/GetTransactions?paymentId=" + paymentId, function (data) {
        $("#transactionsModalBody").html(data);
        $("#transactionsModal").modal("show");
    });
}

function loadUserDetailsModal(userId) {
    $.get("/User/Details/" + userId, function (data) {
        $("#userDetailsModalBody").html(data);
        $("#userDetailsModal").modal("show");
    })
}

function loadAssignSemesterModal(courseId) {
    $.get("/Course/AssignSemesterCourse?courseId=" + courseId, function (data){
        $("#assignSemesterModalBody").html(data);
        $("#assignSemesterModal").modal("show");
    })
}

function assignSemesterCourse_(courseId) {
    var date = $("#assignCourseDateInput").val();
    var term = $("#assignCourseTermInput").val();
    var classroomId = $("#assignCourseClassroomInput").val();
    var teacherId = $("#assignCourseInstructorInput").val();
    var cost = $("#assignCourseCostInput").val();
    $.ajax({
        type: "POST",
        url: "/Course/AssignSemesterCourse",
        data: { courseId, classroomId, teacherId, cost, term, date },
        success: function (data) {
            if (data.success === true) {
                displayAlert("Assigned successfully", true);
            }
            else {
                displayAlert("Failed to assign", false);
            }
        }
    });
}

function loadEditCourse(courseId) {
    $.get("/Course/CourseDetails?id=" + courseId, function (data) {
        $("#courseEditModalBody").html(data);
        $("#courseEditModal").modal("show");
    });
}

function assignStudentsGrades() {
    for (var i = 0; i < studentGradesArray.length; i++) {
        var studentId = studentGradesArray[i].id;
        var grade = studentGradesArray[i].grade;
        $.ajax({
            type: "POST",
            url: "/Course/AddStudentGrade",
            data: { studentId, grade },
            success: function (data) {
                if (data.success === true) {
                    displayAlert("Grade assigned successfully", true);
                }
                else {
                    displayAlert("Failed to assign grade", false);
                }
            }
        });
    }
    $("#courseGradesModal").modal("hide");
}

function checkClassroom(classroomId, startTime, endTime) {
    $.get("/Course/CheckClassroom?classroomId=" + classroomId + "&startTime=" + startTime + "&endTime=" + endTime, function (data) {
        if (data.success === true) {
            $("#classAvailableSpan").show();
            $("#classNotAvailableSpan").hide();

            $("#classIsAvailableField").val(true);
            $("#selectedClassIdField").val(classroomId);
        }
        else {
            $("#classAvailableSpan").hide();
            $("#classNotAvailableSpan").show();
        }
    })
}

function checkClassroomAvailability() {
    var startTime = $("#startTimeInput").val();
    var endTime = $("#endTimeInput").val();
    var classroomId = $("#assignCourseClassroomInput").val();
    var dates = $("#multiDatesInput").multiDatesPicker('getDates');

    var dateObjects = [];

    for (var i = 0; i <= dates.length; i++) {
        var d = new Date(dates[i]);

        var day = d.getDate();
        var month = d.getMonth() + 1;
        var year = d.getFullYear();

        if (isNaN(year) === false) {
            var d = { day: day, month: month, year: year };
            dateObjects.push(d);
        }
    }

    var dateObjectsJSON = JSON.stringify(dateObjects);
    $("#scheduleId").val(null);

    $.ajax({
        type: "GET",
        url: "/Course/CheckClassroom",
        data: {
            classroomId: classroomId,
            startTime: startTime,
            endTime: endTime,
            dates: dateObjectsJSON
        },
        success: function (data) {
            if (data.success === true) {
                $("#selectedClassroomId").val(classroomId);
                $("#classroomIsAvailable").val(true);
                $("#saveScheduleButton").removeAttr("disabled");

                var alertContainer = document.createElement("div");
                alertContainer.setAttribute("class", "alert alert-success");

                var alertText = document.createTextNode(data.message);

                alertContainer.appendChild(alertText);

                $("#createScheduleButtonsPanel").empty();
                $("#createScheduleButtonsPanel").append(alertContainer);
                $("#createScheduleButtonsPanel").show();

                $("#createScheduleButtonsPanel").fadeOut(5000);
            }
            else {
                $("#selectedClassroomId").val(null);
                $("#classroomIsAvailable").val(false);
                $("#saveScheduleButton").prop("disabled", true);

                var alertContainer = document.createElement("div");
                alertContainer.setAttribute("class", "alert alert-danger");

                var alertText = document.createTextNode(data.message);

                alertContainer.appendChild(alertText);

                $("#createScheduleButtonsPanel").empty();
                $("#createScheduleButtonsPanel").append(alertContainer);
                $("#createScheduleButtonsPanel").show();

                $("#createScheduleButtonsPanel").fadeOut(5000);
            }
        }
    })
}

function createSchedule() {
    var isAvailable = $("#classroomIsAvailable").val();

    if (isAvailable == "true") {
        var startTime = $("#startTimeInput").val();
        var endTime = $("#endTimeInput").val();
        var title = $("#scheduleTitle").val();
        var classroomId = $("#assignCourseClassroomInput").val();
        var dates = $("#multiDatesInput").multiDatesPicker('getDates');

        var dateObjects = [];

        for (var i = 0; i <= dates.length; i++) {
            var d = new Date(dates[i]);

            var day = d.getDate();
            var month = d.getMonth() + 1;
            var year = d.getFullYear();

            if (isNaN(year) === false) {
                var d = { day: day, month: month, year: year };
                dateObjects.push(d);
            }
        }

        var dateObjectsJSON = JSON.stringify(dateObjects);

        $.ajax({
            type: "POST",
            url: "/Course/CreateSchedule",
            data: {
                classroomId: classroomId,
                title: title,
                startTime: startTime,
                endTime: endTime,
                dates: dateObjectsJSON
            },
            success: function (data) {
                if (data.success === true) {
                    $("#scheduleId").val(data.scheduleId);
                    $("#continueScheduleButton").removeAttr("disabled");
                    $("#continueScheduleButton").show();

                    var alertContainer = document.createElement("div");
                    alertContainer.setAttribute("class", "alert alert-success");

                    var alertText = document.createTextNode(data.message);

                    alertContainer.appendChild(alertText);

                    $("#createScheduleButtonsPanel").empty();
                    $("#createScheduleButtonsPanel").append(alertContainer);
                    $("#createScheduleButtonsPanel").show();
                    $("#createScheduleButtonsPanel").fadeOut(5000);

                    var success
                }
                else {
                    $("#scheduleId").val(null);
                    $("#continueScheduleButton").prop("disabled", true);
                    $("#continueScheduleButton").hide();
                }
            }
        })
    }
}

function assignSemesterCourse() {
    var scheduleId = $("#scheduleIdField").val();
    var instructorId = $("#instructorInput").val();
    var term = $("#assignCourseTermInput").val();
    var courseId = $("#CourseIdField").val();
    var isOpen = false;
    if ($("#isOpenCheckbox").is(":checked")) {
        isOpen = true;
    }

    if (scheduleId !== undefined && scheduleId != null &&
        instructorId !== undefined && instructorId != null &&
        term !== undefined && term != null &&
        courseId != undefined && courseId != null) {

        $.ajax({
            type: "POST",
            url: "/Course/AssignSemesterCourse",
            data: {
                courseId: courseId,
                scheduleId: scheduleId,
                teacherId: instructorId,
                isOpen: isOpen,
                term: term
            },
            success: function (data) {
                if (data.success == true) {
                    var alertContainer = document.createElement("div");
                    alertContainer.setAttribute("class", "alert alert-success");

                    var alertText = document.createTextNode("Changes were saved successfully");

                    alertContainer.appendChild(alertText);

                    $("#assignSchedulePanelHeading").empty();
                    $("#assignSchedulePanelHeading").append(alertContainer);
                    $("#assignSchedulePanelHeading").show();
                    $("#assignSchedulePanelHeading").fadeOut(5000);
                }
                else {
                    var alertContainer = document.createElement("div");
                    alertContainer.setAttribute("class", "alert alert-danger");

                    var alertText = document.createTextNode("Failed to save changes");

                    alertContainer.appendChild(alertText);

                    $("#assignSchedulePanelHeading").empty();
                    $("#assignSchedulePanelHeading").append(alertContainer);
                    $("#assignSchedulePanelHeading").show();
                    $("#assignSchedulePanelHeading").fadeOut(5000);
                }
            }
        })
    }
    else {
        alert("Invalid input");
    }
}