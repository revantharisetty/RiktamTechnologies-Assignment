using Microsoft.EntityFrameworkCore;
using RiktamTechnologies.Models;
using RiktamTechnologies.Repository.Interfaces;

namespace RiktamTechnologies.Repository

{
    public class MessageRepository: IMessageRepository
    {
        RiktamContext db;
        public MessageRepository(RiktamContext _db)
        {
            db = _db;
        }

        public async Task AddActionToMessage(int messageId, string action)
        {
            if (db != null)
            {
                var messageAudit = await db.MessageAudits.FirstOrDefaultAsync(x => x.AuditId == messageId);
                if (messageAudit != null)
                {
                    if (action.ToLower() == "like")
                    {
                        messageAudit.LikeCount = messageAudit.LikeCount ?? 0;
                        messageAudit.LikeCount += 1;
                    }
                        
                    else if (action.ToLower() == "dislike")

                    {
                        messageAudit.DisLikeCount=messageAudit.DisLikeCount ?? 0;
                        messageAudit.DisLikeCount += 1;
                    }
                        

                    db.MessageAudits.Update(messageAudit);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task<int> DeleteMessage(int messageId)
        {
            int result = 0;

            if (db != null)
            {
                var messageAudit = await db.MessageAudits.FirstOrDefaultAsync(x => x.AuditId == messageId);

                if (messageAudit != null)
                {
                    db.MessageAudits.Remove(messageAudit);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<int> SendMessage(int userId,int groupId,string messageText)
        {
            MessageAudit messageAudit = new MessageAudit() { UserId=userId,GroupId=groupId,MessageText=messageText,AuditDate=DateTime.Now};
            if (db != null)
            {
                await db.MessageAudits.AddAsync(messageAudit);
                await db.SaveChangesAsync();
                return messageAudit.AuditId;
            }

            return 0;
        }

        public async Task<List<MessageAudit>> ShowMessagesByGroupId(int GroupId)
        {
            return await (from m in db.MessageAudits
                          where m.GroupId== GroupId

                          select new MessageAudit
                          {
                              GroupId = m.GroupId,
                              DisLikeCount = m.DisLikeCount??0,    
                              AuditDate = m.AuditDate,  
                              AuditId = m.AuditId,  
                              LikeCount = m.LikeCount??0,  
                              MessageText = m.MessageText,  
                              UserId=m.UserId

                          }).ToListAsync();
        }
    }
}
