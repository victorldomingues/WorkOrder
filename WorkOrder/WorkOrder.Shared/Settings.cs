namespace WorkOrder.Shared
{
    public static class Settings
    {
        public static string ConnectionString { get; set; } = "Data Source=.\\SQL; Initial Catalog=workorder; Integrated Security=True;";

        public static string SecurityKey { get; set; }
    }
}
