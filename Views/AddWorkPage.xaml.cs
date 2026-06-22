

using MauiAppWorkEmployee.Models;
using MauiAppWorkEmployee.Services;
using System;
using Microsoft.Maui.Controls;

namespace MauiAppWorkEmployee.Views
{
  
    public partial class AddWorkPage : ContentPage
    {
        private readonly FirebaseService _firebaseService;
        public AddWorkPage()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
        }

       

            private async void OnSaveJobButtonClicked(object sender, EventArgs e)
            {
                // 1. בדיקת תקינות ראשונית - שדות טקסט לא ריקים
                if (string.IsNullOrWhiteSpace(JobIdEntry.Text) ||
                    string.IsNullOrWhiteSpace(JobTypeEntry.Text) ||
                    string.IsNullOrWhiteSpace(LocationEntry.Text))
                {
                    await DisplayAlert("שגיאה", "אנא מלאי את כל שדות החובה (מזהה, סוג ומיקום).", "אישור");
                    return;
                }

                // 2. המרת ערכים נומריים בבטחה (בדיקה שהוקלדו מספרים תקינים)
                if (!double.TryParse(HoursWorkedEntry.Text, out double hours) || hours <= 0)
                {
                    await DisplayAlert("שגיאה", "אנא הכניסי מספר שעות עבודה תקין וגדול מ-0.", "אישור");
                    return;
                }

                if (!double.TryParse(HourlyRateEntry.Text, out double rate) || rate <= 0)
                {
                    await DisplayAlert("שגיאה", "אנא הכניסי שכר לשעה תקין וגדול מ-0.", "אישור");
                    return;
                }

                // 3. יצירת אובייקט ה-WorkHistory החדש עם הנתונים מהמסך
                var newJob = new WorkHistory
                {
                    //JodId = JobIdEntry.Text.Trim(),
                    JobType = JobTypeEntry.Text.Trim(),
                    Location = LocationEntry.Text.Trim(),
                    WorkDate = WorkDatePicker.Date, // מקבל את ה-DateTime שנבחר בלוח השנה
                    HoursWorked = hours,
                    HourlyRate = rate
                };

                // 4. שליפת המייל של המשתמש המחובר כרגע מתוך ה-Preferences
                string loggedInUserEmail = Preferences.Default.Get("UserEmail", string.Empty);

                if (string.IsNullOrEmpty(loggedInUserEmail))
                {
                    await DisplayAlert("שגיאה", "לא נמצא משתמש מחובר. אנא התחברי מחדש.", "אישור");
                    return;
                }

                // הצגת אינדיקטור טעינה קטן (אופציונלי)
                SaveJobBtn.IsEnabled = false;
                SaveJobBtn.Text = "שומר בשרת...";

                // 5. שליפת אובייקט העובד המלא מהפיירבייס כדי להוסיף לו את העבודה החדשה ברשימה
                Employee currentEmployee = await _firebaseService.GetEmployeeByEmailAsync(loggedInUserEmail);

            bool isSuccess2 = await _firebaseService.SaveWorkAsync(newJob);

            if (currentEmployee != null&& isSuccess2)
                {
                    // הוספת מקום העבודה החדש לרשימת מקומות העבודה של העובד
                    currentEmployee.WorkHistories.Add(newJob);
                    currentEmployee.WorkId.Add(newJob.JodId);

                // עדכון האובייקט המלא בפיירבייס (משתמש בפונקציית העדכון שבנינו)
                bool isSuccess = await _firebaseService.UpdateEmployeeByNameAsync(currentEmployee.Name, currentEmployee);

                    if (isSuccess)
                    {
                        await DisplayAlert("הצלחה", "מקום העבודה נשמר בהצלחה בפיירבייס!", "אישור");
                        ClearForm(); // ניקוי הטופס לאחר שמירה מוצלחת
                    }
                    else
                    {
                        await DisplayAlert("שגיאה", "נכשלה שמירת הנתונים בשרת.", "אישור");
                    }
                }
                else
                {
                    await DisplayAlert("שגיאה", "שגיאה בשליפת פרטי העובד המחובר.", "אישור");
                }

                // החזרת מצב הכפתור לקדמותו
                SaveJobBtn.IsEnabled = true;
                SaveJobBtn.Text = "שמירת נתונים בפיירבייס";
            }

            /// <summary>
            /// פונקציית עזר לניקוי שדות הטופס לאחר השמירה
            /// </summary>
            private void ClearForm()
            {
                JobIdEntry.Text = string.Empty;
                JobTypeEntry.Text = string.Empty;
                LocationEntry.Text = string.Empty;
                HoursWorkedEntry.Text = string.Empty;
                HourlyRateEntry.Text = string.Empty;
                WorkDatePicker.Date = DateTime.Now;
            }
        }
    }


