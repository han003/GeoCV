/////////////////// GET AUTO COMPLETE ITEMS

$(document).ready(function () {
    refreshAutoLists();
})

function refreshAutoLists() {
    


    $.get('/Cv/GetProgrammingLanguages', function (data) {
        var autocomplete = $('#Programmeringsspråk-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');

    $.get('/Cv/GetFrameworks', function (data) {
        var autocomplete = $('#Rammeverk-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');

    $.get('/Cv/GetWebTechnologies', function (data) {
        var autocomplete = $('#WebTeknologier-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');

    $.get('/Cv/GetDatabaseSystems', function (data) {
        var autocomplete = $('#Databasesystemer-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');

    $.get('/Cv/GetServerside', function (data) {
        var autocomplete = $('#Serverside-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');

    $.get('/Cv/GetOperatingSystems', function (data) {
        var autocomplete = $('#Operativsystemer-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');
}


//////////////////// INSERT NEW AUTO STUFF

var autoTextfield;
var userAutoInput;
var databaseUpdateColumn;

// What happens when add button is clicked
$('.add-item-btn').click(function () {
    addItem($(this));
});

// What happens when the Enter key is pressed
$('.add-item-auto').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        addItem($(this));
    }
});

// What happens when removing a button
$('body').on('click', '.added-btn', function () {

    databaseUpdateColumn = $(this).parent().attr('id');
    databaseUpdateColumn = databaseUpdateColumn.substring(0, databaseUpdateColumn.indexOf('-'));

    $(this).remove();
    updateDatabase();
});

function addItem(element) {
    databaseUpdateColumn = element.attr('id');
    databaseUpdateColumn = databaseUpdateColumn.substring(0, databaseUpdateColumn.indexOf('-'));

    console.log('Column: ' + databaseUpdateColumn)

    // Get textfield
    autoTextfield = $('#' + databaseUpdateColumn + '-auto');

    // Get value
    userAutoInput = autoTextfield.val();

    console.log('Value: ' + userAutoInput)

    // Check if the value is already in the database
    var json = autoTextfield.data('json');

    if (itemExists(userAutoInput, json)) {
        $('#' + databaseUpdateColumn + '-group').append('<button type="button" class="btn btn-info added-btn" tabindex="-1">' + userAutoInput + '</button>');
        autoTextfield.val('');
        updateDatabase();
    } else {
        $('#modalAddText').append('<code>' + userAutoInput + '</code> finnes ikke i databasen, vil legge den til?');
        $('#myModal').modal();
    }
}

// Check if item exsists in the json data
function itemExists(userAutoInput, json) {
    var exists = false;
    $.each(json, function (name, value) {
        if (userAutoInput.toLowerCase() == value.toLowerCase()) {
            exists = true;
        }
    });
    return exists;
}

// Reset the text in the modal whenever the modal is hidden
$('#myModal').on('hidden.bs.modal', function (event) {
    $('#modalAddText').text('');
});

// If modal button to add is clicked, add new item to database
$('#modalAddItem').click(function () {
    // Insert into database
    insertAutoItem(databaseUpdateColumn, userAutoInput);

    // Hide modal
    $('#myModal').modal('hide');

    // Append the new button
    $('#' + databaseUpdateColumn + '-group').append('<button type="button" class="btn btn-info added-btn" tabindex="-1">' + userAutoInput + '</button>');

    // Erase the text in the autocomplete textfield
    autoTextfield.val('');

    // Update field in database
    updateDatabase();
});

function insertAutoItem(databaseUpdateColumn, userAutoInput) {
    console.log('Inserting ' + userAutoInput + ' into ' + databaseUpdateColumn)
    $.post('/Cv/InsertItem', { Insert: databaseUpdateColumn, Value: userAutoInput }, function () {
        refreshAutoLists();
    });
}

function updateDatabase() {

    // Empty var to hold text
    var newValue = '';

    // Loop through all buttons
    $('#' + databaseUpdateColumn + '-group button').each(function () {
        newValue += $(this).text() + ';';
    });

    // Remove last ;
    newValue = newValue.substring(0, newValue.length - 1);

    console.log("Table: " + databaseUpdateColumn);
    console.log("Value: " + newValue);

    // Update
    $.post('/Cv/Update', { Update: databaseUpdateColumn, Value: newValue });
}