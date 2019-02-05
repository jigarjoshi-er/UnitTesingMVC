$(function () {
    InitControls();
});

function InitControls() {
    $(".btnManageRoles").click(function () {
        var userId = $(this).data("id");
        GetUserRoles(userId);
    });

    $("#btnCloseManageRoles").click(function () {
        CloseManageUserRoles();
    });

    $("#btnSaveUserRoles").click(function () {
        SaveUserRoles();
    });
}

function GetUserRoles(userId) {
    GetDataById("Users/GetUserRoles", userId, GetUserRolesResult);
}

function GetUserRolesResult(result) {
    if (result != null) {
        for (var i = 0; i < result.Roles.length; i++) {
            $("#ManageUserRoles").find("input[value='" + result.Roles[i] + "']").prop("checked", true);
        }
        $("#hdnUserId").val(result.Id);
        $("#spnUserName").text(result.UserName);
    }

    $("#ManageUserRoles").modal("show");
}

function CloseManageUserRoles() {
    $("#ManageUserRoles").find("input:checkbox").prop("checked", false);
    $("#spnUserName").text("");
    $("#hdnUserId").val(null);
    $("#ManageUserRoles").modal("hide");
}

function SaveUserRoles() {

    var selectedRoles = [];

    $("#ManageUserRoles").find("input[name='UserRole']:checked").each(function () {
        selectedRoles.push($(this).attr("value"));
    });

    var userRoles = {
        Id: $("#hdnUserId").val(),
        Roles : selectedRoles
    };

    PostData("Users/SaveUserRoles", userRoles, SaveUserRolesResult);
}

function SaveUserRolesResult() {
    CloseManageUserRoles();
    location.reload();
}