using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiktamTechnologies.Repository.Interfaces;
using RiktamTechnologies.Models;

namespace RiktamTechnologies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageRepository messageRepository;

        public MessageController(IMessageRepository _messageRepository)
        {
            messageRepository = _messageRepository;
        }

        [HttpPost]
        [Route("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int? messageId)
        {
            int result = 0;

            if (messageId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await messageRepository.DeleteMessage(messageId ?? 0);
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
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage( [FromQuery] int userId,[FromQuery] int groupId,[FromQuery] string messageText )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var messageId = await messageRepository.SendMessage(userId,groupId,messageText);
                    if (messageId > 0)
                    {
                        return Ok(messageId);
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
        [Route("AddActionToMessage")]
        public async Task<IActionResult> AddActionToMessage([FromQuery] int messageId,[FromQuery] string action)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await messageRepository.AddActionToMessage( messageId,  action);

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
        [Route("ShowMessagesByGroupId")]
        public async Task<IActionResult> ShowMessagesByGroupId(int? groupId)
        {
            if (groupId == null)
            {
                return BadRequest();
            }

            try
            {
                var message = await messageRepository.ShowMessagesByGroupId(groupId ?? 0);

                if (message == null)
                {
                    return NotFound();
                }

                return Ok(message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
