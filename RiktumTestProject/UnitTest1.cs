using NUnit.Framework;
using RiktamTechnologies.Models;
using RiktamTechnologies.Repository;
using RiktamTechnologies.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiktumTestProject
{
    [TestFixture]
    public class Tests
    {
        private RiktamContext riktamContext = new RiktamContext();
        private GroupRepository groupRepository = new GroupRepository(new RiktamContext());
        private MessageRepository messageRepository = new MessageRepository(new RiktamContext());
        private UserRepository userRepository = new UserRepository(new RiktamContext());

        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public async Task CreateGroup()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            Assert.True(groupId > 0);
        }
        [Test]
        public async Task AddUserToGroup()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            GroupMember groupMember = new GroupMember();
            groupMember.GroupId = groupId;
            groupMember.UserId = userId;
            var Id = await groupRepository.AddUserToGroup(groupMember);
            Assert.True(Id > 0);
        }

        [Test]
        public async Task GetGroupsById()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            GroupMember groupMember = new GroupMember();
            groupMember.GroupId = groupId;
            groupMember.UserId = userId;
            var result = await groupRepository.AddUserToGroup(groupMember);
            var groups = await groupRepository.GetGroupsById(userId);
            Assert.True(groups.Count > 0);
        }


        [Test]
        public async Task ShowGroupUsers()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            GroupMember groupMember = new GroupMember();
            groupMember.GroupId = groupId;
            groupMember.UserId = userId;
            var Id = await groupRepository.AddUserToGroup(groupMember);
            var result = await groupRepository.ShowGroupUsers(groupId);
            await userRepository.DeleteUser(userId);
            await groupRepository.DeleteGroup(groupId);
            Assert.True(result.Count > 0);
        }

        [Test]
        public async Task DeleteGroup()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            var result = await groupRepository.DeleteGroup(groupId);
            Assert.True(result > 0);
        }

        [Test]
        public async Task SendMessage()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            var auditId = await messageRepository.SendMessage(userId, groupId, "Test Message");
            await userRepository.DeleteUser(userId);
            await messageRepository.DeleteMessage(auditId);
            Assert.True(auditId > 0);
        }

        [Test]
        public async Task DeleteMessage()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            var AuditId = await messageRepository.SendMessage(userId, groupId, "Test Message");
            var result = await messageRepository.DeleteMessage(AuditId);
            await userRepository.DeleteUser(userId);
            Assert.True(result > 0);
        }

        [Test]
        public async Task ShowMessagesByGroupId()
        {
            var groupId = await groupRepository.CreateGroup(new Group());
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            var auditId = await messageRepository.SendMessage(userId, groupId, "Test Message");
            var messageAudit = await messageRepository.ShowMessagesByGroupId(groupId);
            await userRepository.DeleteUser(userId);
            await messageRepository.DeleteMessage(auditId);
            Assert.True(messageAudit.Count > 0);
        }

        [Test]
        public async Task GetUser()
        {
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            var users = await userRepository.GetUsers();
            await userRepository.DeleteUser(userId);
            Assert.True(users.Count > 0);
        }

        [Test]
        public async Task GetUsers()
        {
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            User user1 = new User();
            user1.UserName = "Test User 1";
            var userId1 = await userRepository.AddUser(user1);
            var users = await userRepository.GetUsers();
            await userRepository.DeleteUser(userId);
            await userRepository.DeleteUser(userId1);
            Assert.True(users.Count > 0);
        }

        [Test]
        public async Task AuthenticateUser()
        {
            User user = new User();
            user.UserName = "Test User";
            user.UserPassword = "Password";
            var userId = await userRepository.AddUser(user);
            var result = await userRepository.AuthenticateUser(user.UserName, user.UserPassword);
            await userRepository.DeleteUser(userId);
            Assert.True(result);
        }

        [Test]
        public async Task AddUser()
        {
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            await userRepository.DeleteUser(userId);
            Assert.True(userId > 0);
        }

        [Test]
        public async Task DeleteUser()
        {
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            var  result=await userRepository.DeleteUser(userId);
            Assert.True(result > 0);
        }

        [Test]
        public async Task UpdateUser()
        {
            User user = new User();
            user.UserName = "Test User";
            var userId = await userRepository.AddUser(user);
            user.UserId = userId;   
            user.UserName = "Test User 1";
            await userRepository.UpdateUser(user);
            User result= await userRepository.GetUser(userId);
            await userRepository.DeleteUser(userId);
            Assert.True(result.UserName == "Test User 1");
        }
    }
}