$(document).ready(function () {

    // Popover
    $('[data-toggle="popover"]').popover();

    // Sorter valgt tabell
    $('#ansatt-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#ansatt-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#ansatt-tabell th i'), function (index, value) {

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

$('#ny-ansatt-btn').click(function () {

    var Fornavn = $('#Fornavn-txt').val();
    var Etternavn = $('#Etternavn-txt').val();
    var Epost = $('#Epost-txt').val();
    var Passord = $('#Passord-txt').val();
    var Rolle = $('#Rolle-select').val();

    console.log('Rolle: ' + Rolle);

    $.ajax({
        url: '/Ansatte/RegistrerNyAnsatt',
        data: { Fornavn: Fornavn, Etternavn: Etternavn, Epost: Epost, Passord: Passord, Rolle: Rolle },
        type: 'GET',
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

$(document).on('click', '.deactivate-link', function () {

    console.log('Deactivating..');

    var tdElement = $(this).parent('td');
    var userId = tdElement.data('id');

    console.log('Id: ' + userId);

    $.ajax({
        url: '/Ansatte/Deaktiver',
        data: { Id: userId },
        type: 'POST',
        beforeSend: function () {
            tdElement.html('Deaktiverer <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function () {
            console.log('Success');

            var tekst = 'Ikke Aktiv (<a class="activate-link">Aktiver</a>)';
            tdElement.html(tekst)
            tdElement.updateSortVal(tekst);
        }
    });

});

$(document).on('click', '.activate-link', function () {

    console.log('Activating..');

    var tdElement = $(this).parent('td');
    var userId = tdElement.data('id');

    tdElement.html('Aktiverer <i class="fa fa-spinner fa-spin"></i>');

    console.log('Id: ' + userId);

    $.ajax({
        url: '/Ansatte/Aktiver',
        data: { Id: userId },
        type: 'POST',
        success: function () {
            console.log('Success');

            tdElement.html('Aktiv (<a class="deactivate-link">Deaktiver</a>)');
        }
    });

});

$(document).on('click', '.del-link', function (event) {
    event.preventDefault();

    console.log('Deleting..');

    var elementId = $(this).data('id');
    var trElement = $(this).closest('tr');
    var tdElement = $(this).closest('td');

    tdElement.html('Sletter <i class="fa fa-spinner fa-spin"></i>');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Ansatte/SlettBruker',
        data: { Id: elementId },
        type: 'POST',
        success: function () {
            console.log('Success');

            trElement.remove();
        }
    });

});