$('#categoryTable').DataTable({
    ajax: '/category/getallcategories',
    columns: [
        { defaultContent: '' },
        { data: 'sortOrder' },
        { data: 'name' },
        { data: 'description' },
        { data: 'isActive' },
        { data: 'createdAt' },
        { defaultContent: '' }
    ]
});