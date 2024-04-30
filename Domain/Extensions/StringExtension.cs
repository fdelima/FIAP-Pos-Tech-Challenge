namespace FIAP.Pos.Tech.Challenge.Domain.Extensions
{
    public static class StringExtension
    {
        public static bool IsBlank(this string? text)
        {
            return text != null && string.IsNullOrWhiteSpace(text) && text.Length > 0;
        }
    }
}
