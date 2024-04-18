
using Microsoft.AspNetCore.Mvc;
using HelloWorld240318.Service;

namespace HelloWorld240318.Controllers
{
    public class UsersDetailOneController : Controller
    {
        private readonly IUsersDetailOneService _service;

        public UsersDetailOneController(IUsersDetailOneService service)
        {
            _service = service;
        }

        public IActionResult Index(QueryModel.UsersDetailOne qModel)
        {
            var d = new UsersDetailOneService().GetAllUsersDetailOne(qModel).ToList();

            return View(new ViewModels.UsersDetailOneSearch()
            {
                UsersDetailOneList = _service.GetAllData(qModel),
                QName = qModel.Name,
                QIdentityNum = qModel.IdentityNum
            });
        }

        public IActionResult EditPage(long? id)
        {
            var d = new UsersDetailOneService().GetUsersDetailOneById(id);

            return View(_service.EditPage(id));
        }

        public IActionResult UpdateData(ViewModels.UsersDetailOne data)
        {
            _service.UpDateData(data);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteData(long? id)
        {
            _service.DeleteData(id);
            return RedirectToAction("Index");
        }

        public IActionResult ExcelDownload(long? id)
        {
            try
            {
                var excelDatas = new MemoryStream();
                string fileName = "ExcelReportTest";
                string extension = ".xls";
                string applicationType = "application/vnd.ms-excel";
                extension = ".ods";
                applicationType = "application/vnd.oasis.opendocument.spreadsheet";
                var q = _service.EditPage(id);
                bool isIdNull = false;

                if (id > 0)
                {
                    if (q.ID > 0)
                    {
                        fileName = $"{q.Name}個資{DateTime.Now.ToString("yyMMddHHmmss")}";
                    }
                    else
                    {
                        fileName = $"ExcelReport{DateTime.Now.ToString("yyMMddHHmmss")}";
                    }
                }
                else
                {
                    isIdNull = true;
                }

                excelDatas = new UsersDetailOneService().ExcelCreate(q, isIdNull);
                fileName = fileName + extension;
                return File(excelDatas.ToArray(), applicationType, fileName);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult PdfDownload(long? id)
        {
            var pdfDatas = new MemoryStream();
            string fileName = "PdfReportTest";
            var q = _service.EditPage(id);
            bool isIdNull = false;

            try
            {
                if (id > 0)
                {
                    if (q.ID > 0)
                    {
                        fileName = $"{q.Name}個資{DateTime.Now.ToString("yyMMddHHmmss")}";
                    }
                    else
                    {
                        fileName = $"PdfReport{DateTime.Now.ToString("yyMMddHHmmss")}";
                    }
                }
                else
                {
                    isIdNull = true;
                }

                pdfDatas = new UsersDetailOneService().PdfCreate(q, isIdNull);
                fileName = $"{fileName}.pdf";
                return File(pdfDatas.ToArray(), "application/pdf", fileName);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
