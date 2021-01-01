fetch('/users/getusers')
    .then(response => response.json())
    .then(users => {
        users.forEach(function (user, index) {
            var row = $('#user-data-row').clone();
            row.removeClass('d-none');
            row.removeAttr('id');

            var count = row.find('#count');
            count.removeAttr('id');
            count.text(index + 1);

            var name = row.find('#name');
            name.removeAttr('id');
            name.text(user.Name);

            var email = row.find('#email');
            email.removeAttr('id');
            email.text(user.Email);
            $('tbody').append(row);
        });
        $('#user-data-row').remove();

    });