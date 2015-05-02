$('.prosjekt-filter').keyup(function () {

    var filter = $(this).val().toLowerCase();

    $.each($('.list-group-item-text'), function (index, value) {

        var prosjektNavn = $(this).data('prosjektnavn').toLowerCase();

        if (prosjektNavn.indexOf(filter) >= 0) {
            $(this).parent().removeClass('hidden');
        } else {
            $(this).parent().addClass('hidden');
        }

    });

});

// Legg til prosjekt
$('.list-group-item-default').click(function () {

    var liElement = $(this);

    // Id
    var prosjektId = liElement.data('prosjektid');

    console.log(prosjektId)

    $.ajax({
        url: '/MineProsjekter/LeggTilProsjekt',
        data: { ProsjektId: prosjektId },
        type: 'POST',
        beforeSend: function () {
            liElement.removeClass('list-group-item-default');
            liElement.addClass('list-group-item-warning');
            liElement.children('i').replaceWith('<i class="fa fa-spinner fa-spin"></i>');
        },
        success: function () {
            liElement.removeClass('list-group-item-warning');
            liElement.addClass('list-group-item-success');
            liElement.children('i').replaceWith('<i class="fa fa-check"></i>');
        },
        error: function () {
            liElement.removeClass('list-group-item-warning');
            liElement.addClass('list-group-item-danger');
            liElement.children('i').replaceWith('<i class="fa fa-exclamation-circle"></i>');
        }
    });

});