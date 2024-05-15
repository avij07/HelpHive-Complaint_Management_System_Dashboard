var Details = function (id) {
    var url = "/Comment/Details?id=" + id;
    $('#titleMediumModal').html("Comment Details");
    loadMediumModal(url);
};

var AddEdit = function (id) {
    var url = "/Comment/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit Comment");
    }
    else {
        $('#titleMediumModal').html("Add Comment");
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
                url: "/Comment/Delete?id=" + id,
                success: function (result) {
                    var message = "Comment has been deleted successfully. Comment ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Deleted!',
                        onAfterClose: () => {
                            //location.reload();
                            StatusUpdate(result.ComplaintId);
                        }
                    });
                }
            });
        }
    });
};
