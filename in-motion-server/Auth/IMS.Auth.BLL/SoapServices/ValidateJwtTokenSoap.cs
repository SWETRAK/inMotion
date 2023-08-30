using AutoMapper;
using IMS.Auth.IBLL.Services;
using IMS.Auth.IBLL.SoapServices;
using IMS.Auth.Models.Dto.Outgoing;
using Microsoft.Extensions.Logging;

namespace IMS.Auth.BLL.SoapServices;

public class ValidateJwtTokenSoap: IValidateJwtTokenSoap
{
    private readonly ILogger<ValidateJwtTokenSoap> _logger;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public ValidateJwtTokenSoap(
        ILogger<ValidateJwtTokenSoap> logger, 
        IJwtService jwtService, IMapper mapper)
    {
        _logger = logger;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<UserInfoWithRoleDto> ValidateJwtToken(string jwtToken)
    {
        try
        {
            var result = await _jwtService.ValidateToken(jwtToken);
            return _mapper.Map<UserInfoWithRoleDto>(result);
        }
        catch (Exception exception)
        {
            return null;
        }
    }
}