using Flunt.Validations;

namespace WorkOrder.Shared.Entities
{
    public class AppTenant : Entity
    {
        public AppTenant(string appName, string hostname, string theme, string connectionString)
        {
            AppName = appName;
            Hostname = hostname;
            Theme = theme;
            ConnectionString = connectionString;

            new Contract()
                .Requires()
                .IsNullOrEmpty(appName, "AppName", "Nome da empresa é obrigatório.")
                .IsNullOrEmpty(hostname, "Hostname", "O Nome do host é obrigatório.");
        }

        protected AppTenant()
        {
        }

        public string AppName { get; private set; }
        public string Hostname { get; private set; }
        public string Theme { get; private set; }
        public string ConnectionString { get; private set; }

        public void Update(string appName, string hostname, string theme, string connectionString)
        {
            if (!string.IsNullOrEmpty(appName))
                AppName = appName;
            if (!string.IsNullOrEmpty(hostname))
                Hostname = hostname;
            if (!string.IsNullOrEmpty(theme))
                Theme = theme;
            if (!string.IsNullOrEmpty(connectionString))
                ConnectionString = connectionString;
        }

        public override string ToString()
        {
            return $"Schema: [app{Hostname}] Company: {AppName}";
        }
    }
}
