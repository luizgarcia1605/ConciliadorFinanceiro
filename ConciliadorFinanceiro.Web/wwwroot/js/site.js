$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}
$.validator.methods.date = function (value, element) {
    var date = value.split("/");
    return this.optional(element) || !/Invalid|NaN/.test(new Date(date[2], date[1], date[0]).toString());
}

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