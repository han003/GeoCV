$('#new-project-btn').click(function () {

    var customer = $('#project-customer').val();
    var name = $('#project-name').val();
    var description = $('#project-description').val();

    $.ajax({
        url: '/NewProject/AddNewProject',
        data: { Kunde: customer, Navn: name, Beskrivelse: description },
        type: 'POST',
        beforeSend: function () {
            $('#new-project-btn').html('<i class="fa fa-spinner fa-pulse"></i>');
        },
        success: function (data) {
            console.log(data);

            $('#new-project-link').parent().removeClass('hidden');

            var name = $('#project-name').val();

            var link = 'http://geocv.azurewebsites.net/EditProject/Index/' + data;
            var html = '<a href="' + link + '">' + name + ' opprettet!<br />Klikk her for å legge til tekniske profiler, administrere eller endre ' + name + '</a>';

            $('#new-project-link').html(html);
            
            // Tilbakestiller tekstfelt og tekst
            $('#project-customer').val('');
            $('#project-name').val('');
            $('#project-description').val('');
            $('#new-project-btn').html('Opprett prosjekt');
        }
    });

});