using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace Liliya.Test.Magicodes
{
    [TestClass]
    public class MagicodesTest
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Magicodes()
        {
            SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=liliya;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;",//连接符字串
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息
            });

            var users = await DB.Queryable<UserEntity>().Select(x => new UserExportDto
            {
                Id = x.Id,
                Account = x.Account,
                Password = x.Password,
                Name = x.Name,
                JobNumber = x.JobNumber,
                Department = x.Department,
                Position = x.Position
            }).ToListAsync();

            IExporter exporter = new ExcelExporter();


            var filePath = $"C://Users//SocialMED-260//Desktop//MyStore//Liliya//src//Liliya.Core.API//wwwroot//export//MagicodeTest{DateTime.Now.ToString("yyyyyMMddhhmmss")}.xlsx";

            var filebyte = await exporter.ExportAsByteArray(users);

            FileHelper.CeeateFile(filePath, filebyte);
        }
    }
}
