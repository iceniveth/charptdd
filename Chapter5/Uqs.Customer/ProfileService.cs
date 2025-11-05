using System.Text.RegularExpressions;

namespace Uqs.Customer;

public class ProfileService
{
    // change the argument to be a nullable string

    public void ChangeUsername(string? username)
    {
        if (username == null)
        {
            throw new ArgumentNullException(nameof(username), "Null username is not allowed.");
        }
        else if (username.Length is < 8 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(username), "Length");
        }

        if (!Regex.Match(username, @"^[a-zA-Z0-9_]+$").Success)
        {
            throw new ArgumentOutOfRangeException(nameof(username), "InvalidChar");
        }
    }
}
