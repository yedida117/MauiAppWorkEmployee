using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;


namespace MauiAppWorkEmployee
{
    public partial class MainPage : ContentPage
    {
        private readonly FirebaseService _firebaseService;

        public MainPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
        }


        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // 1. יצירת אובייקט העובד והזנת נתונים
            var newEmployee = new Employee
            {
                Name = "ישראל ישראלי",
                Email = "israel@email.com",
                Password = "SecurePassword123", // שים לב: באפליקציית ייצור מומלץ להשתמש ב-Firebase Authentication לניהול סיסמאות
                BirthYear = 1995
            };

            // 2. הוספת מקומות עבודה מהשנה האחרונה לרשימה
            newEmployee.WorkHistories.Add(new WorkHistory
            {
                JobType = "פיתוח תוכנה",
                Location = "תל אביב",
                WorkDate = DateTime.Now.AddMonths(-3),
                HoursWorked = 160,
                HourlyRate = 85.5
            });

            newEmployee.WorkHistories.Add(new WorkHistory
            {
                JobType = "תמיכה טכנית",
                Location = "חיפה",
                WorkDate = DateTime.Now.AddMonths(-8),
                HoursWorked = 45,
                HourlyRate = 50.0
            });

            // 3. שליחה ושמירה ב-Firebase Realtime Database
            bool isSuccess = await _firebaseService.SaveEmployeeAsync(newEmployee);

            if (isSuccess)
            {
                await DisplayAlert("הצלחה", "נתוני העובד והיסטוריית העבודה נשמרו בהצלחה!", "אישור");
            }
            else
            {
                await DisplayAlert("שגיאה", "נכשלה שמירת הנתונים בשרת.", "אישור");
            }
        }

        private async void OnListButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/ListPage");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/DeletePage");
        }

        private async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("/UpdatePage");
        }
    }
}


