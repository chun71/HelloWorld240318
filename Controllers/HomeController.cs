using HelloWorld240318.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace HelloWorld240318.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MemoManagerContext _memoManagerContext;

        public HomeController(ILogger<HomeController> logger, MemoManagerContext memoManagerContext)
        {
            _logger = logger;
            _memoManagerContext = memoManagerContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowList(long? uid, string name, DateOnly? deadLine, DateTime? insertDate, DateTime? updateDate)
        {
            var q = (from t in _memoManagerContext.Todos
                              join u in _memoManagerContext.Users on t.UID equals u.ID
                              select new ViewModels.Todos
                              {
                                  ID = t.ID,
                                  UID = t.UID,
                                  Name = u.Name,
                                  CnName = Common.Common.nameList.ContainsKey(u.Name) ? Common.Common.nameList[u.Name] : "",
                                  ItemName = t.Name,
                                  DeadLine = t.DeadLine,
                                  IsDone = t.IsDone,
                                  InsertDate =t.InsertDate,
                                  UpdateDate =t.UpdateDate,
                              }).AsNoTracking().AsQueryable();

            if (uid > 0) 
            {
                q = q.Where(m => m.UID == uid).AsNoTracking().AsQueryable();
            }

            if (!name.IsNullOrEmpty())
            {
                q = q.Where(m => m.ItemName.Contains(name)).AsNoTracking().AsQueryable();
            }

            if(deadLine != null)
            {
                q = q.Where(m => m.DeadLine < deadLine.Value).AsNoTracking().AsQueryable();
            }

            if (insertDate != null)
            {
                q = q.Where(m => m.InsertDate < insertDate.Value.AddDays(1) & m.InsertDate > insertDate.Value.AddDays(-1)).AsNoTracking().AsQueryable();
            }

            if (updateDate != null)
            {
                q = q.Where(m => m.UpdateDate < updateDate.Value.AddDays(1) & m.UpdateDate > updateDate.Value.AddDays(-1)).AsNoTracking().AsQueryable();
            }

            List<ViewModels.Todos> todoList = q.AsNoTracking().ToList();
            ViewModels.TodosSearch todosSearch = new ViewModels.TodosSearch();
            todosSearch.TodoList = todoList;
            todosSearch.UID = uid; 
            todosSearch.Name = name;
            todosSearch.DeadLine = deadLine == null ? "" : deadLine.Value.ToString("yyyy-MM-dd");
            todosSearch.UpdateDate = updateDate == null ? "" : updateDate.Value.ToString("yyyy-MM-dd"); 
            todosSearch.InsertDate = insertDate == null ? "" : insertDate.Value.ToString("yyyy-MM-dd");
            todosSearch.NameList = _memoManagerContext.Users.AsNoTracking().Select(m => new ViewModels.IdAndName { ID = m.ID, Name = m.Name }).ToList();

            return View(todosSearch);
            //return View();
        }

        public IActionResult GetTodosData() 
        {
            var q = (from t in _memoManagerContext.Todos
                     join u in _memoManagerContext.Users on t.UID equals u.ID
                     select new ViewModels.Todos
                     {
                         ID = t.ID,
                         UID = t.UID,
                         Name = u.Name,
                         CnName = Common.Common.nameList.ContainsKey(u.Name) ? Common.Common.nameList[u.Name] : "",
                         ItemName = t.Name,
                         DeadLine = t.DeadLine,
                         IsDone = t.IsDone,
                         InsertDate = t.InsertDate,
                         UpdateDate = t.UpdateDate,
                     }).AsNoTracking().AsQueryable();

            //if (uid > 0)
            //{
            //    q = q.Where(m => m.UID == uid).AsNoTracking().AsQueryable();
            //}

            //if (!name.IsNullOrEmpty())
            //{
            //    q = q.Where(m => m.ItemName.Contains(name)).AsNoTracking().AsQueryable();
            //}

            //if (deadLine != null)
            //{
            //    q = q.Where(m => m.DeadLine < deadLine.Value).AsNoTracking().AsQueryable();
            //}

            //if (insertDate != null)
            //{
            //    q = q.Where(m => m.InsertDate < insertDate.Value.AddDays(1) & m.InsertDate > insertDate.Value.AddDays(-1)).AsNoTracking().AsQueryable();
            //}

            //if (updateDate != null)
            //{
            //    q = q.Where(m => m.UpdateDate < updateDate.Value.AddDays(1) & m.UpdateDate > updateDate.Value.AddDays(-1)).AsNoTracking().AsQueryable();
            //}

            List<ViewModels.Todos> todoList = q.AsNoTracking().ToList();
            //ViewModels.TodosSearch todosSearch = new ViewModels.TodosSearch();
            //todosSearch.TodoList = todoList;
            //todosSearch.UID = uid;
            //todosSearch.Name = name;
            //todosSearch.DeadLine = deadLine == null ? "" : deadLine.Value.ToString("yyyy-MM-dd");
            //todosSearch.UpdateDate = updateDate == null ? "" : updateDate.Value.ToString("yyyy-MM-dd");
            //todosSearch.InsertDate = insertDate == null ? "" : insertDate.Value.ToString("yyyy-MM-dd");
            //todosSearch.NameList = _memoManagerContext.Users.AsNoTracking().Select(m => new ViewModels.IdAndName { ID = m.ID, Name = m.Name }).ToList();
            return new JsonResult(todoList);
        }


        public IActionResult Edit(long? id, long? itemNum)
        {
            var data = new ViewModels.TodosEdit();

            if (id == null)
            {
                data.ItemNum = _memoManagerContext.Todos.AsNoTracking().Count() + 1;
            }
            else
            {
                var q = _memoManagerContext.Todos.AsNoTracking().Where(m => m.ID == id).FirstOrDefault();

                data.ID = q.ID;
                data.UID = q.UID;
                data.ItemNum = itemNum;
                data.Name = _memoManagerContext.Users.AsNoTracking().Where(m => m.ID == q.UID).FirstOrDefault().Name;
                data.ItemName = q.Name;
                data.IsDone = q.IsDone;
                data.DeadLineMsg = q.DeadLine.HasValue ? q.DeadLine.Value.ToString("yyyy-MM-dd") : "";
                data.InsertDate = q.InsertDate;
                data.UpdateDate = q.UpdateDate;
            }

            data.NameList = _memoManagerContext.Users.AsNoTracking().Select(m => new ViewModels.IdAndName{ID=m.ID, Name =m.Name }).ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(ViewModels.Todos data)
        {
            var todosModel = new Todos();
            todosModel.ID = data.ID;
            todosModel.UID = data.UID;

            if (data.Name.IsNullOrEmpty())
            {
                todosModel.Name = "";
            }
            else 
            {
                todosModel.Name = data.Name;
            }

            todosModel.IsDone = data.IsDone;
            todosModel.DeadLine = data.DeadLine;

            var q = _memoManagerContext.Todos.AsNoTracking().Where(m => m.ID == data.ID).FirstOrDefault();
            if (q == null)
            {
                todosModel.InsertDate = DateTime.Now;
                _memoManagerContext.Todos.Add(todosModel);
            }
            else
            {
                todosModel.InsertDate = q.InsertDate;
                todosModel.UpdateDate = DateTime.Now;
                _memoManagerContext.Todos.Update(todosModel);
            }

            _memoManagerContext.SaveChanges();
            return RedirectToAction("ShowList");
        }

        public IActionResult Delete(int? id)
        {
            _memoManagerContext.Todos.AsNoTracking().Where(m => m.ID == id).ExecuteDelete();
            return RedirectToAction("ShowList");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
