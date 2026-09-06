using Library.EFCore.Context;
using System;

namespace ProjectLibrary;

public class Program
{
    public static void Main()
    {
        using AppDbContext context = new AppDbContext();
        context.Database.EnsureDeleted();
        Console.WriteLine("Criando Banco de Dados\n");
        context.Database.EnsureCreated();
        Console.WriteLine("Finalizado\n");
    }
}