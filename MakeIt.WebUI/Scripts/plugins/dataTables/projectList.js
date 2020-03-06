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
            //"type": "POST", // ajax type must be match to controllers action type
            //"datatype": "json"
        },
        "columns": [
            { data: "Id" },
            { data: "Name" },
            { data: "Description" },
            {
                data: "LastUpdateDate",
                type: 'date'
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