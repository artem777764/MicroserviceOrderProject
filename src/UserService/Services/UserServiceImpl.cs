using Backend.DTOs.UserDTOs;
using UserService.DTOs;
using UserService.DTOs.UserDataDTOs;
using UserService.DTOs.UserDTOs;
using UserService.Extensions;
using UserService.Models;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidationService _validationService;
    private readonly IEncryptionService _encryptionService;
    private readonly IJwtService _jwtService;

    public UserServiceImpl(
        IUserRepository userRepository,
        IValidationService validationService,
        IEncryptionService encryptionService,
        IJwtService jwtService
    )
    {
        _userRepository = userRepository;
        _validationService = validationService;
        _encryptionService = encryptionService;
        _jwtService = jwtService;
    }

    public async Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        ApiResponseDTOBuilder<IdDTO> builder = new ApiResponseDTOBuilder<IdDTO>();
        if (!_validationService.IsValidEmail(createUserDTO.Email))
        {
            builder.SetError(ResponseErrors.UserEmailNotValid());
            return builder.Build();
        }

        if (!_validationService.IsValidLogin(createUserDTO.Login))
        {
            builder.SetError(ResponseErrors.UserLoginNotValid());
            return builder.Build();
        }

        if (!_validationService.IsValidPassword(createUserDTO.Password))
        {
            builder.SetError(ResponseErrors.UserPasswordNotValid());
            return builder.Build();
        }

        string passwordHash = _encryptionService.HashPassword(createUserDTO.Password);
        UserEntity userEntity = createUserDTO.ToEntity(passwordHash);
        Guid userId = await _userRepository.CreateUserAsync(userEntity);
        IdDTO idDto = new IdDTO() { Id = userId };

        return builder.SetData(idDto)
                      .SetSuccessful()
                      .Build();
    }

    public async Task<ApiResponseDTO<GetLoginUserDTO>> LoginUserAsync(LoginDTO loginDTO)
    {
        ApiResponseDTOBuilder<GetLoginUserDTO> apiResponseDTOBuilder = new ApiResponseDTOBuilder<GetLoginUserDTO>();

        UserEntity? userEntity = await _userRepository.GetByEmailAsync(loginDTO.UserName);
        if (userEntity == null) userEntity = await _userRepository.GetByLoginAsync(loginDTO.UserName);
        if (userEntity == null)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.UserNotFound());
            return apiResponseDTOBuilder.Build();
        }

        if (!_encryptionService.VerifyPassword(loginDTO.Password, userEntity!.PasswordHash))
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.UserPasswordNotValid());
            return apiResponseDTOBuilder.Build();
        }

        string jwtToken = _jwtService.GenerateToken(userEntity);
        string JwtCookieName = _jwtService.GetJwtCookieName();

        GetLoginUserDTO getLoginUserDTO = new GetLoginUserDTO()
        {
            UserId = userEntity.Id,
            JwtToken = jwtToken,
            JwtCookieName = JwtCookieName,
        };

        return apiResponseDTOBuilder.SetData(getLoginUserDTO)
                                    .SetSuccessful()
                                    .Build();
    }

    public async Task<ApiResponseDTO<GetUserDTO>> GetUserByIdAsync(Guid userId)
    {
        ApiResponseDTOBuilder<GetUserDTO> builder = new ApiResponseDTOBuilder<GetUserDTO>();

        UserEntity? userEntity = await _userRepository.GetByIdAsync(userId);
        if (userEntity == null)
        {
            builder.SetError(ResponseErrors.UserNotFound());
            return builder.Build();
        }

        return builder.SetData(userEntity.ToDTO())
                      .SetSuccessful()
                      .Build();
    }

    public async Task<ApiResponseDTO<List<GetUserDTO>>> GetAllAsync()
    {
        ApiResponseDTOBuilder<List<GetUserDTO>> builder = new ApiResponseDTOBuilder<List<GetUserDTO>>();

        List<UserEntity> userEntities = await _userRepository.GetAllAsync();
        return builder.SetData(userEntities.Select(ue => ue.ToDTO()).ToList())
                      .SetSuccessful()
                      .Build();
    }
}