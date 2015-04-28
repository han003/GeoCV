$(document).ready(function () {

    // Sorter valgt tabell
    $('#work-table').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#work-table').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#work-table th i'), function (index, value) {

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

$('#work-add-btn').click(function () {

    $(this).blur();

    // Hent variabler
    var arbeidsplass = $('#text-work-place').val();
    var stilling = $('#select-work-role').val();
    var beskrivelse = $('#text-work-description').val();
    var fra = $('#work-select-from').val();
    var til = $('#work-select-to').val();
    var nåværende = ($('#nåværende-radio-btn label.active input').val() === 'true');

    console.log(stilling);

    $.ajax({
        url: '/Work/AddNewWork',
        data: { Arbeidsplass: arbeidsplass, Stilling: stilling, Beskrivelse: beskrivelse, Nåværende: nåværende, Fra: fra, Til: til },
        type: 'POST',
        beforeSend: function () {
            $('#work-add-btn').html('Legger til ny arbeidserfaring <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function (id) {

            // Tilbakestill ting
            $('#text-work-place, #text-work-role, #text-work-description').val('');
            $('#nåværende-radio-btn label:first').addClass('active');
            $('#nåværende-radio-btn label:last').removeClass('active');
            $('#work-select-to').closest('.form-group').removeClass('hidden');
            $('#work-select-from').val($('#work-select-from option:first').val());
            $('#work-select-to').val($('#work-select-to option:first').val());
            $('#work-add-btn').html('Legg til arbeidserfaring');

            console.log(id);

            // Markup for å legge til utdannelsen i tabellen
            var template = '<tr id="' + id + '">' +
                                '<td data-id="' + id + '" data-stilling="' + stilling + '" data-kolonne="Arbeidsplass" data-verdi="' + arbeidsplass + '" class="update-td col-lg-2">' + arbeidsplass + '</td>' +
                                '<td data-id="' + id + '" data-stilling="' + stilling + '" data-kolonne="Stilling" data-verdi="' + stilling + '" class="update-td col-lg-2">' + stilling + '</td>' +
                                '<td data-id="' + id + '" data-stilling="' + stilling + '" data-kolonne="Beskrivelse" data-verdi="' + beskrivelse + '" class="update-td col-lg-2">' + beskrivelse + '</td>' +
                                '<td data-id="' + id + '" data-stilling="' + stilling + '" data-kolonne="Fra" data-verdi="' + fra + '" class="update-td col-lg-2">' + fra + '</td>' +
                                '<td data-id="' + id + '" data-stilling="' + stilling + '" data-kolonne="Til" data-verdi="' + ((nåværende) ? 'Nåværende' : til) + '" class="update-td col-lg-2">' + ((nåværende) ? 'Nåværende' : til) + '</td>' +
                                '<td><a data-id="' + id + '" class="del-link col-lg-2">Slett</a></td>' +
                            '</tr>';

            // Finn nåværende hvis den eksisterer og bytt ut teksten med året som er nå
            $('tbody td:contains("Nåværende")').html(new Date().getFullYear());

            // Legg til
            if (nåværende) {
                $('tbody').prepend(template);
            } else {
                $('tbody').append(template);
            }
            
        }
    });

});

$(document).on('click', '.del-link', function () {

    var elementId = $(this).data('id');
    var trElement = $(this).closest('tr');
    var tdElement = $(this).closest('td');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Work/DeleteElement',
        data: { Id: elementId },
        type: 'POST',
        beforeSend: function(){
            tdElement.html('Sletter <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function () {
            trElement.remove();
        }
    });

});

$('#editModal').on('shown.bs.modal', function (e) {
    $('#edit-txt').focus();
})

$(document).on('click', '.update-td', function () {

    // Skjul alle elementer i modalen
    $('#edit-txt, #modal-select, #nåværendeTekst, #editLoader').addClass('hidden');

    // Vis OK knappen i tilfellet den er skjult
    $('#editmodalAddItem').removeClass('hidden');

    // Finn indexen til tden som ble valgt
    var tdIndex = $(this).index();

    // IDen til det som skal oppdateres
    var elementId = $(this).data('id');

    // Hent nåverende verdi
    var editVal = $(this).data('verdi');

    // Hent arbeidsplass
    var stilling = $(this).data('stilling');

    // Hent det som skal endres i databasen fra tablehead
    var kolonne = $(this).data('kolonne');

    console.log(elementId + ' - ' + editVal + ' - ' + kolonne);

    // Oppdater header teksten i modalen
    $('#myModalLabel').html('Rediger ' + kolonne.toLowerCase());

    // Lagre ID, index og kolonne til videre bruk
    $('#editModal').data('elementId', elementId);
    $('#editModal').data('dbKolonne', kolonne);
    $('#editModal').data('tdIndex', tdIndex);

    // Vis relevante felter i modalen
    if (editVal === 'Nåværende') {
        $('#nåværendeTekst').removeClass('hidden').html('<strong>' + stilling + '</strong> er satt som din nåværende stilling og dato kan ikke endres.<br />Vennligst legg til en ny stilling som nåværende for å endre.');
        $('#editmodalAddItem').addClass('hidden');

    } else if (kolonne == 'Fra' || kolonne == 'Til') {
        $('#modal-select').removeClass('hidden');
        $('#modal-select').val(editVal);
    }
    else if (kolonne != 'Fra' || kolonne != 'Til') {
        $('#edit-txt').removeClass('hidden');
        $('#edit-txt').val(editVal);
    }

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
