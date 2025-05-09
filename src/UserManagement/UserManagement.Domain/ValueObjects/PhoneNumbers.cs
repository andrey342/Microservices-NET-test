using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.ValueObjects;
public class PhoneNumbers : ValueObject
{
    public string MobilePhone { get; private set; }
    public string HomePhone { get; private set; }

    public PhoneNumbers(){}

    public PhoneNumbers(string mobilePhone, string homePhone)
    {
        MobilePhone = mobilePhone;
        HomePhone = homePhone;
    }

    public PhoneNumbers(PhoneNumbers phoneNumbers)
    {
        MobilePhone = phoneNumbers.MobilePhone;
        HomePhone = phoneNumbers.HomePhone;
    }

    private bool IsValidPhoneNumber(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, @"^\+\d{1,3}\s?\d{4,14}$");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MobilePhone;
        yield return HomePhone;
    }
}
