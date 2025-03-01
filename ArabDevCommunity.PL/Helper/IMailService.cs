using ArabDev.Data.Entities;

namespace ArabDevCommunity.PL.Helper
{
    public interface IMailService
    {
        public void SendEmail (Email email);
    }
}
