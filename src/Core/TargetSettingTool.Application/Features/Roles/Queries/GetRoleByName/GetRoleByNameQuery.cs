using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Roles.Queries.GetRoleByName
{
    public class GetRoleByNameQuery : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
