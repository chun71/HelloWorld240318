using System;
using System.Collections.Generic;

namespace HelloWorld240318.Models;

public partial class UsersDetail
{
    public long ID { get; set; }

    public string Name { get; set; }

    public int Sex { get; set; }

    public bool IsMarry { get; set; }

    public int JobYears { get; set; }

    public int Commuting { get; set; }

    public string IdentityNum { get; set; }

    public DateOnly? Birthday { get; set; }

    public string Address { get; set; }

    public string EmailAddress { get; set; }

    public string TelPhone { get; set; }

    public string CellPhone { get; set; }

    public string Account { get; set; }

    public string Password { get; set; }

    public string Remark { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
