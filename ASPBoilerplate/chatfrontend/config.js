const apiUrl = "http://localhost:5225";
const baseUrl = "http://127.0.0.1:5500"

function setToken (token) {
    localStorage.setItem("token", token);
}
function getToken () {
    return localStorage.getItem("token")
}

function setProfile (profile) {
    localStorage.setItem("profile", JSON.stringify(profile))
}
function getProfile () {
    const profileJSON = localStorage.getItem("profile");
    return profileJSON ? JSON.parse(profileJSON) : null;
}