

namespace CSTest._32_Cryptography
{
    public record class User(string Name, string Salt, string SaltedHashedPassword, string[]? Roles);
}
