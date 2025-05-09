using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.ValueObjects;
public class Email: ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        Value = value;
    }
    public Email(Email email)
    {
        Value = email.Value;
    }

    private static bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        return emailRegex.IsMatch(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower(); // Comparar emails ignorando mayúsculas/minúsculas
    }

    public override string ToString()
    {
        return Value;
    }
}
