using System.Security.Cryptography;
using System.Text;

namespace SodokuAPI.Helpers
{
    public class Security : ISecurity
    {

        public Byte[] CreatePasswordSalt(string password) {
            var HMAC = new HMACSHA512();

            return HMAC.Key;
        }

        public Byte[] CreatePasswordHash(string password, byte[] salt) {
            var HMAC = new HMACSHA512(salt);

            return HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
        }


        public Boolean VerifyPassword(string password, byte[] storedHash, byte[] storedSalt) {
            if (password == null) 
                throw new ArgumentNullException("Password doesn't exist.");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Invalid Password.");
            if (storedHash.Length != 64)
                throw new ArgumentException("Hash size invalid.");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Salt size invalid.");

            var HMAC = new HMACSHA512(storedSalt);

            var computedHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; ++i) {
                if (storedHash[i] != computedHash[i]) {
                    return false;
                }
            }

            return true;
        }
    }
}
