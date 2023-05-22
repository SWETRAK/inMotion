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

    [Produces("video/mp4")]
    [HttpGet("{videoId}")]
    public IActionResult GetUserProfileVideo([FromRoute] string videoId)
    {
        var stream = new MemoryStream();
        return File(stream, "video/mp4", "filename");
    }

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

    [HttpDelete("{videoId}")]
    public IActionResult RemoveUserProfileVideo([FromRoute(Name = "videoId")] string videoId)
    {
        return Ok();
    }
}