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
    private readonly ApiResponseDTOBuilder<IdDTO> _apiResponseIdDTOBuilder;
    private readonly ApiResponseDTOBuilder<GetUserDTO> _apiResponseGetUserDTOBuilder;

    public UserServiceImpl(
        IUserRepository userRepository,
        IValidationService validationService,
        IEncryptionService encryptionService
    )
    {
        _userRepository = userRepository;
        _validationService = validationService;
        _encryptionService = encryptionService;
        _apiResponseIdDTOBuilder = new ApiResponseDTOBuilder<IdDTO>();
        _apiResponseGetUserDTOBuilder = new ApiResponseDTOBuilder<GetUserDTO>();
    }

    public async Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        if (!_validationService.IsValidEmail(createUserDTO.Email))
        {
            _apiResponseIdDTOBuilder.SetError(ResponseErrors.UserEmailNotValid());
            return _apiResponseIdDTOBuilder.Build();
        }

        if (!_validationService.IsValidLogin(createUserDTO.Login))
        {
            _apiResponseIdDTOBuilder.SetError(ResponseErrors.UserLoginNotValid());
            return _apiResponseIdDTOBuilder.Build();
        }

        if (!_validationService.IsValidPassword(createUserDTO.Password))
        {
            _apiResponseIdDTOBuilder.SetError(ResponseErrors.UserPasswordNotValid());
            return _apiResponseIdDTOBuilder.Build();
        }

        string passwordHash = _encryptionService.HashPassword(createUserDTO.Password);
        UserEntity userEntity = createUserDTO.ToEntity(passwordHash);
        Guid userId = await _userRepository.CreateUserAsync(userEntity);
        IdDTO idDto = new IdDTO() { Id = userId };

        return _apiResponseIdDTOBuilder.SetData(idDto)
                                     .SetSuccessful()
                                     .Build();
    }

    public async Task<ApiResponseDTO<GetUserDTO>> GetUserByIdAsync(Guid userId)
    {
        UserEntity? userEntity = await _userRepository.GetByIdAsync(userId);
        if (userEntity == null)
        {
            _apiResponseGetUserDTOBuilder.SetError(ResponseErrors.UserNotFound());
            return _apiResponseGetUserDTOBuilder.Build();
        }

        return _apiResponseGetUserDTOBuilder.SetData(userEntity.ToDTO())
                                            .SetSuccessful()
                                            .Build();
    }
}