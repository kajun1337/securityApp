const { createHash } = require('crypto');
const { read } = require('fs');
async function sendLink() {

    const link = document.getElementById("linkInput").value;
    const uri = `http://localhost:5090/Link/SendLink?link=${link}`;
    console.log(link);
    const response = await fetch(uri, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            link: link
        })
    });
    console.log(response);
    if (response.ok) {
        getResult(link);
    } else {
        console.error('Error sending link');
    }
}

async function getResult(link) {
    console.log("grdi");
    const response = await fetch(`http://localhost:5090/Link/GetLinkResult?link=${encodeURIComponent(link)}`);
    console.log(response);
    if (response.ok) {
        const data = await response.json();
        document.getElementById("result").innerText = data;
        console.log("take your jason");

    } else if (response.status === 404) {
        document.getElementById("result").innerText = "Link not found";
    } else {
        console.error('Error getting link result');
    }
}

async function uploadFile() {
    console.log("file sent");
    const file = document.getElementById("fileInput").files[0];
    const formData = new FormData();
    formData.append('file', file);
    const uri = `http://localhost:5090/File/UploadFile?file=${file}`;

    const response = await fetch(uri, {
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
        console.log(hashed); // Hash deðerini konsola yazdýr
        fetch(`http://localhost:5090/File/GetFileResults?encodedFileSha256=${hashed}`)
            .then(response => {
                console.log(response.status); // Yanýt durumunu konsola yazdýr
            })
            .catch(error => {
                console.error(error); // Hata durumunda hata mesajýný konsola yazdýr
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
function dragOverHandler(event) {
    event.preventDefault();
}

function dropHandler(event) {
    event.preventDefault();
    var files = event.dataTransfer.files;
    uploadFile(files);
}

