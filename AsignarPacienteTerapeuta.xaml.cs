using Plugin.CloudFirestore;
using SleepyTeddy.Models;
using SleepyTeddy.ViewModel;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SleepyTeddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsignarPacienteTerapeuta : ContentPage
    {
        Patients patient;
        string documentId;
        string UID;
        string TerapistId;
        string Apellido;
        string ID_Admin;
        public TabledPageTerapeutaView objTableBinding { get; set; }
        public SearchPatientView objSearch { get; set; }
        public AsignarPacienteTerapeuta()
        {
            InitializeComponent();
        }

        private async void getPatient()
        {
            String role_id = "2";
            var document = await CrossCloudFirestore.Current
                                       .Instance
                                       .Collection("Users")
                                       .WhereEqualsTo("Role_ID", role_id)
                                       .WhereEqualsTo("Pacient_ID", UID)
                                       .GetAsync();
            patient = document.Documents.ElementAt(0).ToObject<Patients>();
            documentId = document.Documents.ElementAt(0).Id;
            ID_Admin = patient.Administrator_ID;

        }
        private async void LoadItems()
        {

            /*objTableBinding = new TabledPageTerapeutaView();
            await objTableBinding.GetPatientsViewAsync(Apellido, ID_Admin);
            BindingContext = objTableBinding;*/

            objSearch = new SearchPatientView();
            await objSearch.GetPatientsViewAsync2();
            BindingContext = objSearch;
        }
        private async void btnAceptar_clicked(object sender, EventArgs e)
        {
            var ObjPatientList = (PatientsView)cbPaciente.SelectedItem;
            var ObjTerapistList =(TerapistView)cbTerapeuta.SelectedItem;

            if (ObjTerapistList == null)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Se debe seleccionar un terapeuta.", new TimeSpan(3));
            }
            else if (ObjPatientList == null)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Se debe seleccionar un paciente.", new TimeSpan(3));
            }

            else
            {
                await CrossCloudFirestore.Current
                             .Instance
                             .Collection("Users")
                             .Document(documentId)
                             .UpdateAsync(patient);
                await DisplayAlert("", "Paciente asignado al terapeuta correctamente", "OK");
            }
        }
    }
}