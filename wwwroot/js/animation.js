
function startFileSpinnerAnimation() {
    console.log("is this the real life");
    let placeOfResult = document.getElementById("VT-file-result");
    placeOfResult.innerHTML = "";
    let spinner = document.getElementById("fileSpinner");
    spinner.style.display = "block";
}

function stopFileSpinnerAnimation() {
    let spinner = document.getElementById("fileSpinner");
    spinner.style.display = "none";

}

function startLinkSpinnerAnimation() {
    let placeOfResuls = document.getElementById("VT-link-result");
    placeOfResuls.innerHTML = "";
    let spinner = document.getElementById("linkSpinner");
    spinner.style.display = "block";
}

function stopLinkSpinnerAnimation() {
    let spinner = document.getElementById("linkSpinner");
    spinner.style.display = "none";
}

function startIpSpinnerAnimation() {
    let resultPlace = document.getElementById("VT-ip-result-container");
    resultPlace.innerHTML = "";
    let spinner = document.getElementById("ipSpinner");
    spinner.style.display = "block";
}
function stopIpSpinnerAnimation() {
    let spinner = document.getElementById("ipSpinner");
    spinner.style.display = "none";
}