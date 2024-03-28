const SendLinkuri = "http://localhost:5090/Link/SendLink?link=";
async function sendLink() {
    startLinkSpinnerAnimation();
    const link = document.getElementById("linkInput").value;
    const uri = `http://localhost:5090/Link/SendLink?link=${link}`;
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
        document.getElementById("linkResult").innerText = "something went wrong";
    }
}

async function getLinkResult(link) {
    console.log("grdi");
    const response = await fetch(`http://localhost:5090/Link/GetLinkResult?link=${encodeURIComponent(link)}`, {
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
        document.getElementById("linkResult").innerText = "Link not found";
    }
    else {
        stopLinkSpinnerAnimation();
        document.getElementById("linkResult").innerText = "something went wrong";
    }
}
async function showLinkResults(data) {
    console.log(data);
    const lastAnalysisResults = data.data.attributes.last_analysis_results;
    console.log(typeof (lastAnalysisResults));
    const lastAnalysisStats = data.data.attributes.last_analysis_stats;
    console.log(lastAnalysisStats);


    let placeOfResults = document.getElementById("linkResult");
    placeOfResults.innerHTML = "";
    placeOfResults.innerHTML += "<h5> Scanning Results </h5>"
    placeOfResults.innerHTML += "<ul>";

    for (var item in lastAnalysisStats) {
        placeOfResults.innerHTML += "<li>" + item + ": " + lastAnalysisStats[item] + "</li>";
    }
    placeOfResults.innerHTML += "</ul>";


    if (lastAnalysisStats.suspicious > 0 || lastAnalysisStats.malicious > 0) {
        console.log("danger");
    }
    else {
        console.log("ok");
    }
}