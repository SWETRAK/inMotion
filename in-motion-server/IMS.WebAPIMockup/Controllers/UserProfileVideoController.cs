using System.Net.Mime;
using System.Text.Json;
using FluentValidation;
using IMS.Shared.Models.Dto.User.Video;
using IMS.Shared.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebAPIMockup.Controllers;

// TODO: Finish documentation
[ApiController]
[Route("user/v{version:apiVersion}/profileVideo")]
[Produces(MediaTypeNames.Application.Json)]
public class UserProfileVideoController: ControllerBase
{
    private readonly IValidator<CreateUserProfileVideoDto> _createUserProfileVideoValidator;

    public UserProfileVideoController(IValidator<CreateUserProfileVideoDto> createUserProfileVideoValidator)
    {
        _createUserProfileVideoValidator = createUserProfileVideoValidator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="videoId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [Produces("video/mp4")]
    [HttpGet("{videoId}")]
    public IActionResult GetUserProfileVideo([FromRoute] string videoId)
    {
        var stream = new MemoryStream();
        return File(stream, "video/mp4", "filename");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="video"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="InvalidIncomingDataException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpPost]
    public async Task<ActionResult<UserProfileVideoDto>> CreateUserProfileVideo(
        [FromForm(Name = "video")] IFormFile[] video,
        [FromForm(Name = "data")] string data)
    {
        var createUserProfileVideoDto = JsonSerializer.Deserialize<CreateUserProfileVideoDto>(data); // can throw JsonException and can be null
        if (createUserProfileVideoDto is null) throw new Exception();
        var validationResult = await _createUserProfileVideoValidator.ValidateAsync(createUserProfileVideoDto); // throw specific exception with this data
        if (!validationResult.IsValid) throw new InvalidIncomingDataException(validationResult);
        
        
        return Created("", new UserProfileVideoDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="videoId"></param>
    /// <param name="video"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="InvalidIncomingDataException"></exception>
    /// <exception cref="JsonException"></exception>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpPut("{videoId}")]
    public async Task<ActionResult<UserProfileVideoDto>> UpdateUserProfileVideo(
        [FromRoute(Name = "videoId")] string videoId,
        [FromForm(Name = "video")] IFormFile[] video,
        [FromForm(Name = "data")] string data)
    {
        var createUserProfileVideoDto = JsonSerializer.Deserialize<CreateUserProfileVideoDto>(data); // can throw JsonException and can be null
        if (createUserProfileVideoDto is null) throw new Exception();
        var validationResult = await _createUserProfileVideoValidator.ValidateAsync(createUserProfileVideoDto); // throw specific exception with this data
        if (!validationResult.IsValid) throw new InvalidIncomingDataException(validationResult);
        
        return Ok(new UserProfileVideoDto());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="videoId"></param>
    /// <returns></returns>
    /// <response code="200">Returns founded item</response>
    /// <response code="404">If the item is not existing</response>
    /// <response code="403">If user is unauthorized</response>
    [HttpDelete("{videoId}")]
    public IActionResult RemoveUserProfileVideo([FromRoute(Name = "videoId")] string videoId)
    {
        return Ok();
    }
}