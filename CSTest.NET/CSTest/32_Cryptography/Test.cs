using System.Diagnostics;           // Подключение класса Stopwatch
using System.Security.Cryptography; // Подключение классов Aes
                                    // и других криптографических типов
                                    // и других вспомогательных методов
using NUnit.Framework;

namespace CSTest._32_Cryptography
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestAes()
        {
            Debug.Write("Enter a message that you want to encrypt: ");
            //string? message = ReadLine();
            string? message = "My secret phrase";
            Debug.Write("Enter a password: ");
            //string? password = ReadLine();
            string? password = "MyPass!!";
            if ((password is null) || (message is null))
            {
                Debug.WriteLine("Message or password cannot be null.");
                return; // Выход из приложения
            }
            string cipherText = Protector.Encrypt(message, password);
            Debug.WriteLine($"Encrypted text: {cipherText}");
            Debug.Write("Enter the password: ");
            //string? password2Decrypt = ReadLine();
            string? password2Decrypt = "MyPass!!";
            if (password2Decrypt is null)
            {
                Debug.WriteLine("Password to decrypt cannot be null.");
                return;
            }
            try
            {
                string clearText = Protector.Decrypt(cipherText, password2Decrypt);
                Debug.WriteLine($"Decrypted text: {clearText}");
            }
            catch (CryptographicException)
            {
                Debug.WriteLine("You entered the wrong password!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Non-cryptographic exception: {ex.GetType().Name}, {ex.Message}");
            }
        }

        [Test]
        public void TestHash()
        {
            Debug.WriteLine("Registering Alice with Pa$$w0rd:");
            User alice = Protector.Register("Alice", "Pa$$w0rd");
            Debug.WriteLine($" Name: {alice.Name}");
            Debug.WriteLine($" Salt: {alice.Salt}");
            Debug.WriteLine($" Password (salted and hashed): {alice.SaltedHashedPassword}");
            Debug.WriteLine("");
            Debug.Write("Enter a new user to register: ");

            //string? username = ReadLine();
            string? username = "Tom";
            if (string.IsNullOrEmpty(username))
                username = "Bob";
            Debug.Write($"Enter a password for {username}: ");

            //string? password = ReadLine();
            string? password = "Pas!!";
            if (string.IsNullOrEmpty(password))
                password = "Pa$$w0rd";

            Debug.WriteLine("Registering a new user:");
            User newUser = Protector.Register(username, password);
            Debug.WriteLine($" Name: {newUser.Name}");
            Debug.WriteLine($" Salt: {newUser.Salt}");
            Debug.WriteLine($" Password (salted and hashed): {newUser.SaltedHashedPassword}");
            Debug.WriteLine("");
            bool correctPassword = false;

            while (!correctPassword)
            {
                Debug.Write("Enter a username to log in: ");
                //string? loginUsername = ReadLine();
                string? loginUsername = "Tom";
                if (string.IsNullOrEmpty(loginUsername))
                {
                    Debug.WriteLine("Login username cannot be empty.");
                    Debug.Write("Press Ctrl+C to end or press ENTER to retry.");
                    //ReadLine();
                    continue; // Возврат к циклу while
                }

                Debug.Write("Enter a password to log in: ");
                string? loginPassword = "Pas!!";
                if (string.IsNullOrEmpty(loginPassword))
                {
                    Debug.WriteLine("Login password cannot be empty.");
                    Debug.Write("Press Ctrl+C to end or press ENTER to retry.");
                    //ReadLine();
                    continue;
                }

                correctPassword = Protector.CheckPassword(
                    loginUsername, loginPassword);

                if (correctPassword)
                {
                    Debug.WriteLine($"Correct! {loginUsername} has been logged in.");
                }
                else
                {
                    Debug.WriteLine("Invalid username or password. Try again.");
                }
            }
        }

        [Test]
        public void TestSignature()
        {
            Debug.Write("Enter some text to sign: ");
            //string? data = ReadLine();
            string? data = "The cat sat on the mat.";
            if (string.IsNullOrEmpty(data))
            {
                Debug.WriteLine("You must enter some text.");
                return; // Завершение работы приложения
            }
            string signature = Protector.GenerateSignature(data);
            Debug.WriteLine($"Signature: {signature}");
            Debug.WriteLine("Public key used to check signature:");
            Debug.WriteLine(Protector.PublicKey);
            if (Protector.ValidateSignature(data, signature))
            {
                Debug.WriteLine("Correct! Signature is valid. Data has not been manipulated.");
            }
            else
            {
                Debug.WriteLine("Invalid signature or the data has been manipulated.");
            }
            // Имитация изменения текста: замена первого символа на X 
            // (или на Y, если он уже X)
            char newFirstChar = 'X';
            if (data[0] == newFirstChar)
            {
                newFirstChar = 'Y';
            }
            string manipulatedData = $"{newFirstChar}{data.Substring(1)}";
            if (Protector.ValidateSignature(manipulatedData, signature))
            {
                Debug.WriteLine("Correct! Signature is valid. Data has not been manipulated.");
            }
            else
            {
                Debug.WriteLine($"Invalid signature or manipulated data: {manipulatedData}");
            }
        }

        [Test]
        public void TestRandomKey()
        {
            Debug.Write("How big do you want the key (in bytes): ");
            //string? size = ReadLine();
            string? size = "256";
            if (string.IsNullOrEmpty(size))
            {
                Debug.WriteLine("You must enter a size for the key.");
                return; // Выход из приложения
            }
            byte[] key = Protector.GetRandomKeyOrIV(int.Parse(size));
            Debug.WriteLine($"Key as byte array:");
            for (int b = 0; b < key.Length; b++)
            {
                Debug.Write($"{key[b]:x2} ");
                if (((b + 1) % 16) == 0)
                    Debug.WriteLine("");
            }
            Debug.WriteLine("");

        }
    }
}
