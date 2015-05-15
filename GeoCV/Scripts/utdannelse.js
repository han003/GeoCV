$(document).ready(function () {

    // Sorter valgt tabell
    $('#education-table').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#education-table').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#education-table thead th i'), function (index, value) {

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

$('#legg-til-utdannelse-btn').click(function () {
    $('#legg-til-utdannelse-modal').modal();
});

$(document).on('click', '.slett-utdannelse', function () {
    $('#slett-utdannelse-navn').html($(this).data('utdannelsestudiested'));
    $('#slett-id').val($(this).data('utdannelseid'))
    $('#slett-utdannelse-modal').modal();
});

$('#education-table tbody tr .rediger-blyant').click(function () {
    $('#rediger-utdannelse-modal').data('utdannelseid', $(this).data('utdannelseid'));
    $('#rediger-studiested').val($(this).data('utdannelsestudiested'));
    $('#rediger-beskrivelse').val($(this).data('utdannelsebeskrivelse'));
    $('#rediger-fra').val($(this).data('utdannelsefra'));
    $('#rediger-til').val($(this).data('utdannelsetil'));
    $('#rediger-form #Id').val($(this).data('utdannelseid'));
    $('#rediger-utdannelse-modal').modal();
});