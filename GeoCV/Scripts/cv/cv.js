$('button').click(function () {
    $(this).blur();
});

$('.downloadOption').click(function () {

    var option = $(this).text();
    var caret = ' <span class="caret"></span>';

    $('#downloadOptionButton').text(option).append(caret);

});