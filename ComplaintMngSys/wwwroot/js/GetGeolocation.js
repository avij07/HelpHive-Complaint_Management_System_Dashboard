
var lat;
var long;

var GetGeolocation = function () {
    $("#btnUserLogin").prop("disabled", true);
    setTimeout(function () {
        $("#btnUserLogin").removeAttr('disabled');
    }, 1000);

    if (navigator.geolocation) {
        parseFloat(navigator.geolocation.getCurrentPosition(showPosition));
    } else {
        lat = "Geolocation is not supported by this browser.";
        long = "Geolocation is not supported by this browser.";
    }
    function showPosition(position) {
        lat = position.coords.latitude;
        long = position.coords.longitude;
    }
};


var SendGeolocation = function () {
    $('#Latitude').val(lat);
    $('#Longitude').val(long);
};

