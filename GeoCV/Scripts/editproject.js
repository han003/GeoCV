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