namespace MayisHDev.EnumClass.Tests;

public sealed class EnumClass2Tests
{
    private struct DontDoThis : EnumClass2<int, int> { }

    private struct Return1 : IFunction<int, int>
    {
        public int Apply(int value) => 1;
    }

    private struct Return2 : IFunction<int, int>
    {
        public int Apply(int value) => 2;
    }

    [Fact]
    public void Switch_ExecutesCallback1_WhenEnumClassIsCase1()
    {
        Case1<int, int> case1 = new();

        case1.Switch((int _) => 1, (int _) => 2).Should().Be(1);
    }

    [Fact]
    public void Switch_ExecutesCallback2_WhenEnumClassIsCase2()
    {
        Case2<int, int> case2 = new();

        case2.Switch((int _) => 1, (int _) => 2).Should().Be(2);
    }

    [Fact]
    public void Switch_ThrowsInvalidOperationException_WhenEnumClassIsAnotherImplementation()
    {
        DontDoThis implementation = new();

        implementation.Invoking(x => x.Switch((int _) => 1, (int _) => 2)).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void SwitchNoAlloc_ExecutesCallback1_WhenEnumClassIsCase1()
    {
        Case1<int, int> case1 = new();

        case1.Switch<Case1<int, int>, int, int, Return1, Return2, int>().Should().Be(1);
    }

    [Fact]
    public void SwitchNoAlloc_ExecutesCallback2_WhenEnumClassIsCase2()
    {
        Case2<int, int> case1 = new();

        case1.Switch<Case2<int, int>, int, int, Return1, Return2, int>().Should().Be(2);
    }

    [Fact]
    public void SwitchNoAlloc_ThrowsInvalidOperationException_WhenEnumClassIsAnotherImplementation()
    {
        DontDoThis implementation = new();

        implementation.Invoking(x =>
                x.Switch<DontDoThis, int, int, Return1, Return2, int>())
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MatchNoAlloc_ExecutesCallback1_WhenEnumClassIsCase1()
    {
        Case1<int, int> case1 = new();
        case1.Match<Case1<int, int>, int, int>().With<Return1, Return2, int>().Should().Be(1);
    }

    [Fact]
    public void MatchNoAlloc_ExecutesCallback2_WhenEnumClassIsCase2()
    {
        Case2<int, int> case2 = new();
        case2.Match<Case2<int, int>, int, int>().With<Return1, Return2, int>().Should().Be(2);
    }

    [Fact]
    public void MatchNoAlloc_ThrowsInvalidOperationException_WhenEnumClassIsAnotherImplementation()
    {
        DontDoThis implementation = new();

        implementation.Invoking(x =>
                x.Match<DontDoThis, int, int>().With<Return1, Return2, int>())
            .Should().Throw<InvalidOperationException>();
    }
}
