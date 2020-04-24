
// Add New Post
$("#post").on("click", function () {
    // npost hold the data of new post to send it to action 
    var npost = {};
    npost.Content = $("#content").val();

    if (document.getElementById("file-input").value != "") {
        var str = document.getElementById("file-input").value;
        npost.PostPhoto = str.substring(11, str.length);
        console.log(str.substring(10, str.length));
    }
    //after loading data send req to add the new post to Database
    $.ajax({
        method: "Post",
        url: "addPost",
        data: {p:npost},
        success: function (data) {
            $("#AllPosts").html(data);
            $("#content").val('');
            document.getElementById("file-input").value="";
        }

    });
});
//------------------------------------------------------------------
// Add/Remove Like To/From Post
function Liked(id) {
    //id is the id of the like button
    //check the class of like button if(light)-> make new like  else make  like before
    var btn = document.getElementById(id).className;
    //loading like data to send to action
    var like = {};
    like.PostID = id.substring(8);
    console.log(like.PostID);
    var nLike = $('#nolike_' + id.substring(8)).text();
    //Make like on post
    if (btn == "btn btn-light") {
            $.ajax({
                method: "Post",
                url: "LikePost",
                data: { like: like },
                success: function () {
                    document.getElementById(id).classList.add("btn-primary");
                    document.getElementById(id).classList.remove("btn-light");
                    nLike = parseInt(nLike)+1 ;
                    $('#nolike_' + id.substring(8)).text(nLike + ' Likes');
                }
            });
        }
    //Remove like from post
        else {
            $.ajax({
                method: "Post",
                url: "RemoveLikePost",
                data: { like: like },
                success: function () {
                    document.getElementById(id).classList.add("btn-light");
                    document.getElementById(id).classList.remove("btn-primary");
                    nLike = parseInt(nLike) - 1;
                    $('#nolike_' + id.substring(8)).text(nLike + ' Likes');
                }
            });
        }

}
//---------------------------------------------------------------------------
//Show Comments
function showComments(id) {
    id = parseInt(id.substring(13));
    console.log(id, typeof id);
    $.ajax({
        method: 'Post',
        url: 'LoadComments',
        data: { postid: id }, 
        success: function (data) {
            document.getElementById('comments_' + id).innerHTML = '';
            data += '<br /> <input type="text" class="col-7 d-inline form-control" id="commentContent_' + id +'"/>';
            data += '<button class="btn animated-button col-4 ml-4 mb-1 " id="addcomment_"' + id + ' onclick="addComment(' + id + ')">Comment</button>';
            $('#comments_' + id).prepend(data);
        }
    });
    $('#comments_' + id).toggle();
}
//---------------------------------------------------------------------------
function addComment(pid) {


    var comm = {}
    comm.PostID = pid;
    comm.CommentText = document.getElementById('commentContent_' + pid).value;
    console.log(comm);
   
    $.ajax({
        method: 'Post',
        url: 'AddComment',
        data: { c: comm },
        success: function (data) {
            console.log(data);
            $('#comments_' + pid).prepend(data);
        }
    });


}
//////////////////////////////////
function showupdatePost(id) {
    console.log(id);
    $.ajax({
        method: 'post',
        url: 'updatePost',
        data: { pid: parseInt(id.substring(7)) },
        success: function (data) {
            console.log(data);
            document.getElementById('update').innerHTML = data;
            $('#mod').modal('show');
        }
    });
}
//////////////////////////////////////
function deletePost(id) {
    $.ajax({
        method: 'Post',
        url: 'deletePost',
        data: { pID: id.substring(7) },
        success: function (data) {
            document.getElementById('AllPosts').innerHTML = data;
        }
    });
}
////////////////////////////////////////////
var img;
function selected(id) {
    img = document.getElementById("iimg").value;
    document.getElementById('newImage').src = '/Images/' + img.substring(12, img.length);
    document.getElementById('newImage').style.visibility = 'visible';
    $('#newImage').show();

}
//////////////////////////////////////////////////
//Update Post
function updatePost(id) {
    var updadetPost = {};
    updadetPost.PostID = id;
    if (img != undefined) {
        updadetPost.PostPhoto = img.substring(12, img.length);
        console.log('img:       '+updadetPost.PostPhoto)
    }
    updadetPost.Content = document.getElementById('newContent').value;
    console.log(updadetPost);
    $.ajax({
        method: 'Post',
        url: 'confirmUpdate',
        data: { post: updadetPost },
        success: function () {
            document.getElementById('content_' + id).innerText = updadetPost.Content;
            if (updadetPost.PostPhoto != null) {
                console.log('old:     ' + document.getElementById('img_' + id).src);
                document.getElementById('img_' + id).src = "/Images/" + updadetPost.PostPhoto;
                console.log('new:     ' + document.getElementById('img_' + id).src);

                $('#img_' + id).show();
            }
            $('#mod').modal('hide');
            img = undefined;
        }
    });
    img = undefined;
}
///////////////////////////////////////////////////////////
function showLikes(id) {
    $.ajax({
        method: 'Post',
        url: 'whoLikePost',
        data: { pid: id },
        success: function (data) {
            document.getElementById('LikeDiv').innerHTML = data;
            $('#Lmodal').modal('show');
        }
    });
}
////////////////////////////////////////////////////
function likeComment(id) {
    var likeComm = {};
    likeComm.CommentID = id;
    var btn = document.getElementById('likecomm_' + id).className;
    if (btn.search('btn-light')>0) {
        $.ajax({
            method: "Post",
            url: "likeComment",
            data: { c: likeComm },
            success: function () {
                document.getElementById('likecomm_'+id).classList.add("btn-primary");
                document.getElementById('likecomm_'+id).classList.remove("btn-light");
            }
        });
    }
    else {
        $.ajax({
            method: "Post",
            url: "removeCommentLike",
            data: { cid: id },
            success: function () {
                document.getElementById('likecomm_' + id).classList.add("btn-light");
                document.getElementById('likecomm_' + id).classList.remove("btn-primary");
            }
        });
    }

}


///////////////////////////////////////////////////////


