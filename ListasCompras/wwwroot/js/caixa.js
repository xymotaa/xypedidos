document.addEventListener("DOMContentLoaded", function () {
    const cart = [];

    const buscaForm = document.getElementById("buscaForm");
    const buscaInput = document.getElementById("buscaInput");
    const itensBody = document.getElementById("itensBody");
    const vazioState = document.getElementById("vazioState");
    const subtotalLabel = document.getElementById("subtotalLabel");
    const subtotalValor = document.getElementById("subtotalValor");
    const descontoValor = document.getElementById("descontoValor");
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

    function subtotalItem(item) {
        const bruto = item.qtd * item.precoUnitario;
        return bruto * (1 - item.desconto / 100);
    }

    function adicionarItem(produto) {
        const existente = cart.find(function (i) { return i.codigo === produto.codigo; });
        if (existente) {
            existente.qtd += 1;
        } else {
            cart.push({ codigo: produto.codigo, nome: produto.nome, precoUnitario: produto.precoUnitario, qtd: 1, desconto: 0 });
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
                    '<div class="inline-flex items-center gap-xs bg-surface-container-low rounded-full px-xs py-xs">' +
                        '<button type="button" data-index="' + index + '" class="qtd-btn qtd-dec w-6 h-6 rounded-full hover:bg-white flex items-center justify-center text-on-surface-variant font-bold leading-none transition-colors">−</button>' +
                        '<span class="qtd-valor w-5 text-center font-label-md text-label-md font-bold text-primary">' + item.qtd + '</span>' +
                        '<button type="button" data-index="' + index + '" class="qtd-btn qtd-inc w-6 h-6 rounded-full hover:bg-white flex items-center justify-center text-on-surface-variant font-bold leading-none transition-colors">+</button>' +
                    '</div>' +
                '</td>' +
                '<td class="px-md py-sm font-body-md text-body-md text-right">' + formatBRL(item.precoUnitario) + '</td>' +
                '<td class="px-md py-sm text-center">' +
                    '<div class="relative inline-block">' +
                        '<input type="number" min="0" max="100" step="1" value="' + item.desconto + '" data-index="' + index + '" ' +
                        'class="desconto-input w-14 text-center bg-surface-container-low border-none rounded-md py-1 pr-4 font-body-md text-body-md" />' +
                        '<span class="absolute right-1 top-1/2 -translate-y-1/2 font-label-sm text-label-sm text-outline">%</span>' +
                    '</div>' +
                '</td>' +
                '<td class="px-md py-sm font-body-md text-body-md font-semibold text-right subtotal-cell">' + formatBRL(subtotalItem(item)) + '</td>' +
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
        let subtotalBruto = 0;
        let totalDesconto = 0;
        let totalItens = 0;
        cart.forEach(function (item) {
            const bruto = item.qtd * item.precoUnitario;
            subtotalBruto += bruto;
            totalDesconto += bruto * (item.desconto / 100);
            totalItens += item.qtd;
        });
        const total = subtotalBruto - totalDesconto;

        subtotalLabel.textContent = "Subtotal (" + totalItens + (totalItens === 1 ? " item)" : " itens)");
        subtotalValor.textContent = formatBRL(subtotalBruto);
        descontoValor.textContent = "- " + formatBRL(totalDesconto);
        totalValor.textContent = formatBRL(total);

        const recebido = parseFloat(valorRecebido.value) || 0;
        const troco = recebido - total;
        trocoValor.textContent = formatBRL(Math.max(troco, 0));
        trocoValor.classList.toggle("text-error", troco < 0 && metodoSelecionado() === "dinheiro");
        trocoValor.classList.toggle("text-primary", !(troco < 0 && metodoSelecionado() === "dinheiro"));

        const precisaDinheiro = metodoSelecionado() === "dinheiro" && total > 0;
        const podeFinalizar = cart.length > 0 && (!precisaDinheiro || recebido >= total);

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

    itensBody.addEventListener("click", function (e) {
        const removeBtn = e.target.closest(".remove-btn");
        if (removeBtn) {
            const index = parseInt(removeBtn.dataset.index, 10);
            cart.splice(index, 1);
            renderTabela();
            return;
        }

        const qtdBtn = e.target.closest(".qtd-btn");
        if (qtdBtn) {
            const index = parseInt(qtdBtn.dataset.index, 10);
            if (qtdBtn.classList.contains("qtd-inc")) {
                cart[index].qtd += 1;
            } else if (cart[index].qtd > 1) {
                cart[index].qtd -= 1;
            }
            const linha = itensBody.querySelector('tr[data-index="' + index + '"]');
            if (linha) {
                linha.querySelector(".qtd-valor").textContent = cart[index].qtd;
                linha.querySelector(".subtotal-cell").textContent = formatBRL(subtotalItem(cart[index]));
            }
            recalcular();
        }
    });

    itensBody.addEventListener("input", function (e) {
        if (!e.target.matches(".desconto-input")) return;
        const index = parseInt(e.target.dataset.index, 10);
        let valor = parseFloat(e.target.value);
        if (isNaN(valor) || valor < 0) valor = 0;
        if (valor > 100) valor = 100;
        cart[index].desconto = valor;
        const linha = itensBody.querySelector('tr[data-index="' + index + '"] .subtotal-cell');
        if (linha) linha.textContent = formatBRL(subtotalItem(cart[index]));
        recalcular();
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
