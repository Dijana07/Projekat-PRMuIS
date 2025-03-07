using System.Security.Cryptography;
using System.Text;

namespace Client.Pomocne_metode
{
    public class Hesiranje
    {
        public Hesiranje() { }

        public string Hesiraj(string naziv)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hesirano = sha256.ComputeHash(Encoding.UTF8.GetBytes(naziv));
                return BitConverter.ToString(hesirano).Replace("-", "").ToLower();
            }
        }
    }
}
