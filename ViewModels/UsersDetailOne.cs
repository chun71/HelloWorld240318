
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

    public DateTime? Birthday { get; set; }

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

    public string SexMsg { get { return Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString(); } }

    public string IsMarryMsg { get { return IsMarry == true ? "已婚" : "未婚"; } }

    public string CommutingMsg { get { return Common.Common.commutingList.ContainsKey(Commuting) ? Common.Common.commutingList[Commuting] : ""; } }

    public string BirthdayMsg { get { return Birthday.HasValue ? Birthday.Value.ToString("yyyy-MM-dd") : ""; } }

    public List<IdAndName> CommutingMsgList
    {
        get
        {
            var commutingList = Common.Common.commutingList;
            var commutingMsgList = new List<ViewModels.IdAndName>();

            for (int i = 0; i < commutingList.Count(); i++)
            {
                if (commutingList.ContainsKey(i))
                {
                    var m = new IdAndName()
                    {
                        ID = i,
                        Name = commutingList[i]
                    };
                    commutingMsgList.Add(m);
                }
            };

            return commutingMsgList;
        }
    }

    public IFormFile Image { get; set; }
}
