namespace UserService.Services.Interfaces;

public interface IValidationService
{
    bool IsValidEmail(string email);
    bool IsValidLogin(string login);
    bool IsValidName(string name);
    bool IsValidPassword(string password);
    bool IsValidPatronymic(string patronymic);
    bool IsValidSurname(string surname);
}
