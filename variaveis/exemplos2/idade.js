//declarar duas variaveis dtAtual, dtNasc
var dtAtual, dtNasc;
//atribuir a data atual a dtAtual
dtAtual = new Date();
//atribuir a data de nascimento a dtNasc
dtNasc = new Date("1990-07-13");
//declar duas variaveis anoAtual anoNasc
var anoAtual, anoNasc;
//atibuir o ano da variavel dtAtual a anoAtual
anoAtual = dtAtual.getFullYear();
//atibuir o ano da variavel dtNasc a anoNasc
anoNasc = dtNasc.getFullYear();
//declarar a variavel idade 
var idade;
//atribuir o calculo de anoAtual - anoNasc a idade
idade = anoAtual -anoNasc;
//imprimir idade
console.log(idade);
