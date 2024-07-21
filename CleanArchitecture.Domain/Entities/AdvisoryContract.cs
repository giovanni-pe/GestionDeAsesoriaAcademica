using System;
//Modelo advisory contract
namespace CleanArchitecture.Domain.Entities
{
    public class AdvisoryContract : Entity
    {
        public Guid ProfessorId { get; private set; }
        public virtual Professor Professor { get; private set; } = null!;
        public Guid StudentId { get; private set; }
        public virtual Student Student { get; private set; } = null!;
        public Guid ResearchLineId { get; private set; }
        public virtual ResearchLine ResearchLine { get; private set; } = null!;
        public string ThesisTopic { get; private set; }
        public string Message { get; private set; }
        public string ProfessorMessage { get; private set; }
        public int Status { get; private set; }

        public DateTime DateCreated { get; private set; }
        public AdvisoryContract(Guid id, Guid professorId, Guid studentId, Guid researchLineId, string thesisTopic, string message, int status,DateTime dateCreated) : base(id)
        {
            ProfessorId = professorId;
            StudentId = studentId;
            ResearchLineId = researchLineId;
            ThesisTopic = thesisTopic;
            Message = message;
            Status = status;
            DateCreated = dateCreated;
            ProfessorMessage="i";
        }

        public void SetThesisTopic(string thesisTopic)
        {
            ThesisTopic = thesisTopic;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }
        public void SetProfessorMessage(string professorMessage)
        {
            ProfessorMessage = professorMessage;
        }

        public void SetStatus(int status)
        {
            Status = status;
        }
    }
}
