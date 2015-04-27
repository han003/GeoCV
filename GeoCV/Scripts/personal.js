// Globale variabler
var dbOppdateringsKolonne;

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
    finnDbKolonne($(this));
    addItem($(this));
});

function finnDbKolonne(element) {
    dbOppdateringsKolonne = element.closest('table').attr('id');
    dbOppdateringsKolonne = dbOppdateringsKolonne.substring(0, dbOppdateringsKolonne.indexOf('-'));

    console.log('Katalog: ' + dbOppdateringsKolonne);
}

// What happens when removing a button
$('body').on('click', '.remove-item-btn', function () {
    finnDbKolonne($(this));

    $(this).closest('tr').remove();

    updateDatabase();
});

function addItem(element) {

    if (!isDuplicate()) {
        // Hent navnet på det som skal legges til
        var nyVerdi = element.closest('td').prev().html();
        var elementId = element.closest('tr').attr('id');

        console.log('Legg til: ' + nyVerdi + '|' + elementId);

        $('#' + dbOppdateringsKolonne + '-bruker-tabell tbody').append('<tr id="' + elementId + '">' +
                                                                       '<td class="col-lg-5">' + nyVerdi + '</td>' +
                                                                       '<td class="col-lg-1"><i class="fa fa-minus-square fa-lg remove-item-btn"></i></td>' +
                                                                       '</tr>');

        // Oppdater databasen
        updateDatabase();
    }

    // Fjern teksten fra input
    $('#' + dbOppdateringsKolonne + '-auto').val('');
}

function updateDatabase() {

    // Tom variabel for å holde teksten
    var newValue = '';

    // Gå gjennom alle radene og legg IDene i en string
    $('#' + dbOppdateringsKolonne + '-bruker-tabell tbody tr').each(function () {
        var idstring = $(this).attr('id');
        var id = idstring.substring(idstring.indexOf('-') + 1);
        newValue += id + ';';
    });

    // Fjern siste ';' fra stringen
    newValue = newValue.substring(0, newValue.length - 1);

    console.log('Table: ' + dbOppdateringsKolonne);
    console.log('Value: ' + newValue);

    // Update
    $.post('/Personal/Update', { Update: dbOppdateringsKolonne, Value: newValue });
}

function isDuplicate() {

    return false;

}
