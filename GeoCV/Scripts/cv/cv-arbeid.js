$('#work-add-btn').click(function () {

    // Variables
    var workplace = $('#text-work-place').val();
    console.log('Workplace: ' + workplace);

    var role = $('#text-work-role').val();
    console.log('Role: ' + role);

    var description = $('#text-work-description').val();
    console.log('Description: ' + description);

    var from = $('#work-select-from').val();
    console.log('From: ' + from);

    var to = $('#work-select-to').val();
    console.log('From: ' + from);

    // Template
    var template = '<div class="container">' +
                        '<div class="panel panel-info col-lg-10 col-lg-offset-1 added-panel">' +
                            '<div class="panel-heading">' + from + ' - ' + to + '</div>' +
                            '<div class="panel-body">' +
                                '<div><strong>' + workplace + '</strong> - ' + role + '</div>' +
                                '<div><small>' + description + '</small></div>' +
                            '</div>' +
                        '</div>' +
                    '</div>';

    // Append div
    $('#work-group').append(template);

    // Update
    $.post('/Cv/AddArbeid', { Arbeidsplass: workplace, Rolle: role, Beskrivelse: description, Fra: from, Til: to });

    // Cleanup
    $('#text-work-place').val('');
    $('#text-work-role').val('');
    $('#text-work-description').val('');
});

$('#current-job-check').change(function () {

    if ($(this).is(':checked')) {
        $('#select-job-year-to').addClass('hidden');
    } else {
        $('#select-job-year-to').removeClass('hidden');
    }

});