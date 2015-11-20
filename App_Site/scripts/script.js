function ResizeContent(id, minHeight, maxHeight) {
    var newHeight;
    var target = $("#" + id);

    if (typeof (target) !== "undefined") {
        newHeight = target[0].scrollHeight;

        if (parseInt(newHeight) > maxHeight) {
            newHeight = maxHeight;
        }

        if (parseInt(newHeight) < minHeight) {
            newHeight = minHeight;
        }

        target.css("height", newHeight + "px");
    }
}

function AutoResize() {
    ResizeContent("divBody", $(window).height() - 160, 10000);
}