$('body').on('click', '.added-btn', function () {

    if ($(this).hasClass('btn-success')) {
        $(this).removeClass('btn-success').addClass('btn-info');
    } else {
        $(this).removeClass('btn-info').addClass('btn-success');
    }

});

$('button').click(function () {
    $(this).blur();
});

$('.downloadOption').click(function () {

    var option = $(this).text();
    var caret = ' <span class="caret"></span>';

    $('#downloadOptionButton').text(option).append(caret);

});