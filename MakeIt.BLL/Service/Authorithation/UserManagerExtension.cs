using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Text;

namespace MakeIt.BLL.Service.Authorithation
{
    public static class UserManagerExtensions
    {
        public static bool IsTokenExpired<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user, string token) where TKey : IEquatable<TKey> where TUser : class, IUser<TKey>
        {
            var tokenProvider = manager.UserTokenProvider as DataProtectorTokenProvider<TUser, TKey>;
            if (tokenProvider == null) return false;

            var unprotectedData = tokenProvider.Protector.Unprotect(Convert.FromBase64String(token));
            var ms = new MemoryStream(unprotectedData);
            using (var reader = ms.CreateReader())
            {
                var creationTime = reader.ReadDateTimeOffset();
                var expirationTime = creationTime + tokenProvider.TokenLifespan;
                if (expirationTime < DateTimeOffset.UtcNow)
                {
                    return true;
                }
                return false;
            }
        }
    }

    internal static class StreamExtensions
    {
        internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

        public static BinaryReader CreateReader(this Stream stream)
        {
            return new BinaryReader(stream, DefaultEncoding, true);
        }

        public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
        {
            return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
        }
    }
}
