using System.Security.Cryptography;
using System.Text;

namespace Illallangi.GitHubPS.Extensions
{
    public static class StringExtensions
    {
        public static byte[] Encrypt(this string input, byte[] optionalEntropy = null, bool useMachineScope = false)
        {
            return ProtectedData.Protect(
                Encoding.ASCII.GetBytes(input),
                optionalEntropy,
                useMachineScope
                    ? DataProtectionScope.LocalMachine
                    : DataProtectionScope.CurrentUser);
        }

        public static string Decrypt(this byte[] input, byte[] optionalEntropy = null, bool useMachineScope = false)
        {
            return Encoding.ASCII.GetString(
                ProtectedData.Unprotect(
                    input,
                    optionalEntropy,
                    useMachineScope
                        ? DataProtectionScope.LocalMachine
                        : DataProtectionScope.CurrentUser));
        }
    }
}