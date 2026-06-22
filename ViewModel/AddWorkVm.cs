using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;

using Firebase.Database.Query;
using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;


namespace MauiAppWorkEmployee.ViewModel
{
    public partial class AddWorkVm: ObservableObject
    {

        // קישור הנתונים לפקד ה-UI
        [ObservableProperty]
        private DateTime _workD = DateTime.Now;

        [ObservableProperty]
        private string _jobT;


        [ObservableProperty]
        private string _locationW;

        [ObservableProperty]
        private double _jobType;


        private readonly FirebaseService _firebaseService;

        public AddWorkVm()
        {
            // החלף בכתובת ה-Firebase URL שלך מהקונסול
            _firebaseService = new FirebaseService();
        }

        [RelayCommand]
        public async Task SaveToFirebase()
        {
            var newJob = new WorkHistory
            {
                JodId = Guid.NewGuid().ToString(),
                //Description = Description,
                //SelectedDate = AppointmentDate // שימוש בתאריך המעודכן מהקישור
                JobType = JobT,
                Location = LocationW,
                WorkDate = WorkD // מקבל את ה-DateTime שנבחר בלוח השנה
               
            };

            try
            {
                // שמירת האובייקט תחת ענף בשם "Appointments"
              

                bool isSuccess2 = await _firebaseService.SaveWorkAsync(newJob);
                if (isSuccess2)
                {
                    await Shell.Current.DisplayAlert("הצלחה", "התאריך נשמר בהצלחה ב-Firebase!", "OK");
                }
               
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("שגיאה", $"השמירה נכשלה: {ex.Message}", "OK");
            }
        }
    }



}
