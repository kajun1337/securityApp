

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

async function UploadFile() {
    console.log("file sent");
    const file = document.getElementById("fileInput").files[0];
    const formData = new FormData();
    formData.append('file', file);
    const uri = `http://localhost:5090/File/UploadFile?file=${file}`;

    const response = await fetch(uri, {
        method: 'POST',
        body: formData

    });

    console.log(response);

}
