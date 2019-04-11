namespace Kelson.Math.Vectors
{
    public interface ICreatable<T, TCreate> where TCreate : new()
    {
        TCreate CreateWith(params T[] values);
    }
}
