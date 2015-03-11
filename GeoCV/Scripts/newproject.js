$('#new-project-btn').click(function () {

    var customer = $('#project-customer').val();
    var name = $('#project-name').val();
    var description = $('#project-description').val();

    $.ajax({
        url: '/NewProject/AddNewProject',
        data: { Kunde: customer, Navn: name, Beskrivelse: description },
        type: 'POST',
        beforeSend: function(){
            $('#new-project-btn').html('<i class="fa fa-spinner fa-pulse">');
        },
        success: function () {
            $('#project-customer').val('');
            $('#project-name').val('');
            $('#project-description').val('');
            $('#new-project-btn').html('Opprett prosjekt');

            var link = '<a href="#">Her</a>';
            $('.alert').html('<strong>' + name + ' lagt til!<br></strong> Klikk ' + link + ' for å legge til tekniske profiler og redigere prosjektet');
            $('.alert-group').removeClass('hidden');
        }
    });

});