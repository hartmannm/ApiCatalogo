using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

namespace ApiCatalogo.Migrations
{
    public partial class PopulateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(GetSQLCategories());
            migrationBuilder.Sql(GetSQLProducts());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Produtos");
        }

        private string GetSQLCategories()
        {
            var categoriesSqlBuilder = new StringBuilder("INSERT INTO Categoria(Nome, ImageUrl) VALUES\n");
            categoriesSqlBuilder
                .Append("('Bebidas', 'https://www.publicdomainpictures.net/pictures/130000/nahled/drinks-set.jpg'),");
            categoriesSqlBuilder
                .Append("('Lanches', 'https://image.freepik.com/vetores-gratis/design-de-pacote-de-hamburguer-e-pizza_72766-327.jpg'),");
            categoriesSqlBuilder
                .Append("('Sobremesas', 'https://img.freepik.com/free-vector/bakery-with-confectionery-sweets-isolated_71374-493.jpg?size=626&ext=jpg');");
            return categoriesSqlBuilder.ToString();
        }

        private string GetSQLProducts()
        {
            var productsSqlBuilder = new StringBuilder("INSERT INTO Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId) VALUES\n");
            productsSqlBuilder
                .Append("('Coca-Cola Diet','Refrigerante de Cola 350 ml',5.45,'https://casafiesta.fbitsstatic.net/img/p/refrigerante-coca-cola-sem-acucar-lata-350ml-69291/236160.jpg',50,now(),(Select CategoriaId from Categoria where Nome='Bebidas')),");
            productsSqlBuilder
                .Append("('Lanche de Atum','Lanche de Atum com maionese',8.50,'https://img.itdg.com.br/tdg/images/recipes/000/003/165/38827/38827_original.jpg',10,now(),(Select CategoriaId from Categoria where Nome='Lanches')),");
            productsSqlBuilder
                .Append("('Pudim 100 g','Pudim de leite condensado 100g',6.75,'https://vivareceita-cdn.s3.amazonaws.com/uploads/2021/05/Aprenda-como-fazer-pudim-de-leite-simples.-Fonte-Broma-Bakery-500x500.jpg',20,now(),(Select CategoriaId from Categoria where Nome='Sobremesas'));");
            return productsSqlBuilder.ToString();
        }
    }
}
