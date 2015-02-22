$('#search-employee').keyup(function () {

    var searchText = $(this).val();

    $.ajax({
        url: '/Employees/Search',
        data: { Search: searchText },
        dataType: 'json',
        type: 'GET',
        success: function (data) {

            var template = '';

            $.each(data, function (index, value) {

                var fornavn = value['Fornavn'];
                var etternavn = value['Etternavn'];

                template += '<tr>' +
                                '<td>' + fornavn + '</td>' +
                                '<td>' + etternavn + '</td>' +
                                '<td>Rediger</td>' +
                                '<td></td>' +
                                '<td></td>' +
                            '</tr>';

            });

            $('tbody').html(template);

        }
    });

});