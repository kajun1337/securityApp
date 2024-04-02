
const vtSendLinkUri = "http://localhost:5090/Link/Vt-SendLink?link=";
const vtGetLinkUri = "http://localhost:5090/Link/Vt-GetLinkResult?link=";
const haSendLinkUri = "http://localhost:5090/Link/Ha-SendLink?link=";

const linkInput = document.getElementById("linkInput");
linkInput.addEventListener("keyup", function (event) {
    if (event.key === "Enter") {
        sendLink();
    }
})
async function sendLink() {
    startLinkSpinnerAnimation();
    const link = document.getElementById("linkInput").value;
    const uri = `${vtSendLinkUri}${link}`;
    console.log(link);
    console.log(uri);

    const response = await fetch(uri, {
        //mode:'no-cors',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            link: link
        })
    });

    console.log(response.status);
    if (response.ok) {

        getLinkResult(link);

    } else {
        stopLinkSpinnerAnimation();
        console.error("errrokee");
        document.getElementById("VT-link-result").innerText = "something went wrong";
    }
}

async function getLinkResult(link) {
    console.log("grdi");
    const response = await fetch(`${vtGetLinkUri}${encodeURIComponent(link)}`, {
        //mode: 'no-cors'
    });
    console.log(response.status);
    if (response.ok) {

        const data = await response.json();
        console.log(data);
        stopLinkSpinnerAnimation();
        showLinkResults(data);

    }
    else if (response.status === 404) {
        stopLinkSpinnerAnimation();
        document.getElementById("VT-link-result").innerText = "Link not found";
    }
    else {
        stopLinkSpinnerAnimation();
        document.getElementById("VT-link-result").innerText = "something went wrong";
    }
}
async function showLinkResults(data) {
    console.log(data);
    const lastAnalysisResults = data.data.attributes.last_analysis_results;
    console.log(typeof (lastAnalysisResults));
    const lastAnalysisStats = data.data.attributes.last_analysis_stats;
    console.log(lastAnalysisStats);


    let placeOfResults = document.getElementById("VT-link-result");
    placeOfResults.innerHTML = "";
    placeOfResults.innerHTML += "<h5> Scanning Results </h5>"
 

    for (var item in lastAnalysisStats) {
        placeOfResults.innerHTML += "<li>" + item + ": " + lastAnalysisStats[item] + "</li>";
    }



    if (lastAnalysisStats.suspicious > 0 || lastAnalysisStats.malicious > 0) {
        console.log("danger");
    }
    else {
        console.log("ok");
    }
}