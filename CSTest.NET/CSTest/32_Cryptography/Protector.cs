using System.Diagnostics;           // Подключение класса Stopwatch
using System.Security.Cryptography; // Подключение классов Aes
                                    // и других криптографических типов
using System.Text;                  // Подключение класса Encoding
using static System.Convert;        // Подключение метода ToBase64String 
                                    // и других вспомогательных методов

namespace CSTest._32_Cryptography
{
    public class Protector
    {
        // Размер соли должен быть не меньше 8 байт — здесь используется 16 байт
        private static readonly byte[] salt =
            Encoding.Unicode.GetBytes("7BANANAS");
        // По умолчанию класс Rfc2898DeriveBytes использует 1000 итераций
        // Количество итераций должно быть достаточно большим, чтобы генерация 
        // ключа и IV занимала на целевой системе не менее 100 мс — 
        // 150 000 итераций занимают примерно 139 мс 
        // на Intel Core i7-1165G7 11-го поколения @ 2.80 ГГц
        private static readonly int iterations = 150_000;
        public static string Encrypt(
            string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

            using (Aes aes = Aes.Create()) // Получение экземпляра с помощью 
                                           // фабричного метода абстрактного класса
            {
                // Измерение времени, требуемого для генерации ключа и IV
                Stopwatch timer = Stopwatch.StartNew();
                using (Rfc2898DeriveBytes pbkdf2 = new(
                    password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    Debug.WriteLine($"PBKDF2 algorithm: {pbkdf2.HashAlgorithm}, Iteration count: {pbkdf2.IterationCount:N0}");
                    aes.Key = pbkdf2.GetBytes(32); // Установка 256-битного ключа
                    aes.IV = pbkdf2.GetBytes(16);  // Установка 128-битного вектора 
                                                   // инициализации (IV)
                }
                timer.Stop();
                Debug.WriteLine($"{timer.ElapsedMilliseconds:N0} milliseconds to generate Key and IV.");
                if (timer.ElapsedMilliseconds < 100)
                {
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;

                    Debug.WriteLine("WARNING: The elapsed time to generate the Key and IV " +
                                "may be too short to provide a secure encryption key.");
                    Console.ForegroundColor = previousColor;
                }
                Debug.WriteLine($"Encryption algorithm: {nameof(Aes)}-{aes.KeySize}, {aes.Mode} mode with {aes.Padding} padding.");
                using (MemoryStream ms = new())
                {
                    using (ICryptoTransform transformer = aes.CreateEncryptor())
                    {
                        using (CryptoStream cs = new(
                            ms, transformer, CryptoStreamMode.Write))
                        {
                            cs.Write(plainBytes, 0, plainBytes.Length);
                            if (!cs.HasFlushedFinalBlock)
                            {
                                cs.FlushFinalBlock();
                            }
                        }
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return ToBase64String(encryptedBytes);
        }

        public static string Decrypt(
            string cipherText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                using (Rfc2898DeriveBytes pbkdf2 = new(
                    password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    aes.Key = pbkdf2.GetBytes(32);
                    aes.IV = pbkdf2.GetBytes(16);
                }
                using (MemoryStream ms = new())
                {
                    using (ICryptoTransform transformer = aes.CreateDecryptor())
                    {
                        using (CryptoStream cs = new(
                            ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                            if (!cs.HasFlushedFinalBlock)
                            {
                                cs.FlushFinalBlock();
                            }
                        }
                    }
                    plainBytes = ms.ToArray();
                }
            }
            return Encoding.Unicode.GetString(plainBytes);
        }

        /////////////////////////////////////////////////////

        protected static Dictionary<string, User> Users = new();

        public static User Register(string username,
            string password, string[]? roles = null)
        {
            // Генерирует случайную соль
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[16];

            rng.GetBytes(saltBytes);
            string saltText = ToBase64String(saltBytes);
            // Создает хеш пароля с использованием соли
            string saltedhashedPassword = SaltAndHashPassword(password, saltText);
            User user = new(username, saltText, saltedhashedPassword, roles);
            Users.Add(user.Name, user);
            return user;
        }


        // Проверяет пароль пользователя по данным в словаре Users
        public static bool CheckPassword(string username, string password)
        {
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            User u = Users[username];
            return CheckPassword(password,
                u.Salt, u.SaltedHashedPassword);
        }

        // Сравнивает введенный пароль с сохраненным хешем, используя соль
        public static bool CheckPassword(string password,
            string salt, string hashedPassword)
        {
            // Повторно вычисляет хеш с той же солью для сравнения
            string saltedHashedPassword = SaltAndHashPassword(
                password, salt);
            return (saltedHashedPassword == hashedPassword);
        }

        private static string SaltAndHashPassword(string password, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string saltedPassword = password + salt;
                return ToBase64String(
                    sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword))
                );
            }
        }

        /////////////////////////////////////////////////////

        public static string? PublicKey;
        public static string GenerateSignature(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            SHA256 sha = SHA256.Create();
            byte[] hashedData = sha.ComputeHash(dataBytes);
            RSA rsa = RSA.Create();
            PublicKey = rsa.ToXmlString(false); // экспорт закрытого ключа
            return ToBase64String(rsa.SignHash(hashedData,
                HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }
        public static bool ValidateSignature(
            string data, string signature)
        {
            if (PublicKey is null)
                return false;

            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            SHA256 sha = SHA256.Create();
            byte[] hashedData = sha.ComputeHash(dataBytes);
            byte[] signatureBytes = FromBase64String(signature);
            RSA rsa = RSA.Create();
            rsa.FromXmlString(PublicKey);
            return rsa.VerifyHash(hashedData, signatureBytes,
                HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static byte[] GetRandomKeyOrIV(int size)
        {
            RandomNumberGenerator r = RandomNumberGenerator.Create();
            byte[] data = new byte[size];
            // Массив заполняется криптографически стойкими случайными байтами
            r.GetBytes(data);
            return data;
        }
    }
}
