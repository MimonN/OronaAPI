using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUserAsync(string username);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<UserDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);

    }
}
