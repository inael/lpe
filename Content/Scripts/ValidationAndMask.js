var NUM_DIGITOS_CPF = 11;
var NUM_DIGITOS_CNPJ = 14;
var NUM_DGT_CNPJ_BASE = 8;

// JavaScript Document
//adiciona mascara de cnpj
function MascaraCNPJ(cnpj) {
    if (mascaraInteiro(cnpj) == false) {
        event.returnValue = false;
    }
    return formataCampo(cnpj, '00.000.000/0000-00', event);
}

//adiciona mascara de cep
function MascaraCep(cep) {
    if (mascaraInteiro(cep) == false) {
        event.returnValue = false;
    }
    return formataCampo(cep, '00.000-000', event);
}

//adiciona mascara de data
function MascaraData(data) {
    if (mascaraInteiro(data) == false) {
        event.returnValue = false;
    }
    return formataCampo(data, '00/00/0000', event);
}

//adiciona mascara ao telefone
function MascaraTelefone(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '(00) 0000-0000', event);
}

//adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (mascaraInteiro(cpf) == false) {
        event.returnValue = false;
    }
    return formataCampo(cpf, '000.000.000-00', event);
}

//valida telefone
function ValidaTelefone(tel) {
    exp = /\(\d{2}\)\ \d{4}\-\d{4}/
    if (!exp.test(tel.value))
        alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep) {
    exp = /\d{2}\.\d{3}\-\d{3}/
    if (!exp.test(cep.value))
        alert('Numero de Cep Invalido!');
}

//valida o CPF digitado
function ValidarCPF(Objcpf) {
    var cpf = Objcpf.value;
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado)
        alert('CPF Invalido!');
}

//valida numero inteiro com mascara
function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {
    var cnpj = ObjCnpj.value;
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;

    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
        return -1;

    return 1;

}

//formata de forma generica os campos
function formataCampo(campo, Mascara, evento) {
    var boleanoMascara;

    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)| /g
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length; ;

    if (Digitato != 8) { // backspace 
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
                                                                || (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
                                                                || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    } else {
        return true;
    }
}

/**
* Adiciona método lpad() à classe String.
* Preenche a String à esquerda com o caractere fornecido,
* até que ela atinja o tamanho especificado.
*/
String.prototype.lpad = function (pSize, pCharPad) {
    var str = this;
    var dif = pSize - str.length;
    var ch = String(pCharPad).charAt(0);
    for (; dif > 0; dif--) str = ch + str;
    return (str);
} //String.lpad


/**
* Adiciona método trim() à classe String.
* Elimina brancos no início e fim da String.
*/
String.prototype.trim = function () {
    return this.replace(/^s*/, "").replace(/s*$/, "");
} //String.trim


/**
* Elimina caracteres de formatação e zeros à esquerda da string
* de número fornecida.
* @param String pNum
*      String de número fornecida para ser desformatada.
* @return String de número desformatada.
*/
function unformatNumber(pNum) {
    return String(pNum).replace(/D/g, "").replace(/^0+/, "");
} //unformatNumber


