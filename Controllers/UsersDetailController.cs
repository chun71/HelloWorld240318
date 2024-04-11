using HelloWorld240318.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;

namespace HelloWorld240318.Controllers
{
    public class UsersDetailController : Controller
    {
        private readonly MemoManagerContext _memoManagerContext;

        public UsersDetailController(MemoManagerContext memoManagerContext)
        {
            _memoManagerContext = memoManagerContext;
        }

        public IActionResult Index(string qName, string qIdentityNum, bool checkSex, int qSex, bool cheakJobYear, int qJobYears)
        {
            var q = _memoManagerContext.UsersDetail.AsNoTracking().AsQueryable();
            var models = new List<ViewModels.UsersDetail>();

            if (!qName.IsNullOrEmpty())
            {
                q = q.Where(m => m.Name.Contains(qName)).AsNoTracking().AsQueryable();
            }

            if (!qIdentityNum.IsNullOrEmpty())
            {
                q = q.Where(m => m.IdentityNum.Contains(qIdentityNum)).AsNoTracking().AsQueryable();
            }

            if (checkSex)
            {
                q = q.Where(m => m.Sex == qSex).AsNoTracking().AsQueryable();
            }

            if (cheakJobYear)
            {
                q = q.Where(m => m.JobYears == qJobYears).AsNoTracking().AsQueryable();
            }

            foreach (var m in q)
            {
                var model = new ViewModels.UsersDetail()
                {
                    ID = m.ID,
                    Name = m.Name,
                    Sex = m.Sex,
                    IsMarry = m.IsMarry,
                    JobYears = m.JobYears,
                    Commuting = m.Commuting,
                    IdentityNum = m.IdentityNum,
                    Birthday = m.Birthday,
                    Address = m.Address,
                    EmailAddress = m.EmailAddress,
                    TelPhone = m.TelPhone,
                    CellPhone = m.CellPhone,
                    Account = m.Account,
                    Password = m.Password,
                    Remark = m.Remark,
                    SexMsg = m.Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString(),
                    BirthdayMsg = m.Birthday.HasValue ? m.Birthday.Value.ToString("yyyy-MM-dd") : "",
                    IsMarryMsg = m.IsMarry ? "已婚" : "未婚",
                    CommutingMsg = Common.Common.commutingList.ContainsKey(m.Commuting) ? Common.Common.commutingList[m.Commuting] : ""
                };
                models.Add(model);
            }

            var data = new ViewModels.UsersDetailSearch()
            {
                UsersDetailList = models,
                QName = qName,
                QIdentityNum = qIdentityNum,
                CheckSex = checkSex,
                QSex = qSex,
                CheakJobYear = cheakJobYear,
                QJobYears = qJobYears
            };

            return View(data);
        }

        public IActionResult EditPage(long? id)
        {
            var model = new UsersDetail();

            if (id > 0)
            {
                model = _memoManagerContext.UsersDetail.AsNoTracking().Where(m => m.ID == id).FirstOrDefault();
            }

            var commutingList = Common.Common.commutingList;
            List<ViewModels.IdAndName> commutingMsgList = new List<ViewModels.IdAndName>();

            for (int i=0;  i < commutingList.Count(); i++) 
            {
                ViewModels.IdAndName commutingMsg = new ViewModels.IdAndName();
                if (commutingList.ContainsKey(i))
                {
                    commutingMsg.ID = i;
                    commutingMsg.Name = commutingList[i];
                    commutingMsgList.Add(commutingMsg);
                }
            }

            var data = new ViewModels.UsersDetail()
            {
                ID = model.ID,
                Name = model.Name,
                Sex = model.Sex,
                IsMarry = model.IsMarry,
                JobYears = model.JobYears,
                Commuting = model.Commuting,
                IdentityNum = model.IdentityNum,
                Birthday = model.Birthday,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                TelPhone = model.TelPhone,
                CellPhone = model.CellPhone,
                Account = model.Account,
                Password = model.Password,
                Remark = model.Remark,
                BirthdayMsg = model.Birthday.HasValue ? model.Birthday .Value.ToString("yyyy-MM-dd") : "",
                CommutingMsgList = commutingMsgList,
            };

            return View(data);
        }

        public IActionResult UpdateData(ViewModels.UsersDetail data)
        {
            var model = new UsersDetail();
            var q = _memoManagerContext.UsersDetail.Where(m => m.ID == data.ID).AsNoTracking().FirstOrDefault();

            if (q != null)
            {
                model = q;
            }

            model.Name = data.Name;
            model.Sex = data.Sex;
            model.IsMarry = data.IsMarry;
            model.JobYears = data.JobYears;
            model.Commuting = data.Commuting;
            model.IdentityNum = data.IdentityNum;
            model.Birthday = data.Birthday;
            model.Address = data.Address;
            model.EmailAddress = data.EmailAddress;
            model.TelPhone = data.TelPhone;
            model.CellPhone = data.CellPhone;
            model.Account = data.Account;
            model.Password = data.Password;
            model.Remark = data.Remark;

            if (q == null)
            {
                model.CreateDate = DateTime.Now;
                _memoManagerContext.UsersDetail.Add(model);
            }
            else
            {
                model.ID = data.ID;
                model.CreateDate = data.CreateDate;
                model.UpdateDate = DateTime.Now;
                _memoManagerContext.UsersDetail.Update(model);
            }
            _memoManagerContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteData(long? id)
        {
            _memoManagerContext.UsersDetail.AsNoTracking().Where(m => m.ID == id).ExecuteDelete();
            return RedirectToAction("Index");
        }
    }
}
