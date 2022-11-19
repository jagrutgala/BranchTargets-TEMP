﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Rights.Queries.GetAllRights
{
    public class GetAllRightsQuery:IRequest<Response<IEnumerable<GetAllRightsVm>>>
    {
    }
}
