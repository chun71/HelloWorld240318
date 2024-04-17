
using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using Aspose.Words.XAttr;
using Dapper;
using HelloWorld240318.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Drawing;

namespace HelloWorld240318.Service
{
    public class UsersDetailOneService : IUsersDetailOneService
    {
        public readonly MemoManagerContext _memoManagerContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private static readonly ConfigurationBuilder configBuilder = new ConfigurationBuilder();

        private readonly string connectionString = configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build()["ConnectionStrings:SqlServer"];

        #region
        public UsersDetailOneService()
        {

        }
        #endregion

        public UsersDetailOneService(MemoManagerContext memoManagerContext, IWebHostEnvironment iWebHostEnvironment)
        {
            _memoManagerContext = memoManagerContext;
            _webHostEnvironment = iWebHostEnvironment;
        }

        #region GetAllData
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
                    Remark9 = m.Remark9
                };
                datas.Add(data);
            }

            return datas;
        }
        #endregion 

        #region ChangeToViewModel
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
                Remark9 = m.Remark9
            };
        }

        #endregion

        #region EditPage
        public ViewModels.UsersDetailOne EditPage(long? id)
        {
            var data = new ViewModels.UsersDetailOne();

            if (id > 0)
            {
                var q = _memoManagerContext.UsersDetailOne.Where(x => x.ID == id).FirstOrDefault();

                if (q != null)
                {
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
                }
            }

            return data;
        }
        #endregion

        #region UpDateData
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
        #endregion

        #region DeleteData
        public void DeleteData(long? id)
        {
            _memoManagerContext.UsersDetailOne.Where(m => m.ID == id).ExecuteDelete();
        }
        #endregion

        #region ExcelCreate
        public MemoryStream ExcelCreate(ViewModels.UsersDetailOne q, bool isTest)
        {
            //建立Excel
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

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

                    sheet.CreateRow(1).CreateCell(0).SetCellValue(q.Name);
                    sheet.GetRow(1).CreateCell(1).SetCellValue(q.Sex == 0 ? Enum.SexList.女.ToString() : Enum.SexList.男.ToString());
                    sheet.GetRow(1).CreateCell(2).SetCellValue(q.IsMarry ? "已婚" : "未婚"); sheet.GetRow(1).CreateCell(3).SetCellValue(q.JobYears);
                    sheet.GetRow(1).CreateCell(4).SetCellValue(Common.Common.commutingList.ContainsKey(q.Commuting) ? Common.Common.commutingList[q.Commuting] : "");
                    sheet.GetRow(1).CreateCell(5).SetCellValue(q.IdentityNum);
                    sheet.GetRow(1).CreateCell(6).SetCellValue(q.Birthday.HasValue ? q.Birthday.Value.ToString("yyyy-MM-dd") : "");
                    sheet.GetRow(1).CreateCell(7).SetCellValue(q.Address);
                    sheet.CreateRow(3).CreateCell(0).SetCellValue(q.EmailAddress); sheet.GetRow(3).CreateCell(1).SetCellValue(q.TelPhone);
                    sheet.GetRow(3).CreateCell(2).SetCellValue(q.CellPhone); sheet.GetRow(3).CreateCell(3).SetCellValue(q.Account);
                    sheet.GetRow(3).CreateCell(4).SetCellValue(q.Password); sheet.GetRow(3).CreateCell(5).SetCellValue(q.Remark1);
                    sheet.GetRow(3).CreateCell(6).SetCellValue(q.Remark2); sheet.GetRow(3).CreateCell(7).SetCellValue(q.Remark3);
                    sheet.CreateRow(5).CreateCell(0).SetCellValue(q.Remark4); sheet.GetRow(5).CreateCell(1).SetCellValue(q.Remark5);
                    sheet.GetRow(5).CreateCell(2).SetCellValue(q.Remark6); sheet.GetRow(5).CreateCell(3).SetCellValue(q.Remark7);
                    sheet.GetRow(5).CreateCell(4).SetCellValue(q.Remark8); sheet.GetRow(5).CreateCell(5).SetCellValue(q.Remark9);

                    int rowShift = 0, cellShift = 0;
                    sheet.CreateRow(rowShift);
                    for (int i = 0; i < titleList.Count; i++)
                    {
                        if (i == 8 || i == 16)
                        {
                            rowShift = rowShift + 2;
                            cellShift = cellShift - 8;
                            sheet.CreateRow(rowShift);
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

                //設定欄位樣式
                ICellStyle hyperlinkStyle = hssfworkbook.CreateCellStyle();
                IFont hyperlinkFont = hssfworkbook.CreateFont();
                hyperlinkStyle.Alignment = HorizontalAlignment.Center; //水平置中
                hyperlinkStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                hyperlinkStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.None;//設定框限線
                hyperlinkStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.None;
                hyperlinkStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
                hyperlinkStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
                hyperlinkFont.FontName = "Microsoft JhengHei";
                hyperlinkFont.FontHeightInPoints = 11;
                hyperlinkFont.Boldweight = (short)FontBoldWeight.Normal;//正常
                hyperlinkFont.Underline = FontUnderlineType.Single;
                hyperlinkFont.Color = HSSFColor.Blue.Index;
                hyperlinkStyle.SetFont(hyperlinkFont);

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

                sheet.CreateFreezePane(0, 1, 0, 1);

                sheet.CreateRow(8).CreateCell(0).SetCellValue("A");
                sheet.GetRow(8).CreateCell(1).SetCellValue("B");
                sheet.GetRow(8).CreateCell(2).SetCellValue("A+B");
                sheet.CreateRow(9).CreateCell(0).SetCellValue(17);
                sheet.GetRow(9).CreateCell(1).SetCellValue(7);
                sheet.GetRow(9).CreateCell(2).CellFormula = "SUM(A10:B10)";

                for (int row = 8; row < 10; row++)
                {
                    for (int cell = 0; cell < 3; cell++)
                    {
                        sheet.GetRow(row).GetCell(cell).CellStyle = headerStyle;
                    }
                }

                sheet.CreateRow(6).CreateCell(1).SetCellValue("google");
                sheet.GetRow(6).GetCell(1).Hyperlink = new HSSFHyperlink(HyperlinkType.Url);
                sheet.GetRow(6).GetCell(1).Hyperlink.Address = "https://www.google.com";
                sheet.GetRow(6).GetCell(1).CellStyle = hyperlinkStyle;

                string imagePath = configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build()["UploadImgPath"];
                string imageTotalPath = $"{imagePath}_ImgTest638487642071642745.jpg";
                int pictureIndex = hssfworkbook.AddPicture(File.ReadAllBytes(imageTotalPath), PictureType.JPEG);
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 8, 2, 14, 14)
                {
                    AnchorType = AnchorType.MoveDontResize // 圖片大小不隨儲存格變化
                };
                patriarch.CreatePicture(anchor, pictureIndex);

                var msg = new HSSFRichTextString("文字顏色測試");
                var redFont = hssfworkbook.CreateFont();
                redFont.Color = IndexedColors.Red.Index;
                msg.ApplyFont(0, 1, redFont);
                var greenFont = hssfworkbook.CreateFont();
                greenFont.Color = IndexedColors.Green.Index;
                msg.ApplyFont(1, 3, greenFont);
                var blueFont = hssfworkbook.CreateFont();
                blueFont.Color = IndexedColors.Blue.Index;
                msg.ApplyFont(3, msg.Length, blueFont);
                sheet.CreateRow(11).CreateCell(1).SetCellValue(msg);

                string password = null;
                sheet.ProtectSheet(password);

                sheet.SetColumnHidden(0, false);
                hssfworkbook.SetSheetHidden(0, false);

                sheet.CreateRow(13).CreateCell(1).SetCellValue("分頁");
                sheet.GetRow(13).CreateCell(2).SetCellValue("行數");
                sheet.CreateRow(14).CreateCell(1).SetCellValue(hssfworkbook.GetSheetIndex("sheet") + 1);
                sheet.GetRow(14).CreateCell(2).SetCellValue(sheet.LastRowNum + 1);
            }

            hssfworkbook.Write(excelDatas);
            return excelDatas;
        }
        #endregion

        #region
        public MemoryStream TradeVanExcelReport(int itemNum = 0)
        {
            //建立Excel
            XSSFWorkbook xssfworkbook = new XSSFWorkbook(); //建立活頁簿
            var excelDatas = new MemoryStream();

            //設定標題樣式
            IFont font1 = xssfworkbook.CreateFont();
            font1.FontName = "PMingLiU";
            font1.FontHeightInPoints = 11;
            font1.Boldweight = (short)FontBoldWeight.Normal;
            font1.Color = HSSFColor.Black.Index;
            ICellStyle style1 = xssfworkbook.CreateCellStyle();
            style1.SetFont(font1);
            style1.Alignment = HorizontalAlignment.Left; //水平置中
            style1.VerticalAlignment = VerticalAlignment.Center; //垂直置中
            style1.WrapText = true;

            if (itemNum == 0 || itemNum == 1)
            {
                ISheet sheet1 = xssfworkbook.CreateSheet("銀行、信合社"); //建立sheet
                sheet1.CreateRow(1); sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 1, 21));
                sheet1.GetRow(1).CreateCell(1).SetCellValue("智能風險監控系統");

                sheet1.CreateRow(2); sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 1, 21));
                sheet1.GetRow(2).CreateCell(1).SetCellValue("經營風險偏高要保機構財務彙總表(銀行、信合社)");

                sheet1.CreateRow(3); sheet1.AddMergedRegion(new CellRangeAddress(3, 3, 1, 21));
                var msg = new XSSFRichTextString();
                var redFont = new XSSFFont();
                redFont.Color = HSSFColor.Red.Index;
                msg.Append("單位：");
                msg.Append("銀行(百萬元)、信合社(千元)", redFont);
                msg.Append("；%");
                sheet1.GetRow(3).CreateCell(1).SetCellValue(msg);

                sheet1.CreateRow(4).CreateCell(1); sheet1.CreateRow(5).CreateCell(1); sheet1.CreateRow(6).CreateCell(1); sheet1.AddMergedRegion(new CellRangeAddress(4, 6, 1, 1));
                sheet1.GetRow(4).GetCell(1).SetCellValue("要保機構名稱");
                sheet1.GetRow(4).CreateCell(2); sheet1.GetRow(5).CreateCell(2); sheet1.GetRow(6).CreateCell(2); sheet1.AddMergedRegion(new CellRangeAddress(4, 6, 2, 2));
                sheet1.GetRow(4).GetCell(2).SetCellValue("警示項目（註1）");
                sheet1.GetRow(4).CreateCell(3); sheet1.GetRow(4).CreateCell(4); sheet1.AddMergedRegion(new CellRangeAddress(4, 4, 3, 4));
                sheet1.GetRow(4).GetCell(3).SetCellValue("112/09/30");

                List<string> tempTitle = new List<string>()
                {
                    "資本適足率", "較上期底增減百分點", "存款總額", "放款總額", "放款/存款", "淨值"
                };

                for (int i = 3; i < 9; i++)
                {
                    sheet1.GetRow(5).CreateCell(i); sheet1.GetRow(6).CreateCell(i); sheet1.AddMergedRegion(new CellRangeAddress(5, 6, i, i));
                    sheet1.GetRow(5).GetCell(i).SetCellValue(tempTitle[i - 3]);
                }

                for (int i = 5; i < 21; i++)
                {
                    sheet1.GetRow(4).CreateCell(i);
                }

                sheet1.AddMergedRegion(new CellRangeAddress(4, 4, 5, 20)); sheet1.GetRow(4).GetCell(5).SetCellValue("要保機構112/12/31申報資料");
                sheet1.GetRow(5).CreateCell(9); sheet1.GetRow(5).CreateCell(10); sheet1.AddMergedRegion(new CellRangeAddress(5, 5, 9, 10));
                sheet1.GetRow(5).GetCell(9).SetCellValue("損益");
                sheet1.GetRow(5).CreateCell(11); sheet1.GetRow(5).CreateCell(15); sheet1.AddMergedRegion(new CellRangeAddress(5, 5, 11, 15));
                sheet1.GetRow(5).GetCell(11).SetCellValue("逾期放款");

                tempTitle.Clear();
                tempTitle.Add("上年度"); tempTitle.Add("稅前"); tempTitle.Add("金額"); tempTitle.Add("比率");
                tempTitle.Add("較上月底增減百分點"); tempTitle.Add("備抵放款損失"); tempTitle.Add("覆蓋率");

                for (int i = 9; i < 16; i++)
                {
                    sheet1.GetRow(6).CreateCell(i); sheet1.GetRow(6).GetCell(i).SetCellValue(tempTitle[i - 9]);
                }

                tempTitle.Clear();
                tempTitle.Add("應予評估資產覆蓋率"); tempTitle.Add("不良放款比率"); tempTitle.Add("不良放款覆蓋率"); tempTitle.Add("大陸曝險占淨值"); tempTitle.Add("流動性覆蓋率");

                for (int i = 16; i < 21; i++)
                {
                    sheet1.GetRow(5).CreateCell(i); sheet1.GetRow(6).CreateCell(i); sheet1.AddMergedRegion(new CellRangeAddress(5, 6, i, i));
                    sheet1.GetRow(5).GetCell(i).SetCellValue(tempTitle[i - 16]);
                }

                sheet1.GetRow(4).CreateCell(21); sheet1.GetRow(5).CreateCell(21); sheet1.GetRow(6).CreateCell(21); sheet1.AddMergedRegion(new CellRangeAddress(4, 6, 21, 21));
                sheet1.GetRow(4).GetCell(21).SetCellValue("警示項目改善情形及其他重大事項");

                int lastRow = sheet1.LastRowNum;

                for (int row = lastRow + 1; row < lastRow + tempTitle.Count() + 1; row++)
                {
                    sheet1.CreateRow(row);
                    for (int cell = 1; cell < 22; cell++)
                    {
                        if (cell == 1)
                        {
                            sheet1.GetRow(row).CreateCell(cell).SetCellValue(tempTitle[row - (lastRow + 1)]);
                        }
                        else
                        {
                            sheet1.GetRow(row).CreateCell(cell).SetCellValue(cell);
                        }
                    }
                }

                lastRow = sheet1.LastRowNum;
                sheet1.CreateRow(lastRow + 1);

                for (int i = 1; i < 22; i++)
                {
                    sheet1.GetRow(lastRow + 1).CreateCell(i);
                }

                sheet1.AddMergedRegion(new CellRangeAddress(lastRow + 1, lastRow + 1, 1, 22));
                sheet1.GetRow(lastRow + 1).GetCell(1).SetCellValue("徐淑媛(測試) 及  製表日期：113/04/02 16:33:14");

                for (int row = lastRow + 2; row < lastRow + 6; row++)
                {
                    sheet1.CreateRow(row);
                    sheet1.GetRow(row).CreateCell(1).SetCellValue($"註{row - (lastRow + 1)}");
                    for (int cell = 2; cell < 22; cell++)
                    {
                        sheet1.GetRow(row).CreateCell(cell);
                    }
                }

                string tempMsg = "";
                string enterString = "";
                for (int i = 0; i < tempTitle.Count; i++)
                {
                    if (i > 0)
                    {
                        enterString = "\n";
                    }

                    tempMsg = tempMsg + enterString + tempTitle[i];
                }

                sheet1.AddMergedRegion(new CellRangeAddress(lastRow + 2, lastRow + 2, 2, 22));
                sheet1.GetRow(lastRow + 2).GetCell(2).SetCellValue(tempMsg);
                sheet1.GetRow(lastRow + 2).GetCell(2).CellStyle = style1;
                sheet1.AddMergedRegion(new CellRangeAddress(lastRow + 3, lastRow + 3, 2, 22));
                sheet1.GetRow(lastRow + 3).GetCell(2).SetCellValue("本表所稱「應予評估資產覆蓋率」= 評價準備/應予評估資產(II至V類)合計。"); ;
                sheet1.AddMergedRegion(new CellRangeAddress(lastRow + 4, lastRow + 4, 2, 22));
                sheet1.GetRow(lastRow + 4).GetCell(2).SetCellValue("本表所稱「不良放款覆蓋率」= 放款備抵呆帳/(逾期放款+其他有欠正常放款)。");
                sheet1.AddMergedRegion(new CellRangeAddress(lastRow + 5, lastRow + 5, 2, 22));
                sheet1.GetRow(lastRow + 5).GetCell(2).SetCellValue("本表所列機構係有前列第(1)至項警訊(13)項警訊(包含但不限於)者。");
            }

            if (itemNum == 0 || itemNum == 2)
            {
                ISheet sheet2 = xssfworkbook.CreateSheet("農漁會信用部"); //建立sheet











            }













            xssfworkbook.Write(excelDatas);
            return excelDatas;
        }
        #endregion

        #region PdfCreate
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
                    builder.InsertCell(); builder.Write(q.Birthday.HasValue ? q.Birthday.Value.ToString("yyyy-MM-dd") : "");
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
        #endregion

        #region GetAllUsersDetailOne
        public IEnumerable<ViewModels.UsersDetailOne> GetAllUsersDetailOne(QueryModel.UsersDetailOne model)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            string sqlCmd = "SELECT * FROM UsersDetailOne WHERE 1 = 1";
            var dynParams = new DynamicParameters();

            if (!model.Name.IsNullOrEmpty())
            {
                sqlCmd = $"{sqlCmd} AND Name = @Name";
                dynParams.Add("@Name", model.Name, DbType.String, ParameterDirection.Input, model.Name.Length);
            }

            if (!model.IdentityNum.IsNullOrEmpty())
            {
                sqlCmd = $"{sqlCmd} AND IdentityNum = @IdentityNum";
                dynParams.Add("@IdentityNum", model.IdentityNum, DbType.String, ParameterDirection.Input, model.IdentityNum.Length);
            }

            return dbConnection.Query<ViewModels.UsersDetailOne>(sqlCmd, dynParams);
        }
        #endregion

        #region GetUsersDetailOneById
        public ViewModels.UsersDetailOne GetUsersDetailOneById(long? id)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection.QueryFirstOrDefault<ViewModels.UsersDetailOne>("SELECT * FROM UsersDetailOne WHERE Id = @Id", new { Id = id });
        }
        #endregion

        #region InsertUsersDetailOne
        public void InsertUsersDetailOne(ViewModels.UsersDetailOne model)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var dynParams = new DynamicParameters();
            dynParams.Add("@Name", model.Name, DbType.String, ParameterDirection.Input, model.Name.Length);
            dynParams.Add("@Sex", model.Sex, DbType.Int32, ParameterDirection.Input);
            dynParams.Add("@IsMarry", model.IsMarry, DbType.Boolean, ParameterDirection.Input);

            dbConnection.Execute(@"INSERT INTO UsersDetailOne 
                                                         (Name, Sex, IsMarry) 
                                                         VALUES (@Name, @Sex, @IsMarry)", dynParams);
        }
        #endregion

        #region UpdateUsersDetailOne
        public void UpdateUsersDetailOne(ViewModels.UsersDetailOne model)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            dbConnection.Execute(@"UPDATE UsersDetailOne SET 
                                                         Name = @Name, Sex = @Sex, IsMarry = @IsMarry 
                                                         WHERE Id = @Id", model);
        }
        #endregion

        #region DeleteUsersDetailOne
        public void DeleteUsersDetailOne(long? id)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM UsersDetailOne WHERE Id = @Id", new { Id = id });
        }
        #endregion
    }
}
