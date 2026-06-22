namespace MauiAppWorkEmployee.Models
{
    public class WorkHistory
    {
        public string JodId { get; set; }   
       
        public string JobType { get; set; }        // סוג העבודה
        public string Location { get; set; }       // מקום
       /* public DateOnly WorkDate { get; set; } */    // תאריך

public DateTime WorkDate { get; set; } 
public double HourlyRate { get; set; }     // שכר לשעה
public double HoursWorked { get; set; }    // מספר שעות עבודה
public double TotalSalary => HoursWorked * HourlyRate; // חישוב שכר כולל (אופציונלי)
}
}
