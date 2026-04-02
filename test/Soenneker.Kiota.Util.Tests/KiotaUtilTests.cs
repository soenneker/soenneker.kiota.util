using Soenneker.Kiota.Util.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Kiota.Util.Tests;

[Collection("Collection")]
public sealed class KiotaUtilTests : FixturedUnitTest
{
    private readonly IKiotaUtil _util;

    public KiotaUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IKiotaUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
