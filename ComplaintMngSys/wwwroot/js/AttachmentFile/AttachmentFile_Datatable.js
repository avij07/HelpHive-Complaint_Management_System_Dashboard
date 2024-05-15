$(document).ready(function () {
    document.title = 'AttachmentFile';

    $("#tblAttachmentFile").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',


        buttons: [
            'pageLength',
        ],

        "processing": true,
        "serverSide": true,
        "filter": true, //Search Box
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/AttachmentFile/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            {
                data: "Id", "name": "Id", render: function (data, type, row) {
                    return "<a href='#' onclick=Details('" + row.Id + "');>" + row.Id + "</a>";
                }
            },
            { "data": "ComplaintId", "name": "ComplaintId" },
            {
                data: "ComplaintName", "name": "ComplaintName", render: function (data, type, row) {
                    return "<a href='#' onclick=ComplaintDetails('" + row.ComplaintId + "');>" + row.ComplaintName + "</a>";
                }
            },           
            { "data": "ContentType", "name": "ContentType" },
            { "data": "FileName", "name": "FileName" },
            { "data": "Length", "name": "Length" },
            { "data": "IsDeleted", "name": "IsDeleted" },
            { "data": "IsAdmin", "name": "IsAdmin" },

            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-success btn-xs' onclick=DownloadFile('" + row.Id + "'); >Download</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [6, 7],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

