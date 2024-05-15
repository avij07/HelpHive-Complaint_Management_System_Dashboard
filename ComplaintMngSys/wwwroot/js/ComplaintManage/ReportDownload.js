var DownloadasjsPDFComplaint = function () {
    const { jsPDF } = window.jspdf;
    let doc = new jsPDF('l', 'mm', [1500, 1400]);

    ButtonEDLoader(false, 'btnDownloadComplaint', '<i class="fa fa-spinner fa-spin"></i> Downloading...')
    var imgIcon = "<img class='img-fluid img-thumbnail' id='imgServiceItemPreview' src='' alt='' />";
    let _divPrint = document.querySelector('#printableArea');

    var _ComplaintCode = $('#ComplaintCode').html();
    doc.html(_divPrint, {
        callback: function (doc) {
            doc.save("Complaint_" + _ComplaintCode + ".pdf");
        },
        x: 16,
        y: 16
    });

    setTimeout(function () {
        ButtonEDLoader(true, 'btnDownloadComplaint', '<span class="fa fa-download">');
    }, 800);
}