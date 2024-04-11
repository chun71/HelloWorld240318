using System;
using System.Collections.Generic;

namespace HelloWorld240318.Models;

public partial class ExcelReport
{
    public long ID { get; set; }

    public string Name { get; set; }

    public int? JobYears { get; set; }

    public string IdentityNum { get; set; }

    public DateOnly? Birthday { get; set; }

    public string Address { get; set; }

    public string EmailAddress { get; set; }

    public string CellPhone { get; set; }

    public string Remark { get; set; }
}
