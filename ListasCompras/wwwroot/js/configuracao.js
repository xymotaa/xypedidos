document.addEventListener("DOMContentLoaded", function () {
    const logoInput = document.getElementById("logoInput");
    const uploadArea = document.getElementById("uploadArea");
    const previewLogoBox = document.getElementById("previewLogoBox");
    const nomeLoja = document.getElementById("nomeLoja");
    const previewNome = document.getElementById("previewNome");

    logoInput.addEventListener("change", function () {
        const file = logoInput.files[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = function (e) {
            uploadArea.innerHTML = '<img src="' + e.target.result + '" alt="Logo" class="logo-preview" />';
            previewLogoBox.innerHTML = '<img src="' + e.target.result + '" alt="Logo" />';
        };
        reader.readAsDataURL(file);
    });

    nomeLoja.addEventListener("input", function () {
        previewNome.textContent = nomeLoja.value || "Nome da Loja";
    });
});
