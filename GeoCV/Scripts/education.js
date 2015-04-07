$('#education-add-btn').click(function () {

    var school = $('#school-text').val();
    var description = $('#education-text').val();
    var from = $('#education-select-from').val();
    var to = $('#education-select-to').val();

    console.log('School: ' + school);
    console.log('Description: ' + description);
    console.log('Period: ' + from + ' - ' + to);

    $.ajax({
        url: '/Education/AddNewEducation',
        data: { Skole: school, Beskrivelse: description, Fra: from, Til: to },
        type: 'POST',
        success: function (data) {
            $('#school-text').val('');
            $('#education-text').val('');
            alert('temp alert box: lagt til!');
        }
    });

});