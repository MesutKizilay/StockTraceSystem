$(function () {

    $('#btnSignIn').on('click', function (e) {
        e.preventDefault(); // Form submit'i engelle

        // Email ve şifre değerlerini al
        var email = $('input[type="email"]').val().trim();
        var password = $('input[type="text"]').val().trim();

        // Boş kontrolü (opsiyonel)
        //if (!email || !password) {
        //    console.warn("Lütfen tüm alanları doldurun.");
        //    return;
        //}

        // AJAX isteği
        $.ajax({
            url: '/Auth/Login',
            type: 'POST',
            data: {
                email: email,
                password: password
            },
            success: function (response) {
                // Giriş başarılıysa yönlendir
                console.log("Giriş başarılı:", response);
                window.location.href = '/';
            },
            error: function (xhr) {
                try {
                    var errorResponse = xhr.responseJSON || JSON.parse(xhr.responseText);

                    if (errorResponse.errors && errorResponse.errors.length > 0)
                    {
                        console.warn("Hata:", errorResponse.errors[0]);
                    }
                    else if (errorResponse.detail)
                    {
                        console.warn("Hata:", errorResponse.detail);
                    }
                    else
                    {
                        console.warn("Bilinmeyen hata:", xhr.responseText);
                    }
                }
                catch (e)
                {
                    console.error("Hata parse edilemedi:", xhr.responseText);
                }
            }
        });
    });
});