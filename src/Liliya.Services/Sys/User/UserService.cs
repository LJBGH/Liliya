using Liliya.Dto.Sys.User;
using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using Liliya.SqlSugar.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Services.Sys.User
{
    public class UserService : IUserService
    {
        private readonly ISqlSugarRepository<UserEntity> _userRepository;

        public UserService(ISqlSugarRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InsertAsync(UserInputDto input)
        {
            input.NotNull(nameof(input));
            var user = await _userRepository.GetByLambdaAsync(x => x.Account == input.Account);
            if (user.Any()) 
            {
                return new AjaxResult("该用户名已存在", AjaxResultType.Error);
            }
            var entity = input.MapTo<UserEntity>();
            return await _userRepository.InsertAsync(entity);
        }


        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UpdateAsync(UserInputDto input)
        {
            input.NotNull(nameof(input));
            var userentity = await _userRepository.GetByIdAsync(input.Id);
            if (userentity == null) 
            {
                return new AjaxResult("该用户不存在", AjaxResultType.Error);
            }
            var user = input.MapTo<UserEntity>();
            return await _userRepository.UpdateAsync(user);
        }


        /// <summary>
        /// 根据Id删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            id.NotNull(nameof(id));
            return await _userRepository.DeleteAsync(id);
        }



        /// <summary>
        /// 根据Id加载用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AjaxResult> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return new AjaxResult("该用户不存在", AjaxResultType.Fail);
            var data = user.MapTo<UserOutDto>();
            return new AjaxResult(ResultMessage.LoadSucces, data, AjaxResultType.Success);
        }



        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>

        public async Task<AjaxResult> GetAllAsync()
        {
            var list = await _userRepository.GetAllAsync();
            var result = list.MapToList<UserOutDto>();
            return new AjaxResult(ResultMessage.LoadSucces, result, AjaxResultType.Success);
        }
    }
}
