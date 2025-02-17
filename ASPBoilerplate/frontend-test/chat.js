"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5225/chatHub", {
    withCredentials: false
}).build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});

connection.start({ withCredentials: false, logging: true }).then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var toUser = document.getElementById("toUserInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message, toUser).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});