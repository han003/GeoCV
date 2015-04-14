$(document).ready(function () {
    getLanguages();
})

$.datepicker.setDefaults($.datepicker.regional['nb']);

$('.update-txt').keyup(function () {
    update($(this));
});

function update(element) {
    var tableColumn = element.attr('id').substring(0, element.attr('id').indexOf('-'));
    var newValue = element.val();

    console.log("Table: " + tableColumn);
    console.log("Value: " + newValue);

    $.post('/Personal/Update', { Update: tableColumn, Value: newValue });
}

function getLanguages() {
    $.get('/Personal/GetLanguages', function (data) {
        $(function () {
            $("#Språk-auto").typeahead({
                minLength: 0,
                source: data
            });
        });

        $("#Språk-load").addClass('hidden');
        $("#Språk-form").removeClass('hidden');

        $("#Språk-auto").data('json', data);
    }, 'json');
}

$.ajax({
    url: '/Personal/GetStillinger',
    type: 'GET',
    success: function (alleStillinger) {
        console.log(alleStillinger);

        $.ajax({
            url: '/Personal/GetValgtStilling',
            type: 'GET',
            success: function (valgtStilling) {
                console.log(valgtStilling);

                var template = '';
                var stillingMatch = false;

                $.each(alleStillinger, function (index, value) {

                    var stilling = value['Element'];
                    var stillingId = value['ListeKatalogId'];

                    if (valgtStilling == stillingId) {
                        stillingMatch = true;
                        template += '<option selected id="stilling-' + stillingId + '">' + stilling + '</option>';
                    } else {
                        template += '<option id="stilling-' + stillingId + '">' + stilling + '</option>';
                    }
                });

                if (!stillingMatch) {
                    template += '<option id="ingen-stilling" selected>Stilling ikke valgt</option>';
                }

                $('#stillinger-select').html(template);
                $('#stillinger-select').removeAttr('disabled');
            }
        });

    }
});

$('#stillinger-select').on('change', function (e) {
    var optionSelected = $("option:selected", this).attr('id');
    var id = optionSelected.substring(optionSelected.indexOf('-')+1, optionSelected.length);

    console.log(id);

    // Update
    $.post('/Personal/Update', { Update: 'Stilling', Value: id });
});

////////////  BURSDAG

$("#birthday-date").datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'd MM yy'
});

$.get('/Personal/GetBursdag', function (data) {
    var dag = data.split(' ')[0].split('.')[0];
    var mnd = data.split(' ')[0].split('.')[1] - 1;
    var år = data.split(' ')[0].split('.')[2];

    var Bursdag = new Date(år, mnd, dag);

    console.log('Bursdag: ' + Bursdag);
    $('#birthday-date').datepicker('setDate', Bursdag);
}, 'json');

$('#birthday-date').on('change', function (e) {

    var birthday = $(this).val();

    console.log(birthday);

    // Update
    $.post('/Personal/UpdateBirthdate', { Value: birthday });
});

////////////  START DATO

$("#geomatikk-start-date").datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'd MM yy'
});

$.get('/Personal/GetStartDato', function (data) {
    var dag = data.split(' ')[0].split('.')[0];
    var mnd = data.split(' ')[0].split('.')[1] - 1;
    var år = data.split(' ')[0].split('.')[2];

    var StartDato = new Date(år, mnd, dag);

    console.log('Start Dato: ' + StartDato);
    $('#geomatikk-start-date').datepicker('setDate', StartDato);
}, 'json');

$('#geomatikk-start-date').on('change', function (e) {

    var startDato = $(this).val();

    console.log(startDato);

    // Update
    $.post('/Personal/UpdateStartDato', { Value: startDato });
});


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
    $.post('/Personal/InsertItem', { Insert: databaseUpdateColumn, Value: userAutoInput }, function () {
        refreshLanguages();
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
    $.post('/Personal/Update', { Update: databaseUpdateColumn, Value: newValue });
}
