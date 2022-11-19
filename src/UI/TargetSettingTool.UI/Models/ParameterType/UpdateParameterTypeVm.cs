using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace TargetSettingTool.UI.Models.ParameterType
{
    public class UpdateParameterTypeVm
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z- ]+$", ErrorMessage = "Name Should Contain Alphabet Letters And Hyphen Only")]
        [Remote("IsParameterTypeExists", "ParameterType", AdditionalFields = "Id", HttpMethod = "GET", ErrorMessage = "Given ParameterType Already Exists")]
        [DisplayName("Parameter Type")]
        public string Name { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
