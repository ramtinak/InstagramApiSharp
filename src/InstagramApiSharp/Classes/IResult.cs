namespace InstagramApiSharp.Classes
{
    /// <summary>
    ///     IResult - common return type for library public methods, can contain some additional info like: Exception details,
    ///     Instagram response type etc.
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    public interface IResult<out T>
    {
        bool Succeeded { get; }
        T Value { get; }
        ResultInfo Info { get; }
    }
}