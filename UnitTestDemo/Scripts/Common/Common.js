function GetDataById(url, Id, callback) {
    $.ajax({
        type: "GET",
        url: `${url}/${Id}`,
        dataType: "json",
        success: function (result) {
            if (result !== null) {
                if (callback) {
                    callback(result);
                }
            }
        },
        error: function (error) {

        }
    });
}

function PostData(url, data, callback) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (callback) {
                callback(result);
            }
        },
        error: function (error) {

        }
    });
}