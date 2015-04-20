$(document).ready(function () {
    getKatalogElementer();
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
                                                               '<td class="col-lg-3">' + value['Element'] + '</td>' +
                                                               '<td class="col-lg-2">' + value['Katalog'] + '</td>' +
                                                               '<td class="col-lg-1">' + '<i class="fa fa-plus-square add-item-btn"></i>' + '</td>' +
                                                               '</tr>');

                // Tabell(er) for å vise tekniske profiler
                /*try {
                    $.each(data[0][0].split(';'), function (index, element) {
                        if (value['ListeKatalogId'] == element) {
                            // ID
                            var elementId = 'element-' + value['ListeKatalogId'];

                            // Lag tabell som viser bruker elementer
                            $('#' + katalog + '-bruker-tabell tbody').append('<tr id="' + elementId + '">' +
                                                                           '<td class="col-lg-5">' + value['Element'] + '</td>' +
                                                                           '<td class="col-lg-1"><i class="fa fa-minus-square remove-item-btn"></i></td>' +
                                                                           '</tr>');
                        }
                    });
                }
                catch (e) {
                    console.log(e);
                }*/

            });

            //$('#' + katalog + '-load').addClass('hidden');
            //$('#' + katalog + '-form').removeClass('hidden');

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
        }
    });
});
