using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.SideBar.Queries.GetSideBar
{
    public class GetSideBarQuery:IRequest<Response<List<GetSideBarVm>>>
    {
    }
}
