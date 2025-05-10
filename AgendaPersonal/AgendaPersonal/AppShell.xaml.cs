namespace AgendaPersonal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("contactosPage", typeof(ContactosPage));
            Routing.RegisterRoute("crearContactoPage", typeof(CrearContactoPage));
            Routing.RegisterRoute("configuracionPage", typeof(ConfiguracionPage));
        }
    }
}
