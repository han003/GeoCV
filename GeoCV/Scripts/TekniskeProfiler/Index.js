$(document).ready(function () {

    // Sorter valgt tabell
    $('#katalog-tabell').stupidtable();
    $('#prosjekt-tabell').stupidtable();

    // Gjør ting etter at tabellen er sortert
    $('#katalog-tabell').bind('aftertablesort', function (event, data) {
        etterSortering(data, $('#katalog-tabell th i'));
    });
    $('#prosjekt-tabell').bind('aftertablesort', function (event, data) {
        etterSortering(data, $('#prosjekt-tabell th i'));
    });
});

$('#prosjekt-filter').keyup(function () {
    var filterTekst = $(this).val().toLowerCase();

    // Gå gjennom alle radene og legg IDene i en string
    $('#prosjekt-tabell tbody tr').each(function () {

        // Prosjekt tekst
        var prosjektNavn = $(this).data('navn').toLowerCase();

        // Kunde tekst
        var prosjektKunde = $(this).data('kunde').toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (prosjektNavn.indexOf(filterTekst) >= 0 || prosjektKunde.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

$('#katalog-filter').keyup(function () {
    var filterTekst = $(this).val().toLowerCase();
    console.log('filter: ' + filterTekst);
    // Gå gjennom alle radene og legg IDene i en string
    $('#katalog-tabell tbody tr').each(function () {

        // Prosjekt tekst
        var element = $(this).data('katalogelement').toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (element.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

function etterSortering(data, tabell) {

    var kolonneIndex = data.column;
    var sorteringRetning = data.direction;

    $.each(tabell, function (index, value) {

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
}

$('.table-filter').keyup(function () {
    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#alle-elementer-tabell tbody tr').each(function () {

        // Element tekst
        var elementTekst = $(this).children('td').html().toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (elementTekst.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

$('.slett-link').click(function () {

    $('#slett-profil-navn').html($(this).data('profilnavn'));
    $('#slett-id').val($(this).data('profilid'));
    $('#slett-teknisk-profil-modal').modal();

});

$('.legg-til-profil-btn').click(function () {

    $('#legg-til-prosjekt-id').val($(this).data('prosjektid'));
    $('#legg-til-teknisk-profil-modal').modal();

});

// Når man klikker på et prosjekt for å endre tekniske profiler
$(document).on('click', '#prosjekt-tabell tbody tr', function () {
    
    // Hvis valgt tr er valgt fra før, ikke gjør noe
    if (!$(this).hasClass('valgt-list-group-item')) {

        // Fjern markeringer fra alle prosjekt profil paneler
        $.each($('.prosjekt-profil-panel'), function (index, value) {
            $(this).find('li.valgt-list-group-item').removeClass('valgt-list-group-item');
            $(this).find('.panel-body i').addClass('hidden');
        });

        // valgt tr
        var valgtTrId = $(this).data('prosjektid');

        // Marker at denne tr er valgt
        $.each($('#prosjekt-tabell tbody tr'), function (index, value) {
            if ($(this).data('prosjektid') == valgtTrId) {
                $(this).addClass('valgt-list-group-item');
                $(this).find('i').removeClass('hidden');
            } else {
                $(this).removeClass('valgt-list-group-item');
                $(this).find('i').addClass('hidden');
            }
        });

        // Skjul alle tekniske profil paneler
        $.each($('.teknisk-profil-panel'), function (index, value) {
            $(this).addClass('hidden');
        });

        // Skjul også katalog panel og katalog filter
        $('#katalog-filter-panel, #katalog-panel').addClass('hidden');

        var prosjektId = $(this).data('prosjektid');

        console.log(prosjektId);

        // For hvert panel som har klassen og rett prosjekt id (bare ett panel)
        $.each($('.prosjekt-profil-panel[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
            $(this).removeClass('hidden');
        });
        // For hvert panel som har klassen men ikke rett prosjekt id (alle de andre)
        $.each($('.prosjekt-profil-panel[data-prosjektid!="' + prosjektId + '"]'), function (index, value) {
            $(this).addClass('hidden');
        });

    }
});

// når man klikker på rediger en teknisk profil i et prosjekt
$(document).on('click', '.rediger-link', function () {

    var tekniskId = $(this).data('tekprofilid');

    var ulListe = $(this).parent();

    // Marker at denne li er valgt
    $.each(ulListe.children('li'), function (index, value) {
        if ($(this).data('tekprofilid') == tekniskId) {
            $(this).addClass('valgt-list-group-item');
            $(this).find('i').removeClass('hidden');
        } else {
            $(this).removeClass('valgt-list-group-item');
            $(this).find('i').addClass('hidden');
        }
    });

    console.log(tekniskId);

    // For hvert panel som har klassen og rett prosjekt id (bare ett panel)
    $.each($('.teknisk-profil-panel[data-tekprofilid="' + tekniskId + '"]'), function (index, value) {
        $(this).removeClass('hidden');
    });
    // For hvert panel som har klassen men ikke rett prosjekt id (alle de andre)
    $.each($('.teknisk-profil-panel[data-tekprofilid!="' + tekniskId + '"]'), function (index, value) {
        $(this).addClass('hidden');
    });

    // Vis også katalog panel og katalog filter
    $('#katalog-filter-panel, #katalog-panel').removeClass('hidden');

});

// Legg til et element i den tekniske profilen som er valgt
$(document).on('click', '#katalog-tabell tbody tr', function () {

    var katalogElementId = $(this).data('katalogelementid');
    var katalogElement = $(this).data('katalogelement');
    var katalog = $(this).data('katalog');

    console.log(katalogElementId);

    // Variabler
    var tekniskProfilPanel = $('.teknisk-profil-panel:not(.hidden)');
    var tekniskProfilId = tekniskProfilPanel.data('tekprofilid');
    var tekniskProfilTabell = tekniskProfilPanel.find('table');
    var finnesFraFør = false;

    // Gå gjennom alle radene for å se om valgt element finnes fra før
    tekniskProfilTabell.find('tbody').children('tr').each(function () {
        if ($(this).data('katalogelementid') == katalogElementId) {
            finnesFraFør = true;
        }
    });
    
    if (!finnesFraFør) {

        var nyHtml = '<tr data-katalogelementid="' + katalogElementId + '">' +
                        '<td class="col-lg-6">' + katalogElement + '</td>' +
                        '<td class="col-lg-6"><div class="td-flex"><div>' + katalog + '</div><i class="fa fa-minus-square hidden"></i></div></td>' +
                     '</tr>';

        tekniskProfilTabell.find('tbody').append(nyHtml);

        // Oppdater database ///////////////
        // Tom variabel for å holde teksten
        var nyVerdi = '';

        // Gå gjennom alle radene og legg IDene i en string
        tekniskProfilTabell.find('tbody').children('tr').each(function () {
            var id = $(this).data('katalogelementid');
            nyVerdi += id + ';';
        });

        // Fjern siste ';' fra stringen
        nyVerdi = nyVerdi.substring(0, nyVerdi.length - 1);

        console.log('Profil Id: ' + tekniskProfilId);
        console.log('Ny verdi: ' + nyVerdi);

        // Update
        $.ajax({
            url: '/TekniskeProfiler/OppdaterProfil',
            data: { ProfilId: tekniskProfilId, Verdi: nyVerdi },
            type: 'POST',
            success: function () {

                console.log('Lagt til');

            }
        });

    }

});

// Fjern et element i den tekniske profilen som er valgt
$(document).on('click', '.teknisk-tabell tbody tr', function () {

    // Id til den tekniske profilen som blir redigert
    var tekniskProfilId = $(this).data('tekprofilid');
    var tbody = $(this).parent();

    // Fjern rad
    $(this).remove();

    // Oppdater database ///////////////
    // Tom variabel for å holde teksten
    var nyVerdi = '';

    // Gå gjennom alle radene og legg IDene i en string
    tbody.children('tr').each(function () {
        var id = $(this).data('katalogelementid');
        nyVerdi += id + ';';
    });

    // Fjern siste ';' fra stringen
    nyVerdi = nyVerdi.substring(0, nyVerdi.length - 1);

    console.log('Profil Id: ' + tekniskProfilId);
    console.log('Ny verdi: ' + nyVerdi);

    // Update
    $.ajax({
        url: '/TekniskeProfiler/OppdaterProfil',
        data: { ProfilId: tekniskProfilId, Verdi: nyVerdi },
        type: 'POST',
        success: function () {

            console.log('Lagt til');

        }
    });

});