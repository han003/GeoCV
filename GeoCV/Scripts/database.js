$(document).ready(function () {

    $.ajax({
        url: '/Database/GetEditData',
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
                                '<td>' + element + '</td>' +
                                '<td>' + katalog + '</td>' +
                                '<td><a class="del-link">Slett</a></td>' +
                            '</tr>';

            });

            $('tbody').html(template);

            $("#edit-elem-load").addClass('hidden');
            $("table").removeClass('hidden');
        }
    });

});

$('#filter-txt').keyup(function () {

    $("#edit-elem-load").removeClass('hidden');
    $("table").addClass('hidden');

    var filter = $('#filter-txt').val();

    $.ajax({
        url: '/Database/FilterElements',
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
                                '<td>' + element + '</td>' +
                                '<td>' + katalog + '</td>' +
                                '<td><a class="del-link">Slett</a></td>' +
                            '</tr>';

            });

            $('tbody').html(template);

            $("#edit-elem-load").addClass('hidden');
            $("table").removeClass('hidden');
        }
    });

});

$(document).on('click', '.del-link', function () {

    console.log('Deleting..');

    var elementId = $(this).closest('tr').attr('id');
    var trElement = $(this).closest('tr');
    var tdElement = $(this).closest('td');

    tdElement.html('Sletter <i class="fa fa-spinner fa-pulse"></i>');

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

$(document).on('click', 'tr', function () {

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

// Close modal if Esc is pressed
$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '27' && $('#editModal').is(":visible")) {
        $('#editModal').modal('hide');
    }
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