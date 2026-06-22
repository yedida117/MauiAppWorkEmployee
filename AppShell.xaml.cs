using MauiAppWorkEmployee.Views;

namespace MauiAppWorkEmployee
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ListPage", typeof(ListPage));
            Routing.RegisterRoute("DeletePage", typeof(DeletePage));
            Routing.RegisterRoute("UpdatePage", typeof(UpdatePage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("AddWorkPage", typeof(AddWorkPage));
            Routing.RegisterRoute("AddWorkPage2", typeof(AddWorkPage2));
            BindingContext = this;
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
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
