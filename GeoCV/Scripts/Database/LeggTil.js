$('#katalog-panel ul li').click(function () {

    $.each($('#katalog-panel ul li'), function (index, value) {
        $(this).removeClass('list-group-item-info');
    });

    $(this).addClass('list-group-item-info');

});

$('#new-item-txt').keyup(function (event) {
    if (event.keyCode == 13) {
        leggTilIKatalog()
    }
});

$('#add-item-btn').click(function () {
    leggTilIKatalog();
});

function leggTilIKatalog() {
    $('#new-item-txt').focus();

    var nyttElement = $('#new-item-txt').val();
    var valgtKatalog = $('#katalog-panel ul li.list-group-item-info').data('katalog');

    console.log('Legg til ' + nyttElement + ' i ' + valgtKatalog);

    $.ajax({
        url: '/Database/LeggTilElement',
        data: { NyttElement: nyttElement, Katalog: valgtKatalog },
        type: 'POST',
        beforeSend: function () {

            $('#new-item-txt').val('');

        },
        success: function (nyttElementId) {

            console.log('Lagt til');

            $('#historikk-ingen').addClass('hidden');
            $('#historikk-panel .panel-body').prepend('<div><span class="label label-info">' + nyttElement + '</span> lagt til i <span class="label label-info">' + valgtKatalog + '</span> <span class="label label-danger historikk-angre" data-elementid="' + nyttElementId + '">Angre</span></div>');

        }
    });
}

$(document).on('click', '.historikk-angre', function(){

    var elementId = $(this).data('elementid');

    $(this).closest('div').remove();

    console.log(elementId);

    $.ajax({
        url: '/Database/SlettElement',
        data: { Id: elementId },
        type: 'POST',
        beforeSend: function () {

        },
        success: function (nyttElementId) {

        }
    });

});

