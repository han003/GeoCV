$('#new-project-btn').click(function () {

    var customer = $('#project-customer').val();
    var name = $('#project-name').val();
    var description = $('#project-description').val();

    $.ajax({
        url: '/Projects/AddNewProject',
        data: { Kunde: customer, Navn: name, Beskrivelse: description },
        type: 'POST',
        beforeSend: function () {
            $('#new-project-btn').html('<i class="fa fa-spinner fa-pulse">');
        },
        success: function () {
            $('#project-customer').val('');
            $('#project-name').val('');
            $('#project-description').val('');
            $('#new-project-btn').html('Opprett prosjekt');

            $('tbody').prepend(' <tr>' +
                                    '<td>' + name + '</td>' +
                                    '<td>' + customer + '</td>' +
                                    '<td>' + description + '</td>' +
                                    '<td>' + (new Date).getFullYear() + '</td>' +
                                    '<td>9999</td>' +
                                '</tr>');
        }
    });

});