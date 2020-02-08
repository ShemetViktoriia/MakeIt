'use strict';
//$(function () {
//    $('#name').keyup(function () {
//        $('#editButton').prop('disabled', $(this).val().length <= 5);
//    });
//});

$(document).ready(function () {
    $("#projectForm").change(function () {
        $("#editButton").removeAttr("disabled");
    });
});
