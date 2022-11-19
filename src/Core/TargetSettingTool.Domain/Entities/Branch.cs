using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class Branch : CommonAuditProperties
    {
        [MaxLength(100, ErrorMessage = "Branch cannot be larger than 100 characters")]
        public string Name { get; set; }

        public Guid? RegionId { get; set; }


        [ForeignKey("RegionId ")]
        public virtual User? Region { get; set; }
    }
}
