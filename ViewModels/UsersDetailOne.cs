﻿using System;
using System.Collections.Generic;
using System.Web;

namespace HelloWorld240318.ViewModels;

public partial class UsersDetailOne
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

    public string Remark1 { get; set; }

    public string Remark2 { get; set; }

    public string Remark3 { get; set; }

    public string Remark4 { get; set; }

    public string Remark5 { get; set; }

    public string Remark6 { get; set; }

    public string Remark7 { get; set; }

    public string Remark8 { get; set; }

    public string Remark9 { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string ImgPath { get; set; }

    public string SexMsg { get; set; }

    public string IsMarryMsg { get; set; }

    public string CommutingMsg { get; set; }

    public string BirthdayMsg { get; set; }

    public  List<IdAndName>CommutingMsgList { get; set; }

    public IFormFile Image { get; set; }
}