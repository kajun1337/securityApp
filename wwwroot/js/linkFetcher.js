

const vtSendLinkUri = "http://localhost:5090/Link/Vt-SendLink?link=";
const vtGetLinkUri = "http://localhost:5090/Link/Vt-GetLinkResult?link=";
const haSendLinkUri = "http://localhost:5090/Link/Ha-SendLink?link=";
const haGetLinkUri = "http://localhost:5090/Link/Ha-GetLinkResult?link=";
let haResponse;
const linkInput = document.getElementById("linkInput");
linkInput.addEventListener("keyup", function (event) {
    if (event.key === "Enter") {
        sendLink();
    }
})
async function sendLink() {
    const link = document.getElementById("linkInput").value;
    startLinkSpinnerAnimation();
    let vtResponse = await sendVtLink(link);
    try {
        haResponse = await sendHaLink(link);
        console.log(haResponse);
;
    } catch (ex){
        console.log(ex);
    }
    //let haResponse = await sendHaLink(link);
    

    console.log(vtResponse.status);
    //console.log(haResponse.status);
    if (vtResponse.ok) {

        getLinkResult(link);

    } else {
        stopLinkSpinnerAnimation();
        console.error("errrokee");
        document.getElementById("VT-link-result").innerText = "something went wrong";
    }
}

async function getLinkResult(link) {
    console.log("grdi");
    const response = await getVtLinkResult(link);
    console.log(response.status);
    if (response.ok) {

        const data = await response.json();
        console.log(data);
        stopLinkSpinnerAnimation();
        showLinkResults(data,haResponse);

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
async function showLinkResults(vtData, haData) {

    console.log(haData);
    const vtLastAnalysisStats = vtData.data.attributes.last_analysis_stats;
    const haAnalysisStats = haData.scanners_v2.bfore_ai;
    let haPlaceOfResults = document.getElementById("HA-link-result");
    let vtPlaceOfResults = document.getElementById("VT-link-result");
    vtPlaceOfResults.innerHTML = "";
    vtPlaceOfResults.innerHTML += "<h5> Scanning Results </h5>"

 

    for (var item in vtLastAnalysisStats) {
        vtPlaceOfResults.innerHTML += "<li>" + item + ": " + vtLastAnalysisStats[item] + "</li>";
    }

    haPlaceOfResults.innerHTML = "";
    haPlaceOfResults.innerHTML += "<h5> Scanning Results </h5>"

    for(var item in haAnalysisStats) {
        haPlaceOfResults.innerHTML += "<li>" + item + ": " + haAnalysisStats[item].result + "</li>";
    }

    if (vtLastAnalysisStats.suspicious > 0 || vtLastAnalysisStats.malicious > 0) {
        console.log("danger");
    }
    else {
        console.log("ok");
    }
}

async function sendHaLink(link) {
    
    const uri = `${haSendLinkUri}${link}`;
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
    let data = await response.json();
    console.log(data);
    return data;
    
}

async function sendVtLink(link) {
    
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

    return response;
}

async function getVtLinkResult(link) {
    const response = await fetch(`${vtGetLinkUri}${encodeURIComponent(link)}`, {
        //mode: 'no-cors'
    });
    return response;
}

async function getHaLinkResult(link) {
    const response = await fetch(`${haGetLinkUri}${encodeURIComponent(link)}`, {
        //mode: 'no-cors'
    });
    const data = response.json();

    console.log(data);
    return response;
}