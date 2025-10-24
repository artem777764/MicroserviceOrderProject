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

    public static ErrorDTO UserNotFound() => new ErrorDTO
    {
        Code = "USER_NOT_FOUND",
        Message = "Пользователь не найден",
    };

    public static ErrorDTO UserSurnameNotValid() => new ErrorDTO
    {
        Code = "USER_SURNAME_NOT_VALID",
        Message = "Некорректная фамилия",
    };

    public static ErrorDTO UserNameNotValid() => new ErrorDTO
    {
        Code = "USER_NAME_NOT_VALID",
        Message = "Некорректное имя",
    };

    public static ErrorDTO UserPatronymicNotValid() => new ErrorDTO
    {
        Code = "USER_PATRONYMIC_NOT_VALID",
        Message = "Некорректное отчество",
    };
}