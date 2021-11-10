namespace MayisHDev.EnumClass.Functions;

public interface IFunction<T1,T2>
{
    T2 Apply(T1 value);
}