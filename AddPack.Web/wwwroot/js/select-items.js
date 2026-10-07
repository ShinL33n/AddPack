document.getElementById('select-all').addEventListener('click', function () {
    const checkboxes = document.querySelectorAll('input[name="ids"]');
    const icon = this;

    const allChecked = Array.from(checkboxes).every(cb => cb.checked);

    checkboxes.forEach(cb => {
        cb.checked = !allChecked
        cb.dispatchEvent(new Event('change', { bubbles: true }));
    });

    icon.classList.toggle('bi-square', allChecked);
    icon.classList.toggle('bi-check-square', !allChecked);
});