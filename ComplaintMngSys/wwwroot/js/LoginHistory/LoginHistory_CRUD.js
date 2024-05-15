var Details = function (id) {
    var url = "/LoginHistory/Details?id=" + id;
    $('#titleMediumModal').html("Login History Details");
    loadMediumModal(url);
};

