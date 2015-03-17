$('#work-add-btn').click(function () {

    var workplace = $('#text-work-place').val();
    var role = $('#text-work-role').val();
    var description = $('#text-work-description').val();
    var from = $('#work-select-from').val();
    var to = $('#work-select-to').val();

    console.log('Arbeidsplass: ' + workplace);
    console.log('Stilling: ' + role);
    console.log('Beskrivelse: ' + description);
    console.log('Periode: ' + from + ' - ' + to);

    $.ajax({
        url: '/Work/AddNewWork',
        data: { Arbeidsplass: workplace, Stilling: role, Beskrivelse: description, Fra: from, Til: to },
        type: 'POST',
        success: function (data) {
            $('#text-work-place').val('');
            $('#text-work-role').val('');
            $('#text-work-description').val('');
            alert('temp alert box: lagt til!');
        }
    });

});