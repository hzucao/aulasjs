using InduMovel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InduMovel.Context
{
    public class AppDbContext : IdentityDbContext<UserAcount>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){

        }

        public DbSet<Categoria> Categorias{get;set;}
        public DbSet<Movel> Moveis{get;set;}
        public DbSet<CarrinhoItem> CarrinhoItens{get;set;}
        public DbSet<Pedido> Pedidos{get;set;}
        public DbSet<PedidoMovel> PedidoMoveis{get;set;}

    }
}