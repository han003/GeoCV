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

$(document).on('click', '.del-link', function () {

    console.log('Deleting..');

    var elementId = $(this).closest('tr').attr('id');
    var trElement = $(this).closest('tr');
    var tdElement = $(this).closest('td');

    tdElement.html('Sletter <i class="fa fa-spinner fa-spin"></i>');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Education/DeleteElement',
        data: { Id: elementId },
        type: 'POST',
        success: function () {
            console.log('Success');

            trElement.remove();
        }
    });

});

$('#editModal').on('shown.bs.modal', function (e) {
    $('#edit-txt').focus();
})

$(document).on('click', '.update-td', function () {

    // Velg td elementet
    var tdElement = $(this);

    // Hent td index
    var tdIndex = tdElement.index();

    // Velg tr elementet
    var trElement = tdElement.closest('tr');

    // IDen til det som skal oppdateres
    var elementId = trElement.attr('id');

    // Hent nåverende verdi
    var editVal = tdElement.html();

    // Hent det som skal endres i databasen fra tablehead
    var kolonneHtml = $('#education-table').find('th').eq(tdIndex).html();
    var kolonne = kolonneHtml.substring(0, kolonneHtml.indexOf('<'));

    console.log(elementId + ' - ' + editVal + ' - ' + kolonne);

    // Oppdater header teksten i modalen
    $('#myModalLabel').html('Rediger ' + kolonne.toLowerCase());

    // Lagre ID, index og kolonne til videre bruk
    $('#editModal').data('elementId', elementId);
    $('#editModal').data('dbKolonne', kolonne);
    $('#editModal').data('tdIndex', tdIndex);

    // Skjul og vis relevant felt i modalen
    if (kolonne.trim() == 'Fra' || kolonne.trim() == 'Til') {
        $('#edit-txt').addClass('hidden');
        $('#modal-select').removeClass('hidden');
        $('#modal-select').val(editVal);
    } else {
        $('#modal-select').addClass('hidden');
        $('#edit-txt').removeClass('hidden');
        $('#edit-txt').val(editVal);
    }

    // Skjul loading animasjonen
    $('#editLoader').addClass('hidden');

    // Åpne modalen
    $('#editModal').modal();
});

// Hva som skjer når man presser 'Enter' og modalen er synlig
$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13' && $('#editModal').is(":visible")) {
        changeElement();
    }
});

// Hva som skjer når man klikker på endre knappen i modalen
$('#editmodalAddItem').click(function () {
    changeElement();
});

function changeElement() {

    // Hent ID og kolonne for oppdatering
    var elementId = $('#editModal').data('elementId');
    var kolonne = $('#editModal').data('dbKolonne');

    // Hent den nye verdien
    var value = '';
    if (kolonne.trim() == 'Fra' || kolonne.trim() == 'Til') {
        value = $('#modal-select').val();
    } else {
        value = $('#edit-txt').val();
    }

    // Hent tr elementet som ble valgt
    var trElement = $('#' + elementId);

    console.log('Ny tekst: ' + value + '(' + elementId + ')');

    $.ajax({
        url: '/Education/ChangeElement',
        data: { Id: elementId, NewValue: value, Kolonne: kolonne },
        type: 'POST',
        beforeSend: function () {

            // Skjul felter
            $('#edit-txt').addClass('hidden');
            $('#modal-select').addClass('hidden');

            // Vis loading animasjon
            $('#editLoader').removeClass('hidden');


        },
        success: function () {
            console.log('Changed');

            // Hent indexen til tden som ble redigert
            var tdIndex = $('#editModal').data('tdIndex');
            var oppdatertTd = trElement.children('td:nth-child(' + (tdIndex + 1) + ')');

            // Endre verdien i td elementet som ble valgt
            oppdatertTd.html(value);

            // Fortell tabell sorteringen at verdien er endret
            oppdatertTd.updateSortVal(value);

            // Skjul modalen
            $('#editModal').modal('hide');
        }
    });
}