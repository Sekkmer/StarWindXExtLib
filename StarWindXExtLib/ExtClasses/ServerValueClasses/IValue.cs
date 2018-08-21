namespace StarWindXExtLib {

    public interface IValue<T> : IAbstactValue {
        T Value { get; set; }
    }
}