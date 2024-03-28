

async function uploadFile() {
    startFileSpinnerAnimation();
    console.log("file sent");
    const file = document.getElementById("fileInput").files[0];
    const formData = new FormData();
    formData.append('file', file);
    const uri = `http://localhost:5090/File/UploadFile?file=${file}`;
    const response = await fetch(uri, {
        //mode:'no-cors',
        method: 'POST',
        body: formData

    });
    getFilesResult();

    

}
async function getFilesResult() {
    const fileToScan = document.getElementById("fileInput").files[0];
    console.log(fileToScan);
    filesToSha256(fileToScan).then(hash => {
        const hashed = hash;
        console.log(hashed); 
        fetch(`http://localhost:5090/File/GetFileResults?encodedFileSha256=${hashed}`, {
            //mode: 'no-cors'
        })

            .then(response => {
                console.log(response.json);
                return response.json();
            })
            .catch(error => {
                console.log(error);
                console.error(error);
            })
            .then(data => {
                
                showFileResults(data);
                console.log(data);
            })
            .catch(error => {
                console.error(error);
            });
    });  
}
function filesToSha256(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = function () {
            const buffer = reader.result;

            crypto.subtle.digest("SHA-256", buffer).then(hash => {
                const hexString = Array.from(new Uint8Array(hash)).map(b => b.toString(16).padStart(2, "0")).join("");
                resolve(hexString);
            }).catch(error => {
                reject(error);
            });
        };

        reader.readAsArrayBuffer(file);
    });
}



function showFileResults(data) {
    stopFileSpinnerAnimation();
    console.log(data);
    const lastAnalysisResults = data.data.attributes.last_analysis_results;
    console.log(typeof (lastAnalysisResults));
    const lastAnalysisStats = data.data.attributes.last_analysis_stats;
    console.log(lastAnalysisStats);


    let placeOfResults = document.getElementById("fileResult");
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
function delay(time) {
    return new Promise(resolve => setTimeout(resolve, time));
}

