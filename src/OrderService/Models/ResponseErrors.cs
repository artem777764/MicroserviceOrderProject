using OrderService.DTOs;

namespace OrderService.Models;

public static class ResponseErrors
{
    public static ErrorDTO ProductNameNotValid() => new ErrorDTO
    {
        Code = "NOT_VALID_NAME",
        Message = "Некорректное название",
    };    
}