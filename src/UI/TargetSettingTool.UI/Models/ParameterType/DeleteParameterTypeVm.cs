namespace TargetSettingTool.UI.Models.ParameterType
{
    public class DeleteParameterTypeVm
    {
        public Guid Id { get; set; }
        public Guid LoggedInUser { get; set; } =  Guid.Empty;
    }
}
