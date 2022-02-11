using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using HtmlAgilityPack;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Aspose.Cells;

namespace Homework_9._4_Telegram_Bot
{
    internal class Methods
    {
        public static List<string> GetExchangeRates()
        {
            const string StartPageLink = @"https://www.cbr.ru/currency_base/daily/";
            var res = new List<string>();
            var htmlDoc = new HtmlWeb().Load(StartPageLink);
            var rows = htmlDoc.DocumentNode.SelectNodes("//table[@class='data']//tr");

            foreach (var cell in rows) 
            { 
                res.Add(cell.InnerText);
            }
            return res;
        }

        public static void PrintList(List<string> list)
        {
            foreach (var e in list)
            {
                Console.WriteLine(e);
            }
        }

        public static void WriteListToFile(List<string> list, string filepath)
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(filepath, json);
            Console.WriteLine($"Файл {filepath} создан");
        }

        public static void GetHtmlFromJsonFile(string filepath)
        {
            Workbook wb = new Workbook(filepath);
            Worksheet ws = wb.Worksheets[0];

            Console.WriteLine(ws);
        }

        [Obsolete]
        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Пришло сообщение с текстом: {msg.Text} от {msg.Chat.FirstName} {msg.Chat.LastName}");
                //await Globals.Client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyToMessageId: msg.MessageId);
                /*
                var stic = await Globals.Client.SendStickerAsync(chatId: msg.Chat.Id, 
                    sticker: "https://tlgrm.ru/_/stickers/dc7/a36/dc7a3659-1457-4506-9294-0d28f529bb0a/192/17.webp",
                    replyToMessageId: msg.MessageId);
                */
                /*
                if (msg.Text == "/start")
                {
                    await Globals.Client.SendTextMessageAsync(msg.Chat.Id, "Добро пожаловать на канал");
                }
                */
                //await Globals.Client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());

                Globals.Currencylist1 = GetExchangeRates();
                

                switch (msg.Text)
                {
                    case "/start": 
                        await Globals.Client.SendTextMessageAsync(msg.Chat.Id, "Добро пожаловать на канал о курсах валют", replyMarkup: GetButtons());
                        break;

                    case Globals.menuItem1:
                        await Globals.Client.SendTextMessageAsync(msg.Chat.Id, Globals.menuItem1, replyMarkup: GetButtons());

                        foreach (var item in Globals.Currencylist1)
                        {
                            await Globals.Client.SendTextMessageAsync(msg.Chat.Id, item, replyMarkup: GetButtons());
                            Thread.Sleep(10);
                        }

                        break;

                    case Globals.menuItem2:
                        WriteListToFile(Globals.Currencylist1, Globals.Filepath);

                        using (var sendFileStream = File.Open(Globals.Filepath, FileMode.Open))
                        {
                            await Globals.Client.SendDocumentAsync(msg.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(sendFileStream, Globals.Filepath));
                        }

                        await Globals.Client.SendTextMessageAsync(msg.Chat.Id, $"Файл {Globals.Filepath} загружен", replyMarkup: GetButtons());

                        GetHtmlFromJsonFile(Globals.Filepath);

                        break;

                    default:
                        await Globals.Client.SendTextMessageAsync(msg.Chat.Id, "Выберите действие", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = Globals.menuItem1 }, new KeyboardButton { Text = Globals.menuItem2 } },
                    new List<KeyboardButton>{new KeyboardButton { Text = Globals.menuItem3 }, new KeyboardButton { Text = Globals.menuItem4 } }
                }
            };
        }
    }
}
