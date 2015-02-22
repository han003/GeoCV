$('#personal-info-panel .update-txt').blur(function () {
    update($(this));
});

function update(element) {

    var tableColumn = element.attr('id').substring(0, element.attr('id').indexOf('-'));
    var newValue = element.val();

    console.log("Table: " + tableColumn);
    console.log("Value: " + newValue);

    $.post('/Cv/Update', { Update: tableColumn, Value: newValue });
}
