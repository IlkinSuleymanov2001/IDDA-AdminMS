
namespace Core.Repository.BaseEntities
{
    public static class DateTimeAz
    {
        private static readonly TimeZoneInfo TimeZoneAz = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");

        public static DateTime Now
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneAz); }
        }

        public static DateTime Today
        {
            get { return Now.Date; }
        }
    }
}
