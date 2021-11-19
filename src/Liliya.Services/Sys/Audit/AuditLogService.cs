using Liliya.Dto.Sys.Audit;
using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using Liliya.SqlSugar.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Liliya.Services.Sys.Audit
{
    /// <summary>
    /// 审计日志
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        private readonly ISqlSugarRepository<AuditLogEntity> _auditRepository;

        public AuditLogService(ISqlSugarRepository<AuditLogEntity> auditRepository)
        {
            this._auditRepository = auditRepository;
        }

        /// <summary>
        /// 添加一条审计日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AjaxResult> AddAsync(AuditLogInputDto input)
        {
            var entity = input.MapTo<AuditLogEntity>();
            return await _auditRepository.InsertNoCheckAsync(entity);
        }

        /// <summary>
        /// 分页获取审计日志
        /// </summary>
        /// <returns></returns>
        public async Task<PageResult<AuditLogOutDto>> GetPageAsync(PageRequest pageRequest)
        {
            var result = await _auditRepository.GetPageListAsync(pageRequest);

            result.Data.MapToList<AuditLogOutDto>();

            return result as PageResult<AuditLogOutDto>;
        }
    }
}
