using Commerce.Domain;

namespace Commerce.Api
{
    public class DefaultTimeProvider: ITimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
