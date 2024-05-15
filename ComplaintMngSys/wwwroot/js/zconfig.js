let IsDemoUser = 0;
let IsAddress = 1;
var DemoUserAccountLockAll = function () {
    if (IsDemoUser == 1) {
        SwalSimpleAlert("You are not allowed to change demo user info", "warning");
        return IsDemoUser;
    }
    else {
        return IsDemoUser;
    }
}

var SetHeaderElements = function (ElementName) {
    if (IsAddress == 1) {
        var _WhatsAppElement = WhatsAppElement();
        var _BuyNowElement = BuyNowElement();
        $('#' + ElementName).empty().append(_BuyNowElement + _WhatsAppElement);
    }
}

var SetLoginInfo = function (divName) {
    if (IsAddress == 1) {
        var _GetLoginInfoElement = GetLoginInfoElement();
        var _GetContactDetails = GetContactDetails();
        $('#' + divName).empty().append(_GetLoginInfoElement + _GetContactDetails);
    }
}

var SetFooterElements = function (divName) {
    if (IsAddress == 1) {
        var _CopyrightElement = CopyrightElement();
        var _GetContactDetails = GetContactDetails();
        $('#' + divName).empty().append(_GetContactDetails + _CopyrightElement);
    }
}


var GetContactDetails = function () {
    var content = '';
    
    return content;
}

var CopyrightElement = function () {
    var elements = '';
   
    return elements;
}

var WhatsAppElement = function () {
    var elements = '';
   
    return elements;
}

var BuyNowElement = function () {
    var elements = '';
   
    return elements;
}

var CopyToClipboard = function (Element) {
    var _Element = $('#' + Element.id).html();
    
}


var GetLoginInfoElement = function () {
    var elements = '';
   
    return elements;
}

var GetLoginCred = function () {
    var _lblEmail = $("#lblEmail").text();
    var _lblPassword = $("#lblPassword").text();
    $("#Email").val(_lblEmail);
    $("#Password").val(_lblPassword);
}

var GetLoginCredClear = function () {
    $("#Email").val('');
    $("#Password").val('');
    $("#Email").focus();
}

var SaveMetaData = function () {

}

var GetDateWithRandomNumber = function (Prefix) {
    //let _RandomNumber = Math.floor((Math.random() * 10000) + 1000);
    var _Date = new Date();
    var datestring = _Date.getFullYear() + ("0" + (_Date.getMonth() + 1)).slice(-2) + ("0" + _Date.getDate()).slice(-2)
        + ("0" + _Date.getHours()).slice(-2) + ("0" + _Date.getMinutes()).slice(-2) + ("0" + _Date.getSeconds()).slice(-2);

    var resut = Prefix + datestring;
    return resut;
}


