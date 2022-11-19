using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery:IRequest<Response<IEnumerable<UserDto>>>
    {
    }
}
