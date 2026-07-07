document.addEventListener("DOMContentLoaded", function () {
    const cart = [];

    const buscaForm = document.getElementById("buscaForm");
    const buscaInput = document.getElementById("buscaInput");
    const itensBody = document.getElementById("itensBody");
    const vazioState = document.getElementById("vazioState");
    const subtotalLabel = document.getElementById("subtotalLabel");
    const subtotalValor = document.getElementById("subtotalValor");
    const totalValor = document.getElementById("totalValor");
    const trocoValor = document.getElementById("trocoValor");
    const valorRecebido = document.getElementById("valorRecebido");
    const painelDinheiro = document.getElementById("painelDinheiro");
    const finalizarBtn = document.getElementById("finalizarBtn");
    const toast = document.getElementById("toast");
    const toastMsg = document.getElementById("toastMsg");

    function formatBRL(valor) {
        return "R$ " + valor.toLocaleString("pt-BR", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    function metodoSelecionado() {
        const marcado = document.querySelector('input[name="metodo"]:checked');
        return marcado ? marcado.value : "dinheiro";
    }

    function atualizarSelecaoPagamento() {
        document.querySelectorAll(".pagamento-opcao").forEach(function (label) {
            const input = label.querySelector('input[type="radio"]');
            label.classList.toggle("border-secondary", input.checked);
            label.classList.toggle("bg-secondary-container/10", input.checked);
        });
        painelDinheiro.classList.toggle("hidden", metodoSelecionado() !== "dinheiro");
    }

    function buscarProduto(termo) {
        const alvo = termo.trim().toLowerCase();
        if (!alvo) return null;
        return PRODUTOS.find(function (p) {
            return p.codigo.toLowerCase() === alvo || p.nome.toLowerCase().includes(alvo);
        }) || null;
    }

    function adicionarItem(produto) {
        const existente = cart.find(function (i) { return i.codigo === produto.codigo; });
        if (existente) {
            existente.qtd += 1;
        } else {
            cart.push({ codigo: produto.codigo, nome: produto.nome, precoUnitario: produto.precoUnitario, qtd: 1 });
        }
        renderTabela();
    }

    function renderTabela() {
        itensBody.innerHTML = "";
        vazioState.classList.toggle("hidden", cart.length > 0);

        cart.forEach(function (item, index) {
            const tr = document.createElement("tr");
            tr.className = "border-t border-outline-variant row-in";
            tr.dataset.index = String(index);
            tr.innerHTML =
                '<td class="px-md py-sm font-body-md text-body-md text-on-surface-variant">' + String(index + 1).padStart(3, "0") + '</td>' +
                '<td class="px-md py-sm font-body-md text-body-md text-on-surface-variant">' + item.codigo + '</td>' +
                '<td class="px-md py-sm font-body-md text-body-md text-on-surface font-semibold">' + item.nome + '</td>' +
                '<td class="px-md py-sm text-center">' +
                    '<input type="number" min="1" value="' + item.qtd + '" data-index="' + index + '" ' +
                    'class="qtd-input w-16 text-center bg-surface-container-low border-none rounded-md py-1 font-body-md text-body-md" />' +
                '</td>' +
                '<td class="px-md py-sm font-body-md text-body-md text-right">' + formatBRL(item.precoUnitario) + '</td>' +
                '<td class="px-md py-sm font-body-md text-body-md font-semibold text-right subtotal-cell">' + formatBRL(item.qtd * item.precoUnitario) + '</td>' +
                '<td class="px-md py-sm text-center">' +
                    '<button type="button" data-index="' + index + '" class="remove-btn w-8 h-8 rounded-full hover:bg-error-container text-error inline-flex items-center justify-center transition-colors">' +
                        '<span class="material-symbols-outlined text-[18px]">delete</span>' +
                    '</button>' +
                '</td>';
            itensBody.appendChild(tr);
        });

        recalcular();
    }

    function recalcular() {
        let subtotal = 0;
        let totalItens = 0;
        cart.forEach(function (item) {
            subtotal += item.qtd * item.precoUnitario;
            totalItens += item.qtd;
        });

        subtotalLabel.textContent = "Subtotal (" + totalItens + (totalItens === 1 ? " item)" : " itens)");
        subtotalValor.textContent = formatBRL(subtotal);
        totalValor.textContent = formatBRL(subtotal);

        const recebido = parseFloat(valorRecebido.value) || 0;
        const troco = recebido - subtotal;
        trocoValor.textContent = formatBRL(Math.max(troco, 0));
        trocoValor.classList.toggle("text-error", troco < 0 && metodoSelecionado() === "dinheiro");
        trocoValor.classList.toggle("text-primary", !(troco < 0 && metodoSelecionado() === "dinheiro"));

        const precisaDinheiro = metodoSelecionado() === "dinheiro" && subtotal > 0;
        const podeFinalizar = cart.length > 0 && (!precisaDinheiro || recebido >= subtotal);

        finalizarBtn.disabled = !podeFinalizar;
        finalizarBtn.classList.toggle("bg-primary", podeFinalizar);
        finalizarBtn.classList.toggle("text-white", podeFinalizar);
        finalizarBtn.classList.toggle("hover:bg-opacity-90", podeFinalizar);
        finalizarBtn.classList.toggle("cursor-pointer", podeFinalizar);
        finalizarBtn.classList.toggle("bg-outline-variant", !podeFinalizar);
        finalizarBtn.classList.toggle("text-outline", !podeFinalizar);
        finalizarBtn.classList.toggle("cursor-not-allowed", !podeFinalizar);
    }

    function mostrarToast(mensagem) {
        toastMsg.textContent = mensagem;
        toast.classList.remove("hidden");
        window.clearTimeout(mostrarToast._timer);
        mostrarToast._timer = window.setTimeout(function () {
            toast.classList.add("hidden");
        }, 3000);
    }

    buscaForm.addEventListener("submit", function (e) {
        e.preventDefault();
        const produto = buscarProduto(buscaInput.value);
        if (!produto) {
            buscaInput.classList.add("ring-2", "ring-error");
            window.setTimeout(function () { buscaInput.classList.remove("ring-2", "ring-error"); }, 1200);
            return;
        }
        adicionarItem(produto);
        buscaInput.value = "";
        buscaInput.focus();
    });

    itensBody.addEventListener("input", function (e) {
        if (!e.target.matches(".qtd-input")) return;
        const index = parseInt(e.target.dataset.index, 10);
        let valor = parseInt(e.target.value, 10);
        if (!valor || valor < 1) valor = 1;
        cart[index].qtd = valor;
        const tr = itensBody.querySelector('tr[data-index="' + index + '"] .subtotal-cell');
        if (tr) tr.textContent = formatBRL(cart[index].qtd * cart[index].precoUnitario);
        recalcular();
    });

    itensBody.addEventListener("click", function (e) {
        const btn = e.target.closest(".remove-btn");
        if (!btn) return;
        const index = parseInt(btn.dataset.index, 10);
        cart.splice(index, 1);
        renderTabela();
    });

    document.querySelectorAll('input[name="metodo"]').forEach(function (input) {
        input.addEventListener("change", function () {
            atualizarSelecaoPagamento();
            recalcular();
        });
    });

    valorRecebido.addEventListener("input", recalcular);

    finalizarBtn.addEventListener("click", function () {
        if (finalizarBtn.disabled) return;
        const totalFinalizado = totalValor.textContent;
        mostrarToast("Venda finalizada: " + totalFinalizado + " (exemplo, ainda não gravado no banco de dados).");
        cart.length = 0;
        valorRecebido.value = "";
        renderTabela();
    });

    document.addEventListener("keydown", function (e) {
        if (e.key === "F2") {
            e.preventDefault();
            buscaInput.focus();
        }
    });

    atualizarSelecaoPagamento();
    renderTabela();
});
