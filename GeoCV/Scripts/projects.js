$(document).ready(function () {
    getProsjekter();
});

$('#prosjekt-filter').keyup(function () {
    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#prosjekt-tabell tbody tr').each(function () {

        // Prosjekt tekst
        var prosjektTekst = $(this).children('td').children('a').html().toLowerCase();
        console.log('Prosjekt: ' + prosjektTekst);

        // Kunde tekst
        var kundeTekst = $(this).children('td').next().html().toLowerCase();

        // Beskrivelse tekst
        var beskrivelseTekst = $(this).children('td').next().next().html().toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (prosjektTekst.indexOf(filterTekst) >= 0 || kundeTekst.indexOf(filterTekst) >= 0 || beskrivelseTekst.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
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
                                '<td class="col-lg-2"><a href="' + linkText + '">' + prosjektNavn + '</a></td>' +
                                '<td class="col-lg-3">' + prosjektKunde + '</td>' +
                                '<td class="col-lg-3">' + prosjektBeskrivelse + '</td>' +
                                '<td class="col-lg-1">' + prosjektFra + '</td>' +
                                '<td class="col-lg-1">' + prosjektTil + '</td>' +
                                '<td><a class="del-link col-lg-2">Slett</a></td>' +
                            '</tr>';
            });

            $('tbody').html(template);

            $('#edit-elem-load').addClass('hidden');
            $('table').removeClass('hidden');
        }
    });
}

$(document).on('click', '.del-link', function () {

    console.log('Deleting..');

    var elementId = $(this).closest('tr').attr('id');
    var trElement = $(this).closest('tr');
    var tdElement = $(this).closest('td');

    tdElement.html('Sletter <i class="fa fa-circle-o-notch fa-spin"></i>');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Projects/SlettProsjekt',
        data: { Id: elementId },
        type: 'POST',
        success: function () {
            console.log('Success');

            trElement.remove();
        }
    });

});


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

            $('#nytt-prosjekt-legg-til-btn').html('Legger til nytt prosjekt.. <i class="fa fa-circle-o-notch fa-spin"></i>');

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
            $('#edit-elem-load').html('Oppdaterer prosjekter.. <i class="fa fa-circle-o-notch fa-spin"></i>');
            $('#nytt-prosjekt-legg-til-btn').html('Legg til nytt prosjekt');

            // Oppdater prosjekter
            getProsjekter();
        }
    });
});


// Går til redigeringssiden for valgt prosjekt
$(document).on('asd', 'tbody tr', function () {
    console.log($(this).attr('href'));
    window.location.replace($(this).attr('href'));
    return false;
});

