using System;
using Telegram.Bot;

namespace Homework_9._4_Telegram_Bot
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            Console.WriteLine("Введите токен");
            Globals.Token = Console.ReadLine();

            Globals.Client = new TelegramBotClient(Globals.Token);

            Globals.Client.StartReceiving();

            Globals.Client.OnMessage += Methods.OnMessageHandler;

            Console.ReadLine();
            Globals.Client.StopReceiving();
        }
    }
}
