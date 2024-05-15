var Details = function (id) {
    var url = "/AttachmentFile/Details?id=" + id;
    $('#titleMediumModal').html("AttachmentFile Details");
    loadMediumModal(url);
};

var DownloadFile = function (id) {
    location.href = "/Complaint/DownloadFile?id=" + id;
};


var AddEdit = function (id) {
    var url = "/AttachmentFile/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit AttachmentFile");
    }
    else {
        $('#titleMediumModal').html("Add AttachmentFile");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/AttachmentFile/Delete?id=" + id,
                success: function (result) {
                    var message = "AttachmentFile has been deleted successfully. AttachmentFile ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Deleted!',
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};
