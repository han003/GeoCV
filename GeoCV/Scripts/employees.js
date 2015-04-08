$(document).ready(function () {

    $.ajax({
        url: '/Employees/GetEmployees',
        dataType: 'json',
        type: 'GET',
        success: function (data) {

            var template = '';

            $.each(data, function (index, value) {

                console.log(index + ': ' + value['Fornavn'] + ' ' + value['Etternavn']);

                var fornavn = (value['Fornavn'] != null) ? value['Fornavn'] : '';
                var mellomnavn = (value['Mellomnavn'] != null) ? value['Mellomnavn'] : '';
                var etternavn = (value['Etternavn'] != null) ? value['Etternavn'] : '';

                template += '<tr>' +
                                '<td>' + fornavn + '</td>' +
                                '<td>' + mellomnavn + '</td>' +
                                '<td>' + etternavn + '</td>' +
                                '<td>Aktiv</td>' +
                            '</tr>';

            });

            $('tbody').html(template);

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