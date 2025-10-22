using Backend.DTOs.UserDTOs;
using UserService.DTOs;
using UserService.Extensions;
using UserService.Models;
using UserService.Models.Entities;
using UserService.Repositories.interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidationService _validationService;
    private readonly IEncryptionService _encryptionService;
    private readonly ApiResponseDTOBuilder<IdDTO> _apiResponseDTOBuilder;

    public UserServiceImpl(
        IUserRepository userRepository,
        IValidationService validationService,
        IEncryptionService encryptionService
    )
    {
        _userRepository = userRepository;
        _validationService = validationService;
        _encryptionService = encryptionService;
        _apiResponseDTOBuilder = new ApiResponseDTOBuilder<IdDTO>();
    }

    public async Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        if (!_validationService.IsValidEmail(createUserDTO.Email))
        {
            _apiResponseDTOBuilder.SetError(ResponseErrors.UserEmailNotValid());
            return _apiResponseDTOBuilder.Build();
        }

        if (!_validationService.IsValidLogin(createUserDTO.Login))
        {
            _apiResponseDTOBuilder.SetError(ResponseErrors.UserLoginNotValid());
            return _apiResponseDTOBuilder.Build();
        }

        if (!_validationService.IsValidPassword(createUserDTO.Password))
        {
            _apiResponseDTOBuilder.SetError(ResponseErrors.UserPasswordNotValid());
            return _apiResponseDTOBuilder.Build();
        }

        string passwordHash = _encryptionService.HashPassword(createUserDTO.Password);
        UserEntity userEntity = createUserDTO.ToEntity(passwordHash);
        Guid userId = await _userRepository.CreateUserAsync(userEntity);
        IdDTO idDto = new IdDTO() { Id = userId };

        return _apiResponseDTOBuilder.SetData(idDto)
                                     .SetSuccessful()
                                     .Build();
    }
}