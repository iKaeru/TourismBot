using TourismBot.Repositories;

namespace TourismBot.Models
{
    public static class TelegramSettings
    {
        public static string BotToken { get; } = SettingsRepository.GetSetting();
    }
}