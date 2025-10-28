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
            builder.SetError(ResponseErrors.ItemNameNotValid());
            return builder.Build();
        }

        ItemEntity itemEntity = createItemDTO.ToEntity();
        Guid itemId = await _itemRepository.CreateItemAsync(itemEntity);
        IdDTO idDto = new IdDTO() { Id = itemId };

        return builder.SetData(idDto)
                      .SetSuccessful()
                      .Build();
    }
    
    public async Task<ApiResponseDTO<GetItemDTO>> GetItemByIdAsync(Guid itemId)
    {
        ApiResponseDTOBuilder<GetItemDTO> builder = new ApiResponseDTOBuilder<GetItemDTO>();

        ItemEntity? itemEntity = await _itemRepository.GetByIdAsync(itemId);
        if (itemEntity == null)
        {
            builder.SetError(ResponseErrors.ItemNotFound());
            return builder.Build();
        }

        return builder.SetData(itemEntity.ToDTO())
                      .SetSuccessful()
                      .Build();
    }

    public async Task<ApiResponseDTO<List<GetItemDTO>>> GetAllAsync()
    {
        ApiResponseDTOBuilder<List<GetItemDTO>> builder = new ApiResponseDTOBuilder<List<GetItemDTO>>();

        List<ItemEntity> itemEntities = await _itemRepository.GetAllAsync();
        return builder.SetData(itemEntities.Select(ue => ue.ToDTO()).ToList())
                      .SetSuccessful()
                      .Build();
    }
    
    public async Task<ApiResponseNoDataDTO> RemoveByIdAsync(Guid itemId)
    {
        ApiResponseNoDataDTOBuilder builder = new ApiResponseNoDataDTOBuilder();
        
        await _itemRepository.RemoveByIdAsync(itemId);
        return builder.SetSuccessful()
                      .Build();
    }
}