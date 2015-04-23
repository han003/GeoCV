// Globale variabler
var dbOppdateringsKolonne;

$(document).ready(function () {

    var kataloger = [
    'Programmeringsspråk',
    'Rammeverk',
    'WebTeknologier',
    'Databasesystemer',
    'Serverside',
    'Operativsystemer',
    'Annet'
    ];

    $.each(kataloger, function (index, element) {
        getKatalog(element);
    });
})

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

function getKatalog(katalog) {

    $.ajax({
        url: '/Expertise/GetElements',
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
    $.post('/Expertise/Update', { Update: dbOppdateringsKolonne, Value: newValue });
}

function isDuplicate() {

    return false;
    
}