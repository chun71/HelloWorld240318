
using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using HelloWorld240318.Models;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Drawing;

namespace HelloWorld240318.Service
{
    public class UsersDetailOneService : IUsersDetailOneService
    {
        public readonly MemoManagerContext _memoManagerContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersDetailOneService()
        {

        }

        public UsersDetailOneService(MemoManagerContext memoManagerContext, IWebHostEnvironment iWebHostEnvironment)
        {
            _memoManagerContext = memoManagerContext;
            _webHostEnvironment = iWebHostEnvironment;
        }

        public List<ViewModels.UsersDetailOne> GetAllData(QueryModel.UsersDetailOne qModel)
        {
            var q = _memoManagerContext.UsersDetailOne.AsNoTracking().AsQueryable();
            var datas = new List<ViewModels.UsersDetailOne>();

            if (!qModel.Name.IsNullOrEmpty())
            {
                q = q.Where(m => m.Name.Contains(qModel.Name)).AsNoTracking().AsQueryable();
            }

            if (!qModel.IdentityNum.IsNullOrEmpty())
            {
                q = q.Where(m => m.IdentityNum.Contains(qModel.IdentityNum)).AsNoTracking().AsQueryable();
            }

            foreach (var m in q)
            {
                var data = new ViewModels.UsersDetailOne()
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
                    Remark1 = m.Remark1,
                    Remark2 = m.Remark2,
                    Remark3 = m.Remark3,
                    Remark4 = m.Remark4,
                    Remark5 = m.Remark5,
                    Remark6 = m.Remark6,
                    Remark7 = m.Remark7,
                    Remark8 = m.Remark8,
                    Remark9 = m.Remark9,
                    SexMsg = m.Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString(),
                    IsMarryMsg = m.IsMarry ? "已婚" : "未婚",
                    CommutingMsg = Common.Common.commutingList.ContainsKey(m.Commuting) ? Common.Common.commutingList[m.Commuting] : ""
                };
                datas.Add(data);
            }

            return datas;
        }

