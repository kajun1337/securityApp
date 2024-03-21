
function startFileSpinnerAnimation() {
    console.log("is this the real life");
    let placeOfResult = document.getElementById("fileResult");
    placeOfResult.innerHTML = '<div id="fileSpinner" class="fileSpinner"></div>';
    let spinner = document.getElementById("fileSpinner");
    spinner.style.display = "block";
}

function stopFileSpinnerAnimation() {
    let spinner = document.getElementById("fileSpinner");
    spinner.style.display = "none";
    let placeOfResult = document.getElementById("fileResult");
    placeOfResult.innerHTML = "";
}

function startLinkSpinnerAnimation() {
    let placeOfResuls = document.getElementById("linkResult");
    placeOfResuls.innerHTML = '<div id="linkSpinner" class="linkSpinner"></div>';
    let spinner = document.getElementById("linkSpinner");
    spinner.style.display = "block";
}

function stopLinkSpinnerAnimation() {
    let spinner = document.getElementById("linkSpinner");
    spinner.style.display = "none";
    let placeOfResult = document.getElementById("linkResult");
    placeOfResult.innerHTML = "";
}
