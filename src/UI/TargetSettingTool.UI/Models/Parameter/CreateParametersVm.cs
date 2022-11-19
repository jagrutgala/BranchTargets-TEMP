using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TargetSettingTool.UI.Models.Parameter
{
    public class CreateParametersVm
    {
        public List<CreateParameterVm> Parameters { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
