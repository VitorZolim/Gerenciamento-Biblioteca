using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoBooks : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Orgulho e Preconceito', 10, 'Jane Austen', 0)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Morro dos Ventos Uivantes', 8, 'Emily Brontë', 0)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Dom Casmurro', 12, 'Machado de Assis', 0)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('A Culpa é das Estrelas', 7, 'John Green', 0)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Como Eu Era Antes de Você', 9, 'Jojo Moyes', 0)");

            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Harry Potter e a Pedra Filosofal', 15, 'J.K. Rowling', 1)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Harry Potter e a Câmara Secreta', 13, 'J.K. Rowling', 1)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Senhor dos Anéis', 12, 'J.R.R. Tolkien', 1)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('As Crônicas de Nárnia', 10, 'C.S. Lewis', 1)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Hobbit', 11, 'J.R.R. Tolkien', 1)");

            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('A Ilha do Tesouro', 7, 'Robert Louis Stevenson', 2)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('As Aventuras de Tom Sawyer', 9, 'Mark Twain', 2)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Viagem ao Centro da Terra', 8, 'Júlio Verne', 2)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Os Três Mosqueteiros', 6, 'Alexandre Dumas', 2)");

            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Drácula', 6, 'Bram Stoker', 3)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Frankenstein', 5, 'Mary Shelley', 3)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Exorcista', 7, 'William Peter Blatty', 3)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('It: A Coisa', 10, 'Stephen King', 3)");

            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Assassinato no Expresso Oriente', 10, 'Agatha Christie', 4)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Morte no Nilo', 8, 'Agatha Christie', 4)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Código Da Vinci', 9, 'Dan Brown', 4)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Sherlock Holmes: Um Estudo em Vermelho', 7, 'Arthur Conan Doyle', 4)");

            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Steve Jobs', 6, 'Walter Isaacson', 5)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Einstein: Sua Vida, Seu Universo', 5, 'Walter Isaacson', 5)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Longa Caminhada até a Liberdade', 7, 'Nelson Mandela', 5)");

            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Uma Breve História do Tempo', 7, 'Stephen Hawking', 6)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('Sapiens', 10, 'Yuval Noah Harari', 6)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('O Poder do Hábito', 8, 'Charles Duhigg', 6)");
            mb.Sql("INSERT INTO Books(BookTitle, Quantity, Author, Category) VALUES ('21 Lições para o Século 21', 9, 'Yuval Noah Harari', 6)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Books");
        }

    }
}
