using UserService.DTOs;

namespace UserService.Models;

public static class ResponseErrors
{
    public static ErrorDTO UserEmailNotValid() => new ErrorDTO
    {
        Code = "USER_EMAIL_NOT_VALID",
        Message = "Некорректная почта",
    };

    public static ErrorDTO UserLoginNotValid() => new ErrorDTO
    {
        Code = "USER_LOGIN_NOT_VALID",
        Message = "Некорректный логин",
    };

    public static ErrorDTO UserPasswordNotValid() => new ErrorDTO
    {
        Code = "USER_PASSWORD_NOT_VALID",
        Message = "Некорректный пароль",
    };
}