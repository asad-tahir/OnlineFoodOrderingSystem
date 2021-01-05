// cart
var cart = [];
// fetching Categories
fetch('/Categories/GetCategories')
    .then(response => response.json())
    .then(categories => {
        categories.forEach(fillItemsByCategory);
        document.getElementById('custom-container').remove();
    });

//Filling Items in Menu
function fillItemsByCategory(category) {

    const customContainer = document.getElementById('custom-container').cloneNode(true);
    customContainer.removeAttribute('id');
    customContainer.classList.remove('d-none');

    const categoryName = customContainer.querySelector('#category-name');
    categoryName.innerHTML = category.Name;
    categoryName.removeAttribute('id');

    fetch('/Items/GetItemsByCategory/' + category.Id)
        .then(response => response.json())
        .then(items => {
            if (items.length !== 0) {
                var cardContainer = customContainer.querySelector('#items-container');

                items.forEach(function (item) {
                    const card = cardContainer.querySelector('#card').cloneNode(true);
                    card.removeAttribute('id');
                    card.setAttribute('data-id', item.Id);

                    card.querySelector('h5').innerHTML = item.Name;
                    card.querySelector('p').innerHTML = item.Price + '$';

                    card.querySelector('img').src = '/' + item.ImageUrl;
                    cardContainer.append(card);
                    card.addEventListener("click", function () {
                        var itemId = this.getAttribute('data-id');
                        $('#view-modal').modal();
                        fillViewModal(itemId);
                    });
                });

                customContainer.querySelector('#card').remove();
            } else {
                customContainer.remove();
            }
        });
    const menu = document.getElementById('menu');
    menu.append(customContainer);
}

// Items details and add to cart view modal
function fillViewModal(itemId) {
    fetch('/Items/Item/' + itemId)
        .then(response => response.json())
        .then(item => {
            var viewModal = $('#view-modal');

            var longTitle = viewModal.find('#item-name');
            longTitle.text(item.Name);

            var priceModal = viewModal.find('#price-modal');
            priceModal.text('Price: ' + item.Price + '$');

            viewModal.find('img').attr('src', '/' + item.ImageUrl);

            var qty = viewModal.find('#qty');
            qty.attr('data-item-id', item.Id);
            qty.attr('data-item-name', item.Name);
            qty.attr('data-item-price', item.Price);

            var itemInCart = cart.find(i => i.Id == item.Id);
            if (itemInCart === undefined) {
                qty.val(0);
            }
            else {
                qty.val(itemInCart.Qty);
            }
            var descPara = viewModal.find('#desc-p');
            descPara.text(item.Description);
        });
}


// event on qty change on view modal
$('#view-modal').on('change', '#qty', function () {
    if ($(this).val() == 0) {
        var itemInCart = cart.find(i => i.Id === $(this).attr('data-item-id'));
        if (itemInCart !== undefined) {
            var index = cart.indexOf(itemInCart);
            cart.splice(index,1);
        }
    }
    else {
        var itemInCart = cart.find(i => i.Id === $(this).attr('data-item-id'));
        if (itemInCart === undefined) {
            cart.push({
                Id: $(this).attr('data-item-id'),
                Name: $(this).attr('data-item-name'),
                Price: $(this).attr('data-item-price'),
                Qty: $(this).val()
            });
        }
        else {
            itemInCart.Qty = $(this).val();
        }
    }
    localStorage.setItem('cart', JSON.stringify(cart));
    drawCart();
    grandTotal();

})

// fill and draw cart
function drawCart() {
    cart = JSON.parse(localStorage.getItem('cart')) || [];
    
    var cartRow = $('#exampleCartRow').clone();
    $('#table-cart').find('tbody').html('');
    $('#table-cart').find('tbody').append(cartRow);

    cart.forEach(fillCartRow);
}

function fillCartRow(item) {
    var cartRow = $('#exampleCartRow').clone();
    cartRow.removeAttr('id');
    cartRow.removeAttr('class');

    var name = cartRow.find('#name');
    name.removeAttr('id');
    name.text(item.Name);

    var itemPrice = cartRow.find('#item-price');
    itemPrice.removeAttr('id');
    itemPrice.text(item.Price);

    var inputQty = cartRow.find('#cart-qty');
    inputQty.removeAttr('id');
    inputQty.val(item.Qty);
    inputQty.attr('data-item-id', item.Id);
    inputQty.attr('data-item-name', item.Name);
    inputQty.attr('data-item-price', item.Price);

    var totalPrice = cartRow.find('#total-price');
    totalPrice.removeAttr('id');
    totalPrice.text(parseFloat(item.Qty * item.Price).toFixed(2));
    

    $('#table-cart').find('tbody').append(cartRow);
}

// Cart change qty event
$('#table-cart').on('change', 'input', function () {
    if ($(this).val() == 0) {
        var itemInCart = cart.find(i => i.Id === $(this).attr('data-item-id'));
        if (itemInCart !== undefined) {
            var index = cart.indexOf(itemInCart);
            cart.splice(index, 1);
        }
        $(this).closest("tr").remove();
    }
    else {
        var itemInCart = cart.find(i => i.Id === $(this).attr('data-item-id'));
        if (itemInCart === undefined) {
            cart.push({
                Id: $(this).attr('data-item-id'),
                Name: $(this).attr('data-item-name'),
                Price: $(this).attr('data-item-price'),
                Qty: $(this).val()
            });
        }
        else {
            itemInCart.Qty = $(this).val();
            $(this).closest("tr").find('.total-price').text(parseFloat(itemInCart.Qty * itemInCart.Price).toFixed(2));
        }
    }
    localStorage.setItem('cart', JSON.stringify(cart));
    grandTotal();
})
// display total to user on cart
function grandTotal(){
    cart = JSON.parse(localStorage.getItem('cart')) || [];
    var totalPrice = 0;
    cart.forEach((item) => {
        totalPrice = totalPrice + (item.Qty * item.Price);
    });
    $('#grand-total').text(parseFloat(totalPrice).toFixed(2)+'$');
}

drawCart();

grandTotal();

// Payment Modal
$('#modal-cart').on('click', '#place-order', function () {
    if (cart.length > 0) {
        $('#modal-cart').modal('toggle');
        $('#payment-modal').modal('toggle');
    }
    else {
        $('#modal-cart').modal('toggle');
    }
});

$('#payment-modal').on('click', '#submit-info', function () {
    $('#payment-modal').modal('toggle');
    var __RequestVerificationToken1 = $('#anti-ft').find('input').val();
    var CardNumber = $('#CardNumber').val();
    var expDateYear = ($('#ExpirationDate').val()).split('/');
    var ExpirationDate = expDateYear[0] + expDateYear[1];
    if (ExpirationDate.length == 3) {
        ExpirationDate = '0' + ExpirationDate;
    }
    var data = {
        __RequestVerificationToken: __RequestVerificationToken1,
        CardNumber: CardNumber,
        ExpirationDate: ExpirationDate,
        Cart: cart
    }

    $.ajax({
        url: '/Orders/PlaceOrder',
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function () {
            cart = [];
            localStorage.removeItem('cart');
            location.href = '/Orders/Index';
        }
    });
})
