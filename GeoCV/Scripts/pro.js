$(document).ready(function () {
    $.get('/Pro/GetProjects', function (data) {
        var autocomplete = $('#project-auto').typeahead();
        autocomplete.data('typeahead').source = data;
        autocomplete.data('json', data);
    }, 'json');
})

