using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using Liliya.Test.Magicodes;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Liliya.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task  SqlSugarCodeFirstTest()
        {
            SqlSugarClient jobDB = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=akliajob;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;AllowLoadLocalInfile=true",//���ӷ��ִ�
                DbType = DbType.MySql,// DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//�����Զ�ȡ����������Ϣ
            });


            SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = "Data Source=101.34.154.180,51433;Initial Catalog=LabelTest;uid=sa;pwd=Pass@Word;",//���ӷ��ִ�
                //DbType =  DbType.SqlServer,// DbType.SqlServer,
                //IsAutoCloseConnection = true,
                //InitKeyType = InitKeyType.Attribute,//�����Զ�ȡ����������Ϣ

                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=liliya;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;AllowLoadLocalInfile=true",//���ӷ��ִ�
                DbType = DbType.MySql,// DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//�����Զ�ȡ����������Ϣ
            });

            DB.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                //��Ӵ���ʱ��
                if (entityInfo.PropertyName == "CreatedAt" && entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue(DateTime.Now);
                }
                //��Ӵ�����
                if (entityInfo.PropertyName == "CreatedId" && entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue("db03fec2-f391-481b-9e24-32370a6a9942".ToGuid());
                }
            };

            //CodeFirst����
            //db.CodeFirst.InitTables(typeof(UserEntity));
            //DB.CodeFirst.InitTables(typeof(DataDictionaryEntity));


            var schedules =  await jobDB.Queryable<ScheduleEntity>().ToListAsync();


            var startime = DateTime.Now;
            await DB.Insertable<ScheduleEntity>(schedules).ExecuteCommandAsync();       //18813����
            //await DB.Fastest<ScheduleEntity>().PageSize(20000).BulkCopyAsync(schedules);                  //5933����

            var totalTime = (DateTime.Now - startime).TotalMilliseconds;

            Console.WriteLine($"��ʱ{totalTime}��");

        }



        [TestMethod]
        public async Task MagicodesTest()
        {
            SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=liliya;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;",//���ӷ��ִ�
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//�����Զ�ȡ����������Ϣ
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

            var filebyte  = await exporter.ExportAsByteArray(users);

            FileHelper.CeeateFile(filePath, filebyte);
        }
    }
}
