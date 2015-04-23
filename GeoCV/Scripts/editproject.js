// Globale variabler
var dbOppdateringsKolonne;

$(document).ready(function () {

    // Sorter valgt tabell
    $('#alle-elementer-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#alle-elementer-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#alle-elementer-tabell th i'), function (index, value) {
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

    getKatalogElementer();
    leggTilTekniskeProfiler();
});

$('.table-filter').keyup(function () {
    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#alle-elementer-tabell tbody tr').each(function () {

        // Element tekst
        var elementTekst = $(this).children('td').html().toLowerCase();

        // Katalog tekst
        var katalogTekst = $(this).children('td').next().html().toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (elementTekst.indexOf(filterTekst) >= 0 || katalogTekst.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

// Søppelbøtte ikonet er klikket på
$(document).on('click', '.fa-trash-o', function (e) {

    e.preventDefault();
    e.stopPropagation();

    // Profil id
    var id = $(this).closest('.panel-heading').next().attr('id');
    id = id.substring(0, id.indexOf('-'));
    $('body').data('id', id);

    // Profilnavn
    var navn = $(this).closest('.panel-heading').attr('id');
    navn = navn.substring(0, navn.indexOf('-'));

    console.log(id);

    $('#profil-slett-etikett').html(navn);

    $('#slettModal').modal();

});

$('#slett-profil-btn').click(function(){

    var id = $('body').data('id');

    console.log(id);

    $.ajax({
        url: '/EditProject/SlettProfil',
        data: { Id: id },
        type: 'POST',
        beforeSend: function(){

            $('#slettModal button').addClass('hidden');
            $('#slettModal i').removeClass('hidden');

        },
        success: function () {
            console.log('Success');

            $('#slettModal').modal('hide');
            
            $('#' + id + '-collapse').closest('.panel').remove();
        }
    });
});

// Det som skjer når modalen er ferdig med skjule animasjonen
$('#slettModal').on('hidden.bs.modal', function () {
    $('#slettModal button').removeClass('hidden');
    $('#slettModal i').addClass('hidden');
})

// Det som skjer når modalen er ferdig med skjule animasjonen
$('#endreModal').on('hidden.bs.modal', function () {
    $('#endreModal button').removeClass('hidden');
    $('#endreModal i').addClass('hidden');
})

// Blyant ikonet er klikket på
$(document).on('click', '.fa-pencil-square-o', function (e) {

    e.preventDefault();
    e.stopPropagation();

    // Profil id
    var id = $(this).closest('.panel-heading').next().attr('id');
    id = id.substring(0, id.indexOf('-'));
    $('body').data('id', id);

    // Profilnavn
    var navn = $(this).closest('.panel-heading').attr('id');
    navn = navn.substring(0, navn.indexOf('-'));

    console.log(id);

    $('#endre-txt').val(navn);

    $('#endreModal').modal();

});

$('#endre-profil-btn').click(function () {
    endreProfilNavn();
});

function endreProfilNavn() {
    var id = $('body').data('id');

    var navn = $('#endre-txt').val();

    console.log(id);

    $.ajax({
        url: '/EditProject/EndreProfilNavn',
        data: { ProfilId: id, Navn: navn },
        type: 'POST',
        beforeSend: function () {

            $('#endreModal button').addClass('hidden');
            $('#endreModal i').removeClass('hidden');

        },
        success: function () {
            console.log('Success');

            $('#endreModal').modal('hide');

            // Nåværende tekst i headeren
            var header = $('#' + id + '-collapse').prev().find('a');
            var headerTekst = header.html();

            // Endre id
            $('#' + id + '-collapse').prev().attr('id', navn + '-heading');

            // Endre teksten i headeren
            header.html(headerTekst.replace(headerTekst.substring(0, headerTekst.indexOf('<')), navn));
        }
    });
}

$('#endreModal').on('shown.bs.modal', function (e) {
    $('#endre-txt').focus();
})

// Trykker enter når endre modalen er synlig
$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13' && $('#endreModal').is(":visible")) {
        endreProfilNavn();
    }
});

function getKatalogElementer() {

    $.ajax({
        url: '/EditProject/GetKatalogElementer',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            console.log(data);

            var elementArray = new Array();
            var idArray = new Array();

            $.each(data, function (index, value) {
                elementArray.push(value['Element']);
                idArray.push(value['ListeKatalogId']);

                // Lag tabell som viser alle elementer
                $('#alle-elementer-tabell tbody').append('<tr id="' + value['ListeKatalogId'] + '">' +
                                                               '<td class="col-lg-2">' + value['Element'] + '</td>' +
                                                               '<td class="col-lg-3">' + value['Katalog'] + '</td>' +
                                                               '<td class="col-lg-1">' + '<i class="fa fa-plus-square fa-lg add-item-btn"></i>' + '</td>' +
                                                               '</tr>');
            });
        }
    });
}

