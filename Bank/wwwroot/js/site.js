$("#search").on("click", function (e) {
    window.location.href = 'Customer/searchCustomer/' + $("#searchvalue").val();
});