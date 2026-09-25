// Auto-generate slug from name field in real-time
const nameInput = document.getElementById('Category_Name');
const slugInput = document.getElementById('Category_Slug');
nameInput.addEventListener('input', () => {
    if (!slugInput.dataset.userEdited) {
        slugInput.value = generateSlug(nameInput.value);
    }
});
slugInput.addEventListener('input', () => {
    slugInput.dataset.userEdited = true;
});
function generateSlug(text) {
    return text.toString().toLowerCase()
        .replace(/\s+/g, '-')           // Replace spaces with -
        .replace(/[^\w\-]+/g, '')       // Remove all non-word chars
        .replace(/\-\-+/g, '-')         // Replace multiple - with single -
        .replace(/^-+/, '')             // Trim - from start of text
        .replace(/-+$/, '');            // Trim - from end of text
}