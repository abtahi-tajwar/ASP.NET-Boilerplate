
console.log("Get token", getToken());
if (getToken() && getToken() !== "") {
  window.location.href = baseUrl + "/chat.html";
}

document.getElementById("loginbtn").addEventListener("click", () => {
  const email = document.getElementById("email").value;
  const password = document.getElementById("password").value;

  // Login post request
  const url = apiUrl + "/admin/auth/login"; // Replace with your API URL
  const data = {
    Email: email,
    Password: password,
    Device: "AbtahiMacbookAirChromeChatApplication",
  };

  // Sending POST request
  fetch(url, {
    method: "POST", // HTTP method
    headers: {
      "Content-Type": "application/json", // Inform the server of the data format
      Authorization: `Bearer ${getToken()}`, // Optional: If the API requires authentication
    },
    body: JSON.stringify(data), // Convert the JavaScript object to a JSON string
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return response.json(); // Parse JSON response
    })
    .then((responseData) => {
      console.log("Success:", responseData); // Handle successful response
      setToken(responseData.data.token);
      window.location.href = baseUrl + "/chat.html";
    })
    .catch((error) => {
      console.error("Error:", error); // Handle errors
    });
});
