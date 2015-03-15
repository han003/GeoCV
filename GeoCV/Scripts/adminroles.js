$('.remove').click(function () {

    var role = $(this).prev().html();

    console.log('Role: ' + role);

    $.post('/AdminRoles/DeleteRole', { RoleName: role });

});

$('#new-role-btn').click(function () {

    var role = $('#new-role-txt').val();

    console.log('Role: ' + role);

    $.post('/AdminRoles/AddRole', { RoleName: role });

});