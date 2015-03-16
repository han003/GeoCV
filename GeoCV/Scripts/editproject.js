$(document).ready(function () {
    $.get('/EditProject/GetElements', function (data) {
        var autocomplete = $('#tech-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');
})

$('.update-txt').keyup(function () {
    update($(this));
});

function update(element) {

    var tableColumn = element.attr('id').substring(0, element.attr('id').indexOf('-'));
    var newValue = element.val();
    var Id = $('.title').attr('id');

    console.log("Table: " + tableColumn);
    console.log("Value: " + newValue);

    $.post('/EditProject/Update', { Id: Id, Update: tableColumn, Value: newValue });

}

$('#tech-add-btn').click(function () {
    var element = $('#tech-auto').val();
    $('#tech-group').append('<button type="button" class="btn btn-info added-btn" tabindex="-1">' + element + '</button>');

    var element = $('#tech-auto').val('');
});

