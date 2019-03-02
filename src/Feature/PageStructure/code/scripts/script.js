
function registerGoal(goalId) {

    var xhr = new XMLHttpRequest();

    xhr.open("POST", "/api/sitecore/PageSection/RegisterGoal");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.onload = function () {
        if (xhr.status === 200 && xhr.responseText !== "true") {
            alert("Something went wrong.  Result is " + xhr.responseText);
        }
        else if (xhr.status !== 200) {
            alert("Request failed.  Returned status of " + xhr.status);
        }
    };
    xhr.send(encodeURI("goalId=" + goalId));
}

function playVideo(element) {
    element.previousElementSibling.play();
    element.hidden = true;
}