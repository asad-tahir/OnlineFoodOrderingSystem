
fetch('/Transactions/GetTransactions')
    .then(response => response.json())
    .then(transactions => fillTable(transactions));

$('#table-container').on('change', '#dateFilter', function () {
    $('#showAll').removeClass('d-none');
    var data = {
        date: $(this).val()
    };
    fetch('/Transactions/GetTransactionsByDate', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(transactions => fillTable(transactions))
});

function fillTable(transactions) {
    var rowBackup = $("#exampleRow").clone();
    $('#transactions-table').find('tbody').html("");
    $('#transactions-table').find('tbody').append(rowBackup);
    var totalAmount = 0;
    transactions.forEach(function (transaction) {

        var row = $("#exampleRow").clone();
        row.removeAttr('id');
        row.removeAttr('class');

        var name = row.find("#name");
        name.removeAttr('id');
        name.text(transaction.CustomerName);

        var date = row.find("#date");
        date.removeAttr('id');
        date.text(transaction.Date);

        var amount = row.find("#amount");
        amount.removeAttr('id');
        amount.text(transaction.Amount + '$');
        totalAmount += transaction.Amount;

        $('#transactions-table').find('tbody').append(row);

    });
    $('#totalAmount').text(totalAmount.toFixed(2) + '$');

}