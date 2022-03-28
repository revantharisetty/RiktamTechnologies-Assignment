using Microsoft.EntityFrameworkCore;
using RiktamTechnologies.Models;
using RiktamTechnologies.Repository.Interfaces;

namespace RiktamTechnologies.Repository
{
    public class GroupRepository: IGroupRepository
    {
        RiktamContext db;
        public GroupRepository(RiktamContext _db)
        {
            db = _db;
        }
        public async Task<int> CreateGroup(Group group)

        {
            if (db != null)
            {
                await db.Groups.AddAsync(group);
                await db.SaveChangesAsync();
                return group.GroupId;
            }

            return 0;
        }
        public async Task<int> DeleteGroup(int groupId)

        {
            int result = 0;

            if (db != null)
            {
                var group = await db.Groups.FirstOrDefaultAsync(x => x.GroupId == groupId);

                if (group != null)
                {
                    db.Groups.Remove(group);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task UpdateGroup(Group group)
        {
            if (db != null)
            {
                db.Groups.Update(group);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Group>> GetGroupsById(int UserId)
        {
            return await (from u in db.GroupMembers
                          from c in db.Groups
                          where u.UserId == UserId && u.GroupId==c.GroupId

                          select new Group
                          {
                              GroupId=c.GroupId,
                              GroupName=c.GroupName

                          }).ToListAsync();
        }

        public async Task<List<User>> ShowGroupUsers(int groupId)
        {
            return await (from u in db.Users
                          from g in db.GroupMembers
                          where g.GroupId == groupId && g.UserId == u.UserId

                          select new User
                          {
                              UserId = u.UserId,
                              UserName = u.UserName,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              MobileNumber = u.MobileNumber
                          }).ToListAsync();
        }
        public async Task<int> AddUserToGroup(GroupMember groupMember)
        {

            if (db != null)
            {
                await db.GroupMembers.AddAsync(groupMember);
                await db.SaveChangesAsync();
                return groupMember.AuditId;
            }

            return 0;
        }

        public async Task<int> RemoveUserFromGroup(int auditId)
        {
            int result = 0;

            if (db != null)
            {
                var groupMember = await db.GroupMembers.FirstOrDefaultAsync(x => x.AuditId== auditId);

                if (groupMember != null)
                {
                    db.GroupMembers.Remove(groupMember);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


    }
}
