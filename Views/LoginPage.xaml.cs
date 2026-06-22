
using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;

namespace MauiAppWorkEmployee.Views
{
   
    public partial class LoginPage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        private Employee _person;
        public Employee person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged(nameof(person)); // מעדכן את ה-XAML בזמן אמת שהנתונים השתנו
            }
        }

        public LoginPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            BindingContext = this;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            string searchName = password.Text; // או מתוך תיבת טקסט Entry

            // שליפת הנתונים מהשרת באמצעות הפונקציה שבנינו בשלב הקודם
            person = await _firebaseService.GetEmployeeByPasswordAsync(searchName);


            if (person != null)
            {
                // הזנת הנתונים לטבלה המעוצבת ב-XAML
                await DisplayAlert("הצלחה", "התחברות הצליחה", "אישור");
                // שמירת נתונים פשוטים
                Preferences.Default.Set("UserEmail", person.Email);
                Preferences.Default.Set("UserName", person.Name);
                Preferences.Default.Set("UserId", person.Id);

                await Shell.Current.GoToAsync("/ProfilePage");
            }
            else
            {
                await DisplayAlert("שגיאה", "נכשל ניסיון טעינת הנתונים", "אישור");
            }
        }
    }
}