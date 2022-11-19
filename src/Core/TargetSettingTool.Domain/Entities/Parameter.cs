using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class Parameter : CommonAuditProperties
    {
        public Guid ParameterTypeId { get; set; }

        public double TargetAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string FinancialYear { get; set; }

        public bool IsLaunched { get; set; }

        public Guid? LaunchedById { get; set; }

        public DateTime? LaunchedDate { get; set; }

        [ForeignKey("ParameterTypeId")]
        public virtual ParameterType ParameterType { get; set; }

        [ForeignKey("LaunchedById")]
        public virtual User? LaunchedBy { get; set; }

    }
}
