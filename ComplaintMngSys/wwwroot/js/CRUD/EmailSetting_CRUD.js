var SMTPEmailSettingDetails = function (id) {
    var url = "/EmailSetting/SMTPEmailSettingDetails?id=" + id;
    $('#titleMediumModal').html("SMTP Email Setting Details ");
    loadMediumModal(url);
};

var SMTPEmailSettingAddEdit = function (id) {
    var url = "/EmailSetting/SMTPEmailSettingAddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit SMTP Email Setting ");
    }
    else {
        $('#titleMediumModal').html("Add SMTP Email Setting ");
    }
    loadMediumModal(url);
};


var SendGridSettingDetails = function (id) {
    var url = "/EmailSetting/SendGridSettingDetails?id=" + id;
    $('#titleMediumModal').html("SendGrid Setting Details ");
    loadMediumModal(url);
};

var SendGridSettingAddEdit = function (id) {
    var url = "/EmailSetting/SendGridSettingAddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit SendGrid Setting");
    }
    else {
        $('#titleMediumModal').html("Add SendGrid Setting ");
    }
    loadMediumModal(url);
};
