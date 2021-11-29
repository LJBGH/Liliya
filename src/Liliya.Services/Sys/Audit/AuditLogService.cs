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
            entity.Id = Guid.NewGuid();
            return await _auditRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 分页获取审计日志
        /// </summary>
        /// <returns></returns>
        public async Task<IPageResult<AuditLogEntity>> GetPageAsync(PageRequest pageRequest)
        {
            var result = await _auditRepository.GetPageListAsync(pageRequest);

            return result;
        }
    }
}
