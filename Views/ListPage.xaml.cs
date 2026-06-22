using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;



namespace MauiAppWorkEmployee.Views
{
  
    public partial class ListPage : ContentPage
    {
       
        private readonly FirebaseService _firebaseService;
        List<WorkHistory> _workHistory = new List<WorkHistory>();
       
        public ListPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            BindingContext=this;
           
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

        private async void OnSearchButtonClicked2(object sender, EventArgs e)
        {
          //  string searchName = "ישראל"; // או מתוך תיבת טקסט Entry
            DateTime dt= DateTime.Now;

            // שליפת הנתונים מהשרת באמצעות הפונקציה שבנינו בשלב הקודם
            List<WorkHistory> jobs = await _firebaseService.GetWorksAsync();

            if (jobs != null)
            {
                foreach (WorkHistory work in jobs)
                {
                    if (work.WorkDate < dt)
                    {
                        _workHistory.Add(work);
                    }
                }
                //הזנת הנתונים לטבלה המעוצבת ב - XAML
                JobsCollectionView.ItemsSource = _workHistory;
               // JobsCollectionView.ItemsSource = jobs;
            }
            else
            {
                await DisplayAlert("שגיאה", "נכשל ניסיון טעינת הנתונים", "אישור");
            }
        }


    }
}