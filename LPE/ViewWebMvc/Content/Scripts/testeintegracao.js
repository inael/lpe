$(document).ready(function () {
    $("#button").click(function () {
        $.ajax({
            url: "http://localhost:62341/LPE/Home/LoginMPE",
            //dataType:"JSON",
            type: "post",
            data: { Email: "ricardo@gmail.com", PasswordHash: "yYHZtvpkTfazcgfmBshi6057f20f883e", Nome: "ricardo" },
            traditional: true,
            success: function (data, text) {
                $("html").html(data);
            },
            error: function (response, status, error) {
                alert('error');
            }
        });
    });
});
        