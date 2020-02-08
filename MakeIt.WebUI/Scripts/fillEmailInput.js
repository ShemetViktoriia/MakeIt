$(function () {

    $('.basicAutoComplete').autoComplete({
        resolverSettings: {
            url: '/Account/GetUserEmailList'
        }
    });
});