

$('.cv-nav-btn').click(function () {

    var element = $(this);

    if (!element.hasClass('btn-success')) {

        var btnIndex = element.index();
        console.log('Button index: ' + btnIndex);
        var text = element.text();

        var oldDiv, newDiv;


        // Find the div containing the current class
        oldDiv = $('body').find('.cv-current');

        // Find div corresponing to the index
        $('.cv-div').each(function (loopIndex) {

            if ((btnIndex + 3) == $(this).index()) {
                newDiv = $(this);
            }

        });

        console.log('Old div index: ' + oldDiv.index());
        console.log('New div index: ' + newDiv.index());

        // Decide animation
        var fadeIn = '';
        var fadeOut = '';

        if (oldDiv.index() > newDiv.index()) {
            fadeOut = 'fadeOutRight';
            fadeIn = 'fadeInLeft';
        } else {
            fadeOut = 'fadeOutLeft';
            fadeIn = 'fadeInRight';
        }

        element.parent().find('.btn-success').removeClass('btn-success').addClass('btn-default');
        element.removeClass('btn-default').addClass('btn-success');

        // Hide old div
        oldDiv.addClass('animated ' + fadeOut);

        oldDiv.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {

            // Hide and remove all relevant classes
            oldDiv.addClass('hidden').removeClass('cv-current animated fadeOutLeft fadeOutRight fadeInLeft fadeInRight');

            // Get new div
            newDiv.removeClass('hidden').addClass('cv-current animated ' + fadeIn);

            console.log('New Id: ' + newDiv.attr('id'));

        });

        
    }

});