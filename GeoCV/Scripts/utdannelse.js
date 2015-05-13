$(document).ready(function () {

    // Sorter valgt tabell
    $('#education-table').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#education-table').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#education-table th i'), function (index, value) {

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

$(document).on('click', '.slett-utdannelse-btn', function () {

    var elementId = $(this).data('utdannelseid');
    var panel = $(this).closest('.panel');
    var tabellTr = $('#education-table tr[data-utdannelseid="' + elementId + '"]');

    tabellTr.remove();
    panel.remove();

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Education/DeleteElement',
        data: { Id: elementId },
        type: 'POST',
        success: function () {
            console.log('Success');
        }
    });

});

$('#education-table tbody tr').click(function () {

    $('#rediger-utdannelse-modal').data('utdannelseid', $(this).data('utdannelseid'));
    $('#rediger-studiested').val($(this).data('utdannelsestudiested'));
    $('#rediger-beskrivelse').val($(this).data('utdannelsebeskrivelse'));
    $('#rediger-fra').val($(this).data('utdannelsefra'));
    $('#rediger-til').val($(this).data('utdannelsetil'));
    $('#rediger-form #Id').val($(this).data('utdannelseid'));

    $('#rediger-utdannelse-modal').modal();

});