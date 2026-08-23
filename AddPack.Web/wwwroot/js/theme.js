// Obsługa przełącznika jasny/ciemny.
// Uwaga: samo WSTĘPNE ustawienie motywu (żeby uniknąć "mignięcia" złym
// motywem przy ładowaniu strony) dzieje się w małym inline <script> w
// _Layout.cshtml - musi wykonać się zanim przeglądarka narysuje stronę,
// więc nie może czekać na wczytanie tego pliku. Ten plik odpowiada tylko
// za interakcję: kliknięcie przycisku i reakcję na zmianę motywu systemu.
(function () {
    'use strict';

    var STORAGE_KEY = 'addpack-theme';

    function getStoredTheme() {
        return localStorage.getItem(STORAGE_KEY);
    }

    function setStoredTheme(theme) {
        localStorage.setItem(STORAGE_KEY, theme);
    }

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
        document.querySelectorAll('[data-theme-icon]').forEach(function (el) {
            el.style.display = el.getAttribute('data-theme-icon') === theme ? 'none' : 'inline-block';
        });
    }

    function toggleTheme() {
        var current = document.documentElement.getAttribute('data-bs-theme');
        var next = current === 'dark' ? 'light' : 'dark';
        setStoredTheme(next);
        applyTheme(next);
    }

    document.addEventListener('DOMContentLoaded', function () {
        // Motyw jest już ustawiony przez inline-skrypt w <head>;
        // tu tylko dopasowujemy widoczność ikon słońce/księżyc.
        applyTheme(document.documentElement.getAttribute('data-bs-theme'));

        var toggleBtn = document.querySelector('[data-theme-toggle]');
        if (toggleBtn) {
            toggleBtn.addEventListener('click', toggleTheme);
        }

        window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
            if (!getStoredTheme()) {
                applyTheme(e.matches ? 'dark' : 'light');
            }
        });
    });
})();
