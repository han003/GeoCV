$(document).ready(function () {

    // Sorter valgt tabell
    $('#database-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#database-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#database-tabell th i'), function (index, value) {

            if (index == kolonneIndex) {
                $(this).removeClass('hidden');
                $(this).removeClass('fa-sort-desc');
                $(this).removeClass('fa-sort-asc');

                if (sorteringRetning == 'asc') {
                    $(this).addClass('fa-sort-asc');
                } else {
                    $(this).addClass('fa-sort-desc');
                }

            } else {
                $(this).addClass('hidden');
            }

        });
    });

});

$('button').click(function () {
    $(this).blur();
});

$('#filter-txt').keyup(function () {
    filter();
});

$('#filter-group li').on('click', function () {

    if ($(this).hasClass('list-group-item-success'))
    {
        $(this).removeClass('list-group-item-success');
    }
    else
    {
        $(this).addClass('list-group-item-success');
    }
    
    filter();
});

function filter() {

    // Fjern alle filter klasser
    $('#database-tabell tbody').children('tr').removeClass('filter');

    // Hent tekst som er skrevet inn som filter
    var filterTekst = $('#filter-txt').val().toLowerCase();

    var antallCheckboxer = 0;

    // Gå gjennom hver checkbox for å se om den er valgt
    $('#filter-group li.list-group-item-success').each(function (index, element) {

        // Navn på valgt katalog
        var checkKatalog = $(this).html();

        $('#database-tabell tbody tr').each(function () {

            // Sjekk om katalogen er valgt eller ikke
            if ($(this).data('katalog').indexOf(checkKatalog) >= 0) {
                // Inneholder, så vis
                $(this).addClass('filter');
            }
        });

        antallCheckboxer++;
    });

    if (antallCheckboxer == 0) {
        // Ingen checkboxer valgt, så vis filter på alle
        $('#database-tabell tbody').children('tr').addClass('filter');
    }

    $('#database-tabell tbody tr').each(function () {

        // Element tekst
        var elementTekst = $(this).data('element').toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if ((elementTekst.indexOf(filterTekst) >= 0) && $(this).hasClass('filter')) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });

}

$(document).on('click', '.slett-element-btn', function () {

    var elementId = $(this).data('elementid');
    var panel = $(this).closest('.panel');
    var tabellTr = $('#database-tabell tr[data-elementid="' + elementId + '"]');

    console.log('Id: ' + elementId);

    tabellTr.remove();
    panel.remove();

    $.ajax({
        url: '/Database/SlettElement',
        data: { Id: elementId },
        type: 'POST',
        success: function () {



        }
    });

});

$(document).on('click', '#database-tabell tbody tr', function () {

    // Valgt element
    var valgtElement = $(this).data('elementid');

    console.log(valgtElement);

    // For hvert panel som har klassen og rett prosjekt id (bare ett panel)
    $.each($('.element-panel[data-elementid="' + valgtElement + '"]'), function (index, value) {
        $(this).removeClass('hidden');
    });
    // For hvert panel som har klassen men ikke rett prosjekt id (alle de andre)
    $.each($('.element-panel[data-elementid!="' + valgtElement + '"]'), function (index, value) {
        $(this).addClass('hidden');
    });

});