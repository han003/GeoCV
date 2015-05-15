$.fn.bootstrapSwitch.defaults.onText = 'Vis';
$.fn.bootstrapSwitch.defaults.offText = 'Skjul';

$('.vis-avslutt-switch').bootstrapSwitch();

$(document).ready(function () {

    // Sorter valgt tabell
    $('#prosjekt-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#prosjekt-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#prosjekt-tabell thead th i'), function (index, value) {

            if (index == kolonneIndex) {
                $(this).removeClass('hidden fa-sort-desc fa-sort-asc');

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

$('input[name="avsluttede-prosjekter-checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
    console.log(this); // DOM element
    console.log(event); // jQuery event
    console.log(state); // true | false

    if (state) {
        $('tr.text-danger').removeClass('hidden');
    } else {
        $('tr.text-danger').addClass('hidden');
    }
});

$('button').click(function () {
    $(this).blur();
});

$('#prosjekt-filter').keyup(function () {
    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#prosjekt-tabell tbody tr').each(function () {

        // Prosjekt tekst
        var prosjektNavn = $(this).data('navn').toLowerCase();

        // Kunde tekst
        var prosjektKunde = $(this).data('kunde').toLowerCase();

        // Beskrivelse tekst
        var prosjektBeskrivelse = $(this).data('beskrivelse').toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (prosjektNavn.indexOf(filterTekst) >= 0 || prosjektKunde.indexOf(filterTekst) >= 0 || prosjektBeskrivelse.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

$(document).on('click', '.slett-prosjekt', function () {
    $('#slett-prosjekt-navn').html($(this).data('prosjektnavn'));
    $('#slett-id').val($(this).data('prosjektid'))
    $('#slett-prosjekt-modal').modal();
});

$('.rediger-blyant').click(function () {

    $('#rediger-prosjektnavn').val($(this).data('prosjektnavn'));
    $('#rediger-beskrivelse').val($(this).data('prosjektbeskrivelse'));
    $('#rediger-kunde').val($(this).data('prosjektkunde'));
    $('#Id').val($(this).data('prosjektid'));

    $('#rediger-prosjekt-modal').modal();

});

$('#legg-til-prosjekt-btn').click(function () {
    $('#legg-til-prosjekt-modal').modal();
});