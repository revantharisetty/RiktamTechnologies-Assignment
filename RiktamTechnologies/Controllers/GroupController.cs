using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiktamTechnologies.Models;
using RiktamTechnologies.Repository;
using RiktamTechnologies.Repository.Interfaces;

namespace RiktamTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        IGroupRepository groupRepository;
        public GroupController(IGroupRepository _groupRepository)
        {
            groupRepository = _groupRepository;
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var groupId = await groupRepository.CreateGroup(group);
                    if (groupId > 0)
                    {
                        return Ok(groupId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteGroup")]
        public async Task<IActionResult> DeleteGroup(int? groupId)
        {
            int result = 0;

            if (groupId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await groupRepository.DeleteGroup(groupId??0);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdateGroup")]
        public async Task<IActionResult> UpdateGroup([FromBody] Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await groupRepository.UpdateGroup(group);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetGroupsById")]
        public async Task<IActionResult> GetGroupsById(int? userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            try
            {
                var group = await groupRepository.GetGroupsById(userId ?? 0);

                if (group == null)
                {
                    return NotFound();
                }

                return Ok(group);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("ShowGroupUsers")]
        public async Task<IActionResult> ShowGroupUsers(int? groupId)
        {
            if (groupId == null)
            {
                return BadRequest();
            }

            try
            {
                var group = await groupRepository.ShowGroupUsers(groupId ?? 0);

                if (group == null)
                {
                    return NotFound();
                }

                return Ok(group);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddUserToGroup")]
        public async Task<IActionResult> AddUserToGroup(GroupMember groupMember)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var groupId = await groupRepository.AddUserToGroup(groupMember);
                    if (groupId > 0)
                    {
                        return Ok(groupId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("RemoveUserFromGroup")]
        public async Task<IActionResult> RemoveUserFromGroup(int? auditId)
        {
            int result = 0;

            if (auditId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await groupRepository.RemoveUserFromGroup(auditId ?? 0);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
