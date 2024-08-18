// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function voltarPagina() {
    window.history.back();
}

function enableAndSubmit() {
    // Enable all input fields and selects
    enableFields();

    // Submit the form after a short delay
    setTimeout(function () {
        document.getElementById("myForm").submit();
    }, 100); // You can adjust the delay as needed
}

function enableFields() {
    // Enable all input fields
    var inputFields = document.querySelectorAll('input');
    inputFields.forEach(function (input) {
        input.disabled = false;
    });

    // Enable all select elements
    var selectFields = document.querySelectorAll('select');
    selectFields.forEach(function (select) {
        select.disabled = false;
    });
}



function desabilitarIdadeMaxima() {
    var categoriaIdadeMaxima = document.getElementById('catIdadeMinimaMaxima').value
    if (categoriaIdadeMaxima.indexOf('até') != 1) {
        document.getElementById('catIdadeMaxima').setAttribute('readonly', 'readonly')
        document.getElementById('catIdadeMaxima').value = 99;
    } else {
        document.getElementById('catIdadeMaxima').removeAttribute('readonly')
        document.getElementById('catIdadeMaxima').value = '';
    }
}

// Write your JavaScript code.
function esconder2(val) {
    const collection = document.getElementsByClassName("esconder-menu");
    collection[val].style.transtion = "display 1s";
    collection[val].style.display = "none";
    const iconsElementFecha = document.getElementsByClassName("bi-arrows-angle-contract");
    const iconsElemntAbri = document.getElementsByClassName("bi-arrows-angle-expand");
    iconsElementFecha[val].style.display = "inline";
    iconsElemntAbri[val].style.display = "none";
}

function esconder(val) {
    const collection = document.getElementsByClassName("esconder-menu");
    collection[val].style.transtion = "display 1s";
    collection[val].style.display = "block";
    const iconsElementFecha = document.getElementsByClassName("bi-arrows-angle-contract");
    const iconsElemntAbri = document.getElementsByClassName("bi-arrows-angle-expand");
    iconsElementFecha[val].style.display = "none";
    iconsElemntAbri[val].style.display = "inline";
}

function ShowFormsParaCadastrarJogo() {
    $(".div-fase1").hide();

    var formsDiv = document.getElementById('formsDivJogo');
    if (formsDiv.style.opacity === '0' || formsDiv.style.opacity === '') {
        formsDiv.style.display = 'block';
        setTimeout(function () {
            formsDiv.style.opacity = '1'; // Define a opacidade para 1 após um pequeno intervalo para mostrar a transição
        }, 10);

        // Limpa os campos input e select
        var elements = formsDiv.querySelectorAll('input, select');
        elements.forEach(function (element) {
            if (element.tagName === 'INPUT') {
                element.value = ''; // Define o valor do input como vazio
            } else if (element.tagName === 'SELECT') {
                element.selectedIndex = -1; // Define a opção selecionada do select como vazia
            }
        });
    } else {
        formsDiv.style.opacity = '0'; // Define a opacidade para 0 antes de ocultar o elemento
        setTimeout(function () {
            formsDiv.style.display = 'none'; // Define a exibição como 'none' após a transição de opacidade
        }, 600); // Tempo correspondente à transição definida no CSS (0.6s)
    }
}

