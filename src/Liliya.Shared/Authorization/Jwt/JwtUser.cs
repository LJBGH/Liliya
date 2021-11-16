using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Shared
{
    public class JwtUser
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [DisplayName("人员姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        public string Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Position { get; set; }
    }
}
