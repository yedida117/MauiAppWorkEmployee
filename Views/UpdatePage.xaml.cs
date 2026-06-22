using MauiAppWorkEmployee.Services;
using MauiAppWorkEmployee.Models;



namespace MauiAppWorkEmployee.Views
{
    
    public partial class UpdatePage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        public UpdatePage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
        }


        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            Employee emp = new Employee
            {
                Name = "dan",
                BirthYear = 1999,
                Email = "dan@gmail.com",
                Password = "dan12345"
            };
            bool isUpdated = await _firebaseService.UpdateEmployeeByNameAsync("ישראל ישראלי",emp);

            if (isUpdated)
                await DisplayAlert("הצלחה", "העובד עודכן", "אישור");
            else
                await DisplayAlert("שגיאה", "העובד לא נמצא או שהפעולה נכשלה", "אישור");
        }
    }
}