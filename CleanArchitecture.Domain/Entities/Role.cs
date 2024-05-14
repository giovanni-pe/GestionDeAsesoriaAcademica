using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public virtual ICollection<User> Users { get; private set; } = new HashSet<User>();

    public Role(
        Guid id,
        string name,string description) : base(id)
    {
        Name = name;
        Description= description;
    }

    public void SetName(string name)
    {
        Name = name;
    }
    public void SetDescription(string description)
    {
        Description = description;
    }
}