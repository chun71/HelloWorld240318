using System;
using System.Collections.Generic;

namespace HelloWorld240318.ViewModels;

public  class UsersDetailSearch
{
    public List<UsersDetail> UsersDetailList { get; set; }

    public string QName { get; set; }

    public string QIdentityNum { get; set; }
    
    public bool CheckSex { get; set; }

    public int QSex { get; set; }

    public bool CheakJobYear { get; set; }
    
    public int QJobYears { get; set; }
}
