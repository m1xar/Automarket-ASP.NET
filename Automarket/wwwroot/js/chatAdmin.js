const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

document.getElementById("connectBtn").addEventListener("click", function () {
    hubConnection.start()
        .then(function () {

            document.getElementById("connectButton").style.display = "none";
            document.getElementById("inputForm").style.display = "block";
            hubConnection.invoke("Start");
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
});

document.getElementById("sendBtn").addEventListener("click", function () {
    let message = document.getElementById("message").value;
    hubConnection.invoke("Send", message)
        .catch(function (err) {
            return console.error(err.toString());
        });
    document.getElementById("message").value = "";
    displayMessage(message, 'sent');
});

document.getElementById("exitBtn").addEventListener("click", function () {
    hubConnection.invoke("Exit")
    hubConnection.stop();
    document.getElementById("connectButton").style.display = "block";
    document.getElementById("inputForm").style.display = "none";
    document.getElementById("chatroom").style.height = "calc(100% - 50px)";
    clearChatroom();
});

hubConnection.on("Receive", function (message) {
    displayMessage(message, 'received');
});

function displayMessage(message, messageType) {
    let messageElement = document.createElement("div");
    messageElement.classList.add('chat-message', messageType);
    messageElement.innerHTML = `
            <div class="message-content">${message}</div>`;
    document.getElementById("chatroom").appendChild(messageElement);
}
function clearChatroom() {
    document.getElementById("chatroom").innerHTML = ""; // Clear chatroom content
}