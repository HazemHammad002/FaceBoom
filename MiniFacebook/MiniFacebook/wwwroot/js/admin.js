$(document).ready(function () {
    loadData();
});
function loadData() {
    $.ajax({
        url: "/Admin/ListRoles",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var c = [];
            $.each(result, function (key, item) {

                c.push("<tr><td>" + item.id + "</td>");
                c.push("<td>" + item.name + "</td>");
                c.push("<td>" + item.description + "</td>");
                c.push(`<td><button type='button' class='btn edit-btn '  onclick='getbyID("${item.id}");'>Edit</button> </td>`);
                c.push(`<td><button type='button' class='btn delete-btn'  onclick='Delete("${item.id}");'>Delete</button></td></tr>`);
            });
            $('.tbody').html(c.join(''));
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Add() {

    $.ajax({
        url: "/Admin/ManageRoles",
        data: { RoleDescription: $('#RoleDescription').val(), RoleName: $('#RoleName').val() },
        type: "POST",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#Id').val("");
            $('#RoleDescription').val("");
            $('#RoleName').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(Id) {
    $('#RoleName').css('border-color', 'lightgrey');
    $('#RoleDescription').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Admin/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            $('#Id').val(result.id);
            $('#RoleName').val(result.roleName);

            $('#RoleDescription').val(result.roleDescription);
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Update() {
    $.ajax({
        url: "/Admin/EditRole",
        data: { Id: $('#Id').val(), RoleDescription: $('#RoleDescription').val(), RoleName: $('#RoleName').val() },
        type: "POST",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#Id').val("");
            $('#RoleDescription').val("");
            $('#RoleName').val("");
            $('#btnUpdate').hide();
            $('#btnAdd').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/admin/DeleteRole/" + ID,
            type: "POST",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}