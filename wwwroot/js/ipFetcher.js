// http://localhost:5090/IpAddress/getIpDbIpAddressResult?ipAddress=192.33.22.23
// http://localhost:5090/IpAddress/getVirusTotalIpAddressResult?ipAddress=192.1.1.1


async function getIpAddressResult() {
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
}

