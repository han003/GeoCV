$(document).ready(function () {

    // Sorter valgt tabell
    $('#database-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#database-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#database-tabell th i'), function (index, value) {
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

$('#filter-txt').keyup(function () {

    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#database-tabell tbody tr').each(function () {

        // Element tekst
        var elementTekst = $(this).children('td').html().toLowerCase();

        // Katalog tekst
        var katalogTekst = $(this).children('td').next().html().toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (elementTekst.indexOf(filterTekst) >= 0 || katalogTekst.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis

            console.log(elementTekst + katalogTekst + ': ' + filterTekst);

            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });

});

function refreshTable() {
    $('#edit-elem-load').removeClass('hidden');
    $('table').addClass('hidden');

    var filter = $('#filter-txt').val();

    $.ajax({
        url: '/Database/GetDatabase',
        data: { Filter: filter },
        type: 'GET',
        success: function (data) {
            console.log(data);

            var template = '';

            $.each(data, function (index, value) {

                var id = value['ListeKatalogId'];
                var element = value['Element'];
                var katalog = value['Katalog'];

                // For valg av tekst å bruke
                template += '<tr id="' + id + '">' +
                                '<td class="element-td col-lg-5">' + element + '</td>' +
                                '<td class="katalog-td col-lg-5">' + katalog + '</td>' +
                                '<td><a class="del-link col-lg-2">Slett</a></td>' +
                            '</tr>';

            });

            $('tbody').html(template);

            $('#edit-elem-load').addClass('hidden');
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

    var elementId = $(this).closest('tr').attr('id');

    var trElement = $(this).closest('tr');

    var editVal = trElement.children('td:first').html();

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
        success: function () {
            console.log('Lagt til');

            refreshTable();

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

            $('#editModal').modal('hide');
        }
    });

}