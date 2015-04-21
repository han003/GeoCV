$(document).on('click', '.fa-plus-square', function () {

    $(this).removeClass('fa-plus-square').addClass('fa-check-square');

});

$('#prosjekt-fra, #prosjekt-til').datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'd MM yy'
});
