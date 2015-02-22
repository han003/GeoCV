$('#current-project-check').change(function () {

    if ($(this).is(':checked')) {
        $('#select-project-year-to').addClass('hidden');
    } else {
        $('#select-project-year-to').removeClass('hidden');
    }

});