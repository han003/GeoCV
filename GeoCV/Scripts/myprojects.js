$(document).ready(function () {
    $.get('/MyProjects/GetEksisterendeProsjekter', function (data) {
        $(function () {
            $("#project-auto").typeahead({
                minLength: 0,
                source: data
            });
        });

        $("#project-load").addClass('hidden');
        $("#project-form").removeClass('hidden');

        $("#project-auto").data('json', data);
    }, 'json');
})

$('#project-add-btn').click(function () {

    var project = $('#project-auto').val();
    var role = $('#project-role-select').val();

    console.log('Prosjekt: ' + project);
    console.log('Rolle: ' + role);

    $.ajax({
        url: '/MyProjects/AddNewProject',
        data: { Prosjekt: project, Rolle: role },
        type: 'POST',
        success: function (data) {
            $('#project-auto').val('');
            alert('temp alert box: lagt til!');
        }
    });

});

