const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 20000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

function parseErrorResponse(xhr) {
    let msgs = [];

    try {
        let errObj = xhr.responseJSON || JSON.parse(xhr.responseText);

        if (errObj.Errors && Array.isArray(errObj.Errors)) {
            errObj.Errors.forEach(e => {
                if (Array.isArray(e.Errors99)) {
                    msgs = msgs.concat(e.Errors99);
                }
            });
        }
        else if (errObj.detail) {
            msgs.push(errObj.detail);
        }
        else if (typeof errObj === "string") {
            msgs.push(errObj);
        }
    }
    catch (e) {
        console.log(e);
        msgs.push("Bilinmeyen bir hata oluştu.");
    }

    // Hataları alt alta HTML olarak bastır
    let html = msgs.map(m => `<li>${m}</li>`).join("");

    Toast.fire({
        icon: 'error',
        title: 'Hata',
        html: `<ul style="margin:0;padding-left:18px;">${html}</ul>`
    });
}