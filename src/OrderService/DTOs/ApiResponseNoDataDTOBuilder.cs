namespace OrderService.DTOs;

public class ApiResponseNoDataDTOBuilder
{
    ApiResponseNoDataDTO _apiResponseNoDataDTO;
    public ApiResponseNoDataDTOBuilder()
    {
        _apiResponseNoDataDTO = new ApiResponseNoDataDTO()
        {
            Success = false,
            Error = default,
        };
    }

    public ApiResponseNoDataDTOBuilder SetSuccessful()
    {
        _apiResponseNoDataDTO.Success = true;
        return this;
    }

    public ApiResponseNoDataDTOBuilder SetError(ErrorDTO errorDTO)
    {
        _apiResponseNoDataDTO.Error = errorDTO;
        return this;
    }

    public ApiResponseNoDataDTO Build()
    {
        return _apiResponseNoDataDTO;
    }
}