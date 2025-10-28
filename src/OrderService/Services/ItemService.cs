using OrderService.DTOs;
using OrderService.DTOs.ItemDTOs;
using OrderService.Extensions;
using OrderService.Models;
using OrderService.Models.Entities;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IValidationService _validationService;

    public ItemService(IItemRepository itemRepository, IValidationService validationService)
    {
        _itemRepository = itemRepository;
        _validationService = validationService;
    }

    public async Task<ApiResponseDTO<IdDTO>> CreateUserAsync(CreateItemDTO createItemDTO)
    {
        ApiResponseDTOBuilder<IdDTO> builder = new ApiResponseDTOBuilder<IdDTO>();
        if (!_validationService.IsValidProductName(createItemDTO.Name))
        {
            builder.SetError(ResponseErrors.ProductNameNotValid());
            return builder.Build();
        }

        ItemEntity itemEntity = createItemDTO.ToEntity();
        Guid itemId = await _itemRepository.CreateItemAsync(itemEntity);
        IdDTO idDto = new IdDTO() { Id = itemId };

        return builder.SetData(idDto)
                      .SetSuccessful()
                      .Build();
    }
    /*
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

    public async Task<ApiResponseDTO<GetLoginUserDTO>> SetRoleAsync(Guid userId, Guid roleId)
    {
        ApiResponseDTOBuilder<GetLoginUserDTO> apiResponseDTOBuilder = new ApiResponseDTOBuilder<GetLoginUserDTO>();

        UserEntity? userEntity = await _userRepository.GetByIdAsync(userId);
        if (userEntity == null)
        {
            apiResponseDTOBuilder.SetError(ResponseErrors.UserNotFound());
            return apiResponseDTOBuilder.Build();
        }

        string jwtToken = _jwtService.GenerateToken(userEntity, roleId);
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

    public async Task RemoveByIdAsync(Guid userId)
    {
        await _userRepository.RemoveByIdAsync(userId);
    }
    */
}