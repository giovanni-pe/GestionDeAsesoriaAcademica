using System;

namespace CleanArchitecture.Domain.Entities
{
    public class Appointment : Entity
    {

        public Guid ProfessorId { get; private set; }
        public virtual Professor Professor { get; private set; } = null!;
        public Guid StudentId { get; private set; }
        public virtual Student Student { get; private set; } = null!;
        public Guid CalendarId { get; private set; }
        public DateTime DateTime { get; private set; }
        public string ProfessorProgress { get; private set; }
        public string StudentProgress { get; private set; }
        public string Status { get; private set; }
        public string GoogleEventId { get; private set; } 


        public Appointment(Guid id, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress,String status,string googleEventId) : base(id)
        {
            ProfessorId = professorId;
            StudentId = studentId;
            CalendarId = calendarId;
            DateTime = dateTime;
            ProfessorProgress = professorProgress;
            StudentProgress = studentProgress;
            Status = status;
            GoogleEventId = googleEventId;
        }

   

        public void SetDateTime(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public void SetProfessorProgress(string professorProgress)
        {
            ProfessorProgress = professorProgress;
        }

        public void SetStudentProgress(string studentProgress)
        {
            StudentProgress = studentProgress;
        }
        public void SetStatus(string status) {  Status = status; }
        public void SetGoogleEventId(string googleEventId)
        {
            GoogleEventId = googleEventId;
        }
    }
}
