using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// Jwt服务
        /// </summary>
        private readonly IJwtApp _jwtAppService;

        public PolicyHandler(IAuthenticationSchemeProvider schemes, IJwtApp jwtAppService)
        {
            Schemes = schemes;
            _jwtAppService = jwtAppService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
