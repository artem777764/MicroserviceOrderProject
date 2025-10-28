using OrderService.DTOs;

namespace OrderService.Models;

public static class ResponseErrors
{
    public static ErrorDTO ItemNameNotValid() => new ErrorDTO
    {
        Code = "NOT_VALID_ITEM_NAME",
        Message = "Некорректное название товара",
    };

    public static ErrorDTO ItemNotFound() => new ErrorDTO
    {
        Code = "ITEM_NOT_FOUND",
        Message = "Товар не найден",
    };
}