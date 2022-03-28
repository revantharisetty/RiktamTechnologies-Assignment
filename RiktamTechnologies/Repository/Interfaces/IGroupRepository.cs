using RiktamTechnologies.Models;

namespace RiktamTechnologies.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<int> CreateGroup(Group group);
        Task UpdateGroup(Group group);
        Task<int> DeleteGroup(int id);
        Task<List<User>> ShowGroupUsers(int groupId);
        Task<List<Group>> GetGroupsById(int userId);
        Task<int> AddUserToGroup(GroupMember groupMember);
        Task<int> RemoveUserFromGroup(int auditId);


    }
}
