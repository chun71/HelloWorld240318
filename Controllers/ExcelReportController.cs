using HelloWorld240318.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;

namespace HelloWorld240318.Controllers
{
    public class ExcelReportController : Controller
    {
        private readonly MemoManagerContext _memoManagerContext;

        public ExcelReportController(MemoManagerContext memoManagerContext)
        {
            _memoManagerContext = memoManagerContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportImport(IFormFile file)
        {

            if (file != null)
            {
                Stream stream = file.OpenReadStream(); //使用Stream(流)對檔案進行操作
                DataTable dataTable = new DataTable();
                IWorkbook wb;
                ISheet sheet;
                IRow headerRow;
                int cellCount; //紀錄共有幾欄

                try
                {
                    //依excel版本，NPOI載入檔案
                    if (file.FileName.ToUpper().EndsWith("XLSX"))
                        wb = new XSSFWorkbook(stream); // excel版本(.xlsx)
                    else
                        wb = new HSSFWorkbook(stream); // excel版本(.xls)

                    //取第一個頁籤   
                    sheet = wb.GetSheetAt(0);

                    //取第一個頁籤的第一列
                    headerRow = sheet.GetRow(0);

                    //計算出第一列共有多少欄位
                    cellCount = headerRow.LastCellNum;

                    //迴圈執行第一列的第一個欄位到最後一個欄位，將抓到的值塞進DataTable做完欄位名稱
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        dataTable.Columns.Add(new DataColumn(headerRow.GetCell(i).StringCellValue));
                    }

                    //int j; //計算每一列讀到第幾個欄位
                    int column = 1; //計算每一列讀到第幾個欄位

                    // 略過第零列(標題列)，一直處理至最後一列
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        //取目前的列(row)
                        IRow row = sheet.GetRow(i);

                        //若該列的第一個欄位無資料，break跳出
                        if (string.IsNullOrEmpty(row.Cells[0].ToString().Trim()))
                        {
                            break;
                        }

                        //宣告DataRow
                        DataRow dataRow = dataTable.NewRow();
                        //宣告ICell
                        ICell cell;

                        try
                        {
                            //依先前取得，依每一列的欄位數，逐一設定欄位內容
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                //計算每一列讀到第幾個欄位(秀在錯誤訊息上)
                                column = j + 1;

                                //設定cell為目前第j欄位
                                cell = row.GetCell(j);

                                if (cell != null) //若cell有值
                                {
                                    //用cell.CellType判斷資料的型別
                                    //再依照欄位屬性，用StringCellValue、DateCellValue、NumericCellValue、DateCellValue取值
                                    switch (cell.CellType)
                                    {
                                        //字串型態欄位
                                        case NPOI.SS.UserModel.CellType.String:
                                            //設定dataRow第j欄位的值，cell以字串型態取值
                                            dataRow[j] = cell.StringCellValue;
                                            break;

                                        //數字型態欄位
                                        case NPOI.SS.UserModel.CellType.Numeric:

                                            if (HSSFDateUtil.IsCellDateFormatted(cell)) //日期格式
                                            {
                                                //設定dataRow第j欄位的值，cell以日期格式取值
                                                dataRow[j] = DateTime.FromOADate(cell.NumericCellValue).ToString("yyyy/MM/dd HH:mm");
                                            }
                                            else //非日期格式
                                            {
                                                //設定dataRow第j欄位的值，cell以數字型態取值
                                                dataRow[j] = cell.NumericCellValue;
                                            }
                                            break;

                                        //布林值
                                        case NPOI.SS.UserModel.CellType.Boolean:

                                            //設定dataRow第j欄位的值，cell以布林型態取值
                                            dataRow[j] = cell.BooleanCellValue;
                                            break;

                                        //空值
                                        case NPOI.SS.UserModel.CellType.Blank:

                                            dataRow[j] = "";
                                            break;

                                        // 預設
                                        default:

                                            dataRow[j] = cell.StringCellValue;
                                            break;
                                    }
                                }
                            }
                            //DataTable加入dataRow
                            dataTable.Rows.Add(dataRow);
                        }
                        catch (Exception ex)
                        {
                            //錯誤訊息
                            throw new Exception("第 " + i + "列第" + column + "欄，資料格式有誤:\r\r" + ex.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "從Excel匯入失敗";
                    Console.Write(ex.Message);
                }
                finally
                {
                    //釋放資源
                    sheet = null;
                    wb = null;
                    stream.Dispose();
                    stream.Close();
                }

                //dataTable跑回圈，insert資料至DB
                var models = new List<ExcelReport>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    try
                    {
                        var m = new ExcelReport();
                        m.Name = dataRow["姓名"].ToString();
                        m.JobYears = Convert.ToInt32(dataRow["年資"].ToString());
                        m.IdentityNum = dataRow["身分證字號"].ToString();
                        m.Birthday = DateOnly.FromDateTime(DateTime.Parse(dataRow["生日"].ToString()));
                        m.Address = dataRow["住址"].ToString();
                        m.EmailAddress = dataRow["信箱位置"].ToString();
                        m.CellPhone = dataRow["手機"].ToString();
                        m.Remark = dataRow["Remark"].ToString();
                        models.Add(m);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "從Excel匯入失敗";
                        Console.Write(ex.Message);
                    }
                }

                try 
                {
                    foreach (var m in models)
                    {
                        _memoManagerContext.ExcelReport.Add(m);
                    }
                    _memoManagerContext.SaveChanges();
                    ViewBag.Message = "匯入成功";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "匯入資料庫失敗";
                    Console.Write(ex.Message);
                }
            }
            return View();
        }
    }
}
