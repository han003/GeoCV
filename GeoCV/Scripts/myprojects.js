$(document).ready(function () {

    // Sorter valgt tabell
    $('#alle-prosjekter-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#alle-prosjekter-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#alle-prosjekter-tabell th i'), function (index, value) {
            console.log(index);
            if (index == kolonneIndex) {
                $(this).removeClass('hidden');
                $(this).removeClass('fa-sort-desc');
                $(this).removeClass('fa-sort-asc');

                if (sorteringRetning == 'asc') {
                    $(this).addClass('fa-sort-asc');
                } else {
                    $(this).addClass('fa-sort-desc');
                }

            } else {
                $(this).addClass('hidden');
            }

        });
    });
});

$(document).on('click', '.fa-plus-square', function () {

    $(this).removeClass('fa-plus-square').addClass('fa-check-square');

});

$('#prosjekt-fra, #prosjekt-til').datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'd MM yy'
});
