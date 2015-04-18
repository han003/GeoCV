$(document).ready(function () {
    getProsjekter();
});

$('#prosjekt-filter').keyup(function () {
    getProsjekter();
});

function getProsjekter() {
    $('#edit-elem-load').removeClass('hidden');
    $('table').addClass('hidden');

    var filter = $('#prosjekt-filter').val();

    $.ajax({
        url: '/Projects/getProsjekter',
        data: { Filter: filter },
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
                template += '<tr>' +
                                '<td class="col-lg-3"><a href="' + linkText + '">' + prosjektNavn + '</a></td>' +
                                '<td class="col-lg-3">' + prosjektKunde + '</td>' +
                                '<td class="col-lg-4">' + prosjektBeskrivelse + '</td>' +
                                '<td class="col-lg-1">' + prosjektFra + '</td>' +
                                '<td class="col-lg-1">' + prosjektTil + '</td>' +
                            '</tr>';
            });

            $('tbody').html(template);

            $('#edit-elem-load').addClass('hidden');
            $('table').removeClass('hidden');
        }
    });
}


$('#nytt-prosjekt-legg-til-btn').click(function () {
    
    var prosjektKunde = $('#ny-kunde-txt').val();
    var prosjektNavn = $('#ny-prosjektnavn-txt').val();
    var prosjektBeskrivelse = $('#ny-beskrivelse-txt').val();

    $.ajax({
        url: '/Projects/LeggTilProsjekt',
        data: { Kunde: prosjektKunde, Navn: prosjektNavn, Beskrivelse: prosjektBeskrivelse },
        type: 'POST',
        beforeSend: function () {

            $('#new-element-row').addClass('hidden');
            $('#new-element-loading').removeClass('hidden');


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

            // Gå til første tab
            $('#prosjekt-tabs a:first').tab('show');

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

