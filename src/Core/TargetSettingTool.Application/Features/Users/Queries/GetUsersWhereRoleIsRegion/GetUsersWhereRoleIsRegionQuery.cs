using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Users.Queries.GetUsersWhereRoleIsRegion
{
    public class GetUsersWhereRoleIsRegionQuery : IRequest<Response<IEnumerable<UserDto>>>
    {
        public string Role { get; } = "Region";
    }
}
