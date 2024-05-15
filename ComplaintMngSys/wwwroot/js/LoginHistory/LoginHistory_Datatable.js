$(document).ready(function () {
    document.title = 'LoginHistory';

    $("#tblLoginHistoryInfo").DataTable({
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
            "url": "/LoginHistory/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [           
            { "data": "Id", "name": "Id" },
            {
                data: "UserName", "name": "UserName", render: function (data, type, row) {
                    return "<a href='#' onclick=Details('" + row.Id + "');>" + row.UserName + "</a>";
                }
            },
            { "data": "LoginTime", "name": "LoginTime" },
            { "data": "LogoutTime", "name": "LogoutTime" },
            { "data": "Duration", "name": "Duration" },

            { "data": "PublicIP", "name": "PublicIP" },
            { "data": "Latitude", "name": "Latitude" },
            { "data": "Longitude", "name": "Longitude" },
            { "data": "Browser", "name": "Browser" },
            { "data": "OperatingSystem", "name": "OperatingSystem" },
            { "data": "Device", "name": "Device" },
            { "data": "Action", "name": "Action" },
            { "data": "ActionStatus", "name": "ActionStatus" },

            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            }
        ],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

