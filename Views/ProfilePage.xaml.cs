
using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;

namespace MauiAppWorkEmployee.Views
{
   
    public partial class ProfilePage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        private Employee _person;

        // שליפת הנתונים (הפרמטר השני הוא ערך ברירת מחדל אם לא נמצא כלום)
        string userEmail = Preferences.Default.Get("UserEmail", "No Email");
        string userName = Preferences.Default.Get("UserName", "Guest");

        public Employee person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged(nameof(person)); // מעדכן את ה-XAML בזמן אמת שהנתונים השתנו
            }
        }
        public ProfilePage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            BindingContext = this;
        }



        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            string searchName = userEmail; // או מתוך תיבת טקסט Entry

            // שליפת הנתונים מהשרת באמצעות הפונקציה שבנינו בשלב הקודם
            person= await _firebaseService.GetEmployeeByEmailAsync(searchName);
        

            if (person != null)
            {
                // הזנת הנתונים לטבלה המעוצבת ב-XAML
                await DisplayAlert("הצלחה", "הצליח ניסיון טעינת הנתונים", "אישור");

             
            }
            else
            {
                await DisplayAlert("שגיאה", "נכשל ניסיון טעינת הנתונים", "אישור");
            }
        }
    }
}