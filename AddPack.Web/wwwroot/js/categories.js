function compareHierarchical(a, b, reverseSiblings) {
    var aParts = String(a).split('|');
    var bParts = String(b).split('|');
    var len = Math.min(aParts.length, bParts.length);

    for (var i = 0; i < len; i++) {
        if (aParts[i] !== bParts[i]) {
            var cmp = aParts[i] < bParts[i] ? -1 : 1;
            return reverseSiblings ? -cmp : cmp;
        }
    }
    // Wspólny prefiks wyczerpany - jeden jest przodkiem drugiego.
    // Przodek ZAWSZE przed potomkiem, niezależnie od kierunku sortowania.
    return aParts.length - bParts.length;
}

$.fn.dataTable.ext.type.order['hierarchical-asc'] = function (a, b) {
    return compareHierarchical(a, b, false);
};
$.fn.dataTable.ext.type.order['hierarchical-desc'] = function (a, b) {
    return compareHierarchical(a, b, true);
};

$('#categoryTable').DataTable({
    ajax: {
        url: '/category/getallcategories',
        dataSrc: function (json) {
            var rows = Array.isArray(json) ? json : json.data;
            computeLevels(rows);
            buildSortKeys(rows, 'name', false);
            buildSortKeys(rows, 'sortOrder', true);   // isNumeric = true!
            buildSortKeys(rows, 'description', false);
            return rows;
        }
    },
    createdRow: function (row, data) {
        if (data.level > 0) {
            var lvl = Math.min(data.level, 5); // limit, żeby nie tworzyć nieskończonej liczby klas
            $(row).addClass('level-' + lvl);
        }
    },
    order: [1, 'asc'],
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
            render: function (data, type, row) {
                var indent = (row.level || 0) * 24;
                return `<label class="btn-select-all" style="margin-left: ${indent}px;">
                            <input type="checkbox" name="ids" value="${data}" class="item-checkbox" form="updateCategory" />
                        </label>`;
            }
        },
        {
            data: 'sortOrder',
            width: '5%',
            type: 'hierarchical',
            render: function (data, type, row) {
                if (type === 'sort') return row.sortKey_sortOrder;
                if (type === 'filter' || type === 'type') return data ?? -1;
                var indent = (row.level || 0) * 24;
                return `<span class="series-sort-badge badge px-2 py-1 rounded-pill" style="margin-left: ${indent}px;">${data ?? '-'}</span>`;
            }
        },
        {
            data: 'name',
            width: '20%',
            className: 'fw-bold',
            type: 'hierarchical',
            render: function (data, type, row) {
                if (type === 'sort') return row.sortKey_name;
                if (type === 'filter' || type === 'type') return data;
                var indent = (row.level || 0) * 24;
                return `<span style="display: inline-block; margin-left: ${indent}px;">${data}</span>`;
            }
        },
        {
            data: 'description',
            width: '25%',
            className: 'text-muted small',
            type: 'hierarchical',
            render: function (data, type, row) {
                if (type === 'sort') return row.sortKey_description;
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
            width: '15%',
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
            width: '10%',
            className: 'text-end pe-3',
            orderable: false,
            render: function (data) {
                return `<div class="series-actions btn-group shadow-sm" role="group">
                            <a href="/Category/Upsert/${data}" class="btn btn-sm btn-edit" title="Edytuj">Edytuj</a>
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

function buildSortKeys(rows, columnKey, isNumeric) {
    var byId = {};
    rows.forEach(function (r) { byId[r.id] = r; });
    var keyProp = 'sortKey_' + columnKey;

    function pad(value) {
        // Zero-padding dla liczb - inaczej string "10" posortuje się PRZED "2"
        return isNumeric ? String(value ?? 0).padStart(10, '0') : String(value ?? '');
    }

    function getSortKey(row, visited) {
        if (row[keyProp]) return row[keyProp];
        visited = visited || {};
        if (visited[row.id]) return pad(row[columnKey]);
        visited[row.id] = true;

        if (!row.parentId || !byId[row.parentId]) {
            row[keyProp] = pad(row[columnKey]);
        } else {
            var parentKey = getSortKey(byId[row.parentId], visited);
            row[keyProp] = parentKey + '|' + pad(row[columnKey]);
        }
        return row[keyProp];
    }

    rows.forEach(function (r) { getSortKey(r); });
    return rows;
}

function computeLevels(rows) {
    var byId = {};
    rows.forEach(function (r) { byId[r.id] = r; });

    function getLevel(row, visited) {
        if (typeof row.level === 'number') return row.level;
        visited = visited || {};
        if (visited[row.id]) return 0;
        visited[row.id] = true;

        if (!row.parentId || !byId[row.parentId]) {
            row.level = 0;
        } else {
            row.level = getLevel(byId[row.parentId], visited) + 1;
        }
        return row.level;
    }

    rows.forEach(function (r) { getLevel(r); });
    return rows;
}