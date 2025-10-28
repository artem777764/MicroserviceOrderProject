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

    public static ErrorDTO OrderNotFound() => new ErrorDTO
    {
        Code = "ORDER_NOT_FOUND",
        Message = "Заказ не найден",
    };

    public static ErrorDTO Unauthorized() => new ErrorDTO
    {
        Code = "UNAUTHORIZED",
        Message = "Пользователь не авторизован",
    };

    public static ErrorDTO Forbidden() => new ErrorDTO
    {
        Code = "FORBIDDEN",
        Message = "Пользователь не имеет необходимой роли",
    };    
}