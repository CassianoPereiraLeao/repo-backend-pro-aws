using System.Text.RegularExpressions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace apiserasa.ownedtypes;

public class Email
{
    public string _email { get; private set; } = default!;
    private string? ValidateErrors { get; set; } = null;
    protected Email() { }

    public Email(string email)
    {
        _email = email;
        ValidateErrors = Validate(email);
    }

    private string? Validate(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return "O campo email não pode ser vazio";
        }
        string pattern = @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, pattern))
        {
            return "O formato de email deve ser: exemplo@exemplo.com";
        }

        return null;
    }

    public bool IsValid() => ValidateErrors == null;

    public string? GetError() => ValidateErrors;

    public override string ToString()
    {
        return _email;
    }
}
