using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Homework_9._4_Telegram_Bot
{
    internal class Globals
    {
        private static string token = "";
        public static string Token { get => token; set => token = value; }

        private static TelegramBotClient client;
        public static TelegramBotClient Client { get => client; set => client = value; }

        private static List<string> Currencylist = new List<string>();
        public static List<string> Currencylist1 { get => Currencylist; set => Currencylist = value; }

        private static string filepath = $"Курсы валют на {DateTime.Now.ToShortDateString()}.json";
        public static string Filepath { get => filepath; set => filepath = value; }

        public const string menuItem1 = "Вывести курсы на экран";
        public const string menuItem2 = "Получить HTML файл курсов";
        public const string menuItem3 = "кнопка";
        public const string menuItem4 = "кнопка";

    }
}
