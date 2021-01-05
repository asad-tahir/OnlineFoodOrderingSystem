fetch("/Items/GetItems")
    .then(response => response.json())
    .then(items => {
        items.forEach(function (item, index) {

            var row = $('#item-row').clone();
            row.removeClass('d-none');
            row.removeAttr('id');

            var count = row.find('#count');
            count.removeAttr('id');
            count.text(index + 1);

            var viewModal = row.find('#view-modal-button');
            viewModal.attr('data-id', item.Id);
            viewModal.removeAttr('id');
            viewModal.text('VIEW');

            var editModal = row.find('#edit-modal-button');
            editModal.attr('data-id', item.Id);
            editModal.removeAttr('id');
            editModal.text('EDIT');

            var name = row.find('#name');
            name.removeAttr('id');
            name.text(item.Name);

            var dlt = row.find('#delete');
            dlt.removeAttr('id');
            dlt.attr('href', '/items/deleteitem/' + item.Id);

            var price = row.find('#price');
            price.removeAttr('id');
            price.text(item.Price + '$');

            var avail = row.find('#avail');
            avail.removeAttr('id');
            avail.text(item.IsAvailable ? 'Yes' : 'No');
            $('#items-table').append(row);
        });
        $('#item-row').remove();
    });

$('tbody').on('click', 'button', function () {
    const actions = {
        showViewModal: "#view-modal",
        showEditModal: "#edit-modal"
    };

    var itemId = $(this).attr('data-id');
    var action = $(this).attr('data-target');
    if (action === actions.showViewModal) {
        fillViewModal(itemId);
    } else {
        fillEditModal(itemId);
    }
});
function fillViewModal(itemId) {
    fetch('/Items/Item/' + itemId)
        .then(response => response.json())
        .then(item => {
            var viewModal = $('#view-modal');

            var longTitle = viewModal.find('#item-name');
            longTitle.text(item.Name);

            var priceModal = viewModal.find('#price-modal');
            priceModal.text('Price:  ' + item.Price + '$');

            viewModal.find('img').attr('src', '/' + item.ImageUrl);

            var descPara = viewModal.find('#desc-p');
            descPara.text(item.Description);
        });
}

function fillEditModal(itemId) {
    fetch('/Items/Item/' + itemId)
        .then(response => response.json())
        .then(item => {

            var editModal = $('#edit-modal');

            var modalTitle = editModal.find('#modal-title');
            modalTitle.text(item.Name);

            var priceModal = editModal.find('#Price');
            priceModal.val(item.Price);

            var priceModal = editModal.find('#Price');
            priceModal.val(item.Price);

            var itemId = editModal.find('#item-id');
            itemId.val(item.Id);

            var issAvailable = editModal.find('#IsAvailable');
            issAvailable.attr('checked', item.IsAvailable);
        });
}