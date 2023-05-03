var diaSemana;
var msg;

diaSemana = 3;

switch(diaSemana){
    case 1:
        msg = "Hoje é: Domingo";
        break;
    case 2: 
        msg = "Hoje é: Segunda";
        break;
    case 3: 
        msg = "Hoje é: Terça";
        break;
    case 4: 
        msg = "Hoje é: Quarta";
        break;
    case 5: 
        msg = "Hoje é: Quinta";
        break;
    case 6:
        msg = "Hoje é: Sexta";
        break;
    case 7:
        msg = "Hoje é: Sabado";
        break;
    default:
            msg = "Dia invalido";
          
    
}

console.log(msg);