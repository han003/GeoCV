$(document).ready(function () {
    refreshTable();
});

$('#work-add-btn').click(function () {

    // Hent variabler
    var workplace = $('#text-work-place').val();
    var role = $('#text-work-role').val();
    var description = $('#text-work-description').val();
    var from = $('#work-select-from').val();
    var to = $('#work-select-to').val();

    $.ajax({
        url: '/Work/AddNewWork',
        data: { Arbeidsplass: workplace, Stilling: role, Beskrivelse: description, Fra: from, Til: to },
        type: 'POST',
        beforeSend: function () {
            $('#work-add-btn').html('<i class="fa fa-spinner fa-pulse"></i>');
        },
        success: function (data) {

            // Tilbakestill ting
            $('#text-work-place').val('');
            $('#text-work-role').val('');
            $('#text-work-description').val('');
            $('#work-select-from').val($('#work-select-from option:first').val());
            $('#work-select-to').val($('#work-select-to option:first').val());
            $('#work-add-btn').html('Legg til arbeidserfaring');

            refreshTable();
        }
    });

});

function refreshTable() {
    $('table').addClass('hidden');
    $('#elem-load').removeClass('hidden');

    $.ajax({
        url: '/Work/GetArbeidserfaring',
        type: 'GET',
        success: function (data) {
            console.log(data);

            var template = '';

            $.each(data, function (index, value) {

                var jobbId = value['ArbeidserfaringId'];
                var arbeidsplass = value['Arbeidsplass'];
                var stilling = value['Stilling'];
                var beskrivelse = value['Beskrivelse'];
                var fra = value['Fra'];
                var til = value['Til'];

                // For valg av tekst å bruke
                template += '<tr id="' + jobbId + '">' +
                                '<td class="update-td col-lg-2">' + arbeidsplass + '</td>' +
                                '<td class="update-td col-lg-2">' + stilling + '</td>' +
                                '<td class="update-td col-lg-4">' + beskrivelse + '</td>' +
                                '<td class="update-td col-lg-1">' + fra + '</td>' +
                                '<td class="update-td col-lg-1">' + til + '</td>' +
                                '<td><a class="del-link col-lg-2">Slett</a></td>' +
                            '</tr>';

            });

            $('tbody').html(template);
            $('#elem-load').addClass('hidden');
            $('table').removeClass('hidden');
        }
    });
}

$(document).on('click', '.del-link', function () {

    console.log('Deleting..');

    var elementId = $(this).closest('tr').attr('id');
    var trElement = $(this).closest('tr');
    var tdElement = $(this).closest('td');

    tdElement.html('Sletter <i class="fa fa-spinner fa-pulse"></i>');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Work/DeleteElement',
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
    var kolonne = $('#work-table').find('th').eq(tdIndex).html();

    console.log(elementId + ' - ' + editVal + ' - ' + kolonne);

    // Oppdater header teksten i modalen
    $('#myModalLabel').html('Rediger ' + kolonne.toLowerCase());

    // Lagre ID, index og kolonne til videre bruk
    $('#editModal').data('elementId', elementId);
    $('#editModal').data('dbKolonne', kolonne);
    $('#editModal').data('tdIndex', tdIndex);

    // Skjul og vis relevant felt i modalen
    if (kolonne == 'Fra' || kolonne == 'Til') {
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
    if (kolonne == 'Fra' || kolonne == 'Til') {
        value = $('#modal-select').val();
    } else {
        value = $('#edit-txt').val();
    }

    // Hent tr elementet som ble valgt
    var trElement = $('#' + elementId);

    console.log('Ny tekst: ' + value + '(' + elementId + ')');

    $.ajax({
        url: '/Work/ChangeElement',
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
            // Hent indexen til tden som ble redigert
            var tdIndex = $('#editModal').data('tdIndex');

            // Endre verdien i td elementet som ble valgt
            trElement.children('td:nth-child(' + (tdIndex + 1) + ')').html(value);

            // Skjul modalen
            $('#editModal').modal('hide');
        }
    });

}