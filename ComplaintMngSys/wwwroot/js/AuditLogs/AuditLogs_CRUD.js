var Details = function(id) {
    var url = "/AuditLogs/Details?id=" + id;
    $('#titleExtraBigModal').html("Audit Logs Details");
    loadExtraBigModal(url);
};