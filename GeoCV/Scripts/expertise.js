// Globale variabler
var dbOppdateringsKolonne;

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

// What happens when add button is clicked
$('body').on('click', '.add-item-btn', function () {

    // Variabler
    var id = $(this).data('id');
    var katalog = $(this).data('katalog');
    var nyttElement = $(this).data('element');

    // Legg til html
    $('#' + katalog + '-bruker-tabell tbody').append('<tr>' +
                                                     '<td class="col-lg-5">' + nyttElement + '</td>' +
                                                     '<td class="col-lg-1"><i data-katalog="' + katalog + '" data-id="' + id + '" data-element="' + nyttElement + '" class="fa fa-minus-square fa-lg remove-item-btn"></i></td>' +
                                                     '</tr>');

    // Oppdater
    oppdaterDatabase(katalog);

});

// What happens when removing a button
$('body').on('click', '.remove-item-btn', function () {

    var katalog = $(this).data('katalog');
    $(this).closest('tr').remove();

    oppdaterDatabase(katalog);
});

function oppdaterDatabase(katalog) {

    // Tom variabel for å holde teksten
    var newValue = '';

    // Gå gjennom alle radene og legg IDene i en string
    $('#' + katalog + '-bruker-tabell tbody tr i').each(function () {
        var id = $(this).data('id');
        newValue += id + ';';
    });

    // Fjern siste ';' fra stringen
    newValue = newValue.substring(0, newValue.length - 1);

    console.log('Table: ' + katalog);
    console.log('Value: ' + newValue);

    // Update
    $.post('/Expertise/Update', { Update: katalog, Value: newValue });
}