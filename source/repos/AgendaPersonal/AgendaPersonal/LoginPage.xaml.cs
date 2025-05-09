namespace AgendaPersonal;

public partial class LoginPage : ContentPage
{

    Dictionary<string, string> userPasswords = new Dictionary<string, string>();

    public LoginPage()
    {
        InitializeComponent();
        userPasswords.Add("admin", "1234");
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }

    private async void TapGestureRecognizerPwd_Tapped(object sender, TappedEventArgs e)
    {
        /*Label Reg = (sender as Label);
        var Msg = Reg.FormattedText.Spans[1].Text;
        //var customerName = (sender as Label).Text;
        DisplayAlert("Recuperar Password", $"Name : {Msg}", "ok"); */

        string username = await DisplayPromptAsync("Recuperar contraseña", "Introduzca su nombre de usuario:");

        if (string.IsNullOrWhiteSpace(username))
        {
            await DisplayAlert("Error", "Introduzca un usuario.", "OK");
            return;
        }

        if (userPasswords.ContainsKey(username))
        {
            await DisplayAlert("Exito!", $"Su contraseña es: {userPasswords[username]}.", "OK");
        }
        else
        {
            await DisplayAlert("Error", $"Usuario incorrecto.", "OK");
        }

    }
    private async void TapGestureRecognizerReg_Tapped(object sender, TappedEventArgs e)
    {
        string username = await DisplayPromptAsync("Registrar Usuario", "Introduzca su nombre de usuario:");

        if (string.IsNullOrWhiteSpace(username))
        {
            await DisplayAlert("Error", "Introduzca un usuario.", "OK");
            return;
        }

        string password = await DisplayPromptAsync("Registrar Usuario", "Introduzca su contraseña:",
                                                   maxLength: 20);

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Introduzca una contraseña.", "OK");
            return;
        }

        if (!userPasswords.ContainsKey(username))
        {
            userPasswords.Add(username, password);
            await DisplayAlert("Exito!", $"Se ha registrado al usuario {username}.", "OK");
        }
        else
        {
            await DisplayAlert("Error", $"El usuario ya existe.", "OK");
        }
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (IsCredentialCorrect(Username.Text, Password.Text))
        {
            Preferences.Set("UsuarioActual", Username.Text.Trim());
            await SecureStorage.SetAsync("hasAuth", "true");
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Login failed", "Username or password if invalid", "Try again");
        }
    }


    bool IsCredentialCorrect(string username, string password)
    {
        // return Username.Text == "admin" && Password.Text == "1234";
        return userPasswords.ContainsKey(username) && userPasswords[username] == password;
    }
}