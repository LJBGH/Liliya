﻿using Liliya.Dto.Sys.User;
using Liliya.Models.Entitys;
using Liliya.Shared;
using Liliya.SqlSugar.Repository;
using System;
using System.Collections.Generic;
using System.Text;
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
            return await _userRepository.DeleteByLambdaAsync(x=>x.Id == id);
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
                return new AjaxResult("该用户不存在", AjaxResultType.Error);
            var data = user.MapTo<UserOutDto>();
            return new AjaxResult(ResultMessage.LoadSucces, data, AjaxResultType.Success);
        }



        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>

        public async Task<AjaxResult> GetAllAsync()
        {
            var list = await _userRepository.GetAsync();
            var result = list.MapToList<UserOutDto>();
            return new AjaxResult(ResultMessage.LoadSucces, result, AjaxResultType.Success);
        }
    }
}
