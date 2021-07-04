using SleepyTeddy.ViewModel;
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
    public partial class BuscarPaciente : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public TabledPageTerapeutaView objTableBinding { get; set; }

        public BuscarPaciente()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var dataItem = e.Item as PatientsView;

            await Navigation.PushAsync(new UpdatePaciente(dataItem.Key));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        private async void btnSearch_clicked_paciente(object sender, EventArgs e)
        {
            string id_admin = LoginViewModel.Administrator_ID;
            if (string.IsNullOrWhiteSpace(apTerapeuta.Text))
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Complete el campo de búsqueda", new TimeSpan(3));
            }
            else
            {
                try
                {
                    objTableBinding = new TabledPageTerapeutaView();
                    await objTableBinding.GetPatientsViewAsync(apTerapeuta.Text, id_admin);
                    list_terapist.ItemsSource = objTableBinding.ListPatient;
                    if ((list_terapist.ItemsSource as ListaPacientes).Count == 0)
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast("No se obtuvieron resultados", new TimeSpan(3));
                    }
                }
                catch (Exception ex) { }
            }
        }
        private async void btnRegistrarPaciente_clicked(object sender, EventArgs e)
        {
            //Nos envía registrar paciente
            await Navigation.PushAsync(new RegPaciente());
            //Nos envía a cuestionario PSQ-9
            //await Navigation.PushAsync(new CuestionarioPSQ9());
        }
    }
}