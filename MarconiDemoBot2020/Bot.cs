using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;

namespace MarconiDemoBot2020
{
    class Bot : IBot
    {
        private ITelegramBotClient client;

        public void Start()
        {
            this.client = new TelegramBotClient("");

            this.client.OnMessage += Client_OnMessage;
            this.client.OnCallbackQuery += Client_OnCallbackQuery;
            this.client.StartReceiving();
        }

        private async void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            long chatId = e.Message.Chat.Id;
            string input = e.Message.Text;

            Console.WriteLine($"Messaggio da <{chatId}>: {input}");

            if (input == "/start")
            {
                StatoPS stato = new StatoPS();
                IEnumerable<ProntoSoccorso> lista = stato.Lista();

                IReplyMarkup replyMarkup = new ReplyKeyboardMarkup(
                    lista.Select(x => new List<KeyboardButton>()
                    {
                        new KeyboardButton(x.Nome)
                    })
                );

                await this.client.SendTextMessageAsync(
                    chatId,
                    "Ciao! Per iniziare, seleziona un pronto soccorso dalla lista.",
                    replyMarkup: replyMarkup);
            }
            else
            {
                StatoPS stato = new StatoPS();
                IEnumerable<ProntoSoccorso> lista = stato.Lista();
                ProntoSoccorso ps = lista.FirstOrDefault(x => x.Nome == input);

                if (ps != null)
                {
                    string text = $"{ps.Nome}\n\n";
                    text += $"⚪️ {ps.Attesa.Bianco} in attesa\n";
                    text += $"🟢 {ps.Attesa.Verde} in attesa\n";
                    text += $"🟡 {ps.Attesa.Giallo} in attesa\n";
                    text += $"🔴 {ps.Attesa.Rosso} in attesa\n";

                    IReplyMarkup replyMarkup = new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithCallbackData(
                            "🔄 Aggiorna", ps.Codice
                        )
                    );

                    await this.client.SendTextMessageAsync(
                        chatId,
                        text,
                        replyMarkup: replyMarkup
                    );
                }
            }
        }

        private async void Client_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string codice = e.CallbackQuery.Data;

            StatoPS stato = new StatoPS();
            IEnumerable<ProntoSoccorso> lista = stato.Lista();

            ProntoSoccorso ps = lista.FirstOrDefault(x => x.Codice == codice);

            string text = $"{ps.Nome}\n\n";
            text += $"⚪ {ps.Attesa.Bianco} in attesa\n";
            text += $"🟢 {ps.Attesa.Verde} in attesa\n";
            text += $"🟡 {ps.Attesa.Giallo} in attesa\n";
            text += $"🔴 {ps.Attesa.Rosso} in attesa";

            InlineKeyboardMarkup replyMarkup = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData(
                    "🔄 Aggiorna", ps.Codice
                )
            );

            try
            {
                await this.client.EditMessageTextAsync(
                    e.CallbackQuery.Message.Chat.Id,
                    e.CallbackQuery.Message.MessageId,
                    text,
                    replyMarkup: replyMarkup
                );
            }
            catch (MessageIsNotModifiedException)
            {
            }

            await this.client.AnswerCallbackQueryAsync(
                e.CallbackQuery.Id,
                "✅ Aggiornato"
            );
        }
    }
}