/**
* Formata a string fornecida como CNPJ ou CPF, adicionando zeros
* à esquerda se necessário e caracteres separadores, conforme solicitado.
* @param String pCpfCnpj
*      String fornecida para ser formatada.
* @param boolean pUseSepar
*      Indica se devem ser usados caracteres separadores (. - /).
* @param boolean pIsCnpj
*      Indica se a string fornecida é um CNPJ.
*      Caso contrário, é CPF. Default = false (CPF).
* @return String de CPF ou CNPJ devidamente formatada.
*/
function formatCpfCnpj(pCpfCnpj, pUseSepar, pIsCnpj) {
    if (pIsCnpj == null) pIsCnpj = false;
    if (pUseSepar == null) pUseSepar = true;
    var maxDigitos = pIsCnpj ? NUM_DIGITOS_CNPJ : NUM_DIGITOS_CPF;
    var numero = unformatNumber(pCpfCnpj);

    numero = numero.lpad(maxDigitos, '0');
    if (!pUseSepar) return numero;

    if (pIsCnpj) {
        reCnpj = /(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/;
        numero = numero.replace(reCnpj, "$1.$2.$3/$4-$5");
    }
    else {
        reCpf = /(\d{3})(\d{3})(\d{3})(\d{2})$/;
        numero = numero.replace(reCpf, "$1.$2.$3-$4");
    }
    return numero;
} //formatCpfCnpj


/**
* Calcula os 2 dígitos verificadores para o número-efetivo pEfetivo de
* CNPJ (12 dígitos) ou CPF (9 dígitos) fornecido. pIsCnpj é booleano e
* informa se o número-efetivo fornecido é CNPJ (default = false).
* @param String pEfetivo
*      String do número-efetivo (SEM dígitos verificadores) de CNPJ ou CPF.
* @param boolean pIsCnpj
*      Indica se a string fornecida é de um CNPJ.
*      Caso contrário, é CPF. Default = false (CPF).
* @return String com os dois dígitos verificadores.
*/
function dvCpfCnpj(pEfetivo, pIsCnpj) {
    if (pIsCnpj == null) pIsCnpj = false;
    var i, j, k, soma, dv;
    var cicloPeso = pIsCnpj ? NUM_DGT_CNPJ_BASE : NUM_DIGITOS_CPF;
    var maxDigitos = pIsCnpj ? NUM_DIGITOS_CNPJ : NUM_DIGITOS_CPF;
    var calculado = formatCpfCnpj(pEfetivo, false, pIsCnpj);
    calculado = calculado.substring(2, maxDigitos);
    var result = "";

    for (j = 1; j <= 2; j++) {
        k = 2;
        soma = 0;
        for (i = calculado.length - 1; i >= 0; i--) {
            soma += (calculado.charAt(i) - '0') * k;
            k = (k - 1) % cicloPeso + 2;
        }
        dv = 11 - soma % 11;
        if (dv > 9) dv = 0;
        calculado += dv;
        result += dv
    }

    return result;
} //dvCpfCnpj


/**
* Testa se a String pCpf fornecida é um CPF válido.
* Qualquer formatação que não seja algarismos é desconsiderada.
* @param String pCpf
*      String fornecida para ser testada.
* @return <code>true</code> se a String fornecida for um CPF válido.
*/
function isCpf(pCpf) {
    var numero = formatCpfCnpj(pCpf, false, false);
    var base = numero.substring(0, numero.length - 2);
    var digitos = dvCpfCnpj(base, false);
    var algUnico, i;

    // Valida dígitos verificadores
    if (numero != base + digitos) return false;

    /* Não serão considerados válidos os seguintes CPF:
    * 000.000.000-00, 111.111.111-11, 222.222.222-22, 333.333.333-33, 444.444.444-44,
    * 555.555.555-55, 666.666.666-66, 777.777.777-77, 888.888.888-88, 999.999.999-99.
    */
    algUnico = true;
    for (i = 1; i < NUM_DIGITOS_CPF; i++) {
        algUnico = algUnico && (numero.charAt(i - 1) == numero.charAt(i));
    }
    return (!algUnico);
} //isCpf


/**
* Testa se a String pCnpj fornecida é um CNPJ válido.
* Qualquer formatação que não seja algarismos é desconsiderada.
* @param String pCnpj
*      String fornecida para ser testada.
* @return <code>true</code> se a String fornecida for um CNPJ válido.
*/
function isCnpj(pCnpj) {
    var numero = formatCpfCnpj(pCnpj, false, true);
    var base = numero.substring(0, NUM_DGT_CNPJ_BASE);
    var ordem = numero.substring(NUM_DGT_CNPJ_BASE, 12);
    var digitos = dvCpfCnpj(base + ordem, true);
    var algUnico;

    // Valida dígitos verificadores
    if (numero != base + ordem + digitos) return false;

    /* Não serão considerados válidos os CNPJ com os seguintes números BÁSICOS:
    * 11.111.111, 22.222.222, 33.333.333, 44.444.444, 55.555.555,
    * 66.666.666, 77.777.777, 88.888.888, 99.999.999.
    */
    algUnico = numero.charAt(0) != '0';
    for (i = 1; i < NUM_DGT_CNPJ_BASE; i++) {
        algUnico = algUnico && (numero.charAt(i - 1) == numero.charAt(i));
    }
    if (algUnico) return false;

    /* Não será considerado válido CNPJ com número de ORDEM igual a 0000.
    * Não será considerado válido CNPJ com número de ORDEM maior do que 0300
    * e com as três primeiras posições do número BÁSICO com 000 (zeros).
    * Esta crítica não será feita quando o no BÁSICO do CNPJ for igual a 00.000.000.
    */
    if (ordem == "0000") return false;
    return (base == "00000000"
		|| parseInt(ordem, 10) <= 300 || base.substring(0, 3) != "000");
} //isCnpj


/**
* Testa se a String pCpfCnpj fornecida é um CPF ou CNPJ válido.
* Se a String tiver uma quantidade de dígitos igual ou inferior
* a 11, valida como CPF. Se for maior que 11, valida como CNPJ.
* Qualquer formatação que não seja algarismos é desconsiderada.
* @param String pCpfCnpj
*      String fornecida para ser testada.
* @return <code>true</code> se a String fornecida for um CPF ou CNPJ válido.
*/
function isCpfCnpj(pCpfCnpj) {
    var numero = pCpfCnpj.replace(/D/g, "");
    if (numero.length > NUM_DIGITOS_CPF)
        return isCnpj(pCpfCnpj)
    else
        return isCpf(pCpfCnpj);
} //isCpfCnpj