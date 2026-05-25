using MauiAppWorkEmployee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MauiAppWorkEmployee.Views
{

    public partial class DeletePage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        public DeletePage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool isDeleted = await _firebaseService.DeleteEmployeeByNameAsync("ישראל ישראלי");

            if (isDeleted)
                await DisplayAlert("הצלחה", "העובד נמחק מהמערכת", "אישור");
            else
                await DisplayAlert("שגיאה", "העובד לא נמצא או שהפעולה נכשלה", "אישור");
        }

    }
}