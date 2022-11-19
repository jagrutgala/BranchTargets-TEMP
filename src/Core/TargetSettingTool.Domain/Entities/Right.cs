using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class Right : CommonAuditProperties
    {
        [MaxLength(100, ErrorMessage = "Right cannot be larger than 100 characters")]
        public string Name { get; set; }
    }
}
