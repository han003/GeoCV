$(document).ready(function () {

    // Sorter valgt tabell
    $('#database-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#database-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#database-tabell th i'), function (index, value) {

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

$('button').click(function () {
    $(this).blur();
});

$('#filter-txt').keyup(function () {
    filter();
});

$('label > input[type=checkbox]').on('change', function () {
    filter();
});

function filter() {

    // Fjern alle filter klasser
    $('#database-tabell tbody').children('tr').removeClass('filter');

    // Hent tekst som er skrevet inn som filter
    var filterTekst = $('#filter-txt').val().toLowerCase();

    var antallCheckboxer = 0;

    // Gå gjennom hver checkbox for å se om den er valgt
    $('label > input[type="checkbox"]:checked').each(function (index, element) {

        var checkbox = $(element); // Checkboxen
        var label = checkbox.parent('label'); // Checkboxen sin label

        // Navn på valgt katalog
        var checkKatalog = checkbox.data('katalog');

        $('#database-tabell tbody tr').each(function () {

            // Sjekk om katalogen er valgt eller ikke
            if ($(this).data('katalog').indexOf(checkKatalog) >= 0) {
                // Inneholder, så vis
                $(this).addClass('filter');
            }
        });

        antallCheckboxer++;
    });

    if (antallCheckboxer == 0) {
        // Ingen checkboxer valgt, så vis filter på alle
        $('#database-tabell tbody').children('tr').addClass('filter');
    }

    $('#database-tabell tbody tr').each(function () {

        // Element tekst
        var elementTekst = $(this).data('element').toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if ((elementTekst.indexOf(filterTekst) >= 0) && $(this).hasClass('filter')) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });

}

$(document).on('click', '.del-link', function () {

    var elementId = $(this).data('id');
    var trElement = $(this).closest('tr');

    $(this).closest('td').html('Sletter <i class="fa fa-spinner fa-spin"></i>');

    console.log('Id: ' + elementId);

    $.ajax({
        url: '/Database/DeleteElement',
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

$(document).on('click', '.element-td', function () {

    var elementId = $(this).data('id');

    var editVal = $(this).data('element');

    console.log(elementId);

    $('#edit-txt').val(editVal);

    $('#editModal').data('elementId', elementId);

    $('#edit-txt').removeClass('hidden');
    $('#editLoader').addClass('hidden');

    $('#editModal').modal();
});

$(document).on('click', '#add-item-btn', function () {
    addNewItem();
});

// What happens when the Enter key is pressed
$('#new-item-txt').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        addNewItem();
    }
});

function addNewItem() {
    var element = $('#new-item-txt').val();
    var katalog = $('#katalog-select option:selected').attr('id');

    console.log('Legg til "' + element + '" i katalogen "' + katalog + '"');

    $.ajax({
        url: '/Database/AddElement',
        data: { NyttElement: element, Katalog: katalog },
        type: 'POST',
        beforeSend: function () {

            $('#new-element-row').addClass('hidden');
            $('#new-element-loading').removeClass('hidden');


        },
        success: function (id) {
            console.log('Lagt til');

            var template = '<tr id="' + id + '" data-katalog="' + katalog + '" data-element="' + element + '">' +
                               '<td class="element-td col-lg-5" data-id="' + id + '" data-element="' + element + '">' + element + '</td>' +
                               '<td class="katalog-td col-lg-5">' + katalog + '</td>' +
                               '<td><a data-id="' + id + '" class="del-link col-lg-2">Slett</a></td>' +
                           '</tr>';

            $('#database-tabell tbody').append(template);

            $('#new-item-txt').val('');
            $('#new-element-row').removeClass('hidden');
            $('#new-element-loading').addClass('hidden');

            $('#new-item-txt').focus();
        }
    });
}


// Enter key when the modal is visible
$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13' && $('#editModal').is(":visible")) {
        changeElement();
    }
});

// If modal button to add is clicked, add new item to database
$('#editmodalAddItem').click(function () {
    changeElement();
});

function changeElement() {

    var elementId = $('#editModal').data('elementId');
    var value = $('#edit-txt').val();
    var trElement = $('#' + elementId);
    var oppdatertTd = trElement.children('td:first');

    console.log('Ny tekst: ' + value + '(' + elementId + ')');

    $.ajax({
        url: '/Database/ChangeElement',
        data: { Id: elementId, NewValue: value },
        type: 'POST',
        beforeSend: function () {

            $('#edit-txt').addClass('hidden');
            $('#editLoader').removeClass('hidden');
            

        },
        success: function () {
            console.log('Changed');

            trElement.children('td:first').html(value);
            trElement.children('td:first').data('element', value);

            // Fortell tabell sorteringen at verdien er endret
            oppdatertTd.updateSortVal(value);

            $('#editModal').modal('hide');
        }
    });

}