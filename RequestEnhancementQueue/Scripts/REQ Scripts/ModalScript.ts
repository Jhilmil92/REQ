var postBackURL = '/Job/ChangeLog';
$(function () {
    $(document).on('click', '.change-log-button', function () {
        debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: "true" };
        $.ajax({
            type: "GET",
            url: postBackURL,
            contentType: "application/json; charset = utf-8",
            data: { "jobId": id },
            datatype: "json",
            success: function (data) {

                $('#showLog').html(data);
                $('#logModal').modal(options);
                $('#logModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed!");
            }
        }
            );
    });
});