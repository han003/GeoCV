$(document).ready(function () {
    getProjects();
})

function getProjects() {
    $.get('/EditProject/GetElements', function (data) {
        $(function () {
            $("#tech-auto").typeahead({
                minLength: 0,
                source: data
            });
        });

        $("#tech-load").addClass('hidden');
        $("#tech-form").removeClass('hidden');

        $("#tech-auto").data('json', data);
    }, 'json');
}

$('.update-txt').keyup(function () {
    updateProjectInfo($(this));
});

function updateProjectInfo(element) {

    var tableColumn = element.attr('id').substring(0, element.attr('id').indexOf('-'));
    var newValue = element.val();

    // Finn ID
    var id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);

    console.log("Id: " + id);
    console.log("Value: " + newValue);

    $.post('/EditProject/UpdateProjectInfo', { Id: id, Update: tableColumn, Value: newValue });

}

//////////////////// INSERT NEW AUTO STUFF

var autoTextfield;
var userAutoInput;
var databaseUpdateColumn;

// What happens when add button is clicked
$('#tech-add-btn').click(function () {
    addItem($(this));
});

// What happens when the Enter key is pressed
$('#tech-auto').keypress(function (event) {
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

// What happens when add button is clicked
$('#tech-save-btn').click(function () {
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

    // Finn ID
    var id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);

    var navn = $('#tech-name').val();

    // Update
    $.ajax({
        url: '/EditProject/AddTechProfile',
        data: { Id: id, Navn: navn, Elementer: newValue },
        type: 'POST',
        success: function (data) {
            $('#tech-group').html('');
            $('#tech-name').val('');
            $('#tech-auto').val('');
            alert('temp alert: la til ny profil');
        }
    });
});
