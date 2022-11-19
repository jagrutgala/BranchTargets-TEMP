using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Commands.CreateParameter
{
    public class CreateParameterCommand : IRequest<Response<Guid>>
    {
        public Guid ParameterTypeId { get; set; }
        public double TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LoggedInUserId { get; set; }
        public string FinancialYear { get; set; }
    }
}
