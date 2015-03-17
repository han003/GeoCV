$('#send-feedback').click(function () {

    var message = $('#message-text').val();

    console.log('Message: ' + message);

    $.ajax({
        url: '/Feedback/SendFeedback',
        data: { Feedback: message },
        type: 'POST',
        success: function () {
            $('#message-text').val('');
            alert("Feedback sendt");
        }
    });

});