$(function () {
    // 1) Demo veri (Logo'dan servis gelene kadar)
    const inventoryData = [
        { code: 'ABC123', name: 'Somun M6', stock: 250, counted: 0, _rank: 0 },
        { code: 'ABC124', name: 'Cıvata M6x20', stock: 180, counted: 0, _rank: 0 },
        { code: 'XYZ001', name: 'Palet 80x120', stock: 35, counted: 0, _rank: 0 },
        { code: 'XYZ002', name: 'Koli 40x40x40', stock: 90, counted: 0, _rank: 0 },
        { code: 'BRK-789', name: 'Barkod Etiketi 58x40', stock: 500, counted: 0, _rank: 0 },
        { code: 'SHP555', name: 'Streç Film', stock: 120, counted: 0, _rank: 0 },
    ];

    // Kod normalize: boşlukları sil, büyük harfe çevir
    const norm = s => String(s ?? '').trim().toUpperCase();

    let scanSeq = 0;

    // 2) DataTable
    const table = $('#invTable').DataTable({
        dom:
            "<'dt--top-section'<'row'<'col-6 col-sm-6 d-flex justify-content-sm-start justify-content-start'l>" +
            "<'col-6 col-sm-6 d-flex justify-content-sm-end justify-content-end mt-sm-0 mt-0'f>>>" +
            "<'table-responsive'tr>" +
            "<'dt--bottom-section d-sm-flex justify-content-sm-between text-center'<'dt--pages-count mb-sm-0 mb-3'i>" +
            "<'dt--pagination'p>>",

        // --- Dil/ikonlar
        oLanguage: {
            oPaginate: {
                sPrevious:
                    '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>',
                sNext:
                    '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>'
            },
            sInfo: "Showing page _PAGE_ of _PAGES_",
            sSearch:
                '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            sSearchPlaceholder: "Ara...",
            sLengthMenu: "_MENU_"
        },

        data: inventoryData,
        rowId: row => 'code_' + norm(row.code), // satırlara stabil id veriyoruz
        pageLength: 10,
        lengthMenu: [10, 25, 50],
        searching: true,
        ordering: true,
        order: [[4, 'desc']],
        columns: [
            { data: 'code', title: 'Ürün Kodu' },
            { data: 'name', title: 'Ürün Adı' },
            { data: 'stock', title: 'Mevcut Stok', className: 'text-end' },
            { data: 'counted', title: 'Sayım', className: 'text-end' },
            { data: '_rank', title: '_rank', visible: false } // gizli
        ]
    });

    // 3) Barkod/ürün kodu okutma
    $('#scanInput').on('keyup', function (e) {
        console.log("Key:", e.key);
        console.log("KeyCode:", e.keyCode);
        console.log("Code:", e.code);

        if (e.key === 'Enter' || e.keyCode === 13) {
            e.preventDefault();
            const raw = $(this).val();
            $(this).val(''); // inputu temizle

            const code = norm(raw);
            if (!code) return;

            // DataTables rowId ile hızlı bul
            const row = table.row('#code_' + code);

            if (row.any()) {
                const data = row.data();
                data.counted = (data.counted || 0) + 1;

                data._rank = ++scanSeq;
                console.log("scanSeq", scanSeq);

                row.data(data).draw(false); // aynı sayfada kal

                // Görsel feedback: satırı kısa süre vurgula
                const $tr = $(row.node());
                $tr.addClass('flash-success');
                setTimeout(() => $tr.removeClass('flash-success'), 900);
            }
            else {
                Toast.fire({
                    icon: 'warning',
                    title: 'Ürün listede bulunamadı.'
                });
            }
        }
    });
});
