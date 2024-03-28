// http://localhost:5090/IpAddress/getIpDbIpAddressResult?ipAddress=192.33.22.23
// http://localhost:5090/IpAddress/getVirusTotalIpAddressResult?ipAddress=192.1.1.1


async function getIpAddressResult() {
    startIpSpinnerAnimation();
    const ipAddress = document.getElementById("ipInput").value;
    const virusTotalUri = `http://localhost:5090/IpAddress/getVirusTotalIpAddressResult?ipAddress=${ipAddress}`;
    const ipDbUri = `http://localhost:5090/IpAddress/getIpDbIpAddressResult?ipAddress=${ipAddress}`;
    console.log(ipAddress);
    console.log(virusTotalUri);

    const virusTotalResponse = await fetch(virusTotalUri, {
        //mode:'no-cors',
    });
    
    const ipDbResponse = await fetch(ipDbUri, {

    });
    const ipDbData = await ipDbResponse.json();
    console.log(ipDbData);
    if (virusTotalResponse.ok) {
        const data = await virusTotalResponse.json();

        console.log(data);
        stopIpSpinnerAnimation();
        showIpAddressResult(data);
    }
    else {
        stopIpSpinnerAnimation();
        document.getElementById("ipResult").innerHTML = "probably it is not a valid ip"
        console.log(virusTotalResponse.status);
    }
}

function showIpAddressResult(data) {
    
    const lastAnalysisResults = data.data.attributes.last_analysis_results;
    
    const lastAnalysisStats = data.data.attributes.last_analysis_stats;
    


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

