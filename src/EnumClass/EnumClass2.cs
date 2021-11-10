namespace MayisHDev.EnumClass;

public interface EnumClass2<T1, T2> { }

public record struct Case1<T, TAny>(T Value) : EnumClass2<T, TAny>;
public record struct Case2<TAny, T>(T Value) : EnumClass2<TAny, T>;

public static class EnumClass2Extensions
{
    public static TOut Switch<T1, T2, TOut>(this EnumClass2<T1, T2> enumClass2, Func<T1, TOut> case1, Func<T2, TOut> case2)
    {
        GuardAgainst.NotOfType<EnumClass2<T1, T2>, Case1<T1, T2>, Case2<T1, T2>>(enumClass2);
#pragma warning disable CS8509
        return enumClass2 switch
        {
            Case1<T1, T2>(var value) => case1(value),
            Case2<T1, T2>(var value) => case2(value),
        };
#pragma warning restore CS8509
    }

    public static TOut Switch<TEnumClass2, T1, T2, TFunc1, TFunc2, TOut>(this TEnumClass2 enumClass2)
        where TEnumClass2 : struct, EnumClass2<T1, T2>
        where TFunc1 : struct, ValueFunc2<T1, TOut>
        where TFunc2 : struct, ValueFunc2<T2, TOut>
    {
        GuardAgainst.NotOfType<TEnumClass2, Case1<T1, T2>, Case2<T1, T2>>(enumClass2);
#pragma warning disable CS8509
        return enumClass2 switch
        {
            Case1<T1, T2>(var value) => new TFunc1().Apply(value),
            Case2<T1, T2>(var value) => new TFunc2().Apply(value),
        };
#pragma warning restore CS8509
    }

    public static ValueMatcher<TEnumClass2, T1, T2> Match<TEnumClass2, T1, T2>(this TEnumClass2 enumClass2)
        where TEnumClass2 : struct, EnumClass2<T1, T2> => new(enumClass2);

    public readonly struct ValueMatcher<TEnumClass2, T1, T2>
        where TEnumClass2 : EnumClass2<T1, T2>
    {
        private readonly TEnumClass2 _value;
        public ValueMatcher(TEnumClass2 value) => _value = value;
        public TOut With<TFunc1, TFunc2, TOut>()
        where TFunc1 : struct, ValueFunc2<T1, TOut>
        where TFunc2 : struct, ValueFunc2<T2, TOut>
        {
            GuardAgainst.NotOfType<TEnumClass2, Case1<T1, T2>, Case2<T1, T2>>(_value);
#pragma warning disable CS8509
            return _value switch
            {
                Case1<T1, T2>(var value) => new TFunc1().Apply(value),
                Case2<T1, T2>(var value) => new TFunc2().Apply(value),
            };
#pragma warning restore CS8509
        }
    }
}