        public ViewModels.UsersDetailOne ChangeToViewModel(UsersDetailOne m)
        {
            return new ViewModels.UsersDetailOne()
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
                Remark1 = m.Remark1,
                Remark2 = m.Remark2,
                Remark3 = m.Remark3,
                Remark4 = m.Remark4,
                Remark5 = m.Remark5,
                Remark6 = m.Remark6,
                Remark7 = m.Remark7,
                Remark8 = m.Remark8,
                Remark9 = m.Remark9,
                SexMsg = m.Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString(),
                IsMarryMsg = m.IsMarry ? "已婚" : "未婚",
                CommutingMsg = Common.Common.commutingList.ContainsKey(m.Commuting) ? Common.Common.commutingList[m.Commuting] : ""
            };
        }

        public ViewModels.UsersDetailOne EditPage(long? id)
        {
            var data = new ViewModels.UsersDetailOne();

            if (id > 0)
            {
                var q = _memoManagerContext.UsersDetailOne.Where(x => x.ID == id).FirstOrDefault();

                if (q != null)
                {
                    var commutingList = Common.Common.commutingList;
                    var commutingMsgList = new List<ViewModels.IdAndName>();

                    for (int i = 0; i < commutingList.Count(); i++)
                    {
                        if (commutingList.ContainsKey(i))
                        {
                            var m = new ViewModels.IdAndName()
                            {
                                ID = i,
                                Name = commutingList[i]
                            };
                            commutingMsgList.Add(m);
                        }
                    }

                    data.ID = q.ID;
                    data.Name = q.Name;
                    data.Sex = q.Sex;
                    data.IsMarry = q.IsMarry;
                    data.JobYears = q.JobYears;
                    data.Commuting = q.Commuting;
                    data.IdentityNum = q.IdentityNum;
                    data.Birthday = q.Birthday;
                    data.Address = q.Address;
                    data.EmailAddress = q.EmailAddress;
                    data.TelPhone = q.TelPhone;
                    data.CellPhone = q.CellPhone;
                    data.Account = q.Account;
                    data.Password = q.Password;
                    data.Remark1 = q.Remark1;
                    data.Remark2 = q.Remark2;
                    data.Remark3 = q.Remark3;
                    data.Remark4 = q.Remark4;
                    data.Remark5 = q.Remark5;
                    data.Remark6 = q.Remark6;
                    data.Remark7 = q.Remark7;
                    data.Remark8 = q.Remark8;
                    data.Remark9 = q.Remark9;
                    data.ImgPath = q.ImgPath;
                    data.BirthdayMsg = q.Birthday.HasValue ? q.Birthday.Value.ToString("yyyy-MM-dd") : "";
                    data.CommutingMsgList = commutingMsgList;
                }
            }

            return data;
        }

        public void UpDateData(ViewModels.UsersDetailOne data)
        {
            var model = new UsersDetailOne();
            var q = _memoManagerContext.UsersDetailOne.Where(m => m.ID == data.ID).AsNoTracking().FirstOrDefault();

            if (q != null)
            {
                model = q;
            }

            if (data.Image != null)
            {
                if (data.Image.Length > 0)
                {
                    var fileExtension = data.Image.FileName.Substring(data.Image.FileName.Length - 4, 4);
                    var fileName = $"_Img{DateTime.Now.ToLong()}{fileExtension}";
                    var filePath = $"{_webHostEnvironment.ContentRootPath}\\wwwroot\\UploadImgs\\";
                    using (var stream = System.IO.File.Create(filePath + fileName))
                    {
                        data.Image.CopyTo(stream);
                    }

                    if (!model.ImgPath.IsNullOrEmpty())
                    {
                        if (!(fileName == model.ImgPath))
                        {
                            if (System.IO.File.Exists(filePath + model.ImgPath))
                            {
                                System.IO.File.Delete(filePath + model.ImgPath);
                            }
                        }
                    }

                    model.ImgPath = fileName;
                }
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
            model.Remark1 = data.Remark1;
            model.Remark2 = data.Remark2;
            model.Remark3 = data.Remark3;
            model.Remark4 = data.Remark4;
            model.Remark5 = data.Remark5;
            model.Remark6 = data.Remark6;
            model.Remark7 = data.Remark7;
            model.Remark8 = data.Remark8;
            model.Remark9 = data.Remark9;

            if (q != null)
            {
                model.ID = q.ID;
                model.CreateDate = q.CreateDate;
                model.UpdateDate = DateTime.Now;
                _memoManagerContext.UsersDetailOne.Update(model);
            }
            else
            {
                model.CreateDate = DateTime.Now;
                _memoManagerContext.UsersDetailOne.Add(model);
            }

            _memoManagerContext.SaveChanges();
        }

        public void DeleteData(long? id)
        {
            _memoManagerContext.UsersDetailOne.Where(m => m.ID == id).ExecuteDelete();
        }

        public MemoryStream ExcelCreate(ViewModels.UsersDetailOne q, bool isTest)
        {
            //建立Excel
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet
            const int roMax = 30;

            for (int i = 0; i < roMax; i++)
            {
                sheet.CreateRow(i);
            }

            var excelDatas = new MemoryStream();
            if (!isTest)
            {
                if (q.ID > 0)
                {
                    //設定標題樣式
                    ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
                    IFont headerfont = hssfworkbook.CreateFont();
                    headerStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                    headerStyle.FillPattern = FillPattern.SolidForeground;
                    headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
                    headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                    headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//設定框限線
                    headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerfont.FontName = "Microsoft JhengHei";
                    headerfont.FontHeightInPoints = 11;
                    headerfont.Boldweight = (short)FontBoldWeight.Bold;//粗體
                    headerStyle.SetFont(headerfont);

                    //設定欄位樣式
                    ICellStyle headerStyle_02 = hssfworkbook.CreateCellStyle();
                    IFont headerfont_02 = hssfworkbook.CreateFont();
                    headerStyle_02.Alignment = HorizontalAlignment.Center; //水平置中
                    headerStyle_02.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                    headerStyle_02.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//設定框限線
                    headerStyle_02.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle_02.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerStyle_02.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    headerfont_02.FontName = "Microsoft JhengHei";
                    headerfont_02.FontHeightInPoints = 11;
                    headerfont_02.Boldweight = (short)FontBoldWeight.Normal;//正常
                    headerStyle_02.SetFont(headerfont_02);

                    //主標題
                    var titleList = new List<string>();
                    titleList.Add("姓名"); titleList.Add("姓別"); titleList.Add("婚姻"); titleList.Add("工作經驗"); titleList.Add("交通方式");
                    titleList.Add("身分證字號"); titleList.Add("生日"); titleList.Add("住址"); titleList.Add("E-Mail"); titleList.Add("市話");
                    titleList.Add("手機"); titleList.Add("帳號"); titleList.Add("密碼"); titleList.Add("Remark1"); titleList.Add("Remark2");
                    titleList.Add("Remark3"); titleList.Add("Remark4"); titleList.Add("Remark5"); titleList.Add("Remark6"); titleList.Add("Remark7");
                    titleList.Add("Remark8"); titleList.Add("Remark9");

                    sheet.GetRow(1).CreateCell(0).SetCellValue(q.Name);
                    sheet.GetRow(1).CreateCell(1).SetCellValue(q.Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString());
                    sheet.GetRow(1).CreateCell(2).SetCellValue(q.IsMarry ? "已婚" : "未婚"); sheet.GetRow(1).CreateCell(3).SetCellValue(q.JobYears);
                    sheet.GetRow(1).CreateCell(4).SetCellValue(Common.Common.commutingList.ContainsKey(q.Commuting) ? Common.Common.commutingList[q.Commuting] : "");
                    sheet.GetRow(1).CreateCell(5).SetCellValue(q.IdentityNum);
                    sheet.GetRow(1).CreateCell(6).SetCellValue(q.Birthday.HasValue ? q.Birthday.ToString() : "");
                    sheet.GetRow(1).CreateCell(7).SetCellValue(q.Address);
                    sheet.GetRow(3).CreateCell(0).SetCellValue(q.EmailAddress); sheet.GetRow(3).CreateCell(1).SetCellValue(q.TelPhone);
                    sheet.GetRow(3).CreateCell(2).SetCellValue(q.CellPhone); sheet.GetRow(3).CreateCell(3).SetCellValue(q.Account);
                    sheet.GetRow(3).CreateCell(4).SetCellValue(q.Password); sheet.GetRow(3).CreateCell(5).SetCellValue(q.Remark1);
                    sheet.GetRow(3).CreateCell(6).SetCellValue(q.Remark2); sheet.GetRow(3).CreateCell(7).SetCellValue(q.Remark3);
                    sheet.GetRow(5).CreateCell(0).SetCellValue(q.Remark4); sheet.GetRow(5).CreateCell(1).SetCellValue(q.Remark5);
                    sheet.GetRow(5).CreateCell(2).SetCellValue(q.Remark6); sheet.GetRow(5).CreateCell(3).SetCellValue(q.Remark7);
                    sheet.GetRow(5).CreateCell(4).SetCellValue(q.Remark8); sheet.GetRow(5).CreateCell(5).SetCellValue(q.Remark9);

                    int rowShift = 0, cellShift = 0;
                    for (int i = 0; i < titleList.Count; i++)
                    {
                        if (i == 8 || i == 16)
                        {
                            rowShift = rowShift + 2;
                            cellShift = cellShift - 8;
                        }
                        sheet.GetRow(0 + rowShift).CreateCell(i + cellShift).SetCellValue(titleList[i]);
                        sheet.GetRow(0 + rowShift).GetCell(i + cellShift).CellStyle = headerStyle;
                        sheet.GetRow(1 + rowShift).GetCell(i + cellShift).CellStyle = headerStyle_02;
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                }
            }
            else
            {
                //設定標題樣式
                ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
                IFont headerfont = hssfworkbook.CreateFont();
                headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
                headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                headerfont.FontName = "Microsoft JhengHei";
                headerfont.FontHeightInPoints = 12;
                headerfont.Boldweight = (short)FontBoldWeight.Bold;//粗體
                headerStyle.SetFont(headerfont);

                //主標題
                sheet.CreateRow(1);//先CreateRow建立,才可GetRow取得該欄位
                sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 6)); // 合併第2行(1,1) A~F列儲存格(0,6)
                sheet.GetRow(1).CreateCell(0).SetCellValue("我是主標題");
                sheet.GetRow(1).GetCell(0).CellStyle = headerStyle; //套用樣式

                //副標題
                sheet.CreateRow(2);
                sheet.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6)); // 合併第3行(2,2) A~F列儲存格(0,6)
                sheet.GetRow(2).CreateCell(0).SetCellValue("我是副標題");
                sheet.GetRow(2).GetCell(0).CellStyle = headerStyle;

                //設定欄位樣式
                ICellStyle headerStyle_02 = hssfworkbook.CreateCellStyle();
                IFont headerfont_02 = hssfworkbook.CreateFont();
                headerStyle_02.Alignment = HorizontalAlignment.Center; //水平置中
                headerStyle_02.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                headerStyle_02.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//設定框限線
                headerStyle_02.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                headerStyle_02.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                headerStyle_02.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                headerfont_02.FontName = "Microsoft JhengHei";
                headerfont_02.FontHeightInPoints = 12;
                headerfont_02.Boldweight = (short)FontBoldWeight.Bold;//粗體
                headerStyle_02.SetFont(headerfont_02);

                sheet.CreateRow(3).CreateCell(0).SetCellValue("序號");
                sheet.GetRow(3).GetCell(0).CellStyle = headerStyle_02;

                sheet.AddMergedRegion(new CellRangeAddress(3, 3, 1, 5));
                sheet.GetRow(3).CreateCell(1).SetCellValue("項目");
                sheet.GetRow(3).GetCell(1).CellStyle = headerStyle_02;
                //合併欄位設定border需連同被合併的座標一起設
                sheet.GetRow(3).CreateCell(2).CellStyle = headerStyle_02;
                sheet.GetRow(3).CreateCell(3).CellStyle = headerStyle_02;
                sheet.GetRow(3).CreateCell(4).CellStyle = headerStyle_02;
                sheet.GetRow(3).CreateCell(5).CellStyle = headerStyle_02;

                sheet.GetRow(3).CreateCell(6).SetCellValue("總計");
                sheet.GetRow(3).GetCell(6).CellStyle = headerStyle_02;

                //設定資料樣式(序號、總計)
                ICellStyle dataStyle = hssfworkbook.CreateCellStyle();
                IFont datafont = hssfworkbook.CreateFont();
                dataStyle.Alignment = HorizontalAlignment.Center; //水平置中
                dataStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                dataStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                dataStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                dataStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                dataStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                datafont.FontName = "Microsoft JhengHei";
                datafont.FontHeightInPoints = 12;
                dataStyle.SetFont(datafont);

                //塞資料
                //下方為寫死資料,可利用迴圈塞資料

                //序號
                sheet.CreateRow(4).CreateCell(0).SetCellValue("1");
                sheet.GetRow(4).GetCell(0).CellStyle = dataStyle;

                //案件項目
                sheet.AddMergedRegion(new CellRangeAddress(4, 4, 1, 5));
                sheet.GetRow(4).CreateCell(1).SetCellValue("測試資料");
                sheet.GetRow(4).GetCell(1).CellStyle = dataStyle;
                //合併欄位設定border需連同被合併的座標一起設
                sheet.GetRow(4).CreateCell(2).CellStyle = dataStyle;
                sheet.GetRow(4).CreateCell(3).CellStyle = dataStyle;
                sheet.GetRow(4).CreateCell(4).CellStyle = dataStyle;
                sheet.GetRow(4).CreateCell(5).CellStyle = dataStyle;

                //總計
                sheet.GetRow(4).CreateCell(6).SetCellValue("100");
                sheet.GetRow(4).GetCell(6).CellStyle = dataStyle;
            }

            hssfworkbook.Write(excelDatas);
            return excelDatas;
        }

        public MemoryStream PdfCreate(ViewModels.UsersDetailOne q, bool isTest)
        {
            Aspose.Words.Document doc = new Aspose.Words.Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            doc.FirstSection.Body.Paragraphs[0].ParagraphFormat.Alignment = Aspose.Words.ParagraphAlignment.Center;
            var pdfDatas = new MemoryStream();

            if (!isTest)
            {
                if (q.ID > 0)
                {
                    // 指定字體格式
                    Aspose.Words.Font font = builder.Font;
                    font.Size = 16;
                    font.Bold = true;
                    font.Color = System.Drawing.Color.Black;
                    font.Name = "Microsoft JhengHei";
                    font.Underline = Underline.None;
                    // 插入文字
                    builder.Writeln($"{q.Name}個資");
                    // 插入表格
                    Aspose.Words.Tables.Table table = builder.StartTable();
                    // 插入單元格
                    builder.InsertCell();
                    // 使用固定的列寬。
                    table.AutoFit(AutoFitBehavior.AutoFitToWindow);
                    builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                    font.Size = 11;

                    builder.Write("姓名");
                    builder.InsertCell(); builder.Write("姓別");
                    builder.InsertCell(); builder.Write("婚姻");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.Name);
                    builder.InsertCell(); builder.Write(q.Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString());
                    builder.InsertCell(); builder.Write(q.IsMarry ? "已婚" : "未婚");
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("工作經驗");
                    builder.InsertCell(); builder.Write("交通方式");
                    builder.InsertCell(); builder.Write("身分證字號");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.JobYears.ToString());
                    builder.InsertCell(); builder.Write(Common.Common.commutingList.ContainsKey(q.Commuting) ? Common.Common.commutingList[q.Commuting] : "");
                    builder.InsertCell(); builder.Write(q.IdentityNum);
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("生日");
                    builder.InsertCell(); builder.Write("住址");
                    builder.InsertCell(); builder.Write("E-Mail");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.Birthday.HasValue ? q.Birthday.ToString() : "");
                    builder.InsertCell(); builder.Write(q.Address);
                    builder.InsertCell(); builder.Write(q.EmailAddress);
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("市話");
                    builder.InsertCell(); builder.Write("手機");
                    builder.InsertCell(); builder.Write("帳號");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.TelPhone);
                    builder.InsertCell(); builder.Write(q.CellPhone);
                    builder.InsertCell(); builder.Write(q.Account);
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("密碼");
                    builder.InsertCell(); builder.Write("Remark1");
                    builder.InsertCell(); builder.Write("Remark2");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.Password);
                    builder.InsertCell(); builder.Write(q.Remark1 != null ? q.Remark1 : "");
                    builder.InsertCell(); builder.Write(q.Remark2 != null ? q.Remark2 : "");
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("Remark3");
                    builder.InsertCell(); builder.Write("Remark4");
                    builder.InsertCell(); builder.Write("Remark5");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.Remark3 != null ? q.Remark3 : "");
                    builder.InsertCell(); builder.Write(q.Remark4 != null ? q.Remark4 : "");
                    builder.InsertCell(); builder.Write(q.Remark5 != null ? q.Remark5 : "");
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("Remark6");
                    builder.InsertCell(); builder.Write("Remark7");
                    builder.InsertCell(); builder.Write("Remark8");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.Remark6 != null ? q.Remark6 : "");
                    builder.InsertCell(); builder.Write(q.Remark7 != null ? q.Remark7 : "");
                    builder.InsertCell(); builder.Write(q.Remark8 != null ? q.Remark8 : "");
                    builder.EndRow();
                    font.Bold = true;
                    builder.InsertCell(); builder.Write("Remark9");
                    builder.EndRow();
                    font.Bold = false;
                    builder.InsertCell(); builder.Write(q.Remark9 != null ? q.Remark9 : "");
                    builder.EndRow();

                    int tempRow = 0; int tempCol = 0;
                    for (int i = 0; i < 22; i++)
                    {
                        if (i % 3 == 0 & i != 0)
                        {
                            tempRow = tempRow + 2;
                            tempCol = 0;
                        }

                        table.Rows[tempRow].Cells[tempCol].CellFormat.Shading.BackgroundPatternColor = Color.LightGray;
                        tempCol++;
                    }
                }
            }
            else
            {
                // 指定字體格式
                Aspose.Words.Font font = builder.Font;
                font.Size = 16;
                font.Bold = true;
                font.Color = System.Drawing.Color.Black;
                font.Name = "Microsoft JhengHei";
                font.Underline = Underline.Single;

                // 插入文字
                builder.Writeln("This is the first page.");
                builder.Writeln();

                // 更改下一個元素的格式。
                font.Underline = Underline.None;
                font.Size = 11;
                font.Color = System.Drawing.Color.Blue;

                builder.Writeln("This following is a table");
                // 插入表格
                Aspose.Words.Tables.Table table = builder.StartTable();
                // 插入單元格
                builder.InsertCell();
                // 使用固定的列寬。
                table.AutoFit(AutoFitBehavior.AutoFitToContents);
                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                builder.Write("This is row 1 cell 1");
                // 插入單元格
                builder.InsertCell();
                builder.Write("This is row 1 cell 2");
                builder.EndRow();
                builder.InsertCell();
                builder.Write("This is row 2 cell 1");
                builder.InsertCell();
                builder.Write("This is row 2 cell 2");
                builder.EndRow();
                builder.EndTable();
                builder.Writeln();

                // 插入圖片
                //builder.InsertImage("image.png");
                // 插入分頁符。分頁後的所有元素將被插入到下一頁。
                //builder.InsertBreak(Aspose.Words.BreakType.PageBreak);
            }

            // 為 PDF17 提供 PDFSaveOption 合規性
            PdfSaveOptions options = new PdfSaveOptions();
            options.Compliance = PdfCompliance.Pdf17;
            // 將 Word 轉換為 PDF
            doc.Save(pdfDatas, options);
            return pdfDatas;
        }
    }
}
