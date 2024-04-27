using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Universorium
{
    public class TelegramBot
    {
        private readonly CancellationTokenSource cts;
        private readonly TelegramBotClient client;
        private static TelegramBot? bot;

        public static TelegramBot GetInstance()
        {
            bot ??= new TelegramBot();
            return bot;
        }

        private TelegramBot()
        {
            var token = "";
            if (System.IO.File.Exists($"./telegram.config"))
            {
                using var reader = new StreamReader($"./telegram.config");
                token = reader.ReadLine() ?? "";
            }
            cts = new CancellationTokenSource();
            client = new TelegramBotClient(token);

            client.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync), null, cts.Token);
        }

        private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var result = Task.CompletedTask;

            if (update.Message == null || update.Type != UpdateType.Message)
                return result;

            switch (update.Message.Type)
            {
                case MessageType.Text:
                    try
                    {
                        result = MessageHandler(update.Message);
                    }
                    catch
                    {
                        result = client.SendTextMessageAsync(update.Message.Chat.Id, "Произошла какая-то ошибка!",
                            cancellationToken: cancellationToken);
                    }
                    break;
                case MessageType.Document:
                    try
                    {
                        result = DocumentHandler(update.Message);
                    }
                    catch
                    {
                        result = client.SendTextMessageAsync(update.Message.Chat.Id, "Произошла какая-то ошибка!",
                            cancellationToken: cancellationToken);
                    }
                    break;
                default:
                    result = Task.CompletedTask;
                    break;
            }

            return result;
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _ = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            return Task.CompletedTask;
        }

        private Task DocumentHandler(Message msg)
        {
            if (msg.Document != null)
            {

            }
            return Task.CompletedTask;
        }

        private Task MessageHandler(Message msg)
        {
            if (msg.From == null || msg.Text == null)
                return Task.CompletedTask;

            var result = Task.CompletedTask;

            switch (msg.Text)
            {
                case "/start":
                    result = client.SendTextMessageAsync(msg.Chat.Id,
                        "Добрый день! Приложите файл с расширением gcode, содержащий электронную подпись, для отправки на печать.");
                    break;
                case "/stop":
                    result = client.SendTextMessageAsync(msg.Chat.Id,
                        "Регистрация удалена.");
                    break;

            }

            return result;
        }
    }
}
