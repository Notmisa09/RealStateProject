using System.Text;

namespace RealStateApp.Core.Application.Helpers
{
    public static class CodeGenerator
    {
        public static string GenerateCode(string Code)
        {
            StringBuilder stringbuild = new();
            Random rdn = new Random();
            for (int i = 0; i <= 5; i++)
            {
               string randomnumber = rdn.Next(1,10).ToString();
               stringbuild.Append(randomnumber);
            }

            Code = stringbuild.ToString();
            return Code;
        }
    }
}
