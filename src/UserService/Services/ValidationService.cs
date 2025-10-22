using System.Text.RegularExpressions;
using UserService.Services.Interfaces;

namespace Backend.Services;

public class ValidationService : IValidationService
{
    const string LoginPattern = @"^[a-zA-Z0-9_]{3,20}$";
    const string EmailPattern = @"^(?![.])(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*)@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$";
    const string PasswordPattern = @"^[^\s]{6,}$";
    const string SurnamePattern = @"^[А-ЯЁа-яё\-]+$";
    const string NamePattern = @"^[А-ЯЁа-яё\-]+$";
    const string PatronymicPattern = @"^([А-ЯЁа-яё\-]*)$";

    private readonly Regex LoginRegex = new Regex(LoginPattern, RegexOptions.Compiled);
    private readonly Regex EmailRegex = new Regex(EmailPattern, RegexOptions.Compiled);
    private readonly Regex PasswordRegex = new Regex(PasswordPattern, RegexOptions.Compiled);
    private readonly Regex SurnameRegex = new Regex(SurnamePattern, RegexOptions.Compiled);
    private readonly Regex NameRegex = new Regex(NamePattern, RegexOptions.Compiled);
    private readonly Regex PatronymicRegex = new Regex(PatronymicPattern, RegexOptions.Compiled);

    public bool IsValidLogin(string login)
    {
        return LoginRegex.IsMatch(login);
    }

    public bool IsValidEmail(string email)
    {
        return EmailRegex.IsMatch(email);
    }

    public bool IsValidPassword(string password)
    {
        return PasswordRegex.IsMatch(password);
    }

    public bool IsValidSurname(string surname)
    {
        return SurnameRegex.IsMatch(surname);
    }

    public bool IsValidName(string name)
    {
        return NameRegex.IsMatch(name);
    }

    public bool IsValidPatronymic(string? patronymic)
    {
        if (patronymic == null) return true;
        return PatronymicRegex.IsMatch(patronymic);
    }
}