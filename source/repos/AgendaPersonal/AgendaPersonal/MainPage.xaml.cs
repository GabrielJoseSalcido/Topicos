namespace AgendaPersonal;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void IrListaContactos(object sender, EventArgs e)
    {
        var button = (Button)sender;
        button.IsEnabled = false;
        await Shell.Current.GoToAsync("contactosPage");
        button.IsEnabled = true;
    }

    private async void IrCrearContacto(object sender, EventArgs e)
    {
        var button = (Button)sender;
        button.IsEnabled = false;
        await Shell.Current.GoToAsync("crearContactoPage");
        button.IsEnabled = true;
    }

    private async void IrConfiguracion(object sender, EventArgs e)
    {
        var button = (Button)sender;
        button.IsEnabled = false;
        await Shell.Current.GoToAsync("configuracionPage");
        button.IsEnabled = true;
    }

}