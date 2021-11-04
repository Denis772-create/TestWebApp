using System.Threading.Tasks;
using TestWebApp.DAL.DTO;
using TestWebApp.DAL.Models.ReviewModels;

namespace TestWebApp.ReviewServer.Signatures
{
    public interface IReviewHub
    {
        Task SendReviewAsync(MessageDto message);
        Task SendErrorAsync(string errorMessage);
        Task SendInfoAsync(
            string infoMessage = null, 
            string name = null, 
            string avatar = null);
    }
}
