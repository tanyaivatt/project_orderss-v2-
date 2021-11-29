using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;

namespace myBD
{
    static class Program
    {
        private static string token { get; set; } = "2136571587:AAGv6V_j8cN5knMs__-Hdyt1JOMULtgiXLE";
        private static TelegramBotClient botClient;
        private static BotHandlers handlers;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            botClient = new TelegramBotClient(token);
            handlers = new BotHandlers(botClient);
            botClient.StartReceiving();
            botClient.OnMessage += handlers.botClient_OnMessage;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            botClient.StopReceiving();


        }

    }
}
