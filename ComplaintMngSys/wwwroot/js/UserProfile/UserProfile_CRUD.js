var EditUserProfile = function (id) {
    if (DemoUserAccountLockAll() == 1) return;
    $('#titleMediumModal').html("<h4>Edit User Profile</h4>");
    var url = "/UserProfile/EditUserProfile?id=" + id;
    loadMediumModal(url);
};

var ResetPassword = function () {
    if (DemoUserAccountLockAll() == 1) return;
    $('#titleSmallModal').html("<h4>Reset Password</h4>");
    var id = $("#ApplicationUserId").val();
    var url = "/UserProfile/ResetPassword?id=" + id;
    loadCommonModal(url);
};

var ViewUserImage = function (imageURL) {
    $('#titleImageViewModal').html("User Profile Image ");
    $("#UserImage").attr("src", imageURL);
    loadImageViewModal();
};
