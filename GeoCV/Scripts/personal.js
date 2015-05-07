$.datepicker.setDefaults($.datepicker.regional['nb']);

$('.update-txt').keyup(function () {
    var tableColumn = $(this).attr('id').substring(0, $(this).attr('id').indexOf('-'));
    var newValue = $(this).val();

    update(tableColumn, newValue);
});

function update(tableColumn, newValue) {
    // Oppdater surfer navn hvis man redigerer en annen bruker enn seg selv
    $('#shadowBrukerLink').html('Surfer som ' + $('#Fornavn-txt').val() + ' ' + $('#Etternavn-txt').val());

    $.ajax({
        url: '/Personal/Update',
        data: { Update: tableColumn, Value: newValue },
        type: 'POST',
        beforeSend: function () {

            // Vis oppdatering
            $('#' + tableColumn + '-label').removeClass('hidden');
        },
        success: function () {
            console.log('Lagt til');

            // Skjul oppdatering
            $('#' + tableColumn + '-label').addClass('hidden');
        }
    });
}

$('.table-filter').keyup(function () {

    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val();
    console.log('Filter: ' + filterTekst);

    // Sjekk om error
    if (filterTekst != '' && $(this).closest('.form-group').hasClass('has-error')) {
        $(this).closest('.form-group').removeClass('has-error');
    }

    // Finn riktig katalog
    var filterKatalog = $(this).attr('id').replace('-filter', '');
    console.log('Katalog: ' + filterKatalog);

    // Gå gjennom alle radene og legg IDene i en string
    $('#' + filterKatalog + '-alle-tabell tbody tr').each(function () {

        // Finn tekst
        var trElementTekst = $(this).children('td').html();

        // Sjekk om elementet inneholder filter teksten
        if (trElementTekst.toLowerCase().indexOf(filterTekst.toLowerCase()) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });

});

// Stilling endring
$('#stillinger-select').change(function (){
    var id = $(this).val();

    console.log(id);

    // Update
    update('Stilling', id)
});

// Nasjonalitet endring
$('#nasjonalitet-select').change(function () {
    var id = $(this).val();

    console.log(id);

    // Update
    update('Nasjonalitet', id)
});

////////////  BURSDAG

$('#birthday-date, #geomatikk-start-date').datepicker({
    changeMonth: true,
    changeYear: true,
    dateFormat: 'dd. MM yy'
});

$('#birthday-date').on('change', function (e) {

    var birthday = $(this).val();

    console.log(birthday);

    // Update
    update('Fødselsår', birthday);
});

////////////  START DATO

$('#geomatikk-start-date').on('change', function (e) {

    var startDato = $(this).val();

    console.log(startDato);

    // Update
    update('StartDato', startDato);
});

function createDate(data) {
    var dag = data.split(' ')[0].split('.')[0];
    var mnd = data.split(' ')[0].split('.')[1] - 1;
    var år = data.split(' ')[0].split('.')[2];

    return new Date(år, mnd, dag);
}

//////////////////// INSERT NEW AUTO STUFF

// What happens when add button is clicked
$('body').on('click', '.add-item-btn', function () {
    
    // Variabler
    var tdElement = $(this).parent();
    var id = $(this).data('id');
    var katalog = $(this).data('katalog');
    var nyttElement = $(this).data('element');

    // Legg til html
    $('#' + katalog + '-bruker-tabell tbody').append('<tr>' +
                                                     '<td class="col-lg-5">' + nyttElement + '</td>' +
                                                     '<td class="col-lg-1"><i data-katalog="' + katalog + '" data-id="' + id + '" data-element="' + nyttElement + '" class="fa fa-minus-square fa-lg remove-item-btn"></i></td>' +
                                                     '</tr>');

    // Oppdater
    oppdaterDatabase(katalog, tdElement);

});

// What happens when removing a button
$('body').on('click', '.remove-item-btn', function () {
    
    var tdElement = $(this).parent();
    var katalog = $(this).data('katalog');
    $(this).closest('tr').remove();

    oppdaterDatabase(katalog, tdElement);

});

function oppdaterDatabase(katalog, tdElement) {

    // Tom variabel for å holde teksten
    var newValue = '';

    // Gå gjennom alle radene og legg IDene i en string
    $('#' + katalog + '-bruker-tabell tbody tr i').each(function () {
        var id = $(this).data('id');
        newValue += id + ';';
    });

    // Fjern siste ';' fra stringen
    newValue = newValue.substring(0, newValue.length - 1);

    console.log('Table: ' + katalog);
    console.log('Value: ' + newValue);

    // Update
    $.ajax({
        url: '/Personal/Update',
        data: { Update: katalog, Value: newValue },
        type: 'POST',
        beforeSend: function () {

            // Vis loading
            tdElement.html('<i class="fa fa-spinner fa-spin"></i>');

        },
        success: function (id) {

            tdElement.html('Lagt til');

        }
    });
}

$('.table-filter').keyup(function (event) {
    if (event.keyCode == 13) {
        var katalog = $(this).data('katalog');
        leggTilIDatabase(katalog);
    }
});

$('.legg-til-element-btn').click(function () {
    $(this).blur();
    var katalog = $(this).data('katalog');
    leggTilIDatabase(katalog);
});

function leggTilIDatabase(katalog) {
    var tekstBoks = $('#' + katalog + '-filter');
    var nyttElement = tekstBoks.val().trim();

    console.log(nyttElement + ' i ' + katalog);

    if (nyttElement != '') {
        // Endre tekst
        $('#modalLeggTilElement').html(nyttElement);
        $('#modalLeggTilKatalog').html(katalog);

        // Endre data i modalen sin legg til knapp
        $('#modal-LeggTil-btn').data('katalog', katalog);
        $('#modal-LeggTil-btn').data('element', nyttElement);

        // Vis modal
        $('#leggTilModal').modal();
    } else {
        tekstBoks.closest('.form-group').addClass('has-error');
    }
}

// Enter trykk mens modal er synlig
$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13' && $('#leggTilModal').is(':visible')) {
        modalLeggTil();
    }
});

$('#modal-LeggTil-btn').click(function () {
    modalLeggTil();
});

function modalLeggTil() {
    // Hent data
    var katalog = $('#modal-LeggTil-btn').data('katalog');
    var element = $('#modal-LeggTil-btn').data('element');

    $.ajax({
        url: '/Database/LeggTilElement',
        data: { NyttElement: element, Katalog: katalog },
        type: 'POST',
        beforeSend: function () {

            // Vis loading
            $('#modal-LeggTil-btn').html('<i class="fa fa-spinner fa-spin"></i>');

        },
        success: function (id) {

            // Gammel tekst
            $('#modal-LeggTil-btn').html('Legg til (Enter)');

            // Fjern modal
            $('#leggTilModal').modal('hide')

            // Legg til html
            $('#' + katalog + '-alle-tabell tbody').append('<tr>' +
                                                             '<td class="col-lg-5">' + element + '</td>' +
                                                             '<td class="col-lg-1"><i data-katalog="' + katalog + '" data-id="' + id + '" data-element="' + element + '" class="fa fa-plus-square fa-lg add-item-btn"></i></td>' +
                                                             '</tr>');
        }
    });
}