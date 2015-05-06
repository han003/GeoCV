$('#ny-ansatt-btn').click(function () {

    var fornavn = $('#Fornavn-txt').val();
    var etternavn = $('#Etternavn-txt').val();
    var epost = $('#Epost-txt').val();
    var passord = $('#Passord-txt').val();
    var rolle = $('#Rolle-select').val();

    console.log('Rolle: ' + rolle);

    $.ajax({
        url: '/Ansatte/RegistrerNyAnsatt',
        data: { Fornavn: fornavn, Etternavn: etternavn, Epost: epost, Passord: passord, Rolle: rolle },
        type: 'POST',
        beforeSend: function () {

            $('#ny-ansatt-btn').html('Legger til <i class="fa fa-spinner fa-pulse"></i>');

        },
        success: function (data) {
            // Rydd opp
            $('input').val('');
            $('#Rolle-select').val('Bruker');
            $('#ny-ansatt-btn').html('Legg til');
        }
    });

});