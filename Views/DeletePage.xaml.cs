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
            bool isDeleted = await _firebaseService.DeleteEmployeeByNameAsync("dan");

            if (isDeleted)
                await DisplayAlert("הצלחה", "העובד נמחק מהמערכת", "אישור");
            else
                await DisplayAlert("שגיאה", "העובד לא נמצא או שהפעולה נכשלה", "אישור");
        }


        private async void OnBackToMainClicked(object sender, EventArgs e)
        {
            // ניווט ישיר למסך הראשי ואיפוס מחסנית הניווט
            await Shell.Current.GoToAsync("//MainPage");
        }


        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // 1. הסרת נתוני המשתמש הספציפיים מה-Preferences
            Preferences.Default.Remove("UserEmail");
            Preferences.Default.Remove("UserName");
            Preferences.Default.Remove("UserId");

            // לחלופין, אם את רוצה למחוק את *כל* מה ששמור באפליקציה בבת אחת, תוכלי להשתמש בזה:
            // Preferences.Default.Clear();

            // 2. במידה והשתמשת גם ב-SecureStorage עבור הסיסמה, מנקים גם אותו:
            SecureStorage.Default.Remove("UserPassword");

            // 3. במידה והשתמשת גם במשתנה סטטי, מאפסים אותו ל-null:
            //   App.LoggedInUser = null;

            // 4. הודעה למשתמש וניווט חזרה למסך ההתחברות (למשל LoginPage)
            await DisplayAlert("התנתקות", "התנתקת מהמערכת בהצלחה", "אישור");

            // ניווט שמנקה את היסטוריית המסכים ומחזיר למסך הראשי
            await Shell.Current.GoToAsync("//LoginPage");
        }


    }
}