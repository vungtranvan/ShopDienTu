var CartController = function () {
    this.initialize = function () {
        var culture = $('#hideCulture').val();
        loadData(culture);
        registerEvents(culture);
    }

    function registerEvents(culture) {
        $('body').on('click', '.btn-plus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quantity_' + id).val()) + 1;
            updateCart(culture, id, quantity);
        });

        $('body').on('click', '.btn-minus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quantity_' + id).val()) - 1;
            updateCart(culture, id, quantity);
        });
        $('body').on('click', '.btn-remove', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            updateCart(culture, id, 0);
        });
    }
    function updateCart(culture, id, quantity) {
        $.ajax({
            type: "POST",
            url: "/" + culture + '/Cart/UpdateCart',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                loadData(culture);
                toastr.success('Cập nhật giỏ hàng thành công', 'Thông báo');
            },
            error: function (err) {
                toastr.error('Cập nhật giỏ hàng thất bại', 'Thông báo');
            }
        });
    }
    function loadData(culture) {
        $.ajax({
            type: "GET",
            url: "/" + culture + '/Cart/GetListItems',
            success: function (res) {
                if (res.length === 0) {
                    $('#tbl_cart').html('<p>Giỏ hàng trống</p>');
                }
                var html = '';
                var total = 0;
                var quantityItem = 0;
                $.each(res, function (i, item) {
                    var amount = item.price * item.quantity;
                    html += "<tr>"
                        + "<td>" + item.name + "</td>"
                        + "<td> <img width=\"60\" src=\"" + item.image + "\" alt=\"\" /></td>"
                        + "<td>" + item.description + "</td>"
                        + "<td><div class=\"input-append\"><input class=\"span1\" style=\"max-width: 34px\" placeholder=\"1\" id=\"txt_quantity_" + item.productId + "\" value=\"" + item.quantity + "\" size=\"16\" type=\"text\">"
                        + "<button class=\"btn btn-minus\" data-id=\"" + item.productId + "\" type =\"button\"> <i class=\"icon-minus\"></i></button>"
                        + "<button class=\"btn btn-plus\" type=\"button\" data-id=\"" + item.productId + "\"><i class=\"icon-plus\"></i></button>"
                        + "<button class=\"btn btn-danger btn-remove\" type=\"button\" data-id=\"" + item.productId + "\"><i class=\"icon-remove icon-white\"></i></button>"
                        + "</div>"
                        + "</td>"

                        + "<td>" + numberWithCommas(item.price) + "</td>"
                        + "<td>" + numberWithCommas(amount) + "</td>"
                        + "</tr>";
                    total += amount;
                    quantityItem += item.quantity;
                });
                $('#cart_body').html(html);
                $('.lbl_number_of_items').text(quantityItem);
                $('#lbl_total').text(numberWithCommas(total));
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}
