$(document).ready(function () {
    document.title = 'Notification';

    $("#tblNotification").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',


        buttons: [
            'pageLength',
        ],


        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/Notification/GetDataTabelData",
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
                data: "ComplaintTitle", "name": "ComplaintTitle", render: function (data, type, row) {
                    return "<a href='#' onclick=ComplaintDetails('" + row.ComplaintId + "');>" + row.ComplaintTitle + "</a>";
                }
            },
            { "data": "Message", "name": "Message" },
            { "data": "CreatedBy", "name": "CreatedBy" },
            { "data": "CreatedDateDisplay", "name": "CreatedDateDisplay" },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=MarkedAsRead('" + row.Id + "'); >Marked as Read</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [6],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

