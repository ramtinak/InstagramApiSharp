namespace InstagramApiSharp.Converters
{
    public interface IObjectConverter<out T, TT>
    {
        TT SourceObject { get; set; }
        T Convert();
    }
}