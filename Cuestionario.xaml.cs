using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SleepyTeddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cuestionario : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public Cuestionario()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Indice de Gravedad de Insomnio",
                "Indice de Calidad del Sueño de Pittsburgh",
                "Indice de Estabilidad",
                "Indice de Coeficiente Intelectual",
                "Indice de Gravedad de Sonambulismo"
            };

            MyListView.ItemsSource = Items;
        }
        private async void AccountSelectPaciente(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MiCuentaPaciente());
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        private async void btnBuscar_clicked(object sender, EventArgs e)
        {

        }
        private async void btnRegistrarPaciente_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPaciente());
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new Cuestionario());
            });
            return true;
        }
    }
}