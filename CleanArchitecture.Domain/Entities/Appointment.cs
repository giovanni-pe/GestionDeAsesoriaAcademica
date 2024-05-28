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

        public Appointment(Guid id, Guid professorId, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(id)
        {
            ProfessorId = professorId;
            StudentId = studentId;
            CalendarId = calendarId;
            DateTime = dateTime;
            ProfessorProgress = professorProgress;
            StudentProgress = studentProgress;
        }

        public Appointment(Guid id, Guid studentId, Guid calendarId, DateTime dateTime, string professorProgress, string studentProgress) : base(id)
        {
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
    }
}
