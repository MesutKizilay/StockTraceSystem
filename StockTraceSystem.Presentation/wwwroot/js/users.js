$(function () {
    var claim = window.claim;
    //console.log("claim", claim);

    const dt = $('#usersTable').DataTable({

        dom: "<'dt--top-section'<'row'<'col-6 col-sm-6 d-flex justify-content-sm-start justify-content-start'l><'col-6 col-sm-6 d-flex justify-content-sm-end justify-content-end mt-sm-0 mt-0'f>>>" +
            "<'table-responsive'tr>" +
            "<'dt--bottom-section d-sm-flex justify-content-sm-between text-center'<'dt--pages-count  mb-sm-0 mb-3'i><'dt--pagination'p>>",
        oLanguage: {
            "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>' },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            "sSearchPlaceholder": "Search...",
            "sLengthMenu": "_MENU_",
        },
        //stripeClasses: [],
        pageLength: 10,
        order: [[0, 'asc']],
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        lengthMenu: [10, 25, 50],
        ajax: function (dtReq, callback) {
            var page = Math.floor(dtReq.start / dtReq.length);
            var pageSize = dtReq.length;

            $.ajax({
                url: '/Users/GetList',
                type: 'POST',
                data: {
                    'Index': page,
                    'Size': pageSize
                },
                success: function (res) {
                    // Beklenen örnek response: { items:[...], count:123, page:1, size:10 }
                    console.log("res", res);
                    var items = res.items || [];
                    //var total = res.count ?? res.totalCount ?? items.length;
                    //console.log("res.count", res.count);
                    //console.log("res.totalCount", res.totalCount);
                    //console.log("items.length", items.length);
                    //console.log("total", total);

                    callback({
                        draw: dtReq.draw,
                        recordsTotal: res.noOfItem,
                        recordsFiltered: res.noOfItem,
                        //recordsFiltered: res.noOfItemInAPage,
                        data: items
                    });
                },
                error: function (xhr) {
                    console.log("xhr.responseText", xhr.responseText);

                    callback({ draw: dtReq.draw, recordsTotal: 0, recordsFiltered: 0, data: [] });
                }
            });
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'firstName', title: 'Ad' },
            { data: 'lastName', title: 'Soyad' },
            { data: 'email', title: 'E‑posta' },
            { data: 'password', title: 'Şifre' },
            //{
            //    data: 'operationClaim.name',
            //    title: 'Rol'
            //},
            //{
            //    data: 'operationClaim',
            //    title: 'Rol',
            //    render: function (oc) {
            //        if (!oc) return '';  // boşsa - yaz
            //        return oc.name;       // varsa adı yaz
            //    }
            //},
            {
                data: 'userOperationClaims',
                title: 'Rol',
                render: function (arr, type, row) {
                    if (!Array.isArray(arr) || arr.length === 0)
                        return '';

                    const names = arr.map(x => x?.operationClaim?.name)
                        .filter(Boolean);

                    return names.length ? names.join(', ') : '';
                }
            },
            {
                data: 'status',
                title: 'Durum',
                render: function (d) {
                    //console.log("d:", d);
                    return d === true
                        ? '<span class="badge bg-success">Aktif</span>'
                        : '<span class="badge bg-danger">Pasif</span>';
                }
            },
            {
                data: null,
                title: 'İşlem',
                orderable: false,
                searchable: false,
                //className: 'text-center action-col',
                render: function (data, type, row) {
                    return `
            <div class="dt-actions">
              <a href="#" class="btn-action edit" data-id="${row.id}" title="Düzenle">
                  <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"
                       viewBox="0 0 24 24" fill="none" stroke="currentColor"
                       stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
                       class="feather feather-edit-2">
                    <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z"></path>
                  </svg>
              </a>
              <a href="#" class="btn-action delete" data-id="${row.id}" title="Sil">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24"
                     viewBox="0 0 24 24" fill="none" stroke="currentColor"
                     stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
                     class="feather feather-trash-2">
                  <polyline points="3 6 5 6 21 6"></polyline>
                  <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                  <line x1="10" y1="11" x2="10" y2="17"></line>
                  <line x1="14" y1="11" x2="14" y2="17"></line>
                </svg>
              </a>
            </div>`;
                }
            }
        ],

        initComplete: function () {
            if (claim !== "Supervisor") {
            //if (claim === "Depocu") {
                dt.column(4).visible(false);
                dt.column(5).visible(false);
                dt.column(7).visible(false);
            }
            else {
                // Filtre inputunun olduğu sağ üst bölüme buton ekle
                var $right = $('.dt--top-section .row .col-6.col-sm-6.d-flex.justify-content-sm-end');
                if ($right.length) {
                    var btnHtml = `
                        <button type="button" id="btnAddUser" class="btn btn-primary ms-2">
                          + Yeni Kullanıcı
                        </button>`;
                    $right.append(btnHtml);
                }
                else {
                    // Olur da custom DOM değişirse, tablo üstüne fallback olarak ekle
                    $('#usersTable').before('<div class="d-flex justify-content-end mb-2"><button type="button" id="btnAddUser" class="btn btn-primary">+ Yeni Kullanıcı</button></div>');
                }
            }
        }
    });

    $.ajax({
        url: '/OperationClaims/GetList',
        method: 'GET',
        success: function (data) {
            var $addSelect = $('#add-claim');
            $addSelect.empty();
            var $editSelect = $('#edit-claim');
            $editSelect.empty();

            // Eğer default seçili boş option istersen:
            $addSelect.append('<option value="">Seçiniz</option>');
            $editSelect.append('<option value="">Seçiniz</option>');

            // Listeyi doldur
            $.each(data, function (i, item) {
                $addSelect.append($('<option>', {
                    value: item.id,       // backend’den dönen id alanı
                    text: item.name       // backend’den dönen rol adı
                }));

                $editSelect.append($('<option>', {
                    value: item.id,       // backend’den dönen id alanı
                    text: item.name       // backend’den dönen rol adı
                }));
            });
        },
        error: function (xhr) {
            console.error("Rol listesi alınamadı", xhr);
        }
    });

    $('#usersTable').on('click', '.btn-action.edit', function (e) {
        e.preventDefault();

        let $tr = $(this).closest('tr');
        if ($tr.hasClass('child')) $tr = $tr.prev('.parent');
        const row = dt.row($tr).data();
        if (!row) return;

        $('#edit-id').val(row.id);
        $('#edit-firstName').val(row.firstName ?? '');
        $('#edit-lastName').val(row.lastName ?? '');
        $('#edit-email').val(row.email ?? '');
        $('#edit-status').val(String(row.status));
        $('#edit-password').val(row.password ?? '');

        console.log("row", row);
        console.log("row.userOperationClaims", row.userOperationClaims);
        //if (row.operationClaim) {
        //    //console.log("row.operationClaim", row.operationClaim);
        //    $('#edit-claim').val(row.operationClaim.id);
        //}
        //else
        //    $('#edit-claim').val('');

        let uocId = null;
        let currentClaimId = null;
        if (Array.isArray(row.userOperationClaims) && row.userOperationClaims.length > 0) {
            //const currentClaimId = row.userOperationClaims[0].operationClaimId;
            //$('#edit-claim').val(String(currentClaimId));
            uocId = row.userOperationClaims[0].id;                  // UserOperationClaim tablosundaki id
            currentClaimId = row.userOperationClaims[0].operationClaimId;
        }
        //else {
        //    $('#edit-claim').val('');
        //}

        $('#edit-uoc-id').val(uocId || '');        // yoksa boş bırak
        $('#edit-claim').val(currentClaimId ? String(currentClaimId) : '');

        $('#editUserModal').modal('show');
    });

    // Kaydet -> AJAX update
    $('#editUserForm').on('submit', function (e) {
        e.preventDefault();

        $('#edit-saveBtn').prop('disabled', true);
        $('#edit-spinner').removeClass('d-none');

        var claimId = $('#edit-claim').val();
        var uocId = $('#edit-uoc-id').val();
        console.log("claimId", claimId);
        console.log("uocId", uocId);


        const updateUserCommand = {
            id: $('#edit-id').val(),
            firstName: $('#edit-firstName').val(),
            lastName: $('#edit-lastName').val(),
            email: $('#edit-email').val(),
            status: $('#edit-status').val() === 'true',
            password: $('#edit-password').val(),
            userOperationClaims: claimId
                ? [{
                    operationClaimId: parseInt(claimId, 10),
                    id: uocId ? parseInt(uocId, 10) : 0,
                }]
                : []
        };

        $.ajax({
            url: '/Users/Update',
            type: 'POST',
            data: updateUserCommand,
            success: function (res) {
                Toast.fire({ icon: 'success', title: 'Kullanıcı başarıyla güncellendi.' });

                dt.ajax.reload(null, false);
                //const el = document.getElementById('editUserModal');
                //bootstrap.Modal.getInstance(el)?.hide();

                $('#editUserModal').modal('hide');
            },
            error: function (xhr) {
                //const msg = xhr.responseJSON?.message || xhr.responseText || 'Güncelleme başarısız.';
                //console.log("msg", msg);
                //Toast.fire({ icon: 'error', title: 'Güncelleme başarısız.' });
                parseErrorResponse(xhr);
            },
            complete: function () {
                $('#edit-saveBtn').prop('disabled', false);
                $('#edit-spinner').addClass('d-none');
            }
        });
    });

    $(document).on('click', '#btnAddUser', function () {
        // formu sıfırla
        $('#addUserForm')[0].reset();
        $('#addUserModal').modal('show');
    });

    // Kaydet (Add) – AJAX
    $('#addUserForm').on('submit', function (e) {
        e.preventDefault();

        // spinner on
        $('#add-saveBtn').prop('disabled', true);
        $('#add-spinner').removeClass('d-none');

        var claimId = $('#add-claim').val();
        console.log("claimIds", claimId);

        var user = {
            firstName: $('#add-firstName').val(),
            lastName: $('#add-lastName').val(),
            email: $('#add-email').val(),
            password: $('#add-password').val(),
            status: $('#add-status').val() === 'true',
            userOperationClaims: claimId ? [{ operationClaimId: parseInt(claimId, 10) }] : []
        };

        $.ajax({
            url: '/Users/Create',
            type: 'POST',
            data: user,
            success: function (res) {
                Toast.fire({ icon: 'success', title: 'Kullanıcı başarıyla kaydedildi.' });
                dt.ajax.reload(null, false);
                $('#addUserModal').modal('hide');
            },
            error: function (xhr) {
                parseErrorResponse(xhr);
            },
            complete: function () {
                $('#add-saveBtn').prop('disabled', false);
                $('#add-spinner').addClass('d-none');
            }
        });
    });

    $(document).on('click', '.btn-action.delete', function (e) {
        e.preventDefault();

        const id = $(this).data('id');

        Swal.fire({
            title: 'Kullanıcıyı silmek istediğinizden emin miniz?',
            text: 'Kullanıcı pasifleştirildikten sonra sisteme giriş yapamayacaktır.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sil',
            cancelButtonText: 'Vazgeç',
            reverseButtons: true,
            focusCancel: true
        }).then(function (result) {
            if (!result.isConfirmed) return;

            // İstek atılırken küçük bir loading göstermek istersen:
            Swal.fire({ title: 'Siliniyor...', didOpen: () => Swal.showLoading(), allowOutsideClick: false, allowEscapeKey: false });

            $.ajax({
                url: '/Users/Delete',
                type: 'POST',
                data: { id: id },
                success: function () {
                    Swal.close();

                    Toast.fire({ icon: 'success', title: 'Kullanıcı başarıyla pasifleştirildi.' });

                    dt.ajax.reload(null, false);
                },
                error: function (xhr) {
                    Swal.close();
                    parseErrorResponse(xhr);
                }
            });
        });
    });
});