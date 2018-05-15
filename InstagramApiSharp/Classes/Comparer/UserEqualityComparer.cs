using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.Classes.Comparer
{
    internal class UserEqualityComparer : IEqualityComparer<InstaUserShort>
    {
        public bool Equals(InstaUserShort user, InstaUserShort anotherUser)
        {
            return user?.Pk == anotherUser?.Pk;
        }

        public int GetHashCode(InstaUserShort user)
        {
            return user.Pk.GetHashCode();
        }
    }
}