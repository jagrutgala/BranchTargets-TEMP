using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetParameterById
{
    public class GetParameterByIdVm
    {
        public Guid ParameterTypeId { get; set; }
        public double TargetAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string FinancialYear { get; set; }
    }
}
