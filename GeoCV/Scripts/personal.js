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

    console.log('Ny verdi: ' + newValue);

    $.post('/Personal/Update', { Update: tableColumn, Value: newValue });
}

function getLanguages() {
    $.get('/Personal/GetLanguages', function (data) {

        console.log(data);
        var språkarray = new Array();
        var idarray = new Array();

        $.each(data[1], function (index, value) {
            språkarray.push(value['Element']);
            idarray.push(value['ListeKatalogId']);

            $.each(data[0][0].split(';'), function (index, brukerspråk) {
                if (value['ListeKatalogId'] == brukerspråk) {
                    $('#Språk-group').append('<button id="språk-' + value['ListeKatalogId'] + '" type="button" class="btn btn-info added-btn" tabindex="-1">' + value['Element'] + '</button>');
                }

            });
        });

        $(function () {
            $("#Språk-auto").typeahead({
                minLength: 0,
                source: språkarray
            });
        });

        $("#Språk-load").addClass('hidden');
        $("#Språk-form").removeClass('hidden');

        $("#Språk-auto").data('element', språkarray);
        $("#Språk-auto").data('elementId', idarray);
    }, 'json');
}

$.ajax({
    url: '/Personal/GetStillinger',
    type: 'GET',
    success: function (alleStillinger) {

        $.ajax({
            url: '/Personal/GetValgtStilling',
            type: 'GET',
            success: function (valgtStilling) {

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

$.ajax({
    url: '/Personal/GetNasjonaliteter',
    type: 'GET',
    success: function (alleNasjonaliteter) {

        $.ajax({
            url: '/Personal/GetValgtNasjonalitet',
            type: 'GET',
            success: function (valgtNasjonalitet) {

                var template = '';
                var nasjonalitetIdMatch = false;

                $.each(alleNasjonaliteter, function (index, value) {

                    var nasjonalitet = value['Element'];
                    var nasjonalitetId = value['ListeKatalogId'];

                    if (valgtNasjonalitet == nasjonalitetId) {
                        nasjonalitetIdMatch = true;
                        template += '<option selected id="nasjonalitet-' + nasjonalitetId + '">' + nasjonalitet + '</option>';
                    } else {
                        template += '<option id="nasjonalitet-' + nasjonalitetId + '">' + nasjonalitet + '</option>';
                    }
                });

                if (!nasjonalitetIdMatch) {
                    template += '<option id="ingen-nasjonalitet" selected>Nasjonalitet ikke valgt</option>';
                }

                $('#nasjonalitet-select').html(template);
                $('#nasjonalitet-select').removeAttr('disabled');
            }
        });

    }
});

// Stilling endring
$('#stillinger-select').on('change', function (e) {
    var optionSelected = $("option:selected", this).attr('id');
    var id = optionSelected.substring(optionSelected.indexOf('-')+1, optionSelected.length);

    console.log(id);

    // Update
    $.post('/Personal/Update', { Update: 'Stilling', Value: id });
});

// Nasjonalitet endring
$('#nasjonalitet-select').on('change', function (e) {
    var optionSelected = $("option:selected", this).attr('id');
    var id = optionSelected.substring(optionSelected.indexOf('-') + 1, optionSelected.length);

    console.log(id);

    // Update
    $.post('/Personal/Update', { Update: 'Nasjonalitet', Value: id });
});

////////////  BURSDAG

$('#birthday-date, #geomatikk-start-date').datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'd MM yy'
});

$.get('/Personal/GetBursdag', function (data) {
    var Bursdag = createDate(data);

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

$.get('/Personal/GetStartDato', function (data) {
    var StartDato = createDate(data);

    console.log('Start Dato: ' + StartDato);
    $('#geomatikk-start-date').datepicker('setDate', StartDato);
}, 'json');

$('#geomatikk-start-date').on('change', function (e) {

    var startDato = $(this).val();

    console.log(startDato);

    // Update
    $.post('/Personal/UpdateStartDato', { Value: startDato });
});

function createDate(data) {
    var dag = data.split(' ')[0].split('.')[0];
    var mnd = data.split(' ')[0].split('.')[1] - 1;
    var år = data.split(' ')[0].split('.')[2];

    return new Date(år, mnd, dag);
}


//////////////////// INSERT NEW AUTO STUFF

var autoTextfield;
var userAutoInput;
var databaseUpdateColumn;

// Hva som skjer når man trykker på 'legg til' knappen
$('.add-item-btn').click(function () {
    addItem($(this));
});

// Hva som skjer når man trykker på enter når tekstfeltet har fokus
$('.add-item-auto').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        addItem($(this));
    }
});

// Hva som skjer når man klikker på en knapp for å fjerne den
$('body').on('click', '.added-btn', function () {
    databaseUpdateColumn = $(this).parent().attr('id');
    databaseUpdateColumn = databaseUpdateColumn.substring(0, databaseUpdateColumn.indexOf('-'));

    $(this).remove();
    updateDatabase();
});

function addItem(element) {
    databaseUpdateColumn = element.attr('id');
    databaseUpdateColumn = databaseUpdateColumn.substring(0, databaseUpdateColumn.indexOf('-'));

    if (isDuplicate()) {
        // Hent rett tekstfelt
        autoTextfield = $('#' + databaseUpdateColumn + '-auto');

        // Legg til ny knapp med valgt element
        appendNewLanguage(autoTextfield.val());

        // Oppdater databasen
        updateDatabase();
    }

    // Fjern teksten fra input
    $('#' + databaseUpdateColumn + '-auto').val('');
}

function appendNewLanguage(userAutoInput) {
    var språkarray = autoTextfield.data('element');
    var idarray = autoTextfield.data('elementId');

    $.each(språkarray, function (index, value) {
        if (userAutoInput == value) {
            $('#' + databaseUpdateColumn + '-group').append('<button id="språk-' + idarray[index] + '" type="button" class="btn btn-info added-btn" tabindex="-1">' + value + '</button>');
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
    $.post('/Personal/Update', { Update: databaseUpdateColumn, Value: newValue });
}

function isDuplicate() {

    // Variabel for å sjekke om det et duplikat
    var dupe = false;

    // Tom variabel for å holde teksten
    var newValue = '';
    var newId = '';

    // Gå gjennom alle knappene å legg IDene i en string
    $('#' + databaseUpdateColumn + '-group button').each(function () {
        var idstring = $(this).attr('id');
        var id = idstring.substring(idstring.indexOf('-') + 1);
        newValue += id + ';';
    });

    // Hent rett tekstfelt
    autoTextfield = $('#' + databaseUpdateColumn + '-auto');

    // Tekst fra input
    var userAutoInput = autoTextfield.val();

    // Arrayer
    var elementArray = autoTextfield.data('element');
    var idArray = autoTextfield.data('elementId');

    // Finn ID
    $.each(elementArray, function (index, value) {
        if (userAutoInput == value) {
            newId = idArray[index];
        }
    });

    if (newValue.indexOf(newId) == -1) {
        dupe = true;
    }

    return dupe;
}
