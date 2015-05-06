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

$('#education-add-btn').click(function () {

    var studiested = $('#school-text').val();
    var beskrivelse = $('#education-text').val();
    var fra = $('#education-select-from').val();
    var til = $('#education-select-to').val();

    $.ajax({
        url: '/Education/AddNewEducation',
        data: { Skole: studiested, Beskrivelse: beskrivelse, Fra: fra, Til: til },
        type: 'POST',
        beforeSend: function () {
            $('#education-add-btn').html('Legger til utdannelse <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function (data) {
            // Tilbakestill ting
            $('#school-text').val('');
            $('#education-text').val('');
            $('#education-select-from').val($('#education-select-from option:first').val());
            $('#education-select-to').val($('#education-select-to option:first').val());
            $('#education-add-btn').html('Legg til ny utdannelse');

            console.log(data);

            // Stokker om hvis datoene er i feil rekkefølge
            if (fra > til) {
                var nyFra = til;
                til = fra;
                fra = nyFra;
            }

            // Markup for å legge til utdannelsen i tabellen
            var template =  '<tr id="' + data + '">' +
                                '<td class="update-td col-lg-3">' + studiested + '</td>' +
                                '<td class="update-td col-lg-5">' + beskrivelse + '</td>' +
                                '<td class="update-td col-lg-1">' + fra + '</td>' +
                                '<td class="update-td col-lg-1">' + til + '</td>' +
                                '<td><a class="del-link col-lg-2">Slett</a></td>' +
                            '</tr>';

            $('tbody').append(template);
        }
    });

});

$(document).on('click', '.slett-utdannelse-btn', function () {

    var elementId = $(this).data('utdannelseid');
    var panel = $(this).closest('.panel');
    var tabellTr = $('#education-table tr[data-utdannelseid="' + elementId + '"]');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Education/DeleteEdlement',
        data: { Id: elementId },
        type: 'POST',
        success: function () {
            console.log('Success');

            tabellTr.remove();
            panel.remove();
        }
    });

});

$('#education-table tbody tr').click(function () {

    // Valgt utdannelse
    var valgtUtdannelseId = $(this).data('utdannelseid');

    console.log(valgtUtdannelseId);

    // For hvert panel som har klassen og rett prosjekt id (bare ett panel)
    $.each($('.utdannelse-panel[data-utdannelseid="' + valgtUtdannelseId + '"]'), function (index, value) {
        $(this).removeClass('hidden');
    });
    // For hvert panel som har klassen men ikke rett prosjekt id (alle de andre)
    $.each($('.utdannelse-panel[data-utdannelseid!="' + valgtUtdannelseId + '"]'), function (index, value) {
        $(this).addClass('hidden');
    });

});

$('.tekst-oppdaterings-input').keyup(function () {

    var panelLoading = $(this).closest('.panel').find('i');
    var elementId = $(this).data('utdannelseid');
    var value = $(this).val();
    var kolonne = $(this).data('kolonne');
    
    endreElement(elementId, value, kolonne, panelLoading);

});

$('.select-oppdaterings-input').change(function () {

    var panelLoading = $(this).closest('.panel').find('i');
    var elementId = $(this).data('utdannelseid');
    var value = $(this).val();
    var kolonne = $(this).data('kolonne');

    endreElement(elementId, value, kolonne, panelLoading);

});

function endreElement(elementId, value, kolonne, panelLoading) {

    console.log('Id: ' + elementId);
    console.log('Verdi: ' + value);
    console.log('Kolonne: ' + kolonne);

    $.ajax({
        url: '/Education/ChangeElement',
        data: { Id: elementId, NewValue: value, Kolonne: kolonne },
        type: 'POST',
        beforeSend: function () {
            panelLoading.removeClass('hidden');
        },
        success: function () {
            panelLoading.addClass('hidden');

            $('.update-td[data-utdannelseid="' + elementId + '"][data-kolonne="' + kolonne + '"]').html(value);
        }
    });
}