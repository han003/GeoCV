// Globale variabler
var dbOppdateringsKolonne;

$(document).ready(function () {
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
                                                               '<td class="col-lg-1">' + '<i class="fa fa-plus-square add-item-btn"></i>' + '</td>' +
                                                               '</tr>');
            });

            // Lagre arrayer
            $('#alle-elementer-tabell').data('elementer', elementArray);
            $('#alle-elementer-tabell').data('elementerId', idArray);
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
            $('#ny-profil-lagre-btn').html('Lagrer <i class="fa fa-circle-o-notch fa-spin"></i>');
        },
        success: function () {
            $('#ny-profil-navn-txt').val('');
            $('#ny-profil-lagre-btn').html('Legg til');

            oppdaterPaneler();
        }
    });
});

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

// What happens when add button is clicked
$('body').on('click', '.add-item-btn', function () {

    // Hent katalogen til det som skal legges til
    var nyKatalog = $(this).closest('td').prev().html();
    console.log('Katalog: ' + nyKatalog);

    // Hent navnet på det som skal legges til
    var nyVerdi = $(this).closest('td').prev().prev().html();
    console.log('Legg til: ' + nyVerdi);

    // Legg til ny knapp med valgt element

    // Finn rett tabell
    $.each($('.panel-collapse'), function (index, value) {

        // Hvis den har 'in' klassen så er det den som er åpen
        if ($(this).hasClass('in')) {

            var profilTabell = $(this).find('table tbody');

            // Legg til
            $.each($('#alle-elementer-tabell tbody tr'), function (index, value) {
                
                var trVerdi = $(this).children('td:first').html();
                
                if (nyVerdi == trVerdi) {

                    profilTabell.append('<tr id="' + 0 + '">' +
                                                       '<td class="col-lg-2">' + nyVerdi + '</td>' +
                                                       '<td class="col-lg-3">' + nyKatalog + '</td>' +
                                                       '<td class="col-lg-1"><i class="fa fa-minus-square remove-item-btn"></i></td>' +
                                                       '</tr>');
                }
            });

        }

    });


    

    // Oppdater databasen
    // updateDatabase();
});

function oppdaterPaneler() {

}


function leggTilNyttPanel(navn, elementer){

    var panelMal = '';

    panelMal = '<div class="panel panel-default">' +
                            '<div class="panel-heading" role="tab" id="' + navn + '-heading">' +
                                '<h4 class="panel-title">' +                           // href="#' + navn + '-collapse"
                                    '<a data-toggle="collapse" data-parent="#accordion" aria-expanded="false" aria-controls="' + navn + '-collapse">' +
                                        navn +
                                    '</a>' +
                                '</h4>' +
                            '</div>' +
                            '<div id="' + navn + '-collapse" class="panel-collapse collapse" role="tabpanel" aria-labelledby="' + navn + '-heading">' +
                                '<div class="panel-body">' +
                                    '<table id="' + navn + '-table" class="table profil-tabell">' +
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

    $('#accordion').append(panelMal);
}

function hentElementer(navn, elementer) {
    $.ajax({
        url: '/EditProject/HentElementer',
        data: { Elementer: elementer },
        type: 'GET',
        success: function (data) {

            

            $.each(data, function (index, value) {

                $('#' + navn + '-table tbody').append('<tr id="' + value['ListeKatalogId'] + '">' +
                                                               '<td class="col-lg-3">' + value['Element'] + '</td>' +
                                                               '<td class="col-lg-2">' + value['Katalog'] + '</td>' +
                                                               '<td class="col-lg-1">' + '<i class="fa fa-minus-square"></i>' + '</td>' +
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

            var elementArray = new Array();
            var idArray = new Array();

            $.each(data, function (index, value) {

                elementArray.push(value['Element']);
                idArray.push(value['ListeKatalogId']);

                leggTilNyttPanel(value['Navn'], value['Elementer']);

                // Lag tabell som viser alle elementer
                hentElementer(value['Navn'], value['Elementer']);

            });

            //$('#' + katalog + '-load').addClass('hidden');
            //$('#' + katalog + '-form').removeClass('hidden');

            // Lagre arrayer
            $('#alle-elementer-tabell').data('elementer', elementArray);
            $('#alle-elementer-tabell').data('elementerId', idArray);
        }
    });
}

