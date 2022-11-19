using TargetSettingTool.Application.Models.Mail;
using System.Threading.Tasks;

namespace TargetSettingTool.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
