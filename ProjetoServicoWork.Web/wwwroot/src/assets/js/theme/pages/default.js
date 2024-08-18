this.contLoading = 0;

function loading(oop) {
    if (oop === true) {
       $("#spinner").modal('show');
        this.contLoading += 1;
    } else {
        this.contLoading -= 1;
        if (this.contLoading <= 0) {
            setTimeout(() => {
                $("#spinner").modal('hide');
            }, 600);
        }
    }
}

function ValidateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

jQuery(document).ready(function () {
    if ($(".mask").length !== 0) {
        $(".maskDateYearMonth").inputmask("99/99");
        $(".maskPhone").inputmask("0000-00009");
        $(".maskDdd").inputmask("99");
        $(".maskCellPhone").inputmask("(99)99999-9999");
        $(".maskCpf").inputmask("999.999.999-99");
        $(".maskMatriculaSesc").inputmask("9999-999999-99");
        $(".maskCnpj").inputmask("99.999.999/9999-99");
        $(".maskDecimal").inputmask("#0,00", { reverse: true });
        $(".maskCurrency").inputmask('decimal', {
            'alias': 'numeric',
            'groupSeparator': ',',
            'autoGroup': true,
            'digits': 2,
            'radixPoint': ".",
            'digitsOptional': false,
            'allowMinus': false,
            'prefix': 'R$ ',
            'placeholder': ''
        });
        $(".maskPlacaVeicular").inputmask("AAA-9999");
        $(".maskProtocolo").inputmask("9999.99.9999999-9");
        $(".maskHour").inputmask("99:99");
        $(".maskCep").inputmask("99999-999");
        $(".maskMatricula").inputmask("9999-999999-9");
        $(".maskCreditCard").inputmask("9999 9999 9999 9999");
        $(".maskLogin").inputmask("99999999999");

        $(".maskDate").each(function () {
            $(this)
                .inputmask("99/99/9999", { reverse: true })
                .datepicker({
                    rtl: KTUtil.isRTL(),
                    format: "dd/mm/yyyy",
                    todayHighlight: true,
                    autoclose: true,
                    language: "pt-BR",
                    orientation: "bottom left",
                    templates: {
                        leftArrow: '<i class="la la-angle-left"></i>',
                        rightArrow: '<i class="la la-angle-right"></i>',
                    },
                });
        });

        $("form").submit(function () {
            $(".maskCpf, .maskCnpj, .maskPhone").inputmask("remove");
        });
    }
});

!function(a){a.fn.datepicker.dates["pt-BR"]={days:["Domingo","Segunda","Terça","Quarta","Quinta","Sexta","Sábado"],daysShort:["Dom","Seg","Ter","Qua","Qui","Sex","Sáb"],daysMin:["Do","Se","Te","Qu","Qu","Se","Sa"],months:["Janeiro","Fevereiro","Março","Abril","Maio","Junho","Julho","Agosto","Setembro","Outubro","Novembro","Dezembro"],monthsShort:["Jan","Fev","Mar","Abr","Mai","Jun","Jul","Ago","Set","Out","Nov","Dez"],today:"Hoje",monthsTitle:"Meses",clear:"Limpar",format:"dd/mm/yyyy"}}(jQuery);