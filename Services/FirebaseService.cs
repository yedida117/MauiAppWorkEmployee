using Firebase.Database;
using Firebase.Database.Query;
using MauiAppWorkEmployee.Models;

namespace MauiAppWorkEmployee.Services
{
    public class FirebaseService
    {

        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            // החלף את הכתובת הזו בכתובת ה-URL של ה-Firebase Realtime Database שלך
            string firebaseDatabaseUrl = "https://mauiappworkemployee-default-rtdb.firebaseio.com/";

            _firebaseClient = new FirebaseClient(firebaseDatabaseUrl);
        }

        /// <summary>
        /// שמירת עובד חדש יחד עם היסטוריית העבודה שלו ב-Firebase
        /// </summary>
        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                // יצירת רשומה חדשה תחת ענף "Employees" וקבלת מזהה ייחודי
                var result = await _firebaseClient
                    .Child("Employees")
                    .PostAsync(employee);

                // עדכון ה-Id של האובייקט המקומי במפתח שנוצר ב-Firebase (אופציונלי)
                employee.Id = result.Key;

                return true;
            }
            catch (Exception ex)
            {
                // כאן ניתן להוסיף לוג לשגיאות
                System.Diagnostics.Debug.WriteLine($"Error saving to Firebase: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// שליפת רשימת מקומות העבודה של עובד לפי השם שלו
        /// </summary>
        public async Task<List<WorkHistory>> GetWorkHistoryByEmployeeNameAsync(string employeeName)
        {
            try
            {
                // 1. שליפת כל העובדים ששמם שווה לשם המבוקש
                // הערה: בשביל יעילות ב-Firebase, מומלץ להגדיר ".indexOn": "Name" בחוקי השרת
                var employees = await _firebaseClient
                    .Child("Employees")
                    //.OrderBy("Name")
                    //.EqualTo(employeeName)
                    .OnceAsync<Employee>();

                // 2. לקיחת העובד הראשון שנמצא (בהנחה שהשמות ייחודיים או שרוצים את התוצאה הראשונה)
                var targetEmployee = employees.FirstOrDefault();

                if (targetEmployee != null && targetEmployee.Object.WorkHistories != null)
                {
                    // 3. החזרת רשימת עבודותיו של העובד שנמצא
                    return targetEmployee.Object.WorkHistories;
                }

                // אם העובד לא נמצא או שאין לו היסטוריית עבודה
                return new List<WorkHistory>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving data: {ex.Message}");
                return null;
            }
        }


            public async Task<List<WorkHistory>> GetWorkHistoryByEmployeeNameAsync2(string employeeName)
        {
            try
            {
                // 1. שליפת כל העובדים מהשרת
                var allEmployees = await _firebaseClient
                    .Child("Employees")
                    .OnceAsync<Employee>();

                // 2. מעבר על כל העובדים בלולאה כדי למצוא את העובד המתאים
                foreach (var item in allEmployees)
                {
                    // חילוץ אובייקט העובד מתוך ה-Firebase Object
                    Employee employee = item.Object;

                    // תנאי if: בדיקה האם השם של העובד הנוכחי שווה לשם שחיפשנו
                    if (employee.Name == employeeName)
                    {
                        // אם מצאנו אותו, נבדוק שיש לו היסטוריית עבודה ונחזיר אותה מיד
                        if (employee.WorkHistories != null)
                        {
                            return employee.WorkHistories;
                        }
                    }
                }

                // אם הלולאה הסתיימה ולא מצאנו עובד בשם זה, נחזיר רשימה ריקה
                return new List<WorkHistory>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving data: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// עדכון נתוני עובד והיסטוריית העבודה שלו לפי השם שלו
        /// </summary>
        public async Task<bool> UpdateEmployeeByNameAsync(string employeeName, Employee updatedEmployee)
        {
            try
            {
                // 1. שליפת כל העובדים מהשרת
                var allEmployees = await _firebaseClient
                    .Child("Employees")
                    .OnceAsync<Employee>();

                // 2. לולאה למציאת העובד המתאים
                foreach (var item in allEmployees)
                {
                    if (item.Object.Name == employeeName)
                    {
                        // 3. עדכון הנתונים בשרת לפי המפתח הייחודי (Key) שלו
                        await _firebaseClient
                            .Child("Employees")
                            .Child(item.Key) // המפתח הייחודי ב-Firebase
                            .PutAsync(updatedEmployee); // PutAsync מחליף את הנתונים הישנים בחדשים

                        return true; // העדכון הצליח
                    }
                }

                return false; // העובד לא נמצא
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// מחיקת עובד לחלוטין מהשרת לפי השם שלו
        /// </summary>
        public async Task<bool> DeleteEmployeeByNameAsync(string employeeName)
        {
            try
            {
                // 1. שליפת כל העובדים מהשרת
                var allEmployees = await _firebaseClient
                    .Child("Employees")
                    .OnceAsync<Employee>();

                // 2. לולאה למציאת העובד המתאים
                foreach (var item in allEmployees)
                {
                    if (item.Object.Name == employeeName)
                    {
                        // 3. מחיקת הרשומה הספציפית מהשרת באמצעות הפקודה DeleteAsync
                        await _firebaseClient
                            .Child("Employees")
                            .Child(item.Key) // המפתח הייחודי ב-Firebase
                            .DeleteAsync();

                        return true; // המחיקה הצליחה
                    }
                }

                return false; // העובד לא נמצא
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting: {ex.Message}");
                return false;
            }
        }


    }


}

  







