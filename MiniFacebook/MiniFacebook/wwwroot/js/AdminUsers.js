
function loadstateData(id) {
    $.ajax({
        
        data: { UserId: $('#'+(id-1)).val(), UserState: $('#'+id).val() },
        url: "/Admin/updatestatus",
        
        type: "POST",
        dataType: "json",
        
        success: function (result) {
           
            var state = $('#' + id).val()
            var c = [];
            $.each(result, function (key, item) {
                
                if (state == true)
                    $('#' + id).val(false);
                else
                    $('#' + id).val(true);
                
        
            });
        },
        error: function (errormessage) {
            //console.log(id);
            //console.log("test");
            alert("Faild to update user status");
        }
    });
}  




function loadsroleData(id) {
    $.ajax({

        data: { UserId: $('#' + (id - 2)).val(), RoleName: $('#' + id).val() },
        url: "/Admin/updateRole",

        type: "POST",
        dataType: "json",

        success: function (result) {
            //console.log("result");

            //console.log(result);

            var c = [];
            $.each(result, function (key, item) {
               // console.log(item.RoleName);
               // $('#' + id).val('item.RoleName');

            });
        },
        error: function (errormessage) {
            //console.log(id);
            //console.log("test");
            alert("Faild to update user role");
        }
    });
}





function loadSearch() {

    var i = 3;
    var search = $("#1000").val();


    while (document.getElementsByTagName("th")[i]) {

        var item = document.getElementsByTagName("th")[i].innerText
 

        if (item.includes(search)) {
            document.getElementsByTagName("tr")[i - 2].style.display = "table-row";
        }
        else {
            document.getElementsByTagName("tr")[i - 2].style.display = "none";
 

        }
        i++;
    }
    

}











//function loadData() {
//    $.ajax({
//        url: "/Admin/getdata",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
        
//        success: function (result) {
//            console.log(result);

//            var c = [];
//            var id = 1;
//            $.each(result, function (key,item)
//            {
//                console.log(key);
//                console.log(item);

//                $("#1000").val(item.userState)
//                c.push("<tr><form asp-action='Users' method='post'>)<th scope='row'>" + item.userName +"</th>");
//                //c.push("<tr><th scope='row'>" + item.UserName +"</th>");
//                //c.push("<tr><td" + item.userName +"</td>");
//                c.push("<td><input type='hidden' asp-for='" + item.UserId + "' name='UserId'/></td>");
//                //c.push("<td>" + item.userId + "/></td>");
//                c.push("<td><input type='checkbox' name='UserState' asp-for='" + item.userState + "' onclick='submit()' /></td>");

//                c.push("<td>")
//                //c.push("< select name = 'RoleName' asp -for= '"+item.roleName+"' asp-items= "(SelectList)@ViewData[" Roles"]" onchange = "this.form.submit()" ></select>")
//                c.push("</td>");
//                c.push("</form></tr >");
//                c.push("</tr>");
//            });
//            $('.tbody').html(c.join(''));
//        },
//        error: function (errormessage) {
//            console.log("test");
//            alert(errormessage.responseText);
//        }
//    });
//}  
