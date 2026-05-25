using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;



namespace MauiAppWorkEmployee.Views
{
  
    public partial class ListPage : ContentPage
    {
        Task task;
        private readonly FirebaseService _firebaseService;
        public ListPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
           
        }


        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string searchName = "ישראל"; // או מתוך תיבת טקסט Entry

            // שליפת הנתונים מהשרת באמצעות הפונקציה שבנינו בשלב הקודם
            List<WorkHistory> jobs = await _firebaseService.GetWorkHistoryByEmployeeNameAsync2(searchName);

            if (jobs != null)
            {
                // הזנת הנתונים לטבלה המעוצבת ב-XAML
                JobsCollectionView.ItemsSource = jobs;
            }
            else
            {
                await DisplayAlert("שגיאה", "נכשל ניסיון טעינת הנתונים", "אישור");
            }
        }

    }
}