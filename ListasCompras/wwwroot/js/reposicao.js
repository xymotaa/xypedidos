document.addEventListener("DOMContentLoaded", function () {
    const categoriaSelect = document.getElementById("categoriaSelect");
    const marcaField = document.getElementById("marcaField");
    const modeloField = document.getElementById("modeloField");
    const marcaSelect = document.getElementById("marcaSelect");
    const modeloSelect = document.getElementById("modeloSelect");
    const produtoSelect = document.getElementById("produtoSelect");

    function popularProdutos(categoriaId) {
        produtoSelect.innerHTML = '<option value="">Selecione...</option>';
        if (!categoriaId) return;
        produtos
            .filter(function (p) { return p.categoriaId == categoriaId; })
            .forEach(function (p) {
                const opt = document.createElement("option");
                opt.value = p.id;
                opt.textContent = p.nome;
                produtoSelect.appendChild(opt);
            });
    }

    function popularModelos(marcaId) {
        modeloSelect.innerHTML = '<option value="">Selecione...</option>';
        if (!marcaId) return;
        modelos
            .filter(function (m) { return m.marcaCelularId == marcaId; })
            .forEach(function (m) {
                const opt = document.createElement("option");
                opt.value = m.id;
                opt.textContent = m.nome;
                modeloSelect.appendChild(opt);
            });
    }

    categoriaSelect.addEventListener("change", function () {
        const selected = categoriaSelect.selectedOptions[0];
        const requerModelo = selected && selected.getAttribute("data-requer-modelo") === "true";

        marcaField.classList.toggle("d-none", !requerModelo);
        modeloField.classList.toggle("d-none", !requerModelo);
        if (!requerModelo) {
            marcaSelect.value = "";
            modeloSelect.innerHTML = '<option value="">Selecione...</option>';
        }

        popularProdutos(categoriaSelect.value);
    });

    marcaSelect.addEventListener("change", function () {
        popularModelos(marcaSelect.value);
    });
});

function editarItem(id, quantidade, observacao) {
    const novaQuantidade = prompt("Nova quantidade:", quantidade);
    if (novaQuantidade === null) return;
    const novaObservacao = prompt("Nova observação:", observacao || "");
    if (novaObservacao === null) return;

    document.getElementById("editarId").value = id;
    document.getElementById("editarQuantidade").value = novaQuantidade;
    document.getElementById("editarObservacao").value = novaObservacao;
    document.getElementById("editarForm").submit();
}
