using RiktamTechnologies.Models;

namespace RiktamTechnologies.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<int> SendMessage(int userId, int groupId, string messageText);

        Task<int> DeleteMessage(int messageId);

        Task AddActionToMessage(int messageId,string action);

        Task<List<MessageAudit>> ShowMessagesByGroupId(int GroupId);
    }
}
