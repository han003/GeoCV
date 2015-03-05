$('input[type=radio]').change(function () {

    // Find name of field to be updated
    var fieldName = $(this).prop('name').substring(0, $(this).prop('name').indexOf('-'));
    console.log('Field Name: ' + fieldName);

    // Save value as a boolean
    var value = ($(this).val() == "show") ? true : false;
    console.log('Value: ' + value);


    // Update
    $.post('/Settings/Update', { Update: fieldName, Value: value });

});