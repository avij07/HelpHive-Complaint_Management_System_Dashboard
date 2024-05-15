$(document).ready(function () {
    document.title = 'Complaint';
    var _ComplaintList = $("#ComplaintList").val();

    $("#tblComplaint").DataTable({
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
            "url": "/Complaint/GetDataTabelData?ComplaintList=" + _ComplaintList,
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            { "data": "Id", "name": "Id" },
            {
                data: "Code", "name": "Code", render: function (data, type, row) {
                    return "<a href='#' onclick=Details('" + row.Id + "');>" + row.Code + "</a>";
                }
            },
            { "data": "Name", "name": "Name" },
            { "data": "CategoryName", "name": "CategoryName" },
            { "data": "AssignTo", "name": "AssignTo" },
            { "data": "Priority", "name": "Priority" },
            { "data": "Status", "name": "Status" },
            { "data": "Complainant", "name": "Complainant" },

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
                    return "<button class='btn btn-link btn-xs' onclick=PrintComplaint('" + row.Id + "');><span class='fa fa-print'>Print</span></button>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    if (row.Status == 'New')
                        return "<a href='#' class='btn btn-danger btn-xs' onclick=SubmitComplaint('" + row.Id + "'); >Submit</a>";
                    else
                        return "<a href='#' class='btn btn-danger btn-xs disabled' onclick=SubmitComplaint('" + row.Id + "'); >Submit</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    if (row.Status == 'New' || _ComplaintList == 'AssignTo' )
                        return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                    else
                        return "<a href='#' class='btn btn-info btn-xs disabled' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    if (row.Status == 'New')
                        return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
                    else
                        return "<a href='#' class='btn btn-danger btn-xs disabled' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [9, 10, 11, 12],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

