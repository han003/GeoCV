$('#nytt-prosjekt-legg-til-btn').click(function () {
    LeggTilProsjekt();
});

$('.nytt-prosjekt-input').keyup(function (event) {
    if (event.keyCode == 13) {
        LeggTilProsjekt();
    }
});

function LeggTilProsjekt() {
    var prosjektKunde = $('#ny-kunde-txt').val();
    var prosjektNavn = $('#ny-prosjektnavn-txt').val();
    var prosjektBeskrivelse = $('#ny-beskrivelse-txt').val();

    $.ajax({
        url: '/Prosjekter/LeggTilProsjekt',
        data: { Kunde: prosjektKunde, Navn: prosjektNavn, Beskrivelse: prosjektBeskrivelse },
        type: 'POST',
        beforeSend: function () {
            $('#nytt-prosjekt-legg-til-btn').html('Legger til prosjekt <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function (prosjektId) {
            console.log('Lagt til');

            // Tilbakestill tekstfelt
            $('#nytt-prosjekt-panel input').val('');

            // Endre tekst
            $('#nytt-prosjekt-legg-til-btn').html('Legg til nytt prosjekt');
        }
    });
}

