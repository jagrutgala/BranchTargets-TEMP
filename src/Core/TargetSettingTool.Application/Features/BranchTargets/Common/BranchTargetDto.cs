using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Application.Features.UserParameters.Common;

namespace TargetSettingTool.Application.Features.BranchTargets.Common
{
    public class BranchTargetDto
    {
        public Guid UserParameterId { get; set; }

        public int AchievedTargetAmount { get; set; }

        public UserParameterDto UserParameter { get; set; }

    }
}
