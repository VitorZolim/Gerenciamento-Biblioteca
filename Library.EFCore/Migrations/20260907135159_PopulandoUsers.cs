using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoUsers : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Users (UserName) VALUES ('João Silva')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Maria Oliveira')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Carlos Santos')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Ana Souza')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Pedro Costa')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Lucas Pereira')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Juliana Almeida')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Rafael Rodrigues')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Beatriz Ferreira')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Gabriel Martins')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Larissa Gomes')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Mateus Barbosa')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Camila Ribeiro')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Bruno Carvalho')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Amanda Lopes')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Felipe Teixeira')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Mariana Moreira')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Thiago Correia')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Isabela Mendes')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Gustavo Nascimento')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Fernanda Araújo')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Diego Lima')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Letícia Vieira')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Rodrigo Monteiro')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Bianca Cardoso')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Eduardo Ramos')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Patrícia Freitas')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('André Batista')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Carolina Duarte')");
            mb.Sql("INSERT INTO Users (UserName) VALUES ('Vinícius Castro')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Users");
        }

    }
}
