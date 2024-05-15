var Details = function (id) {
    var url = "/Complaint/Details?id=" + id;
    $('#titleExtraBigModal').html("Complaint Details");
    loadExtraBigModal(url);
};

var PrintComplaint = function (id) {
    location.href = "/ComplaintManage/PrintComplaint?id=" + id;
};

var AddEdit = function (id) {
    var url = "/Complaint/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Complaint");
    }
    else {
        $('#titleExtraBigModal').html("Add Complaint");
    }
    loadExtraBigModal(url);
};

var AddComplaint = function () {
    if (!$("#frmComplaint").valid()) {
        return;
    }
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/Complaint/AddComplaint",
        data: frmComplaintFormObj(),
        processData: false,
        contentType: false,
        success: function (result) {
            var message = "Complaint Created Successfully. Complaint ID: " + result.Id;
            console.log(result);
            Swal.fire({
                title: message,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblComplaint').DataTable().ajax.reload();
            });
        }
    });
};

var UpdateComplaint = function () {
    if (!$("#frmUpdateComplaint").valid()) {
        return;
    }

    var _frmUpdateComplaint = $("#frmUpdateComplaint").serialize();
    if ($("#Name").val() === "" || $("#Name").val() === null) {
        Swal.fire({
            title: 'Name field can not be null or empty.',
            icon: "warning",
            onAfterClose: () => {
                $("#Name").focus();
            }
        });
        return;
    }
    if ($("#Description").val() === "" || $("#Description").val() === null) {
        Swal.fire({
            title: 'Description field can not be null or empty.',
            icon: "warning",
            onAfterClose: () => {
                $("#Description").focus();
            }
        });
        return;
    }

    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/Complaint/UpdateComplaint",
        data: _frmUpdateComplaint,
        success: function (result) {
            var message = "Complaint Updated Successfully. Complaint ID: " + result.Id;
            Swal.fire({
                title: message,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblComplaint').DataTable().ajax.reload();
            });
        }
    });
};


var Delete = function (id) {
    if (DemoUserAccountLockAll() == 1) return;
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
                url: "/Complaint/Delete?id=" + id,
                success: function (result) {
                    var message = "Complaint has been deleted successfully. Complaint ID: " + result.Id;
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


var AddNewAttachmentFile = function () {
    if ($("#AttachmentFile").val() === "" || $("#AttachmentFile").val() === null) {
        Swal.fire({
            title: 'Attachment File field can not be null or empty.',
            icon: "warning",
            onAfterClose: () => {
                $("#AttachmentFile").focus();
            }
        });
        return;
    }

    var formData = new FormData()
    formData.append('ComplaintId', $("#ComplaintId").val())
    formData.append('AttachmentFile', $('#AttachmentFile')[0].files[0])

    $.ajax({
        type: "POST",
        url: "/Complaint/AddNewAttachmentFile",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            AddEdit(result.ComplaintId);
            activaTab('divAttachmentFileTab');
        }
    });
};

var DeleteAttachmentFile = function (id) {
    Swal.fire({
        title: 'Do you want to download this file?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "Post",
                url: "/Complaint/DeleteAttachmentFile?id=" + id,
                success: function (result) {
                    Details(result.ComplaintId);
                }
            });
        }
    });
};


var AddNewComment = function () {
    var myformdata = $("#frmComplaintDetails").serialize();
    if ($("#NewComment").val() === "" || $("#NewComment").val() === null) {
        Swal.fire({
            title: 'New comment field can not be null or empty.',
            icon: "warning",
            onAfterClose: () => {
                $("#NewComment").focus();
            }
        });
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Complaint/AddNewComment",
        data: myformdata,
        success: function (result) {
            //$('#ComplaintTab').removeClass('active');
            //$('#CommentHistoryTab').addClass('active');
            //AddEdit(result.ComplaintId);
            Details(result.ComplaintId);
        }
    });
};


var AddNewCommentFromDetails = function () {
    var myformdata = $("#frmComplaintDetails").serialize();
    if ($("#NewComment").val() === "" || $("#NewComment").val() === null) {
        Swal.fire({
            title: 'New comment field can not be null or empty.',
            icon: "warning",
            onAfterClose: () => {
                $("#NewComment").focus();
            }
        });
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Complaint/AddNewComment",
        data: myformdata,
        success: function (result) {
            Details(result.ComplaintId);
        }
    });
};

var DeleteComment = function (id, IsStatusUpdate) {
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
                url: "/Comment/Delete?id=" + id,
                success: function (result) {
                    if (IsStatusUpdate)
                        StatusUpdate(result.ComplaintId);
                    else
                        Details(result.ComplaintId);
                }
            });
        }
    });
};




var StatusUpdate = function (id) {
    var url = "/ComplaintManage/StatusUpdate?id=" + id;
    $('#titleBigModal').html("Complaint Status Update");
    loadBigModal(url);
};

var SubmitComplaint = function (id) {
    Swal.fire({
        title: 'Do you want to Submit this Complaint?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Complaint/Submit?id=" + id,
                success: function (result) {
                    var message = "Complaint has been Submited successfully. Complaint ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Success!',
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};


var frmComplaintFormObj = function () {
    var _FormData = new FormData()
    _FormData.append('Id', $("#Id").val())
    _FormData.append('Name', $("#Name").val())
    _FormData.append('Description', $("#Description").val())
    _FormData.append('DueDate', $("#DueDate").val())
    _FormData.append('Category', $("#Category").val())
	
    _FormData.append('AssignTo', $("#AssignTo").val())
    _FormData.append('Status', $("#Status").val())
    _FormData.append('Priority', $("#Priority").val())
    _FormData.append('Remarks', $("#Remarks").val())
    
	_FormData.append('AttachmentFile', $('#AttachmentFile')[0].files[0])
    return _FormData;
}







