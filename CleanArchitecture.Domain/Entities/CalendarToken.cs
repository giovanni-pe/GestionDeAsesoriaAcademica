using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class CalendarToken : Entity
    {
        public string AccessToken { get;  set; }
        public string RefreshToken { get;  set; }
        public DateTime ExpiryDate { get;  set; }
        public string UserEmail { get;  set; }
        public Guid UserId { get; set; }
        public virtual User User { get;  set; } = null!;
        

        public CalendarToken(Guid id,Guid userId, string accessToken, string refreshToken, DateTime expiryDate, string userEmail) : base(id)
        {
            UserId = userId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiryDate = expiryDate;
            UserEmail = userEmail;
        }

        public void UpdateToken(string accessToken, string refreshToken, DateTime expiryDate)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiryDate = expiryDate;
        }
    }
}
