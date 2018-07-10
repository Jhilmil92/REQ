$('#Files').on("change", function () {
    var formData = new FormData($('form').get(0));
    CallUploadService(formData);
});

function CallUploadService(file) {
    $.ajax({
        url: "/File/UploadFile",
        type: 'POST',
        data: file,
        cache: false,
        processData: false,
        contentType : false,
        success: function (response) {
            
        },
        error: function () {
            alert("Error Occured")
        }
    });
}