$(document).ready(function () {

    var kataloger = [
    'Programmeringsspråk',
    'Rammeverk',
    'WebTeknologier',
    'Databasesystemer',
    'Serverside',
    'Operativsystemer'
    ];

    $.each(kataloger, function (index, element) {
        getKatalog(element);
    });
})

function getKatalog(katalog) {

    $.ajax({
        url: '/Expertise/GetElements',
        data: { Katalog: katalog },
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data)

            var elementArray = new Array();
            var idArray = new Array();

            $.each(data[1], function (index, value) {
                elementArray.push(value['Element']);
                idArray.push(value['ListeKatalogId']);

                $.each(data[0][0].split(';'), function (index, element) {
                    if (value['ListeKatalogId'] == element) {
                        $('#' + katalog + '-group').append('<button id="' + katalog + '-' + value['ListeKatalogId'] + '" type="button" class="btn btn-info added-btn" tabindex="-1">' + value['Element'] + '</button>');
                    }

                });
            });

            $(function () {
                $('#' + katalog + '-auto').typeahead({
                    minLength: 0,
                    source: elementArray
                });
            });

            $('#' + katalog + '-load').addClass('hidden');
            $('#' + katalog + '-form').removeClass('hidden');

            $('#' + katalog + '-auto').data('element', elementArray);
            $('#' + katalog + '-auto').data('elementId', idArray);
        }
    });
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

    // Hent rett tekstfelt
    autoTextfield = $('#' + databaseUpdateColumn + '-auto');

    // Legg til ny knapp med valgt element
    appendNewElement(autoTextfield.val());

    // Fjern teksten fra input
    $('#' + databaseUpdateColumn + '-auto').val('');

    // Oppdater databasen
    updateDatabase();
}

function appendNewElement(userAutoInput) {
    var elementArray = autoTextfield.data('element');
    var idArray = autoTextfield.data('elementId');

    $.each(elementArray, function (index, value) {
        if (userAutoInput == value) {
            $('#' + databaseUpdateColumn + '-group').append('<button id="' + databaseUpdateColumn + '-' + idArray[index] + '" type="button" class="btn btn-info added-btn" tabindex="-1">' + value + '</button>');
        }
    });
}

function updateDatabase() {

    // Tom variabel for å holde teksten
    var newValue = '';

    // Gå gjennom alle knappene å legg IDene i en string
    $('#' + databaseUpdateColumn + '-group button').each(function () {
        var idstring = $(this).attr('id');
        var id = idstring.substring(idstring.indexOf('-') + 1);
        newValue += id + ';';
    });

    // Fjern siste ';' fra stringen
    newValue = newValue.substring(0, newValue.length - 1);

    console.log('Table: ' + databaseUpdateColumn);
    console.log('Value: ' + newValue);

    // Update
    $.post('/Expertise/Update', { Update: databaseUpdateColumn, Value: newValue });
}