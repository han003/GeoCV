$(document).ready(function () {

    $('.kalender').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd. MM yy'
    });

    // Sorter valgt tabell
    $('#alle-prosjekter-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#alle-prosjekter-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#alle-prosjekter-tabell th i'), function (index, value) {

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

    // Id
    var prosjektId = $(this).closest('tr').attr('id');
    console.log('Prosjekt: ' + prosjektId);

    // <td> element
    var tdElement = $(this).parent();

    $.ajax({
        url: '/MyProjects/LeggTilProsjekt',
        data: { ProsjektId: prosjektId },
        type: 'POST',
        beforeSend: function(){

            tdElement.html('<i class="fa fa-spinner fa-spin"></i>');

        },
        success: function () {

            tdElement.html('Lagt til');
        }
    });

});

$('.stilling-select').change(function () {

    // Id
    var selectId = $(this).attr('id');
    var prosjektId = selectId.substring(selectId.indexOf('-') + 1, selectId.length);

    // Ny stilling id
    var nyStilling = $(this).val();

    $.ajax({
        url: '/MyProjects/EndreStilling',
        data: { ProsjektId: prosjektId, NyStilling: nyStilling },
        type: 'POST',
        beforeSend: function () {

        },
        success: function () {

            console.log('Endret');

        }
    });

});

$('.kalender').change(function () {

    // Id
    var datoVelger = $(this).attr('id');
    var prosjektId = datoVelger.substring(datoVelger.lastIndexOf('-') + 1, datoVelger.length);

    // Ny stilling id
    var nyDato = $(this).val();

    // Fra eller til?
    var type = $(this).parent().prev().html().toLowerCase();

    console.log(type);

    $.ajax({
        url: '/MyProjects/EndreDato',
        data: { ProsjektId: prosjektId, NyDato: nyDato, Type: type },
        type: 'POST',
        beforeSend: function () {

        },
        success: function () {

            console.log('Endret');

        }
    });

});