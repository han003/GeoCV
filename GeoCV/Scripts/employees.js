$(document).ready(function () {

    $.ajax({
        url: '/Employees/GetEmployees',
        dataType: 'json',
        type: 'GET',
        success: function (data) {

            console.log(data);

            var template = '';

            $.each(data, function (index, value) {

                var fornavn = (value['Fornavn'] != null) ? value['Fornavn'] : '';
                var mellomnavn = (value['Mellomnavn'] != null) ? value['Mellomnavn'] : '';
                var etternavn = (value['Etternavn'] != null) ? value['Etternavn'] : '';
                var id = value['CVVersjonId'];
                var aktiv = value['Aktiv'];
                
                // For valg av tekst å bruke
                var aktivTekst = (aktiv) ? '<td>Aktiv (<a class="deactivate-link">Deaktiver</a>)</td>' : '<td>Ikke Aktiv (<a class="activate-link">Aktiver</a>)</td>';

                template += '<tr id="' + id + '">' +
                                '<td>' + fornavn + '</td>' +
                                '<td>' + mellomnavn + '</td>' +
                                '<td>' + etternavn + '</td>' +
                                aktivTekst +
                                '<td><a href="ChangeUser/' + id + '">Rediger bruker</a></td>' +
                            '</tr>';

            });

            $('tbody').html(template);

            $("#employee-load").addClass('hidden');
            $("table").removeClass('hidden');
        }
    });

});


$('#ny-ansatt-btn').click(function () {

    var Fornavn = $('#Fornavn-txt').val();
    var Etternavn = $('#Etternavn-txt').val();
    var Epost = $('#Epost-txt').val();
    var Passord = $('#Passord-txt').val();
    var Rolle = $('#role-select').val();

    console.log('Rolle: ' + Rolle);

    $.ajax({
        url: '/Employees/NewEmployee',
        data: { Fornavn: Fornavn, Etternavn: Etternavn, Epost: Epost, Passord: Passord, Rolle: Rolle },
        type: 'POST',
        success: function (data) {
            console.log('Success');
        }
    });

});

$(document).on('click', '.deactivate-link', function () {

    console.log('Deactivating..');

    var userId = $(this).closest('tr').attr('id');
    var tdElement = $(this).closest('td');

    tdElement.html('Deaktiverer <i class="fa fa-circle-o-notch fa-spin"></i>');

    console.log('Id: ' + userId);

    $.ajax({
        url: '/Employees/Deactivate',
        data: { Id: userId },
        type: 'POST',
        success: function () {
            console.log('Success');

            tdElement.html('Ikke Aktiv (<a class="activate-link">Aktiver</a>)')
        }
    });

});

$(document).on('click', '.activate-link', function () {

    console.log('Activating..');

    var userId = $(this).closest('tr').attr('id');
    var tdElement = $(this).closest('td');

    tdElement.html('Aktiverer <i class="fa fa-circle-o-notch fa-spin"></i>');

    console.log('Id: ' + userId);

    $.ajax({
        url: '/Employees/Activate',
        data: { Id: userId },
        type: 'POST',
        success: function () {
            console.log('Success');

            tdElement.html('Aktiv (<a class="deactivate-link">Deaktiver</a>)');
        }
    });
    
});