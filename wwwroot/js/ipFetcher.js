// http://localhost:5090/IpAddress/getIpDbIpAddressResult?ipAddress=192.33.22.23
// http://localhost:5090/IpAddress/getVirusTotalIpAddressResult?ipAddress=192.1.1.1

const vtGetIpUri = "http://localhost:5090/IpAddress/Vt-GetIpResult?ipAddress=";
const ipdbGetIpUri = "http://localhost:5090/IpAddress/IpDb-GetIpResult?ipAddress="

const ipInput = document.getElementById("ipInput");
ipInput.addEventListener("keyup", function (event) {
    if (event.key === "Enter") {
       getIpAddressResult();
    }
})
async function getIpAddressResult() {
    startIpSpinnerAnimation();
    const ipAddress = document.getElementById("ipInput").value;
    const virusTotalUri = `${vtGetIpUri}${ipAddress}`;
    const ipDbUri = `${ipdbGetIpUri}${ipAddress}`;
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
        document.getElementById("VT-ip-result-container").innerHTML = "probably it is not a valid ip"
        console.log(virusTotalResponse.status);
    }
}

function showIpAddressResult(data) {
    
    const lastAnalysisResults = data.data.attributes.last_analysis_results;
    
    const lastAnalysisStats = data.data.attributes.last_analysis_stats;
    


    let placeOfResults = document.getElementById("VT-ip-result-container");
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

