$('#categoryTable').DataTable({
    ajax: '/category/getallcategories',
    order: [],
    language: {
        lengthMenu: 'Pokaż _MENU_ kategorii na stronę',
        info: 'Pokazano od _START_ do _END_ z _TOTAL_ kategorii',
        infoEmpty: 'Brak pozycji do wyświetlenia',
        infoFiltered: '(przefiltrowano z _MAX_ kategorii)',
        search: 'Szukaj:',
        zeroRecords: 'Nie znaleziono pasujących kategorii',
        processing: 'Przetwarzanie...',
        paginate: {
            first: 'Pierwsza',
            last: 'Ostatnia',
            next: 'Następna',
            previous: 'Poprzednia'
        }
    },
    columns: [
        {
            data: 'id',
            width: '5%',
            className: 'selectSection',
            orderable: false,
            render: function (data) {
                return `<label class="btn-select-all">
                            <input type="checkbox" name="ids" value="${data}" class="item-checkbox" form="updateCategory" />
                        </label>`;
            }
        },
        {
            data: 'sortOrder',
            width: '5%',
            className: 'dt-center text-center',
            render: function (data, type) {
                if (type !== 'display') return data ?? -1;
                return `<span class="series-sort-badge badge px-2 py-1 rounded-pill">${data ?? '-'}</span>`;
            }
        },
        {
            data: 'name',
            width: '20%',
            className: 'fw-bold'
        },
        {
            data: 'description',
            width: '25%',
            className: 'text-muted small',
            render: function (data, type) {
                if (type !== 'display') return data ?? '';
                if (!data) return '—';
                return data.length > 40 ? data.substring(0, 40) + '...' : data;
            }
        },
        {
            data: 'createdAt',
            width: '20%',
            className: 'text-muted small',
            render: function (data, type) {
                if (type !== 'display') return data;
                return formatDatePl(data);
            }
        },
        {
            data: 'isActive',
            width: '10%',
            className: 'dt-center',
            render: function (data, type) {
                if (type !== 'display') return data;
                return data
                    ? '<span class="badge badge-ap px-2 py-1 rounded-pill">Tak</span>'
                    : '<span class="badge bg-secondary px-2 py-1 rounded-pill">Nie</span>';
            }
        },
        {
            data: 'id',
            width: '15%',
            className: 'text-end pe-3',
            orderable: false,
            render: function (data) {
                return `<div class="series-actions btn-group shadow-sm" role="group">
                            <a href="/Category/Edit/${data}" class="btn btn-sm btn-edit" title="Edytuj">Edytuj</a>
                            <a href="/Category/Delete/${data}" class="btn btn-sm btn-outline-danger" title="Usuń">Usuń</a>
                        </div>`;
            }
        }
    ]
});

function formatDatePl(isoString) {
    if (!isoString) return '';
    var date = new Date(isoString);
    var dni = ['niedz.', 'pon.', 'wt.', 'śr.', 'czw.', 'pt.', 'sob.'];

    var dzien = dni[date.getDay()];
    var dd = String(date.getDate()).padStart(2, '0');
    var mm = String(date.getMonth() + 1).padStart(2, '0');
    var yyyy = date.getFullYear();
    var hh = String(date.getHours()).padStart(2, '0');
    var min = String(date.getMinutes()).padStart(2, '0');

    return `${dzien} ${dd}.${mm}.${yyyy}r. ${hh}:${min}`;
}