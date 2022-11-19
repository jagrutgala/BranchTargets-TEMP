using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Application.Features.ParameterTypes.Queries.GetAllParamterTypes;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetAllParameters
{
    public class ParameterDto
    {
        public Guid Id { get; set; }
        public Guid ParameterTypeId{ get; set; }
        public double TargetAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string FinancialYear { get; set; }

        public ParameterTypesDto ParameterType { get; set; }
    }
}
