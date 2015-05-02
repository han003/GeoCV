$(document).ready(function () {

    // Sorter valgt tabell
    $('#prosjekt-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#prosjekt-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#prosjekt-tabell th i'), function (index, value) {

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

$(document).on('click', '.del-link', function () {

    // Prosjekt navn
    var prosjektNavn = $(this).data('navn');
    console.log(prosjektNavn);

    // Prosjekt id
    var prosjektId = $(this).data('id');
    $('body').data('prosjektId', prosjektId);
    console.log(prosjektId);

    $('#prosjekt-slett-etikett').html(prosjektNavn);

    $('#slettModal').modal();

});

$('#slett-prosjekt-btn').click(function () {

    var prosjektId = $('body').data('prosjektId');
    var trElement = $('#' + prosjektId).remove();

    $.ajax({
        url: '/Projects/SlettProsjekt',
        data: { Id: prosjektId },
        type: 'POST',
        beforeSend: function(){

            $('#slettModal button').addClass('hidden');
            $('#slettModal i').removeClass('hidden');

        },
        success: function () {
            console.log('Success');
            $('#slettModal').modal('hide');
            $('#' + prosjektId).remove();
        }
    });
});

// Det som skjer når modalen er ferdig med skjule animasjonen
$('#slettModal').on('hidden.bs.modal', function () {
    $('#slettModal button').removeClass('hidden');
    $('#slettModal i').addClass('hidden');
})

$('#nytt-prosjekt-legg-til-btn').click(function () {
    
    $(this).blur();

    var prosjektKunde = $('#ny-kunde-txt').val();
    var prosjektNavn = $('#ny-prosjektnavn-txt').val();
    var prosjektBeskrivelse = $('#ny-beskrivelse-txt').val();

    $.ajax({
        url: '/Projects/LeggTilProsjekt',
        data: { Kunde: prosjektKunde, Navn: prosjektNavn, Beskrivelse: prosjektBeskrivelse },
        type: 'POST',
        beforeSend: function () {
            $('#nytt-prosjekt-legg-til-btn').html('Legger til prosjekt <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function (prosjektId) {
            console.log('Lagt til');

            // Tilbakestill tekstfelt
            $('#nytt-prosjekt-tab input').val('');

            // Fjern loading animasjon
            $('#new-element-row').removeClass('hidden');
            $('#new-element-loading').addClass('hidden');

            // Endre tekst
            $('#nytt-prosjekt-legg-til-btn').html('Legg til nytt prosjekt');

            // Legg til html
            var linkText = '/EditProject/Index/' + prosjektId;
            var template = '<tr id="' + prosjektId + '" data-id="' + prosjektId + '" data-kunde="' + prosjektKunde + '" data-navn="' + prosjektNavn + '" data-beskrivelse="' + prosjektBeskrivelse + '">' +
                                '<td class="col-lg-3">' + prosjektKunde + '</td>' +
                                '<td class="col-lg-3"><a href="' + linkText + '" target="_blank">' + prosjektNavn + '</a></td>' +
                                '<td class="col-lg-3">' + prosjektBeskrivelse + '</td>' +
                                '<td class="col-lg-1"><a class="del-link" data-id="' + prosjektId + '" data-navn="' + prosjektNavn + '">Slett</a></td>' +
                            '</tr>';

            $('#prosjekter-tab tbody').append(template);
        }
    });
});

