using Soenneker.Kiota.Util.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Kiota.Util.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class KiotaUtilTests : HostedUnitTest
{
    private readonly IKiotaUtil _util;

    public KiotaUtilTests(Host host) : base(host)
    {
        _util = Resolve<IKiotaUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
