using Liliya.Dto.Sys.Login;
using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using Liliya.SqlSugar.Repository;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Liliya.Services.Sys.Login
{
    public class LoginService : ILoginService
    {
        private ISqlSugarRepository<UserEntity> _userRepository;
        private readonly IJwtApp _jwtApp;

        public LoginService(ISqlSugarRepository<UserEntity> userRepository, IJwtApp jwtApp)
        {
            _userRepository = userRepository;
            _jwtApp = jwtApp;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AjaxResult> LoginAsync(LoginInputDto input)
        {
            input.NotNull(nameof(input));
            var user = await _userRepository.GetSingleByLambdaAsync(x => x.Account == input.Account);
            if (user == null)
                return new AjaxResult("账号不存在", AjaxResultType.Fail);
            if (user.Password != input.Password.ToMD5())
                return new AjaxResult("密码错误", AjaxResultType.Fail);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),   //唯一标识,用户Id
                new Claim("Account",user.Account.ToString()),
                new Claim("UserName",user.Name),
            };
            var token = _jwtApp.GenerateToken(claims);

            return new AjaxResult("登录成功", token, AjaxResultType.Success);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public async Task<AjaxResult> SignOutAsync()
        {
            var issuccess = await _jwtApp.DeactivateTokenAsync();

            return new AjaxResult(issuccess?"登出成功":"登出失败", issuccess?AjaxResultType.Success:AjaxResultType.Fail);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UpdatePasswordAsync(PasswordDto input)
        {
            input.NotNull(nameof(input));
            var user = await _userRepository.GetSingleByLambdaAsync(x => x.Account == input.Account);
            if (user == null)
                return new AjaxResult("账号不存在", AjaxResultType.Fail);
            if (user.Password != input.Password.ToMD5())
                return new AjaxResult("密码错误", AjaxResultType.Fail);
            user.Password = input.NewPassword.ToMD5();
            return await _userRepository.UpdateAsync(user);
        }
    }
}
