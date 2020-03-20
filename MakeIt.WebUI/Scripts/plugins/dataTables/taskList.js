$(document).ready(function () {
    $.ajaxSetup({
        cache: false
    });

    $('#taskTable').DataTable({
        "paging": true,
        "serverSide": "true",
        "processing": "true",
        "aLengthMenu": [[1, 2, 3, 4, 5], [1, 2, 3, 4, 5]],
        "ajax": {
            "url": "/Task/TaskListViewData"
        },
        "columns": [
            { data: "Id" },
            { data: "Title" },
            { data: "Description" },
            { data: "Status" },
            { data: "Priority" },
            { data: "CreatedUser" },
            { data: "Project" },
            { data: "DueDate" }
        ],
        "columnDefs": [
            {
            "targets": 7,
            "render": function (data, type, row, meta) {
                return moment(data).format('D/M/YYYY');
                }
            },
            {
                "targets": 0,
                "render": function (data) {
                    return '<a href="/Task/Edit?taskId=' + data + '">Edit</a>';
                }
            }
        ],                    
        searchDelay: 350,
        "pagingType": "full_numbers",
        "language": {
            "processing": "processing... please wait"
        },
        stateSave: true,
        autoFill: true,
        bAutoWidth: true,
        aoColumns: [
            { sWidth: '25%' },
            { sWidth: '60%' },
            { sWidth: '15%' }
        ],
        scrollY: '60vh',
        scrollCollapse: true
    });
});