$('.update-txt').keyup(function () {
    updateProjectInfo($(this));
});

function updateProjectInfo(element) {
    var tableColumn = element.attr('id').substring(0, element.attr('id').indexOf('-'));
    var newValue = element.val();

    // Finn ID
    var id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);

    $.ajax({
        url: '/EditProject/UpdateProjectInfo',
        data: { Id: id, Update: tableColumn, Value: newValue },
        type: 'POST',
        beforeSend: function () {
            // Vis oppdatering
            element.parent().next().removeClass('hidden');
        },
        success: function () {
            console.log('Lagt til');
            // Skjul oppdatering
            element.parent().next().addClass('hidden');
        }
    });
}

$('#ny-profil-lagre-btn').click(function () {
    nyProfil();
});

$('#ny-profil-navn-txt').keypress(function (event) {
    if (event.which == 13) {
        nyProfil();
    }
});

function nyProfil() {
    // Finn ID
    var id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);

    // Navn
    var navn = $('#ny-profil-navn-txt').val();

    console.log(id + ' - ' + navn);

    // Update
    $.ajax({
        url: '/EditProject/LeggTilProfil',
        data: { Id: id, Navn: navn },
        type: 'POST',
        beforeSend: function () {
            $('#ny-profil-lagre-btn').html('Lagrer <i class="fa fa-spinner fa-spin"></i>');
        },
        success: function (data) {

            console.log('lagt til profil id: ' + data);

            $('#ny-profil-navn-txt').val('');
            $('#ny-profil-lagre-btn').html('Legg til');

            leggTilNyttPanel(data, navn);
        }
    });
}

$(document).on('click', '.panel-heading', function () {

    var valgtElement = $(this).next();

    $.each($('.panel-collapse'), function (index, value) {

        if ($(this).attr('id') == valgtElement.attr('id')) {
            $(this).collapse('toggle');
        } else {
            if ($(this).hasClass('in')) {
                $(this).collapse('toggle');
            }

        }

    });

});

// What happens when remove button is clicked
$(document).on('click', '.remove-item-btn', function () {

    // Tabell
    var profilTabell = $(this).closest('tbody')

    // Fjern element
    var fjernId = $(this).closest('tr').remove();

    oppdaterDatabase(profilTabell);
});

// What happens when add button is clicked
$(document).on('click', '.add-item-btn', function () {

    // Hent katalogen til det som skal legges til
    var nyKatalog = $(this).closest('td').prev().html();
    console.log('Katalog: ' + nyKatalog);

    // Hent navnet på det som skal legges til
    var nyVerdi = $(this).closest('td').prev().prev().html();
    console.log('Legg til: ' + nyVerdi);

    // Hent IDen til det som skal legges til
    var nyID = $(this).closest('tr').attr('id');

    // Legg til ny knapp med valgt element

    var profilTabell;

    // Finn rett tabell
    $.each($('.panel-collapse'), function (index, value) {

        // Hvis den har 'in' klassen så er det den som er åpen
        if ($(this).hasClass('in')) {

            profilTabell = $(this).find('table tbody');

            // Legg til
            $.each($('#alle-elementer-tabell tbody tr'), function (index, value) {
                
                var trVerdi = $(this).children('td:first').html();
                
                if (nyVerdi == trVerdi) {

                    profilTabell.append('<tr id="' + nyID + '">' +
                                                       '<td class="col-lg-2">' + nyVerdi + '</td>' +
                                                       '<td class="col-lg-3">' + nyKatalog + '</td>' +
                                                       '<td class="col-lg-1"><i class="fa fa-minus-square fa-lg remove-item-btn"></i></td>' +
                                                       '</tr>');
                }
            });

        }

    });

    // Oppdater databasen
    oppdaterDatabase(profilTabell);
});

