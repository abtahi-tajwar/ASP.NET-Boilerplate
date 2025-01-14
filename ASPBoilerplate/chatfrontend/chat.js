"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5225/chatHub", {
    withCredentials: false,
    accessTokenFactory: () => getToken(),
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
    var user = getProfile()?.id;
    debugger;
    var toUser = document.getElementById("toUserInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message, toUser).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

async function fetchUsers () {
    const url = `${apiUrl}/admin/user/list`;

  try {
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${getToken()}`,
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const data = await response.json();

    var selectInnerHtml = '';
    data.forEach(user => {
        selectInnerHtml += /*html*/`
            <option value="${user.id}">${user.username}</option>
        `
    })
    document.getElementById("toUserInput").innerHTML = selectInnerHtml;
    return data;
  } catch (error) {
    console.error('Error fetching users:', error.message);
    throw error; // Re-throw the error to handle it later if needed
  }
}

async function fetchProfile () {
    const url = `${apiUrl}/admin/user/my-profile`;

  try {
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${getToken()}`,
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const data = await response.json();
    setProfile(data.data);
    return data;
  } catch (error) {
    console.error('Error fetching users:', error.message);
    throw error; // Re-throw the error to handle it later if needed
  }
}

fetchUsers();
fetchProfile();