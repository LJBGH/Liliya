using Liliya.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Text;
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
                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=akliajob;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;AllowLoadLocalInfile=true",//连接符字串
                DbType = DbType.MySql,// DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息
            });


            SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = "Data Source=101.34.154.180,51433;Initial Catalog=LabelTest;uid=sa;pwd=Pass@Word;",//连接符字串
                //DbType =  DbType.SqlServer,// DbType.SqlServer,
                //IsAutoCloseConnection = true,
                //InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息

                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=liliya;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;AllowLoadLocalInfile=true",//连接符字串
                DbType = DbType.MySql,// DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息
            });

            DB.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                //添加创建时间
                if (entityInfo.PropertyName == "CreatedAt" && entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue(DateTime.Now);
                }
                //添加创建人
                if (entityInfo.PropertyName == "CreatedId" && entityInfo.OperationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue("db03fec2-f391-481b-9e24-32370a6a9942".ToGuid());
                }
            };

            //CodeFirst测试
            //db.CodeFirst.InitTables(typeof(UserEntity));
            //DB.CodeFirst.InitTables(typeof(DataDictionaryEntity));


            var schedules =  await jobDB.Queryable<ScheduleEntity>().ToListAsync();


            var startime = DateTime.Now;
            await DB.Insertable<ScheduleEntity>(schedules).ExecuteCommandAsync();       //18813毫秒
            //await DB.Fastest<ScheduleEntity>().PageSize(20000).BulkCopyAsync(schedules);                  //5933毫秒

            var totalTime = (DateTime.Now - startime).TotalMilliseconds;

            Console.WriteLine($"用时{totalTime}秒");

        }

        [TestMethod]
        public void JsonTest()
        {


            var str1 = @"""dsds"" dsads";

            Console.WriteLine(str1);
            var jsonStr = new StringBuilder("{\"data1\":[{\"Key\":\"ProvinceID\",\"Value\":null}," +
                "{\"Key\":\"CityID\",\"Value\":null},{\"Key\":\"DistrictID\",\"Value\":null}," +
                "{\"Key\":\"CityLevel\",\"Value\":\"1\"},{\"Key\":\"HospitalGradeID\",\"Value\":null}," +
                "{\"Key\":\"MedicalTitleID\",\"Value\":null},{\"Key\":\"OnlyPhone\",\"Value\":null}," +
                "{\"Key\":\"OnlyWeChat\",\"Value\":null},{\"Key\":\"PhoneAndWeChat\",\"Value\":null}," +
                "{\"Key\":\"PhoneOrWeChat\",\"Value\":null}],\"data2\":[{\"Prop\":\"ProvinceName\"," +
                "\"Label\":\"省\",\"Display\":true,\"ThemeID\":2,\"ThemeRelLabelID\":\"da770443-92a0-4f96-b2ea-183ae8619668\",\"Sort\":3}," +
                "{\"Prop\":\"FirstDepartmentID\",\"Label\":\"首选科室ID\",\"Display\":true,\"ThemeID\":2," +
                "\"ThemeRelLabelID\":\"e2c00e17-7e08-4233-b1e2-1be959fef14d\",\"Sort\":2},{\"Prop\":\"ProvinceID\"," +
                "\"Label\":\"省ID\",\"Display\":false,\"ThemeID\":2,\"ThemeRelLabelID\":\"e33bff9c-b036-411a-b422-1ee72875de56\"," +
                "\"Sort\":4},{\"Prop\":\"DoctorID\",\"Label\":\"医生ID\",\"Display\":true,\"ThemeID\":2," +
                "\"ThemeRelLabelID\":\"f3f1a15b-c8a9-405a-a065-6e85c4d881ea\",\"Sort\":1}]}");

            var str ="{\"data1\":[{\"Key\":\"ProvinceID\",\"Value\":null},{\"Key\":\"CityID\",\"Value\":null},{\"Key\":\"DistrictID\",\"Value\":null},{\"Key\":\"CityLevel\",\"Value\":\"1\"},{\"Key\":\"HospitalGradeID\",\"Value\":null},{\"Key\":\"MedicalTitleID\",\"Value\":null},{\"Key\":\"OnlyPhone\",\"Value\":null},{\"Key\":\"OnlyWeChat\",\"Value\":null},{\"Key\":\"PhoneAndWeChat\",\"Value\":null},{\"Key\":\"PhoneOrWeChat\",\"Value\":null}],\"data2\":[{\"Prop\":\"ProvinceName\",\"Label\":\"省\",\"Display\":true,\"ThemeID\":2,\"ThemeRelLabelID\":\"da770443-92a0-4f96-b2ea-183ae8619668\",\"Sort\":3},{\"Prop\":\"FirstDepartmentID\",\"Label\":\"首选科室ID\",\"Display\":true,\"ThemeID\":2,\"ThemeRelLabelID\":\"e2c00e17-7e08-4233-b1e2-1be959fef14d\",\"Sort\":2},{\"Prop\":\"ProvinceID\",\"Label\":\"省ID\",\"Display\":false,\"ThemeID\":2,\"ThemeRelLabelID\":\"e33bff9c-b036-411a-b422-1ee72875de56\",\"Sort\":4},{\"Prop\":\"DoctorID\",\"Label\":\"医生ID\",\"Display\":true,\"ThemeID\":2,\"ThemeRelLabelID\":\"f3f1a15b-c8a9-405a-a065-6e85c4d881ea\",\"Sort\":1}]}";

            var jsonObject = (JObject)JsonConvert.DeserializeObject(jsonStr.ToString());

            Console.WriteLine("");
        }
    }
}
