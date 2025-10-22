using Backend.DTOs.UserDTOs;
using UserService.DTOs;
using UserService.DTOs.UserDataDTOs;
using UserService.Extensions;
using UserService.Models;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services;

public class UserDataService : IUserDataService
{
    private readonly IUserDataRepository _userDataRepository;
    private readonly IValidationService _validationService;

    public UserDataService(
        IUserDataRepository userDataRepository,
        IValidationService validationService
    )
    {
        _userDataRepository = userDataRepository;
        _validationService = validationService;
    }

    public async Task<ApiResponseDTO<IdDTO>> CreateUserDataAsync(Guid userId, CreateUserDataDTO createUserDataDTO)
    {
        ApiResponseDTOBuilder<IdDTO> builder = new ApiResponseDTOBuilder<IdDTO>();

        if (!_validationService.IsValidSurname(createUserDataDTO.Surname))
        {
            builder.SetError(ResponseErrors.UserSurnameNotValid());
            return builder.Build();
        }

        if (!_validationService.IsValidName(createUserDataDTO.Name))
        {
            builder.SetError(ResponseErrors.UserNameNotValid());
            return builder.Build();
        }

        if (!_validationService.IsValidPatronymic(createUserDataDTO.Patronymic))
        {
            builder.SetError(ResponseErrors.UserPatronymicNotValid());
            return builder.Build();
        }

        UserDataEntity userDataEntity = createUserDataDTO.ToEntity(userId);
        Guid userDataId = await _userDataRepository.CreateUserDataAsync(userDataEntity);
        IdDTO idDto = new IdDTO() { Id = userDataId };

        return builder.SetData(idDto)
                      .SetSuccessful()
                      .Build();
    }
}