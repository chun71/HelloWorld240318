using System;
using System.Collections.Generic;

namespace HelloWorld240318.Models;

public partial class Users
{
    public long ID { get; set; }

    public string Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Todos> Todos { get; set; } = new List<Todos>();
}
