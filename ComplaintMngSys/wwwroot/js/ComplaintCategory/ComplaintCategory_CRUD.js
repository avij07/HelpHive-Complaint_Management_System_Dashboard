var Details = function (id) {
    var url = "/ComplaintCategory/Details?id=" + id;
    $('#titleBigModal').html("Complaint Category Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/ComplaintCategory/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit Complaint Category");
    }
    else {
        $('#titleMediumModal').html("Add Complaint Category");
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
                url: "/ComplaintCategory/Delete?id=" + id,
                success: function (result) {
                    var message = "Complaint Category has been deleted successfully. ComplaintCategory ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblComplaintCategory').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