function CarregaQuantidadeEquipeModalidade(IDMMC) {
    $.ajax({
        url: "/Grupo/GetQuantidadeEquipeModalidadeController",
        type: "GET",
        data: { IDMMC: IDMMC },
        dataType: "json",
        success: function (data) {
            var quantidadeEquipe = data; // Assuming the returned value is an integer
            $("#qEquipe").val(quantidadeEquipe); // Set the value of the input field
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function carregarNumeroDosJogos() {
    var idEmmc = document.getElementById('Modalidade').value; // Substitua pelo valor desejado
    var fase = document.getElementById('cmbFase').value; // Obtenha o valor selecionado
    console.log(fase); // Exiba a fase selecionada no console
    $.ajax({
        url: '/Resultado/CarregarNumeroDosJogosTela', // Substitua pelo URL correto do controlador
        type: 'GET', // ou 'POST' dependendo do método da sua ação
        data: { IDEMMC: idEmmc, Fase: fase },
        dataType: 'json',
        success: function (data) {
            var options = "";
            $.each(data, function (index, item) {
                options += '<option value="' + item.value + '">' + item.text + '</option>';
            });
            $("#NJogos").html(options);

        },
        error: function (error) {
            // Lidar com erros, se houver
            console.log(error);
        }
    });
}

function CarregaFaseGrupoModalidade(fase) {
    var idEMMC = $(".Modalidade").val();
    $(".div-fase1").hide();
    if (fase == '1F') {
        $(".div-fase1 select, .div-fase1 input").prop("disabled", false);
        $(".div-fase2 select, .div-fase1 input").prop("disabled", true);
        $(".div-fase1").show();
        $(".div-fase2").hide();
        $.ajax({
            url: "/Jogo/ListaFaseDeGrupoF1",
            type: "GET",
            data: { idEMMC: idEMMC, fase: fase },
            dataType: "json",
            success: function (data) {
                var options = "";
                $.each(data, function (index, item) {
                    options += '<option value="' + item.value + '">' + item.text + '</option>';
                });
                $(".grupo").html(options);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    } else if (fase != '1F') {
        $(".div-fase1").hide();
        $(".div-fase2").show();
        $(".div-fase1 select, .div-fase1 input").prop("disabled", true);
        $(".div-fase2 select, .div-fase1 input").prop("disabled", false);
        $.ajax({
            url: "/Jogo/ListaFaseDeGrupoF2",
            type: "GET",
            data: { idEMMC: idEMMC, fase: fase },
            dataType: "json",
            success: function (data) {
                var options = "";
                $.each(data, function (index, item) {
                    options += '<option value="' + item.value + '">' + item.text + '</option>';
                });
                $(".grupo").html(options);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    }
}

function CarregaTimesFaseGrupoModalidade(grpCodigo) {
    var idEMMC = $(".Modalidade").val();
    $.ajax({
        url: "/Jogo/ListaTimeFaseDeGrupo",
        type: "GET",
        data: { idEMMC: idEMMC, grpCodigo: grpCodigo },
        dataType: "json",
        success: function (data) {
            var options = "";
            $.each(data, function (index, item) {
                options += '<option value="' + item.value + '">' + item.text + '</option>';
            });
            $(".time1").html(options);
            $(".time2").html(options);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}



function CarregaTimesFase2GrupoModalidadeTime1(val) {
    var idEMMC = $(".Modalidade").val();
    var url = "/Jogo/ListaTimeFase2DeGrupo";

    $.ajax({
        url: url,
        type: "GET",
        data: { idEMMC: idEMMC, TipoRegra: val },
        dataType: "json",
        success: function (data) {
            if (val >= 1 && val <= 5) {
                $(".label-Colocado1").text("Grupo");
            }
            else if (val === "V") {
                $(".label-Colocado1").text("Jogo");
            } else if (val === "C") {
                $(".label-Colocado1").text("Time");
            }
            var options = "";
            $.each(data, function (index, item) {
                options += '<option value="' + item.value + '">' + item.text + '</option>';
            });
            $(".time1").html(options);
        },
        error: function (xhr, status, error) {
            console.log(error);
            alert("erro");
        }
    });
}


function CarregaTimesFase2GrupoModalidadeTime2(val) {
    var idEMMC = $(".Modalidade").val();
    var url = "/Jogo/ListaTimeFase2DeGrupo";

    $.ajax({
        url: url,
        type: "GET",
        data: { idEMMC: idEMMC, TipoRegra: val },
        dataType: "json",
        success: function (data) {
            if (val >= 1 && val <= 5) {
                $(".label-Colocado2").text("Grupo");
            }
            else if (val === "V") {
                $(".label-Colocado2").text("Jogo");
            } else if (val === "C") {
                $(".label-Colocado2").text("Time");
            }
            var options = "";
            $.each(data, function (index, item) {
                options += '<option value="' + item.value + '">' + item.text + '</option>';
            });
            $(".time2").html(options);
        },
        error: function (xhr, status, error) {
            console.log(error);
            alert("erro");
        }
    });
}


function CarregarDados() {
    try {
        var matricula = $("#catCodigo").val();
        var url = "CarregarDadosEmTela";
        $.getJSON(url, { id: matricula }, function (data) {
            if (data != null) {
                $("#catIdadeMinima").val(data.catIdadeMinima);
                $("#catIdadeMinimaMaxima").val(data.catIdadeMinimaMaxima);
                $("#catIdadeMaxima").val(data.catIdadeMaxima);
                $("#catSexo").val(data.catSexo);
            } else {
                $("#pesNome").val('');
                $("#pesSexo").val('');
                $("#DtNasc").val('');
            }
        });
    } catch (error) {
        // Handle the exception here
        console.error("An error occurred while loading data:", error);
        // Optionally, you can display an error message to the user
        alertify.error("An error occurred while loading data. Please try again later.");
    }
}

function formatarData(data) {
    var dataObj = new Date(data);
    var dia = ("0" + dataObj.getDate()).slice(-2);
    var mes = ("0" + (dataObj.getMonth() + 1)).slice(-2);
    var ano = dataObj.getFullYear();

    return ano + "-" + mes + "-" + dia;
}

function CarreagarDadosEmtelaMatricula(Ndocumento) {
    $.ajax({
        url: "/Atleta/CarregarDadosEmTelaMatricula",
        type: "GET",
        data: { id: Ndocumento },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $("#pesNome").val(data.nmcliente).prop('disabled', true);
                if (data.cdsexo == 0) {
                    $("#pesSexo").val('M').prop('disabled', true);
                } else if (data.cdsexo == 1) {
                    $("#pesSexo").val('F').prop('disabled', true);
                } else {
                    $("#pesSexo").val(data.cdsexo).prop('disabled', true);
                }
                $("#DtNasc").val(formatarData(data.dtnascimen)).prop('disabled', true);
                $("#PesTipo").val(data.tipoatleta);
            } else {
                $("#pesNome").val('');
                $("#pesSexo").val('');
                $("#DtNasc").val('');
            }
            console.log(data);
            alertify.success('CPF válido');
        },
        error: function (xhr, status, error) {
            $("#pesNome").val('').prop('disabled', false);
            $("#pesNumDoc").val('').prop('disabled', false);
            $("#pesSexo").val('').prop('disabled', false);
            $("#DtNasc").val('').prop('disabled', false);
            console.log(error);
            alertify.error("Erro, tente novamente");
        }
    });
}

function CarreagarDadosEmtelaCPF(Ndocumento) {
    $.ajax({
        url: "/Atleta/CarregarDadosEmTelaCPF",
        type: "GET",
        data: { id: Ndocumento }, // Corrigido para usar Ndocumento em vez de cpf
        dataType: "json",
        success: function (data) {
            if (data.nmcliente === null || data.nmcliente == "") {
                $("#pesNome").val(data.nmcliente).prop('disabled', false);
            } else {
                $("#pesNome").val(data.nmcliente).prop('disabled', true);
            }
            if (data.cdsexo == "" || data.cdsexo === null) {
                $("#pesSexo").val('').prop('disabled', false);
            } else {
                if (data.cdsexo == 0) {
                    $("#pesSexo").val('M').prop('disabled', true);
                } else if (data.cdsexo == 1) {
                    $("#pesSexo").val('F').prop('disabled', true);
                } else {
                    $("#pesSexo").val(data.cdsexo).prop('disabled', true);
                }
            }
            if (data.dtnascimen == "" || data.dtnascimen === null) {
                $("#DtNasc").val(formatarData(data.dtnascimen)).prop('disabled', false);
            } else {
                $("#DtNasc").val(formatarData(data.dtnascimen)).prop('disabled', true);
            }
            $("#PesTipo").val(data.tipoatleta).prop('disabled', true);
            alertify.success('CPF válido');
        },
        error: function (xhr, status, error) {
            $("#pesNome").val('').prop('disabled', false);
            //$("#pesNumDoc").val('').prop('disabled', false);
            $("#pesSexo").val('').prop('disabled', false);
            $("#DtNasc").val('').prop('disabled', false);
            console.log(error);
        }
    });
}
function validarCPF(Ndocumento) {
    if (document.getElementById("pesTipoDoc").value === 'CPF') {
        var input = document.getElementById("pesNumDoc");
        var cpf = input.value;
        // Remover qualquer formatação do CPF (pontos e hífen)
        cpf = cpf.replace(/\D/g, '');

        // Verificar o tamanho do CPF
        if (cpf.length !== 11) {
            $("#pesNome").val('');
            $("#pesSexo").val('');
            $("#DtNasc").val('');
            handleValidationError(input, 'CPF inválido');
            return;
        }

        // Verificar se todos os dígitos são iguais (CPF inválido)
        if (/^(\d)\1+$/.test(cpf)) {
            $("#pesNome").val('');
            $("#pesSexo").val('');
            $("#DtNasc").val('');
            handleValidationError(input, 'CPF inválido');
            return;
        }

        // Calcular os dígitos verificadores
        var digito1 = calcularDigitoVerificador(cpf.substring(0, 9), 10);
        var digito2 = calcularDigitoVerificador(cpf.substring(0, 9) + digito1, 11);

        // Verificar se os dígitos verificadores estão corretos
        if (cpf.substring(9, 11) !== digito1.toString() + digito2.toString()) {
            $("#pesNome").val('');
            $("#pesSexo").val('');
            $("#DtNasc").val('');
            handleValidationError(input, 'CPF inválido');
            return;
        }
        CarreagarDadosEmtelaCPF(Ndocumento);
    }
    else if (document.getElementById("pesTipoDoc").value === 'CS') {
        CarreagarDadosEmtelaMatricula(Ndocumento);
    }
}

function handleValidationError(input, errorMessage) {
    clearInput(input);
    alertify.error(errorMessage);
}

function clearInput(input) {
    input.value = "";
}

function calcularDigitoVerificador(cpfParcial, multiplicador) {
    var soma = 0;
    for (var i = 0; i < cpfParcial.length; i++) {
        soma += parseInt(cpfParcial.charAt(i)) * multiplicador;
        multiplicador--;
    }
    var resto = soma % 11;
    if (resto < 2) {
        return 0;
    } else {
        return 11 - resto;
    }
}




function toggleSidebar() {
    var sidebar = document.getElementById("sidebar");
    sidebar.classList.toggle("show");
}

function validarEmail() {
    var emailInput = document.getElementById('eqpEmail');
    var email = emailInput.value;

    // Expressão regular para validar o formato do email
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailRegex.test(email)) {
        alertify.success('Email valido');
    } else {
        alertify.error('Email invalido');
        emailInput.value = "";
    }
}

// Chame a função para carregar os dados ao carregar a página
$(document).ready(function () {
    $('#eqpTecnicoTel').mask('(99) 99999-9999');
    $('#eqpAssistTecTel').mask('(99) 99999-9999');
    $('#Telefone').mask('(99) 99999-9999');
});




function AlteraLabelTipoDocumento() {
    var tipoDocumento = document.getElementById("pesTipoDoc").value;
    var numDocLabel = document.getElementById("pesNumDocLabel");
    if (tipoDocumento === "CS") {
        numDocLabel.innerText = "N° documento Carteira";

    } else if (tipoDocumento === "CPF") {
        numDocLabel.innerText = "N° documento CPF";
    }
    var input = document.getElementById("pesNumDoc");

    input.value = "";
    $("#pesNome").val('');
    $("#pesSexo").val('');
    $("#DtNasc").val('');
    $("#Telefone").val('');
    $("#eqpEmail").val('');
}

function calcularDigitoVerificador(cpfParcial, peso) {
    var soma = 0;

    for (var i = 0; i < cpfParcial.length; i++) {
        soma += parseInt(cpfParcial.charAt(i)) * peso--;
    }

    var digito = (soma % 11) < 2 ? 0 : 11 - (soma % 11);
    return digito;
}



//**********************Validar dateTime*******************************************************************
function validateDateTime() {
    let input = document.getElementById('dataJogoValores').value;
    let errorMessage = document.getElementById('error-message');

    // O formato de data-hora do input 'datetime-local' é: 'YYYY-MM-DDTHH:MM'
    let pattern = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/;

    if (!pattern.test(input)) {
        errorMessage.textContent = "Data e hora inválidas!";
    } else {
        // Aqui você pode adicionar validações adicionais se necessário. 
        // Como verificar se a data está no passado ou se a hora está dentro de um intervalo específico.

        errorMessage.textContent = ""; // Limpar mensagem de erro se a entrada for válida.
    }
}



/*******************************************************************************************/
function calcularPontosEquipe(inputs, resultadoInput) {
    var somaPontos = 0;

    inputs.forEach(function (input) {
        somaPontos += parseFloat(input.value) || 0;
    });

    resultadoInput.value = somaPontos;
}


var equipe1PontosInputs = document.querySelectorAll('input[id^="equipe1_ponto_"]');
var resultadoEqp1Input = document.getElementById('resultadoEqp1');

equipe1PontosInputs.forEach(function (input) {
    input.addEventListener('change', function () {
        calcularPontosEquipe(equipe1PontosInputs, resultadoEqp1Input);
    });
});

var equipe2PontosInputs = document.querySelectorAll('input[id^="equipe2_ponto_"]');
var resultadoEqp2Input = document.getElementById('resultadoEqp2');

equipe2PontosInputs.forEach(function (input) {
    input.addEventListener('change', function () {
        calcularPontosEquipe(equipe2PontosInputs, resultadoEqp2Input);
    });
});



//Aplicação resultado não aparecer os valores tela 

const obtPenaltisSelect = document.getElementById("ObtPenaltis");
const valoresDosPenaltisDiv = document.querySelector(".ValoresDosPenaltis");

obtPenaltisSelect.addEventListener("change", function () {
    if (obtPenaltisSelect.value === "Sim") {
        valoresDosPenaltisDiv.style.display = "block"; // Mostrar os valores dos pênaltis
    } else {
        valoresDosPenaltisDiv.style.display = "none"; // Ocultar os valores dos pênaltis
    }
});

//************************************************************************************************* */
function UpdateResultadoEqp1(valores1) {
    var resultadoEqp1Input = document.getElementById('resultadoEqp1');
    var resultadoEqp2Input = document.getElementById('resultadoEqp2');

    var selectElement = document.getElementById('comparecimentoTime2');
    var valores2 = selectElement.value;

    if (valores1 === 'nao' && valores2 === 'nao') {
        resultadoEqp1Input.value = 'W';
        resultadoEqp2Input.value = 'W';
        disableElementsByClass('eqp1Disabled');
        disableElementsByClass('eqp2Disabled');
    } else if (valores1 === 'sim' && valores2 === 'nao') {
        resultadoEqp1Input.value = 'W';
        resultadoEqp2Input.value = 'O';
        disableElementsByClass('eqp1Disabled');
        disableElementsByClass('eqp2Disabled');
    } else if (valores1 === 'nao' && valores2 === 'sim') {
        resultadoEqp1Input.value = 'O';
        resultadoEqp2Input.value = 'W';
        disableElementsByClass('eqp1Disabled');
        disableElementsByClass('eqp2Disabled');
    } else {
        resultadoEqp1Input.value = '';
        resultadoEqp2Input.value = '';
        enableElementsByClass('eqp1Disabled');
        enableElementsByClass('eqp2Disabled');
    }
}

function disableElementsByClass(className) {
    var elements = document.getElementsByClassName(className);

    for (var i = 0; i < elements.length; i++) {
        elements[i].disabled = true;
    }
}

function enableElementsByClass(className) {
    var elements = document.getElementsByClassName(className);

    for (var i = 0; i < elements.length; i++) {
        elements[i].disabled = false;
    }
}

function CarregaModalidade(municipioId) {
    $.ajax({
        url: "/Equipe/CarregarDadosEmTelaModalidade",
        type: "GET",
        data: { id: municipioId },
        dataType: "json",
        success: function (data) {
            var options = "";
            $.each(data, function (index, item) {
                options += '<option value="' + item.value + '">' + item.text + '</option>';
            });
            $("#Modalidade").html(options);
            $("#qEquipe").val('');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}