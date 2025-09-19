using System.Text.RegularExpressions;

namespace apiserasa.ownedtypes;

public class Password
{
    public string _password { get; private set; } = default!;
    private string? ValidateErrors { get; set; } = null;

    protected Password() { }

    public Password(string password)
    {
        _password = password;
        ValidateErrors = Validate(password);
    }

    private string? Validate(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return "O campo senha não pode estar vazio";
        }

        if (password.Length < 8)
        {
            return "O campo senha deve ter no mínimo 8 caracteres";
        }

        string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%&*]).{6,15}$";

        if (!Regex.IsMatch(password, pattern))
        {
            return "O campo senha deve conter pelo menos: 1 Letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial";
        }

        return null;
    }

    public bool IsValid() => ValidateErrors == null;
    public string? GetError() => ValidateErrors;

    public string ToHash()
    {
        return BCrypt.Net.BCrypt.HashPassword(_password.ToString());
    }

    public bool ValidateRequest(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, _password);
    }

    public override string ToString()
    {
        return _password;
    }
}
