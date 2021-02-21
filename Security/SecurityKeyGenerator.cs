using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Security
{
    public class SecurityKeyGenerator
    {
        private static SecurityKeyGenerator instance = null;
        private static SymmetricSecurityKey key = null;

        public static SecurityKeyGenerator Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SecurityKeyGenerator();
                }
                return instance;
            }
        }

        public SymmetricSecurityKey GetKey()
        {
            return key;
        }

        private SecurityKeyGenerator()
        {
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider(); 
            tripleDES.GenerateKey();
            key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(tripleDES.Key.ToString()));
        }
    }
}