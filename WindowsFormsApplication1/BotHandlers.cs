using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;
using Message = Telegram.Bot.Types.Message;
using System.Data;
using MySql.Data.MySqlClient;

namespace myBD
{
    public class BotHandlers
    {
        public BotHandlers(ITelegramBotClient _botclient)
        {
            botClient = _botclient;
            order = new Order();
        }
        private ITelegramBotClient botClient;
        private Order order;
        public async void botClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message != null &&
                e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text &&
                !string.IsNullOrEmpty(e.Message.Text))
            {
                if (e.Message.Text.ToLower().Contains("замовлення"))
                {
                    var message = AddItemToOrder(e.Message);

                }
                else if (e.Message.Text.ToLower().Contains("iphone12"))
                {
                    order.Iphone12Count++;
                    var message = AddItemToOrder(e.Message);
                }
                else if (e.Message.Text.ToLower().Contains("airpods2"))
                {
                    order.AirpodsCount++;
                    var message = AddItemToOrder(e.Message);
                }
                else if (e.Message.Text.ToLower().Contains("watch6"))
                {
                    order.Watch6Count++;
                    var message = AddItemToOrder(e.Message);
                }
                else if (e.Message.Text.ToLower().Contains("watch7"))
                {
                    order.Watch7Count++;
                    var message = AddItemToOrder(e.Message);
                }
                else if (e.Message.Text.ToLower().Contains("статистика") || e.Message.Text.ToLower().Contains("статистику"))
                {
                    DataTable table = h.myfunDT("select * from Orders");
                    int count = table.Rows.Count;
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id,
                        text: $"Всього замовлень: {count}");
                }
                else if (e.Message.Text.ToLower().Contains("корзина") || e.Message.Text.ToLower().Contains("корзину"))
                {
                    int count = order.AirpodsCount + order.Iphone12Count + order.Watch6Count + order.Watch7Count;
                    string text = $"Товарів в корзині: {count}\n" +
                        $"airpods2 : {order.AirpodsCount} шт\n" +
                        $"iphone12 : {order.Iphone12Count} шт\n" +
                        $"watch6 : {order.Watch6Count} шт\n" +
                        $"watch7 : {order.Watch7Count} шт\n";

                    using (MySqlConnection con1 = new MySqlConnection(h.conStr))
                    {
                        

                        string sql = "INSERT INTO Orders" +
                                               "(id_orders, status, date, client, name_of_product, price)" +
                                               " VALUES (@TK1, @TK2, @TK3, @TK4, @TK5, @TK6)";
                        MySqlCommand cmd = new MySqlCommand(sql, con1);

                        Random random = new Random();
                        DateTime dateTime = new DateTime();
                        int number = random.Next(0, 100000);
                        cmd.Parameters.AddWithValue("@TK1", number.ToString());
                        cmd.Parameters.AddWithValue("@TK2", "Send");
                        cmd.Parameters.AddWithValue("@TK3", dateTime.ToString());
                        cmd.Parameters.AddWithValue("@TK4", "Client");
                        cmd.Parameters.AddWithValue("@TK5", "Airpods2");
                        cmd.Parameters.AddWithValue("@TK6", "135");

                        con1.Open();
                        cmd.ExecuteNonQuery();
                        con1.Close();
                    }

                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id,
                        text: text,
                        replyMarkup: new ReplyKeyboardRemove()
                        );
                }
                else if (e.Message.Text.ToLower() == "/keyboard")
                {
                    var message = SendReplyKeyboard(e.Message);
                }
                else if (e.Message.Text.ToLower() == "/remove")
                {
                    var mesage = RemoveKeyboard(e.Message);
                }
                else
                {
                    var message = Usage(e.Message);
                    Console.WriteLine(message.Result.Text);
                }
            }
        }

        public async Task<Message> Usage(Telegram.Bot.Types.Message message)
        {
            const string usage = "Використовуй ^_^:\n" +
                                 "/keyboard - показати клавіатуру\n" +
                                 "/remove   - приховати клавіатуру\n";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }

        public async Task<Message> SendReplyKeyboard(Message message)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                        new KeyboardButton[] { "Статистика"},
                        new KeyboardButton[] { "Добавити замовлення" },
                })
            {
                ResizeKeyboard = true
            };

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Обирай",
                                                        replyMarkup: replyKeyboardMarkup);
        }

        public async Task<Message> RemoveKeyboard(Message message)
        {
            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Клавіатуру приховано",
                                                        replyMarkup: new ReplyKeyboardRemove());
        }

        public async Task<Message> AddItemToOrder(Message message)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                        new KeyboardButton[] { "airpods2", "iphone12"},
                        new KeyboardButton[] { "watch6", "watch7" },
                        new KeyboardButton[] { "Перейти в корзину" }
                })
            {
                ResizeKeyboard = true
            };

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Обирай фрукт",
                                                        replyMarkup: replyKeyboardMarkup);
        }
    }
    class Order
    {
        public int AirpodsCount { get; set; } = 0;
        public int Iphone12Count { get; set; } = 0;
        public int Watch6Count { get; set; } = 0;
        public int Watch7Count { get; set; } = 0;
    }
}