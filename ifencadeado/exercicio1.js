var atrasado;
var adiantado;

atrasado = false;
adiantado = true;

if(atrasado == true)
{
    console.log("Cobrar juros e multa");
}
else
{
    if(adiantado)
    {
        console.log("Conceder desconto de 10%");
    }
    else
    {
        console.log("Cobrar valor do boleto");
    }
}