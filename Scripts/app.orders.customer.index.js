fetch('/Orders/GetOrders')
    .then(response => response.json())
    .then(orders => {
        orders.forEach(addTableRow);
        $('#order-data-row').remove();
    });
function addTableRow(order) {

    var row = $('#order-data-row').clone();
    row.removeAttr('id');
    row.removeClass('d-none');
    row.data('id', order.Id);

    var date = row.find('#date');
    date.removeAttr('id');
    date.text(order.OrderDate);

    var amount = row.find('#amount');
    amount.removeAttr('id');
    amount.text(order.Amount + '$');

    var status = row.find('#status');
    status.removeAttr('id');
    status.text(order.Status);

    $('#orders-table').find('tbody').append(row);
}
$('tbody').on('click', 'tr', function () {
    var row = $(this);
    var id = row.data('id');
    fetch('/Orders/GetOrder/' + id)
        .then(response => response.json())
        .then(orderItems => {
            var row = $('#order-item-row').clone();
            $('#table-cart').find('tbody').html('');
            $('#table-cart').find('tbody').append(row);

            var grandTotal = 0.0;
            for (var item of orderItems) {
                grandTotal += item.Price;
            }
            orderItems.forEach(fillDetailsModal);

            $('#grand-total').text(grandTotal.toFixed(2) + '$');

            return $('#modal-order-details').modal('toggle');
        });
});
function fillDetailsModal(item) {
    var row = $('#order-item-row').clone();
    row.removeAttr('id');
    row.removeClass('d-none');

    var name = row.find('#name');
    name.removeAttr('id');
    name.text(item.Name);

    var qty = row.find('#qty');
    qty.removeAttr('id');
    qty.text(item.Qty);

    var price = row.find('#total-price');
    price.removeAttr('id');
    price.text(item.Price + '$');

    $('#table-cart').find('tbody').append(row);
    return item.Qty * item.Price;
}