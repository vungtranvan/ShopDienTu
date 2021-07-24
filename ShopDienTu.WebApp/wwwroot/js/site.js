var SiteController = function () {
    this.initialize = function () {
        const culture = $('#hideCulture').val();
        regsiterEvents(culture);
        loadCart(culture);
    }
    function loadCart(culture) {
        $.ajax({
            type: "GET",
            url: "/" + culture + '/Cart/GetListItems',
            success: function (res) {
                var quantityItem = 0;
                $.each(res, function (i, item) {
                    quantityItem += item.quantity;
                });
                $('.lbl_number_of_items').text(quantityItem);
            }
        });
    }
    function regsiterEvents(culture) {
        $('body').on('click', '.btn-add-cart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            addToCart(culture, id, 1);
        });
        $('body').on('click', '.btn-add-cart-number', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            var quantity = parseInt($('.number-quantity-to-cart').val());
            if (quantity <= 0 || quantity == null) {
                toastr.error('Số lượng thêm vào giỏ hàng phải lớn hơn 0', 'Thông báo');
                return;
            } else {
                addToCart(culture, id, quantity);
            }

        });
    }
    function addToCart(culture, id, quantity) {
        $.ajax({
            type: "POST",
            url: "/" + culture + '/Cart/AddToCart',
            data: {
                id: id,
                languageId: culture,
                quantity: quantity
            },
            success: function (res) {
                loadCart(culture);
                toastr.success('Đã thêm vào giỏ hàng!!!', 'Thông báo');
            },
            error: function (err) {
                toastr.error('Lỗi thêm vào giỏ hàng!!!', 'Thông báo');
            }
        });
    }
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}