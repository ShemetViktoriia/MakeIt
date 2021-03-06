$(document).ready(function () {
    $.ajaxSetup({
        cache: false
    });

    $('#projectTable').DataTable({
        "paging": true,
        "serverSide": "true",
        "processing": "true",
        "aLengthMenu": [[10, 25, -1], [10, 25, "All"]],
        "ajax": {
            "url": "/Project/ProjectListViewData"
        },
        "columns": [
            { data: "Id" },
            { data: "Name" },
            { data: "Description" },
            { data: "LastUpdateDate" }
        ],
        "columnDefs": [
            {
            "targets": 3,
            "render": function (data, type, row, meta) {
                return moment(data).format('D/M/YYYY');
                }
            },
            {
                "targets": 0,
                "render": function (data) {
                    return '<a href="/Project/Edit?projectId=' + data + '">Edit</a>';
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