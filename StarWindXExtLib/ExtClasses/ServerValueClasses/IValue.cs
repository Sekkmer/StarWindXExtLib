namespace StarWindXExtLib
{
    public interface IValue<T> : IAbstractValue
    {
        T Value { get; set; }
    }
}