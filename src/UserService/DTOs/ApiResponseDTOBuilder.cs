using UserService.Models;

namespace UserService.DTOs;

public class ApiResponseDTOBuilder<T>
{
    ApiResponseDTO<T> _apiResponseDTO;
    public ApiResponseDTOBuilder()
    {
        _apiResponseDTO = new ApiResponseDTO<T>()
        {
            Success = false,
            Data = default,
            Error = default,
        };
    }

    public ApiResponseDTOBuilder<T> SetSuccessful()
    {
        _apiResponseDTO.Success = true;
        return this;
    }

    public ApiResponseDTOBuilder<T> SetData(T data)
    {
        _apiResponseDTO.Data = data;
        return this;
    }

    public ApiResponseDTOBuilder<T> SetError(ErrorDTO errorDTO)
    {
        _apiResponseDTO.Error = errorDTO;
        return this;
    }

    public ApiResponseDTO<T> Build()
    {
        return _apiResponseDTO;
    }
}