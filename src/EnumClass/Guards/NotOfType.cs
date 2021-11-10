using System.Runtime.CompilerServices;

namespace MayisHDev.EnumClass.Guards;

internal static class GuardAgainst
{
    internal static void NotOfType<T, T1, T2>(this T value, [CallerMemberName] string paramName = "")
    {
        if (value is not (T1 or T2))
            throw new InvalidOperationException($"{paramName} is not of type");
    }
}
