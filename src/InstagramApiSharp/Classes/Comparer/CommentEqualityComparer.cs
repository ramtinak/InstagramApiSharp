using System.Collections.Generic;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.Classes.Comparer
{
    internal class CommentEqualityComparer : IEqualityComparer<InstaComment>
    {
        public bool Equals(InstaComment comment, InstaComment anotherComment)
        {
            return comment?.Pk == anotherComment?.Pk;
        }

        public int GetHashCode(InstaComment comment)
        {
            return comment.Pk.GetHashCode();
        }
    }
}