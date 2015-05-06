$(document).ready(function () {

    $('.kalender').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd. MM yy'
    });

});

$('.stilling-select').change(function () {

    // Id
    var prosjektId = $(this).data('prosjektid');

    // Ny stilling id
    var nyStilling = $(this).val();

    console.log(prosjektId + ': ' + nyStilling)

    $.ajax({
        url: '/MineProsjekter/EndreStilling',
        data: { ProsjektId: prosjektId, NyStilling: nyStilling },
        type: 'POST',
        beforeSend: function () {

        },
        success: function () {

            console.log('Endret');

        }
    });

});

$('.teknisk-profil-select').change(function () {

    // Id
    var prosjektId = $(this).data('prosjektid');

    // Ny stilling id
    var nyTekniskProfil = $(this).val();

    console.log(prosjektId + ': ' + nyTekniskProfil)

    $.ajax({
        url: '/MineProsjekter/EndreTekniskProfil',
        data: { ProsjektId: prosjektId, NyTekniskProfil: nyTekniskProfil },
        type: 'POST',
        beforeSend: function () {

            // Animasjon

        },
        success: function () {

            $.each($('.prosjekt-panel[data-prosjektid="' + prosjektId + '"] .well[data-tekniskprofilid="' + nyTekniskProfil + '"]'), function (index, value) {
                $(this).removeClass('hidden');
            });
            $.each($('.prosjekt-panel[data-prosjektid="' + prosjektId + '"] .well[data-tekniskprofilid!="' + nyTekniskProfil + '"]'), function (index, value) {
                $(this).addClass('hidden');
            });

        }
    });

});

$('.list-group-item').click(function () {

    var prosjektId = $(this).data('prosjektid');
    console.log(prosjektId);

    // Vis eller skjul => ikonet
    $.each($('#mine-prosjekter-panel ul i[data-prosjektid!="' + prosjektId + '"]'), function (index, value) {
        $(this).addClass('hidden');
    });
    $.each($('#mine-prosjekter-panel ul i[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
        $(this).removeClass('hidden');
    });

    // Vis paneler relatert til prosjektet som ble valgt
    $.each($('.prosjekt-panel[data-prosjektid!="' + prosjektId + '"]'), function (index, value) {
        $(this).addClass('hidden');
    });
    $.each($('.prosjekt-panel[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
        $(this).removeClass('hidden');
    });

});

$('.kalender').change(function () {

    // Id
    var prosjektId = $(this).data('prosjektid');

    // Ny stilling id
    var nyDato = $(this).val();

    // Fra eller til?
    var type = $(this).parent().prev().html().toLowerCase();

    console.log(type);

    $.ajax({
        url: '/MineProsjekter/EndreDato',
        data: { ProsjektId: prosjektId, NyDato: nyDato, Type: type },
        type: 'POST',
        beforeSend: function () {

        },
        success: function () {

            console.log('Endret');

        }
    });

});

$('.fjern-prosjekt-btn').click(function () {

    var prosjektId = $(this).data('prosjektid');

    $.ajax({
        url: '/MineProsjekter/FjernProsjekt',
        data: { ProsjektId: prosjektId },
        type: 'POST',
        beforeSend: function () {

        },
        success: function () {

            console.log('Endret');

        }
    });

});