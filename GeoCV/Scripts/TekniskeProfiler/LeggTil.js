$('#velg-prosjekt-panel .list-group-item').click(function () {

    var prosjektId = $(this).data('prosjektid');
    console.log(prosjektId);

    if (!$(this).hasClass('valgt-li')) {

        // Fjern klasse før vi setter den på den <li>en som ble valgt
        $.each($('#velg-prosjekt-panel ul .valgt-li'), function (index, value) {
            $(this).removeClass('valgt-li');
        });

        // Sett på
        $(this).addClass('valgt-li');

        // Vis eller skjul => ikonet
        $.each($('#velg-prosjekt-panel ul i[data-prosjektid!="' + prosjektId + '"]'), function (index, value) {
            $(this).addClass('hidden');
        });
        $.each($('#velg-prosjekt-panel ul i[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
            $(this).removeClass('hidden');
        });

        // Vis eller skjul relevante paneler
        $.each($('#valgt-prosjekt-kolonne .panel[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
            $(this).removeClass('hidden');
        });
        $.each($('#valgt-prosjekt-kolonne .panel[data-prosjektid!="' + prosjektId + '"]'), function (index, value) {
            $(this).addClass('hidden');
        });
    }
    
});

$('.ny-profil-navn-txt').keyup(function (event) {
    if (event.keyCode == 13) {
        var prosjektId = $(this).data('prosjektid');
        var prosjektNavn = $(this).data('prosjektnavn');
        var nyProfilNavn = $(this).val();
        console.log(prosjektId + ': ' + nyProfilNavn);
        lagreProfil(prosjektId, prosjektNavn, nyProfilNavn);
    }
});


$('.ny-profil-lagre-btn').click(function () {
    var prosjektId = $(this).data('prosjektid');
    var prosjektNavn = $(this).data('prosjektnavn');
    var nyProfilNavn = $(this).closest('.form-horizontal').find('input').val();

    console.log(prosjektId + ': ' + nyProfilNavn);
    lagreProfil(prosjektId, prosjektNavn, nyProfilNavn);
});

function lagreProfil(prosjektId, prosjektNavn, nyProfilNavn) {
    $.ajax({
        url: '/TekniskeProfiler/LeggTilProfil',
        data: { ProsjektId: prosjektId, NyProfilNavn: nyProfilNavn },
        type: 'POST',
        beforeSend: function () {

            $('.ny-teknisk-profil-loader').removeClass('hidden');

        },
        success: function (tekniskProfilId) {
            console.log('Lagt til');

            // Tilbakestill tekstfelt
            $('#nytt-prosjekt-panel input').val('');

            // Skjul loader
            $('.ny-teknisk-profil-loader').addClass('hidden');

            $('#suksess-panel').removeClass('hidden');
            $('#profil-navn').html(nyProfilNavn);
            $('#prosjekt-navn').html(prosjektNavn);
            $('#suksess-panel a').attr('href', '/TekniskeProfiler/?ProsjektId=' + prosjektId);
            $('.ny-profil-navn-txt').val('');

            var liTemplate = '<li class="list-group-item">' +
                                '<div>' + nyProfilNavn + '</div>' +
                                '<span class="label label-primary">' +
                                    '<a href="/TekniskeProfiler/?ProsjektId=' + prosjektId + '?TekniskId=' + tekniskProfilId + '">Rediger</a>' +
                                '</span>' +
                                '<span class="label label-danger slett-label" data-tekniskid="' + tekniskProfilId + '">Slett</span>' +
                            '</li>';

            $('#valgt-prosjekt-kolonne .panel[data-prosjektid="' + prosjektId + '"] ul').append(liTemplate);

            $('#valgt-prosjekt-kolonne .eksisterende-profiler-panel[data-prosjektid="' + prosjektId + '"] .panel-body').remove();
        }
    });
}

$('#skjul-suksess-panel').click(function () {
    $('#suksess-panel').addClass('hidden');
});

$(document).on('click', '.slett-label', function () {

    var liElement = $(this).closest('li');
    var tekniskId = $(this).data('tekniskid');

    console.log(tekniskId);

    $.ajax({
        url: '/TekniskeProfiler/SlettProfil',
        data: { TekniskId: tekniskId },
        type: 'POST',
        beforeSend: function () {

            liElement.remove();

        },
        success: function () {

        }
    });
});