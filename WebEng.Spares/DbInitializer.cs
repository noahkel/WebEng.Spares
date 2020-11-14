using System.Threading.Tasks;
using WebEng.ReplacementParts.Data;

namespace WebEng.ReplacementParts
{
    public static class DbInitializer
    {
        public static async Task Initialize()
        {
            InitAppData initializer = new InitAppData();
            await initializer.Init();
        }
    }
}