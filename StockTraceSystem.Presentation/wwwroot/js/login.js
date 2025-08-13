$(function () {

    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 30000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });

    $('#btnSignIn').on('click', function (e) {
        e.preventDefault();

        var email = $('input[type="email"]').val().trim();
        var password = $('input[type="password"]').val().trim();

        $.ajax({
            url: '/Auth/Login',
            type: 'POST',
            data: {
                email: email,
                password: password
            },
            success: function (response) {
                Toast.fire({ icon: 'success', title: 'Giriş Başarılı' });

                setTimeout(() => { window.location.href = '/Home/Index'; }, 500);            },
            error: function (xhr) {

                //var errorResponse = xhr.responseJSON || JSON.parse(xhr.responseText);

                //console.log("xhr", xhr);
                //console.log("xhr.responseText", xhr.responseText);
                //console.log("xhr.responseJSON", xhr.responseJSON);

                //if (errorResponse.Errors && errorResponse.Errors.length > 0) {
                //    var errors = errorResponse.Errors[0].Errors99;
                //    //
                //    // HTML olarak alt alta listele
                //    var html = errors.map(function (err) {
                //        return '<li>' + err + '</li>';
                //    }).join('');

                //    Toast.fire({
                //        icon: 'error',
                //        title: 'Hata',
                //        html: html
                //        //text: errorResponse.Errors[0].Errors99[0]
                //    });
                //}
                //else if (errorResponse.detail) {
                //    Toast.fire({
                //        icon: 'error',
                //        title: 'Hata',
                //        html: '<li>' + errorResponse.detail + '</li>'
                //    });
                //}
                //else {
                //    console.warn("Bilinmeyen hata:", xhr.responseText);
                //}

                parseErrorResponse(xhr);
            }
        });
    });


});