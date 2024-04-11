using System;
using System.Collections.Generic;

namespace HelloWorld240318.Models;

public partial class Todos
{
    public long ID { get; set; }

    public long UID { get; set; }

    public string Name { get; set; }

    public bool IsDone { get; set; }

    public DateOnly? DeadLine { get; set; }

    public DateTime? InsertDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Users UIDNavigation { get; set; }
}
