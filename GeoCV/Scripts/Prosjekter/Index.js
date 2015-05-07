$.fn.bootstrapSwitch.defaults.onText = 'Vis';
$.fn.bootstrapSwitch.defaults.offText = 'Skjul';

$('.vis-avslutt-switch').bootstrapSwitch();

$(document).ready(function () {

    // Sorter valgt tabell
    $('#prosjekt-tabell').stupidtable();

    // Gjør stuff etter at tabellen er sortert
    $('#prosjekt-tabell').bind('aftertablesort', function (event, data) {
        var kolonneIndex = data.column;
        var sorteringRetning = data.direction;
        // $(this) - this table object

        $.each($('#prosjekt-tabell th i'), function (index, value) {

            if (index == kolonneIndex) {
                $(this).removeClass('hidden fa-sort-desc fa-sort-asc');

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

$('input[name="avsluttede-prosjekter-checkbox"]').on('switchChange.bootstrapSwitch', function (event, state) {
    console.log(this); // DOM element
    console.log(event); // jQuery event
    console.log(state); // true | false

    if (state) {
        $('tr.text-danger').removeClass('hidden');
    } else {
        $('tr.text-danger').addClass('hidden');
    }
});

$('button').click(function () {
    $(this).blur();
});

$('#prosjekt-filter').keyup(function () {
    // Hent tekst som er skrevet inn
    var filterTekst = $(this).val().toLowerCase();
    console.log('Filter: ' + filterTekst);

    // Gå gjennom alle radene og legg IDene i en string
    $('#prosjekt-tabell tbody tr').each(function () {

        // Prosjekt tekst
        var prosjektNavn = $(this).data('navn').toLowerCase();

        // Kunde tekst
        var prosjektKunde = $(this).data('kunde').toLowerCase();

        // Beskrivelse tekst
        var prosjektBeskrivelse = $(this).data('beskrivelse').toLowerCase();

        // Sjekk om elementet inneholder filter teksten
        if (prosjektNavn.indexOf(filterTekst) >= 0 || prosjektKunde.indexOf(filterTekst) >= 0 || prosjektBeskrivelse.indexOf(filterTekst) >= 0) {
            // Inneholder, så vis
            $(this).removeClass('hidden');
        } else {
            // Skjul
            $(this).addClass('hidden');
        }

    });
});

// Når man klikker på et prosjekt for å endre prosjektet
$(document).on('click', '#prosjekt-tabell tbody tr', function () {

    // Hvis valgt tr er valgt fra før, ikke gjør noe
    if (!$(this).hasClass('valgt-list-group-item')) {

        // valgt tr
        var prosjektId = $(this).data('prosjektid');

        // Marker at denne tr er valgt
        $.each($('#prosjekt-tabell tbody tr'), function (index, value) {
            if ($(this).data('prosjektid') == prosjektId) {
                $(this).addClass('valgt-tr');
                $(this).find('i').removeClass('hidden');
            } else {
                $(this).removeClass('valgt-tr');
                $(this).find('i').addClass('hidden');
            }
        });

        // Skjul alle prosjekter
        $.each($('.prosjekt-info-panel'), function (index, value) {
            $(this).addClass('hidden');
        });

        console.log(prosjektId);

        // For hvert panel som har klassen og rett prosjekt id (bare ett panel)
        $.each($('.prosjekt-info-panel[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
            $(this).removeClass('hidden');
        });

    }
});

$('.slett-prosjekt').click(function () {

    $(this).parent().next().removeClass('hidden');

});

$('.bekreft-slett').click(function () {

    var spinner = $(this).parent().parent().prev().find('i.fa-spinner');
    var prosjektId = $(this).data('prosjektid');

    console.log(spinner.html());

    $.ajax({
        url: '/Prosjekter/SlettProsjekt',
        data: { Id: prosjektId },
        type: 'POST',
        beforeSend: function () {

            spinner.removeClass('hidden');

        },
        success: function () {

            console.log('Slettet');

            // For hvert panel som har klassen og rett prosjekt id
            $.each($('.prosjekt-info-panel[data-prosjektid="' + prosjektId + '"]'), function (index, value) {
                $(this).remove();
            });

            $.each($('#prosjekt-tabell tbody tr'), function (index, value) {
                if ($(this).find('i').hasClass('hidden')) {
                    // Gjør ingenting
                } else {
                    $(this).remove();
                }
            });

        }
    });

});

$('.endre-status-btn').click(function () {

    var spinner = $(this).closest('.panel').find('i.fa-spinner');
    var prosjektId = $(this).data('prosjektid');
    var avsluttetProsjekt = $(this).data('prosjektstatus');

    // Elementer
    var endreStatusBtn = $(this);
    var statusPanelHeader = $('.status-panel-header[data-prosjektid="' + prosjektId + '"]');
    var prosjektTr = $('#prosjekt-panel tbody tr[data-prosjektid="' + prosjektId + '"]');

    // Hvis true så betyr det at prosjektet et satt som avsluttet
    if (avsluttetProsjekt == 'False' || avsluttetProsjekt == false) {
        avsluttetProsjekt = true;
    } else {
        avsluttetProsjekt = false;
    }

    $.ajax({
        url: '/Prosjekter/EndrePorsjektStatus',
        data: { Id: prosjektId, Status: avsluttetProsjekt },
        type: 'POST',
        beforeSend: function () {

            spinner.removeClass('hidden');

        },
        success: function () {

            if (avsluttetProsjekt) {

                // Hvis switch er satt til skjul avsluttede prosjekter
                if ($('.vis-avslutt-switch').bootstrapSwitch('state') == false) {
                    // Skjul tr
                    prosjektTr.addClass('hidden');

                    // Skjul paneler
                    $('.panel[data-prosjektid="' + prosjektId + '"]').addClass('hidden');

                    // Fjern valgt tr
                    $.each($('#prosjekt-tabell tbody tr'), function (index, value) {
                        $(this).removeClass('valgt-tr');
                        $(this).find('i').addClass('hidden');
                    });
                }

                endreStatusBtn.data('prosjektstatus', true);
                endreStatusBtn.html('Gjenåpne prosjekt');
                statusPanelHeader.html('Gjenåpne');
                prosjektTr.addClass('text-danger');
            } else {
                endreStatusBtn.data('prosjektstatus', false);
                endreStatusBtn.html('Avslutt prosjekt');
                statusPanelHeader.html('Avslutt');
                prosjektTr.removeClass('text-danger');
                prosjektTr.removeClass('hidden');
            }

            spinner.addClass('hidden');
            $(this).addClass('valgt-list-group-item');
        }
    });

});

$('.avbryt-slett').click(function () {

    $(this).parent().addClass('hidden');

});

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

$('.prosjekt-info-panel input').keyup(function () {

    var spinner = $(this).closest('.panel-body').prev().find('i.fa-spinner');
    var inputTekstfelt = $(this);

    delay(function () {

        var prosjektId = inputTekstfelt.data('prosjektid');
        var tekstfelt = inputTekstfelt.data('tekstfelt').toLowerCase();
        var nyVerdi = inputTekstfelt.val();

        // Update
        $.ajax({
            url: '/Prosjekter/EndreProsjektInfo',
            data: { Id: prosjektId, NyVerdi: nyVerdi, Tekstfelt: tekstfelt },
            type: 'POST',
            beforeSend: function () {

                console.log(prosjektId);
                console.log(nyVerdi);
                console.log(tekstfelt);
                spinner.removeClass('hidden');

            },
            success: function () {

                spinner.addClass('hidden');

                $('.prosjekt-td[data-prosjektid="' + prosjektId + '"][data-tdfelt="' + tekstfelt + '"]').html(nyVerdi);

            }
        });

    }, 400);

});

