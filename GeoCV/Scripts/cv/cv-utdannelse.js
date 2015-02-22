$('#education-add-btn').click(function () {
    // Variables
    var description = $('#education-text').val();
    console.log('Description: ' + description);

    var from = $('#education-select-from').val();
    console.log('From: ' + from);

    var to = $('#education-select-to').val();
    console.log('To: ' + to);

    var template = '<div class="container">' +
                        '<div class="panel panel-info col-lg-10 col-lg-offset-1 added-panel">' +
                            '<div class="panel-heading">' + from + ' - ' + to + '</div>' +
                            '<div class="panel-body">' +
                                '<div>' + description + '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>';

    // Append div
    $('#education-group').append(template);

    // Update
    $.post('/Cv/AddUtdannelse', { Beskrivelse: description, Fra: from, Til: to });

    // Cleanup
    $('#education-text').val('');
});