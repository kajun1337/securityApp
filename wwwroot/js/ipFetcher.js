// http://localhost:5090/IpAddress/getIpDbIpAddressResult?ipAddress=192.33.22.23
// http://localhost:5090/IpAddress/getVirusTotalIpAddressResult?ipAddress=192.1.1.1


async function getIpAddressResult() {
    startIpSpinnerAnimation();
    const ipAddress = document.getElementById("ipInput").value;
    const uri = `http://localhost:5090/IpAddress/getVirusTotalIpAddressResult?ipAddress=${ipAddress}`;
    console.log(ipAddress);
    console.log(uri);

    const response = await fetch(uri, {
        //mode:'no-cors',
    });

    console.log(response.status);
    const data = await response.json();

    console.log(data);
    stopIpSpinnerAnimation();
    showIpAddressResult(data);
}

function showIpAddressResult(data) {
    console.log(data);
    const lastAnalysisResults = data.data.attributes.last_analysis_results;
    console.log(typeof (lastAnalysisResults));
    const lastAnalysisStats = data.data.attributes.last_analysis_stats;
    console.log(lastAnalysisStats);


    let placeOfResults = document.getElementById("ipResult");
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
