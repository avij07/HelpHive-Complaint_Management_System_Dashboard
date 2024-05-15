var Details = function (id) {
    var url = "/Priority/Details?id=" + id;
    $('#titleMediumModal').html("Priority Details");
    loadMediumModal(url);
};


var AddEdit = function (id) {
    var url = "/Priority/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit Priority");
    }
    else {
        $('#titleMediumModal').html("Add Priority");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Priority/Delete?id=" + id,
                success: function (result) {
                    var message = "Priority has been deleted successfully. Priority ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblPriority').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
