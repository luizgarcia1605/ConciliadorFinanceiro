function PesquisarLancamentoPorId() {

    var idtxt = document.getElementById('txtPesquisar').value;

    debugger;
    $.ajax({
        url: "/LancamentoFinanceiro/Detalhar/" + idtxt,
        type: "GET",
        dataType: 'html',
        success: function () { },
        error: function (xhr) {
            console.log(xhr);
        }
    });

    window.location.href = "/LancamentoFinanceiro/Detalhar/" + document.getElementById('txtPesquisar').value;
};