function oppdaterDatabase(profilTabell) {

    // Finn profil id
    var tabellId = profilTabell.closest('table').attr('id');
    console.log(tabellId);
    var profilId = tabellId.substring(0, tabellId.indexOf('-'));

    // Tom variabel for å holde teksten
    var newValue = '';

    // Gå gjennom alle radene og legg IDene i en string
    $.each($(profilTabell).children('tr'), function (index, value) {

        var id = $(this).attr('id');
        newValue += id + ';';

    });

    // Fjern siste ';' fra stringen
    newValue = newValue.substring(0, newValue.length - 1);

    console.log('Profil Id: ' + profilId);
    console.log('Value: ' + newValue);

    // Update
    $.post('/EditProject/OppdaterProfil', { ProfilId: profilId, Verdi: newValue });

}


function leggTilNyttPanel(profilId, navn){

    var panelMal = '';

    panelMal = '<div class="panel panel-default">' +
                            '<div class="panel-heading" role="tab" id="' + navn + '-heading">' +
                                '<h4 class="panel-title">' +                           // href="#' + navn + '-collapse"
                                    '<a data-toggle="collapse" data-parent="#accordion" aria-expanded="false" aria-controls="' + navn + '-collapse">' +
                                        navn + '<i class="fa fa-trash-o pull-right"></i><i class="fa fa-pencil-square-o pull-right"></i>' +
                                    '</a>' +
                                '</h4>' +
                            '</div>' +
                            '<div id="' + profilId + '-collapse" class="panel-collapse collapse" role="tabpanel" aria-labelledby="' + navn + '-heading">' +
                                '<div class="panel-body">' +
                                    '<table id="' + profilId + '-profil-table" class="table profil-tabell">' +
                                        '<thead>' +
                                            '<tr>' +
                                                '<th class="col-lg-3">Element</th>' +
                                                '<th class="col-lg-2">Katalog</th>' +
                                                '<th class="col-lg-1"></th>' +
                                            '</tr>' +
                                        '</thead>' +
                                        '<tbody>' +
                                        '</tbody>' +
                                    '</table>' +
                                '</div>' +
                            '</div>' +
                        '</div>';

    $('#accordion').prepend(panelMal);
}

function hentElementer(profilId, navn, elementer) {
    $.ajax({
        url: '/EditProject/HentElementer',
        data: { Elementer: elementer },
        type: 'GET',
        success: function (data) {



            $.each(data, function (index, value) {

                $('#' + profilId + '-profil-table tbody').append('<tr id="' + value['ListeKatalogId'] + '">' +
                                                                       '<td class="col-lg-3">' + value['Element'] + '</td>' +
                                                                       '<td class="col-lg-2">' + value['Katalog'] + '</td>' +
                                                                       '<td class="col-lg-1">' + '<i class="fa fa-minus-square fa-lg remove-item-btn"></i>' + '</td>' +
                                                                       '</tr>');
            });

        }
    });
}


function leggTilTekniskeProfiler() {

    // Finn ID
    var id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);

    $.ajax({
        url: '/EditProject/GetAlleTeknologier',
        type: 'GET',
        data: { Id: id },
        dataType: 'json',
        success: function (data) {

            console.log(data);

            $.each(data, function (index, value) {

                leggTilNyttPanel(value['TekniskProfilId'], value['Navn']);

                // Lag tabell som viser alle elementer
                hentElementer(value['TekniskProfilId'], value['Navn'], value['Elementer']);

            });
        }
    });
}

