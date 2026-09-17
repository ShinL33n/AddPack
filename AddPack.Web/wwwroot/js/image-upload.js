(function () {
    'use strict';
    var MAX_SIZE_MB = 5;
    var ALLOWED_TYPES = ['image/jpg', 'image/jpeg', 'image/png', 'image/webp', 'image/gif'];

    function init(dz, inputId, prevId, contentId, removeId, errId) {
        var dropzone = document.getElementById(dz);
        if (!dropzone) return;
        var input = document.getElementById(inputId);
        var preview = document.getElementById(prevId);
        var content = document.getElementById(contentId);
        var removeBtn = document.getElementById(removeId);
        var errorBox = document.getElementById(errId);

        function showError(msg) { errorBox.textContent = msg; errorBox.classList.remove('d-none'); }
        function clearError() { errorBox.classList.add('d-none'); errorBox.textContent = ''; }

        function validate(file) {
            if (ALLOWED_TYPES.indexOf(file.type) === -1) {
                showError('Dozwolone są tylko pliki obrazów (JPG, JPEG, PNG, WEBP, GIF).');
                return false;
            }
            if (file.size > MAX_SIZE_MB * 1024 * 1024) {
                showError('Plik jest zbyt duży - maksymalnie ' + MAX_SIZE_MB + ' MB.');
                return false;
            }
            clearError();
            return true;
        }

        function handleFiles(files) {
            if (!files || !files.length) return;
            var file = files[0];
            if (!validate(file)) { input.value = ''; return; }

            var reader = new FileReader();
            reader.onload = function (e) {
                preview.src = e.target.result;
                preview.classList.remove('d-none');
                content.classList.add('d-none');
                removeBtn.classList.remove('d-none');
            };
            reader.readAsDataURL(file);
        }

        dropzone.addEventListener('click', function (e) {
            if (e.target !== removeBtn) input.click();
        });
        input.addEventListener('change', function () { handleFiles(input.files); });

        ['dragenter', 'dragover'].forEach(function (evt) {
            dropzone.addEventListener(evt, function (e) {
                e.preventDefault(); dropzone.classList.add('ap-dropzone-active');
            });
        });
        ['dragleave', 'drop'].forEach(function (evt) {
            dropzone.addEventListener(evt, function (e) {
                e.preventDefault(); dropzone.classList.remove('ap-dropzone-active');
            });
        });
        dropzone.addEventListener('drop', function (e) {
            input.files = e.dataTransfer.files;
            handleFiles(e.dataTransfer.files);
        });

        removeBtn.addEventListener('click', function (e) {
            e.stopPropagation();
            input.value = ''; preview.src = '';
            preview.classList.add('d-none'); content.classList.remove('d-none');
            removeBtn.classList.add('d-none'); clearError();
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        init('dropzone', 'fileInput', 'previewImg', 'dropzoneContent', 'removeImageBtn', 'fileError');
    });
})();