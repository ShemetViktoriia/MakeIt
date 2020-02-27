$(document).ready(function () {
    $('#projectTable').DataTable({
        "paging": true,
        "serverSide": "true",
        "processing": "true",
        "aLengthMenu": [[10, 25, -1], [10, 25, "All"]],
        "ajax": {
            "url": $("#projectTable").prop("data-url"),
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { data: "Name" },
            { data: "Description" },
            {
                data: "LastUpdateDate",
                type: 'date'//,
                //render: function (data) { return data ? moment(data).format('ddd DD/MM/YY') : ''; }
            }
        ],
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