'use strict';
$(function () {
    $('#emailInvite').keyup(function () {
        $('#sendButton').prop('disabled', $(this).val().length <= 2);
    });
});