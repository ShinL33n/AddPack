document.addEventListener('DOMContentLoaded', function () {
    const quillEl = document.getElementById('quill-editor');
    const textAreaDescription = document.querySelector('textarea[name="Category.Description"]');

    if (!quillEl || !textAreaDescription) return;

    const quill = new Quill('#quill-editor', {
        theme: 'snow',
        modules: {
            toolbar: [
                [{ header: [1, 2, 3, false] }],
                ['bold', 'italic', 'underline', 'strike'],
                ['blockquote', 'code-block'],
                [{ list: 'ordered' }, { list: 'bullet' }],
                ['link'],
                ['clean']
            ]
        }
    });

    if (textAreaDescription.value) quill.root.innerHTML = textAreaDescription.value;

    quill.on('text-change', () => textAreaDescription.value = quill.root.innerHTML);
});
