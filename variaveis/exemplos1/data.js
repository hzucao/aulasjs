
const today = new Date();
const dtnasc = new Date("1990-07-13");
var ano1, ano2;
console.log(today.toDateString());
console.log(today.toISOString());

console.log(today.getDate()); //retorna o dia
console.log(today.getMonth() +1); //retorna o mes

ano1 = today.getFullYear();
ano2 = dtnasc.getFullYear();

console.log(ano1-ano2);