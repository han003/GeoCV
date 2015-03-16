$(document).ready(function () {
    $.get('/MyProjects/GetProjects', function (data) {
        var autocomplete = $('#project-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');
})

$('#project-add-btn').click(function () {

    var project = $('#project-auto').val();
    var role = $('#project-role-select').val();

    console.log('Prosjekt: ' + project);
    console.log('Rolle: ' + role);

    $.post('/MyProjects/AddNewProject', { Prosjekt: project, Rolle: role });

});

