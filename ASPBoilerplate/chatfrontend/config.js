const apiUrl = "http://localhost:5225";
const baseUrl = "http://127.0.0.1:5500"

function setToken (token) {
    localStorage.setItem("token", token);
}
function getToken () {
    return localStorage.getItem("token")
}