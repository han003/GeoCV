$(document).ready(function () {

    // Sorter valgt tabell
    $('#work-table').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#work-table').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#work-table th i'), function (index, value) {
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

    refreshTable();
});

$('#work-add-btn').click(function () {

    $(this).blur();

    // Hent variabler
    var workplace = $('#text-work-place').val();
    var role = $('#text-work-role').val();
    var description = $('#text-work-description').val();
    var from = $('#work-select-from').val();
    var to = $('#work-select-to').val();
    var nåværende = ($('#nåværende-radio-btn label.active input').val() === 'true');

    $.ajax({
        url: '/Work/AddNewWork',
        data: { Arbeidsplass: workplace, Stilling: role, Beskrivelse: description, Nåværende: nåværende, Fra: from, Til: to },
        type: 'POST',
        beforeSend: function () {
            $('#work-add-btn').html('Legger til ny arbeidserfaring <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function (data) {

            // Tilbakestill ting
            $('#text-work-place').val('');
            $('#text-work-role').val('');
            $('#text-work-description').val('');
            $('#nåværende-radio-btn label:first').addClass('active');
            $('#nåværende-radio-btn label:last').removeClass('active');
            $('#work-select-to').closest('.form-group').removeClass('hidden');
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

            // Fjern html så vi kan legge til på nytt
            $('tbody').html('');

            $.each(data, function (index, value) {

                

                var jobbId = value['ArbeidserfaringId'];
                var arbeidsplass = value['Arbeidsplass'];
                var stilling = value['Stilling'];
                var nåværende = value['Nåværende'];
                var beskrivelse = value['Beskrivelse'];
                var fra = value['Fra'];
                var til = (nåværende) ? 'Nåværende' : value['Til'];

                // For valg av tekst å bruke
                var template = '<tr id="' + jobbId + '">' +
                                '<td class="update-td col-lg-2">' + arbeidsplass + '</td>' +
                                '<td class="update-td col-lg-2">' + stilling + '</td>' +
                                '<td class="update-td col-lg-4">' + beskrivelse + '</td>' +
                                '<td class="update-td col-lg-1">' + fra + '</td>' +
                                '<td class="update-td col-lg-1">' + til + '</td>' +
                                '<td><a class="del-link col-lg-2">Slett</a></td>' +
                            '</tr>';

                if (nåværende) {
                    $('tbody').prepend(template);
                } else {
                    $('tbody').append(template);
                }
            });

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

    tdElement.html('Sletter <i class="fa fa-spinner fa-spin"></i>');

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

    // Skjul alle elementer i modalen
    $('#edit-txt').addClass('hidden');
    $('#modal-select').addClass('hidden');
    $('#nåværendeTekst').addClass('hidden');

    // Vis OK knappen i tilfellet den er skjult
    $('#editmodalAddItem').removeClass('hidden');

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

    // Hent arbeidsplass
    var arbeidsplass = tdElement.parent().children('td').first().html();

    // Hent det som skal endres i databasen fra tablehead
    var kolonneHtml = $('#work-table').find('th').eq(tdIndex).html();
    var kolonne = kolonneHtml.substring(0, kolonneHtml.indexOf('<'));

    if (kolonne == '') {
        kolonne = kolonneHtml;
    }

    console.log(elementId + ' - ' + editVal + ' - ' + kolonne);

    // Oppdater header teksten i modalen
    $('#myModalLabel').html('Rediger ' + kolonne.toLowerCase());

    // Lagre ID, index og kolonne til videre bruk
    $('#editModal').data('elementId', elementId);
    $('#editModal').data('dbKolonne', kolonne);

    // Vis relevante felter i modalen
    if (editVal === 'Nåværende') {
        $('#nåværendeTekst').removeClass('hidden').html('<strong>' + arbeidsplass + '</strong> er satt som din nåværende stilling og dato kan ikke endres.<br />Vennligst legg til en ny stilling som nåværende for å endre.');
        $('#editmodalAddItem').addClass('hidden');

    } else if (kolonne == 'Fra' || kolonne == 'Til') {
        $('#modal-select').removeClass('hidden');
        $('#modal-select').val(editVal);
    }
    else if (kolonne != 'Fra' || kolonne != 'Til') {
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

// Fjern 'til' felt siden nåværende arbeidsplass er valgt
$(document).on('change', 'input:radio[id^="ja"]', function (event) {
    $('#work-select-to').closest('.form-group').addClass('hidden');
});

// Legg til 'til' felt siden nåværende arbeidsplass ikke er valgt
$(document).on('change', 'input:radio[id^="nei"]', function (event) {
    $('#work-select-to').closest('.form-group').removeClass('hidden');
});
