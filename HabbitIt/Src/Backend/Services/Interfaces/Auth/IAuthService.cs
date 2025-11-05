using HabbitIt.Dto.Auth;
using HabbitIt.Dto.Login;
using HabbitIt.Dto.RegisterDto;

namespace HabbitIt.Services.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}