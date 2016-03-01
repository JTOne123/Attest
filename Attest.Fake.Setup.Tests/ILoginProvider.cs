using System.Threading.Tasks;

namespace Attest.Fake.Setup.Tests
{
    public interface ILoginProvider
    {
        bool IsLoggedIn { get; }

        Task Login();
    }
}