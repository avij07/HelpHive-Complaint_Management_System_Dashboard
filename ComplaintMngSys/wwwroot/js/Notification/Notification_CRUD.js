var Details = function (id) {
    var url = "/Notification/Details?id=" + id;
    $('#titleMediumModal').html("Notification Details");
    loadMediumModal(url);
};

var MarkedAsRead = function (id) {
    Swal.fire({
        title: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Notification/MarkedAsRead?id=" + id,
                success: function (result) {
                    location.reload();
                }
            });
        }
    });
};

var MarkedAllAsRead = function (id) {
    Swal.fire({
        title: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Notification/MarkedAllAsRead?id=" + id,
                success: function (result) {
                    location.reload();
                }
            });
        }
        else {
            $("#MarkedAllasRead").prop("checked", false);
        }
    });
};
