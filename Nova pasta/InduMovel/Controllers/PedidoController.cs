using InduMovel.Models;
using InduMovel.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace InduMovel.Controllers
{
    [Authorize]
    public class PedidoController: Controller
    {
        private readonly IPedidoRepository _pedidoR;
        private readonly Carrinho _carrinho;

        public PedidoController(IPedidoRepository pedidoR, Carrinho carrinho)
        {
            _pedidoR = pedidoR;
            _carrinho = carrinho;
        }

        public IActionResult Checkout(){
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido){
            int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;

        //obtem os itens do carrinho de compra do cliente
        List<CarrinhoItem> items = _carrinho.GetCarrinhoCompraItems();
        _carrinho.CarrinhoItens = items;

        //verifica se existem itens de pedido
        if(_carrinho.CarrinhoItens.Count == 0)
        {
            ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
        }

        //calcula o total de itens e o total do pedido
        foreach(var item in items)
        {
            totalItensPedido += item.Quantidade;
            precoTotalPedido += (Convert.ToDecimal(item.Movel.Valor) * item.Quantidade);
        }

        //atribui os valores obtidos ao pedido
        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;

        //valida os dados do pedido
        if(ModelState.IsValid)
        {
            //cria o pedido e os detalhes
            _pedidoR.CriarPedido(pedido);

            //define mensagens ao cliente
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinho.GetCarrinhoCompraTotal();

            //limpa o carrinho do cliente
            _carrinho.LimparCarrinho();

            //exibe a view com dados do cliente e do pedido
            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }
        return View(pedido);
        }

    }
}