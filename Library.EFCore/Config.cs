
namespace Library.EFCore
{
    internal static class AppConfig
    {
        public static string GetConnectionString()
        {
            return "Data Source=Asus-Note-Vitor\\sqlexpress;Initial Catalog=LibraryManager;Integrated Security=True;TrustServerCertificate=True;";
        }
    }
}
