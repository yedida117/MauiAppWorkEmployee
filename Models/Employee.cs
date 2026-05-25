using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppWorkEmployee.Models
{
    public class Employee
    {
        public string Id { get; set; }             // מזהה ייחודי ב-Firebase
        public string Name { get; set; }           // שם
        public string Email { get; set; }          // מייל
        public string Password { get; set; }       // סיסמה (הערה: מומלץ להצפין!)
        public int BirthYear { get; set; }         // שנת לידה

        // רשימת מקומות העבודה בשנה האחרונה
        public List<WorkHistory> WorkHistories { get; set; } = new List<WorkHistory>();
    }
}
