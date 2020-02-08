$(document).ready(function () {
    $('#projectTable').DataTable({
        "info": true,
        stateSave: true,
        "pagingType": "full_numbers",
        "pageLength": 10,
        autoFill: true,
        bAutoWidth: true,
        aoColumns: [
            { sWidth: '25%' },
            { sWidth: '60%' },
            { sWidth: '15%' }
        ],
        scrollY: '60vh',
        scrollCollapse: true,
        "lengthMenu": [[10, 25, -1], [10, 25, "All"]]
    });
});