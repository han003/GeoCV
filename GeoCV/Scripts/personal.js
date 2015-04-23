// Globale variabler
var dbOppdateringsKolonne;

$(document).ready(function () {
    getLanguages();
    getStillinger();
    GetNasjonaliteter();
})

$.datepicker.setDefaults($.datepicker.regional['nb']);

$('.update-txt').keyup(function () {
    var tableColumn = $(this).attr('id').substring(0, $(this).attr('id').indexOf('-'));
    var newValue = $(this).val();

    update(tableColumn, newValue);
});

function update(tableColumn, newValue) {
    // Oppdater surfer navn hvis man redigerer en annen bruker enn seg selv
    $('#shadowBrukerLink').html('Surfer som ' + $('#Fornavn-txt').val() + ' ' + $('#Etternavn-txt').val());

    $.ajax({
        url: '/Personal/Update',
        data: { Update: tableColumn, Value: newValue },
        type: 'POST',
        beforeSend: function () {

            // Vis oppdatering
            $('#' + tableColumn + '-label').removeClass('hidden');
        },
        success: function () {
            console.log('Lagt til');

            // Skjul oppdatering
            $('#' + tableColumn + '-label').addClass('hidden');
        }
    });
}

function getLanguages() {
    var katalog = "Språk";

    $.ajax({
        url: '/Personal/GetLanguages',
        data: { Katalog: katalog },
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            var elementArray = new Array();
            var idArray = new Array();

            $.each(data[1], function (index, value) {
                elementArray.push(value['Element']);
                idArray.push(value['ListeKatalogId']);

                // Lag tabell som viser alle elementer
                $('#' + katalog + '-alle-tabell tbody').append('<tr id="' + value['ListeKatalogId'] + '">' +
                                                               '<td class="col-lg-5">' + value['Element'] + '</td>' +
                                                               '<td class="col-lg-1">' + '<i class="fa fa-plus-square fa-lg add-item-btn"></i>' + '</td>' +
                                                               '</tr>');

                try {
                    $.each(data[0][0].split(';'), function (index, element) {
                        if (value['ListeKatalogId'] == element) {
                            // ID
                            var elementId = katalog + '-' + value['ListeKatalogId'];

                            // Lag tabell som viser bruker elementer
                            $('#' + katalog + '-bruker-tabell tbody').append('<tr id="' + elementId + '">' +
                                                                           '<td class="col-lg-5">' + value['Element'] + '</td>' +
                                                                           '<td class="col-lg-1"><i class="fa fa-minus-square fa-lg remove-item-btn"></i></td>' +
                                                                           '</tr>');
                        }
                    });
                }
                catch (e) {
                    console.log(e);
                }

            });

            $('#' + katalog + '-load').addClass('hidden');
            $('#' + katalog + '-form').removeClass('hidden');

            // Lagre arrayer
            $('#' + katalog + '-alle-tabell').data('elementer', elementArray);
            $('#' + katalog + '-alle-tabell').data('elementerId', idArray);
        }
    });
}

$('.table-filter').keyup(function () {

    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val();
    console.log('Filter: ' + filterTekst);

    // Finn riktig katalog
    var filterKatalog = $(this).attr('id').replace('-filter', '');
    console.log('Katalog: ' + filterKatalog);

    // Gå gjennom alle radene og legg IDene i en string
    $('#' + filterKatalog + '-alle-tabell tbody tr').each(function () {

        // Finn tekst
        var trElementTekst = $(this).children('td').html();

        // Sjekk om elementet inneholder filter teksten
        if (trElementTekst.toLowerCase().indexOf(filterTekst.toLowerCase()) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });

});

function getStillinger() {
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
}

function GetNasjonaliteter() {
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
}

// Stilling endring
$('#stillinger-select').on('change', function (e) {
    var optionSelected = $("option:selected", this).attr('id');
    var id = optionSelected.substring(optionSelected.indexOf('-')+1, optionSelected.length);

    console.log(id);

    // Update
    update('Stilling', id)
});

// Nasjonalitet endring
$('#nasjonalitet-select').on('change', function (e) {
    var optionSelected = $("option:selected", this).attr('id');
    var id = optionSelected.substring(optionSelected.indexOf('-') + 1, optionSelected.length);

    console.log(id);

    // Update
    update('Nasjonalitet', id)
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
    update('Fødselsår', birthday);
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
    update('StartDato', startDato);
});

function createDate(data) {
    var dag = data.split(' ')[0].split('.')[0];
    var mnd = data.split(' ')[0].split('.')[1] - 1;
    var år = data.split(' ')[0].split('.')[2];

    return new Date(år, mnd, dag);
}

//////////////////// INSERT NEW AUTO STUFF

// What happens when add button is clicked
$('body').on('click', '.add-item-btn', function () {
    finnDbKolonne($(this));
    addItem($(this));
});

function finnDbKolonne(element) {
    dbOppdateringsKolonne = element.closest('table').attr('id');
    dbOppdateringsKolonne = dbOppdateringsKolonne.substring(0, dbOppdateringsKolonne.indexOf('-'));

    console.log('Katalog: ' + dbOppdateringsKolonne);
}

// What happens when removing a button
$('body').on('click', '.remove-item-btn', function () {
    finnDbKolonne($(this));

    $(this).closest('tr').remove();

    updateDatabase();
});

function addItem(element) {

    if (!isDuplicate()) {
        // Hent navnet på det som skal legges til
        var nyVerdi = element.closest('td').prev().html();

        console.log('Legg til: ' + nyVerdi);

        // Legg til ny knapp med valgt element
        var elementArray = element.closest('table').data('elementer');
        var idArray = element.closest('table').data('elementerId');

        $.each(elementArray, function (index, value) {
            if (nyVerdi == value) {
                $('#' + dbOppdateringsKolonne + '-bruker-tabell tbody').append('<tr id="' + idArray[index] + '">' +
                                                                               '<td class="col-lg-5">' + value + '</td>' +
                                                                               '<td class="col-lg-1"><i class="fa fa-minus-square fa-lg remove-item-btn"></i></td>' +
                                                                               '</tr>');
            }
        });

        // Oppdater databasen
        updateDatabase();
    }

    // Fjern teksten fra input
    $('#' + dbOppdateringsKolonne + '-auto').val('');
}

function updateDatabase() {

    // Tom variabel for å holde teksten
    var newValue = '';

    // Gå gjennom alle radene og legg IDene i en string
    $('#' + dbOppdateringsKolonne + '-bruker-tabell tbody tr').each(function () {
        var idstring = $(this).attr('id');
        var id = idstring.substring(idstring.indexOf('-') + 1);
        newValue += id + ';';
    });

    // Fjern siste ';' fra stringen
    newValue = newValue.substring(0, newValue.length - 1);

    console.log('Table: ' + dbOppdateringsKolonne);
    console.log('Value: ' + newValue);

    // Update
    $.post('/Personal/Update', { Update: dbOppdateringsKolonne, Value: newValue });
}

function isDuplicate() {

    return false;

}
