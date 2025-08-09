$(function () {

    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });

    Toast.fire({
        icon: 'success',
        title: 'Signed in successfully',
        //theme:'dark'
    })


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

                    console.log("xhr", xhr);
                    console.log("xhr.responseText", xhr.responseText);
                    console.log("xhr.responseJSON", xhr.responseJSON);

                    if (errorResponse.Errors && errorResponse.Errors.length > 0) {
                        console.warn("ifHata:", errorResponse.Errors[0].Errors99);
                    }
                    else if (errorResponse.detail) {
                        console.warn("elseifHata:", errorResponse.detail);
                    }
                    else {
                        console.warn("Bilinmeyen hata:", xhr.responseText);
                    }
                }
                catch (e) {
                    console.error("Hata parse edilemedi:", xhr.responseText);
                }
            }
        });
    });
});