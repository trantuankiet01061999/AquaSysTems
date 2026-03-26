namespace AquaSolution.Client.Common.ConvertNumber
{
    public static class ConvertNumberCommon
    {
        public static decimal ConvertNumber(decimal number)
        {
            return Math.Round(number, 3, MidpointRounding.AwayFromZero);
        }
    }
}
