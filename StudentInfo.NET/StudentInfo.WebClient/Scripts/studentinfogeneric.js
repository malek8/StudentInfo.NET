function showElement(isShown, el) {
    if (isShown) {
        $(el).show();
    }
    else {
        $(el).hide();
    }
}

function fillValidationErrorsGeneric(errors, el) {
    $(el).html("");

    var errorsList = document.createElement("ul");

    for (var i = 0; i < errors.length; i++) {
        var item = document.createElement("li");
        item.appendChild(document.createTextNode(errors[i]));
        errorsList.appendChild(item);
    }

    $(el).html(errorsList);
}