using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Liliya.Common.Excel
{
    /// <summary>
    /// Epplus导出Excel
    /// </summary>
    public class ExcelHelper<T> where T : class, new()
    {
        #region 导出Excel
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">泛型列表对象</param>
        /// <param name="FileName">保存的路径</param>
        /// <param name="OpenPassword">创建Excel打开密码</param>
        public static byte[] ToExcel(IList<T> data, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            //获取泛型实体类的所有列头
            List<ExcelParameterVo> excelParameters = GetExcelParameters();
            return ToExcelbyByte(data, excelParameters);

        }
        public static Stream ToExcel1(IList<T> data, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            //获取泛型实体类的所有列头
            List<ExcelParameterVo> excelParameters = GetExcelParameters();
            return ToExcelbyByte1(data, excelParameters);

        }

        /// <summary>
        /// 创建Excel;并返回文件流
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">导出的数据</param>
        /// <param name="excelParameters">通过反射得到的列头对象</param>
        /// <param name="FileName">文件存放路径</param>
        /// <param name="dataIndex">预留参数,暂未用到</param>
        /// <param name="excelVersion">导出的格式后缀</param>
        public static Stream ToExcelbyByte1(IList<T> data, IList<ExcelParameterVo> excelParameters, int dataIndex = 1, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            MemoryStream ms = new MemoryStream();
            ms.Seek(0, SeekOrigin.Begin);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage();
            try
            {
                if (data != null && data.Count > 0)
                {
                    excelParameters = (from s in excelParameters orderby s.Sort select s).ToList();
                    object obj = null;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                    //传入的列名 col用于得到list的下标，i用于写入Excel的某一列
                    for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                    {
                        ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                        worksheet.Cells[1, i].Value = excelParameterVo.ColumnName;//设置列名
                        worksheet.Column(i).Width = excelParameterVo.ColumnWidth;//设置列宽
                    }
                    //传入的列名 row用于得到data的下标，j用于写入Excel的某一行
                    for (int row = 0, j = 2; row < data.Count; row++, j++)
                    {
                        //传入的列名 col用于得到excelParameters的下标，i用于写入Excel的某一列
                        for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                        {
                            ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                            var item = data[row];
                            obj = excelParameterVo.Property.GetValue(item);//通过反射获取item
                            //Type type = excelParameterVo.Property.GetValue(item).GetType();//通过反射获取字段类型
                            //if (type.IsEnum)
                            //{
                            //    obj = type.GetEnumDescription();
                            //}
                            if (obj == null)//如果obj=null的话该列直接写空
                            {
                                worksheet.Cells[j, i].Value = "";
                            }
                            else
                            {
                                worksheet.Cells[j, i].Value = obj.ToString();
                            }
                        }
                    }
                    package.SaveAs(ms);
                    return ms;
                }
                return package.Stream;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 创建Excel;并返回文件流
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">导出的数据</param>
        /// <param name="excelParameters">通过反射得到的列头对象</param>
        /// <param name="FileName">文件存放路径</param>
        /// <param name="dataIndex">预留参数,暂未用到</param>
        /// <param name="excelVersion">导出的格式后缀</param>
        public static byte[] ToExcelbyByte(IList<T> data, IList<ExcelParameterVo> excelParameters, int dataIndex = 1, ExcelVersion excelVersion = ExcelVersion.xlsx)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                try
                {
                    if (data != null && data.Count > 0)
                    {
                        excelParameters = (from s in excelParameters orderby s.Sort select s).ToList();
                        object obj = null;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                        //传入的列名 col用于得到list的下标，i用于写入Excel的某一列
                        for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                        {
                            ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                            worksheet.Cells[1, i].Value = excelParameterVo.ColumnName;//设置列名
                            worksheet.Column(i).Width = excelParameterVo.ColumnWidth;//设置列宽
                        }
                        //传入的列名 row用于得到data的下标，j用于写入Excel的某一行
                        for (int row = 0, j = 2; row < data.Count; row++, j++)
                        {
                            //传入的列名 col用于得到excelParameters的下标，i用于写入Excel的某一列
                            for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                            {
                                ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                                var item = data[row];
                                obj = excelParameterVo.Property.GetValue(item);//通过反射获取item
                                if (obj == null)//如果obj=null的话该列直接写空
                                {
                                    worksheet.Cells[j, i].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[j, i].Value = obj.ToString();
                                }
                            }
                        }
                        package.Save();
                        return package.GetAsByteArray();
                    }
                    return package.GetAsByteArray();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
      
        
        /// <summary>
        /// 创建Excel并保存到服务器
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">泛型列表对象</param>
        /// <param name="FileName">保存的路径</param>
        /// <param name="OpenPassword">创建Excel打开密码</param>
        public static string SaveExcel(IList<T> data, string FileName, ExcelVersion excelVersion = ExcelVersion.xlsx, bool isShow = true)
        {
            //获取泛型实体类的所有列头
            List<ExcelParameterVo> excelParameters = GetExcelParameters();
            return SaveExcel(data, excelParameters, FileName, 1, excelVersion, isShow);

        }
        
        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="data">导出的数据</param>
        /// <param name="excelParameters">通过反射得到的列头对象</param>
        /// <param name="FileName">文件存放路径</param>
        /// <param name="dataIndex">预留参数,暂未用到</param>
        /// <param name="excelVersion">导出的格式后缀</param>
        public static string SaveExcel(IList<T> data, IList<ExcelParameterVo> excelParameters, string FileName, int dataIndex = 1, ExcelVersion excelVersion = ExcelVersion.xlsx, bool isShow = true)
        {
            if (isShow == false)
            {
                excelParameters.Where(x => x.ColumnName == "起止日期").FirstOrDefault().GetType().GetProperty("ShowState").SetValue(excelParameters.Where(x => x.ColumnName == "起止日期").FirstOrDefault(), false);
            }
            FileInfo fileInfo = new FileInfo(FileName);
            if (fileInfo.Exists)
            {
                //删除原有文件
                //fileInfo.Delete();
                fileInfo = new FileInfo(FileName);//创建新文件
            }
            else
            {
                fileInfo = new FileInfo(FileName);//创建新文件
            }
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage())
                {
                    if (data != null && data.Count > 0)
                    {
                        excelParameters = (from s in excelParameters where s.ShowState == true orderby s.Sort select s).ToList();
                        object obj = null;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("shett1");

                        //(设置表头)传入的列名 col用于得到list的下标，i用于写入Excel的某一列，列头默认从1开始
                        for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                        {
                            ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                            worksheet.Cells[1, i].Value = excelParameterVo.ColumnName;//设置列名
                          
                            worksheet.Cells[1, i].Style.Font.Bold = true;//字体为粗体
                            worksheet.Cells[1, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//水平居中

                            //worksheet.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;//设置样式类型
                            //worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(159, 197, 232));//设置单元格背景色
                           
                            worksheet.Column(i).Width = excelParameterVo.ColumnWidth;//设置列宽
                        }
                        //传入的列名 row用于得到data的下标，j用于写入Excel的某一行，从第二行开始写入
                        for (int row = 0, j = 2; row < data.Count; row++, j++)
                        {
                            //传入的列名 col用于得到excelParameters的下标，i用于写入Excel的某一列
                            for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                            {
                                ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                                var item = data[row];
                                obj = excelParameterVo.Property.GetValue(item);
                                if (obj == null)//如果obj=null的话该列直接写空
                                {
                                    worksheet.Cells[j, i].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[j, i].Value = obj.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        excelParameters = (from s in excelParameters where s.ShowState == true orderby s.Sort select s).ToList();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("shett1");
                        //传入的列名 col用于得到list的下标，i用于写入Excel的某一列
                        for (int col = 0, i = 1; col < ((ICollection<ExcelParameterVo>)excelParameters).Count; col++, i++)
                        {
                            ExcelParameterVo excelParameterVo = excelParameters[col];//得到列名对象
                            worksheet.Cells[1, i].Value = excelParameterVo.ColumnName;//设置列名
                            worksheet.Column(i).Width = excelParameterVo.ColumnWidth;//设置列宽
                            //worksheet.Cells[1, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//水平居中
                        }
                    }
                    package.SaveAs(fileInfo);
                }
                return FileName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过反射和特性获取导出列;并生成列头对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<ExcelParameterVo> GetExcelParameters()
        {
            Type typeFromHandle = typeof(T);
            List<ExcelParameterVo> excelParameters = new List<ExcelParameterVo>();
            PropertyInfo[] properties = typeFromHandle.GetProperties();//通过反射获取到对象属性
            foreach (PropertyInfo propertyInfo in properties)
            {
                //通过反射获取自定义特性
                ExcelColumnNameAttribute excelColumnNameAttribute = ((MemberInfo)propertyInfo).GetCustomAttribute<ExcelColumnNameAttribute>();
                if (excelColumnNameAttribute != null)
                {
                    excelParameters.Add(new ExcelParameterVo
                    {
                        ColumnName = excelColumnNameAttribute.ColumnName,
                        ColumnWidth = excelColumnNameAttribute.ColumnWith,
                        Sort = excelColumnNameAttribute.Sort,
                        Property = propertyInfo,
                        ShowState = excelColumnNameAttribute.ShowState
                    });
                }
            }
            return excelParameters;
        }
        #endregion

        #region Excel导入
        /// <summary>
        /// 导入Excel文件
        /// </summary>
        /// <param name="excelfile"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="_hostingEnvironment"></param>
        /// <returns></returns>
        public static List<T> UpLoad(IFormFile file, int sheetIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                List<T> list = worksheet.ConvertSheetToObjects<T>().ToList();
                return list;
            }
        }
        #endregion
    }
}
