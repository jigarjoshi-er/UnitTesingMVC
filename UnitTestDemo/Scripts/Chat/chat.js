$(function () {
    // Reference the auto-generated proxy for the hub.  
    var chat = $.connection.chatHub;

    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page. 
        //$('#discussion').append('<li><strong>' + htmlEncode(name)
        //    + '</strong>: ' + htmlEncode(message) + '</li>');

        alert(`User : ${htmlEncode(name)}\nMessage: ${htmlEncode(message)}`);
    };

    chat.client.userConnected = function (name, connectionId) {

        $("#listUsers").append('<li class="list-group-item" data-connectionid="' + connectionId + '">\
                                    ' + name + '\
                                    <span class="badge badge-success font-18 mt--3">&#9679;</span>\
                                </li>')
    };

    chat.client.userDisconnected = function (name, connectionId) {
        $("#listUsers").find("li[data-connectionid='" + connectionId + "']").remove();
    };

    $.connection.hub.start().done(function () {
        chat.server.send("Jigar", "hello !");
        // Clear text box and reset focus for next comment. 
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}