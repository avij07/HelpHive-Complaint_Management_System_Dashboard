$(document).ready(function() {
    document.title = 'Audit Logs';

    $("#tblAuditLogs").DataTable({
        paging: true,
        select: true,
        "order": [
            [0, "desc"]
        ],
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
            "url": "/AuditLogs/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [{
                data: "Id",
                "name": "Id",
                render: function(data, type, row) {
                    return "<a href='#' class='fa fa-eye' onclick=Details('" + row.Id + "');>" + row.Id + "</a>";
                }
            },
            { "data": "UserId", "name": "UserId" },
            { "data": "Type", "name": "Type" },
            { "data": "TableName", "name": "TableName" },           
            {
                "data": "DateTime",
                "name": "DateTime",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            //{ "data": "OldValues", "name": "OldValues" },
            //{ "data": "NewValues", "name": "NewValues" },
            //{ "data": "AffectedColumns", "name": "AffectedColumns" },
            { "data": "PrimaryKey", "name": "PrimaryKey" },
        ],

        "lengthMenu": [
            [20, 10, 15, 25, 50, 100, 200],
            [20, 10, 15, 25, 50, 100, 200]
        ]
    });
});