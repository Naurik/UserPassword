using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace UserPass
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            string random = rand.Next(1001, 9999).ToString();

            const string accountSid = "AC773cca6940429e6402d846c1d1a09393";
            const string authToken = "df57b227dccdd47077570a1a7b436e22";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create
                (
               body: random,
                from: new Twilio.Types.PhoneNumber("+16194923738"),
                to: new Twilio.Types.PhoneNumber("+77072003966")
                );


            Console.WriteLine("Введите логин пользователя: ");
            string login = Console.ReadLine();


            Console.WriteLine("Введите пароль пользователя: ");
            #region Password
            bool repeat = true;
            string password = "";
            Regex reg = new Regex(@"(?=^.{6,32}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (repeat)
            {
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        Console.Write("*");
                        password += info.KeyChar;
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(password))
                        {
                            password = password.Substring(0, password.Length - 1);
                            int pos = Console.CursorLeft;
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    info = Console.ReadKey(true);
                }
                if (reg.IsMatch(password))
                {
                    repeat = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Введите пароль заново!");
                    password = "";
                    info = Console.ReadKey(true);
                }
            }
            #endregion


            Console.WriteLine();
            Console.WriteLine("Введите павторный пароль пользователя: ");
            #region RepeatPassword
            bool repeat1 = true;
            string repeatPassword = "";
            ConsoleKeyInfo info1 = Console.ReadKey(true);
            while (repeat1)
            {
                while (info1.Key != ConsoleKey.Enter)
                {
                    if (info1.Key != ConsoleKey.Backspace)
                    {
                        Console.Write("*");
                        repeatPassword += info1.KeyChar;
                    }
                    else if (info1.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(repeatPassword))
                        {
                            repeatPassword = repeatPassword.Substring(0, repeatPassword.Length - 1);
                            int pos = Console.CursorLeft;
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    info1 = Console.ReadKey(true);
                }
                if (string.Equals(repeatPassword, password))
                {
                    repeat1 = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Пароли не совпадают! Введите снова!");
                    repeatPassword = "";
                    info1 = Console.ReadKey(true);
                }
            }
            #endregion


            Console.WriteLine();
            Console.WriteLine("Введите email пользователя: ");
            #region Email
            string email = Console.ReadLine();
            bool repeat2 = true;
            Regex trueEmail = new Regex(@"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$");
            while (repeat2)
            {
                if (trueEmail.IsMatch(email))
                {
                    repeat2 = false;
                }
                else
                {
                    Console.WriteLine("Введите email заново!");
                    email = Console.ReadLine();
                }
            }
            #endregion


            Console.WriteLine("Введите номер пользователя: ");
            #region PhoneNumber
            string phoneNumber = Console.ReadLine();
            Regex truePhoneNumber = new Regex(@"^\+?[7]\d{10}$");
            bool repeat3 = true;
            while (repeat3)
            {
                if (truePhoneNumber.IsMatch(phoneNumber))
                {
                    repeat3 = false;
                }
                else
                {
                    Console.WriteLine("Введите номер заново!");
                    phoneNumber = Console.ReadLine();
                }
                   
            }
            #endregion

            #region Sms
            Console.WriteLine("Введите смс: ");
            string code = Console.ReadLine();
            bool repeat4 = true;
            while (repeat4)
            {
                if (string.Equals(random, code))
                {
                    repeat4 = false;
                }
                else
                {
                    Console.WriteLine("Неправильный код. Введите заново");
                    code = Console.ReadLine();
                }
                    
            }
            #endregion



            User firstUser = new User
            {
                Login = login,
                Password = password,
                PasswordRepeat = repeatPassword,
                Email = email,
                PhoneNumber = phoneNumber,
                Sms = random
            };
            Console.WriteLine("Поздравляем вы зарегестрировались успешно!!!");
            Console.WriteLine("Логин: " + firstUser.Login);
            Console.WriteLine("Пароль: " + firstUser.Password);
            Console.WriteLine("Номер: " + firstUser.PhoneNumber);
            Console.WriteLine("Email: " + firstUser.Email);
            Console.ReadLine();

        }
    }
}
