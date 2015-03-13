$('.remove').click(function () {

    var role = $(this).prev().html();

    console.log('Role: ' + role);

    $.post('/AdminRoles/DeleteRole', { Role: role });

});