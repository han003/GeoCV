﻿$('table tr').click(function () {
    window.location = $(this).attr('href');
    return false;
});