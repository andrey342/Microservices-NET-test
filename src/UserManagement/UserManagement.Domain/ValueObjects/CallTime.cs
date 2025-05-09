using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.SeedWork;

namespace UserManagement.Domain.ValueObjects;
public class CallTime : ValueObject
{
    public string Value { get; private set; }

    public CallTime() { }

    public CallTime(string value)
    {
        Value = value;
    }
    public CallTime(CallTime callTime)
    {
        Value = callTime.Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
