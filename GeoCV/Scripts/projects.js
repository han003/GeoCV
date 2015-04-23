$(document).ready(function () {

    // Sorter valgt tabell
    $('#prosjekt-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#prosjekt-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#prosjekt-tabell th i'), function (index, value) {
            console.log(index);
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

    getProsjekter();
});

$('#prosjekt-filter').keyup(function () {
    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#prosjekt-tabell tbody tr').each(function () {

        // Prosjekt tekst
        var prosjektTekst = $(this).find('a').html().toLowerCase();

        // Kunde tekst
        var kundeTekst = $(this).children('td').html().toLowerCase();

        // Beskrivelse tekst
        var beskrivelseTekst = $(this).children('td').next().next().html().toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (prosjektTekst.indexOf(filterTekst) >= 0 || kundeTekst.indexOf(filterTekst) >= 0 || beskrivelseTekst.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis

            console.log(prosjektTekst + kundeTekst + beskrivelseTekst + ': ' + filterTekst);

            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

function getProsjekter() {
    $('#edit-elem-load').removeClass('hidden');
    $('table').addClass('hidden');

    $.ajax({
        url: '/Projects/getProsjekter',
        type: 'GET',
        success: function (data) {
            console.log(data);

            var template = '';

            $.each(data, function (index, value) {

                var prosjektId = value['ProsjektId'];
                var prosjektKunde = value['Kunde'];
                var prosjektNavn = value['Navn'];
                var prosjektBeskrivelse = value['Beskrivelse'];
                var prosjektFra = value['Fra'];
                var prosjektTil = value['Til'];

                var linkText = '/EditProject/Index/' + prosjektId;

                // For valg av tekst å bruke
                template += '<tr id="' + prosjektId + '">' +
                                '<td class="col-lg-3">' + prosjektKunde + '</td>' +
                                '<td class="col-lg-3"><a href="' + linkText + '">' + prosjektNavn + '</a></td>' +
                                '<td class="col-lg-3">' + prosjektBeskrivelse + '</td>' +
                                '<td class="col-lg-1"><a class="del-link">Slett</a></td>' +
                            '</tr>';
            });

            $('tbody').html(template);

            $('#edit-elem-load').addClass('hidden');
            $('table').removeClass('hidden');
        }
    });
}

$(document).on('click', '.del-link', function () {

    // Prosjekt navn
    var prosjektNavn = $(this).closest('tr').children('td').children('a').html();
    console.log(prosjektNavn);

    // Prosjekt id
    var prosjektId = $(this).closest('tr').attr('id');
    $('body').data('prosjektId', prosjektId);
    console.log(prosjektId);

    $('#prosjekt-slett-etikett').html(prosjektNavn);

    $('#slettModal').modal();

});

$('#slett-prosjekt-btn').click(function () {

    var prosjektId = $('body').data('prosjektId');

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

            $('#nytt-prosjekt-legg-til-btn').html('Legger til.. <i class="fa fa-spinner fa-spin"></i>');

        },
        success: function () {
            console.log('Lagt til');

            // Tilbakestill tekstfelt
            $('#ny-kunde-txt').val('');
            $('#ny-prosjektnavn-txt').val('');
            $('#ny-beskrivelse-txt').val('');

            // Fjern loading animasjon
            $('#new-element-row').removeClass('hidden');
            $('#new-element-loading').addClass('hidden');

            // Endre tekst
            $('#edit-elem-load').html('Oppdaterer prosjekter.. <i class="fa fa-spinner fa-spin"></i>');
            $('#nytt-prosjekt-legg-til-btn').html('Legg til nytt prosjekt');

            // Oppdater prosjekter
            getProsjekter();
        }
    });
});

