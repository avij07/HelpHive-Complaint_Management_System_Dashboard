//PIE
$(document).ready(function () {
    var titleMessage = "Total Complaint: ";
    $.ajax({
        type: "GET",
        url: "/Dashboard/GetComplaintStatusGroupBy",
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            var keys = Object.keys(result);
            var _ArrayData = new Array();
            var totalspent = 0.0;
            for (var i = 0; i < keys.length; i++) {
                var arrL = new Array();
                arrL.push(keys[i]);
                arrL.push(result[keys[i]]);
                totalspent += result[keys[i]];
                _ArrayData.push(arrL);
            }
            createPIECharts(_ArrayData, titleMessage, totalspent.toFixed(0));
        }
    })
})

function createPIECharts(sum, titleText, totalspent) {
    Highcharts.chart('containerPIE', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: 'Browser market shares in January, 2018',
            text: titleText + ' ' + totalspent
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b> Total: {point.total}'                         
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                }
            }
        },
        series: [{
            name: 'Complaint Status',
            colorByPoint: true,
            data: sum,
        }]
    });
}