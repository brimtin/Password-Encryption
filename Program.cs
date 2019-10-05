using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Password_Encryption
{
    class Program
    {
        static Dictionary<string, string> dic = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            Console.WriteLine("PASSWORD AUTHENTICATION SYSTEM");
            PrintMenu();
            Menu();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\nPlease select from below options:");
            Console.WriteLine("1.Establish an account");
            Console.WriteLine("2.Authenticate a user");
            Console.WriteLine("3.Exit the system");
        }

        static void Menu()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Authenticate();
                    break;
                case "3":
                    PrintDic();
                    Environment.Exit(1);
                    break;
                default:
                    Console.Write("Not a valid choice. Please try again > ");
                    break;
            }
            Menu();
        }

        private static void PrintDic()
        {
            foreach (var item in dic)
            {
                Console.WriteLine($"Login: {item.Key}           Password: {item.Value}");
            }
        }

        private static void Authenticate()
        {
            Console.WriteLine("Enter your username:");
            string account = Console.ReadLine();
            var result = dic.Where(x => x.Key == account).FirstOrDefault();
            if (result.Key != null)
            {
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                if (result.Value == GetHashString(password))
                {
                    Console.WriteLine("Authentication was successful.");
                }
                else
                {
                    Console.WriteLine("Wrong password.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
            PrintMenu();
        }

        static void CreateAccount()
        {
            Console.WriteLine("Enter a new account:");
            string account = Console.ReadLine();
            var result = dic.Where(x => x.Key == account).FirstOrDefault();
            if (result.Key == null)
            {
                Console.WriteLine("Enter a new password:");
                string password = Console.ReadLine();
                string hash = GetHashString(password);
                Console.WriteLine(hash);
                try
                {
                    dic.Add(account, hash);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    PrintMenu();
                }
            }
            else
            {
                Console.WriteLine($"Account {account} already exist.");
                CreateAccount();
            }

        }
        static string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
