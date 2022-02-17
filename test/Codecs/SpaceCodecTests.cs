using numpy;

using Python.Runtime;

using RL.Spaces;

namespace PyGym.Codecs;

public class SpaceCodecTests {
    public SpaceCodecTests() {
        PyGymSetup.EnsureInitialized();
    }

    [Test]
    public void Box() {
        using var _ = Py.GIL();

        var pyBox = PyGymSetup.box.Invoke(new PyFloat(-5), new PyFloat(+5), new PyTuple());

        var box = pyBox.As<Box<ndarray<float>>>();

        Assert.AreEqual(-5, box.Low.AsScalar());
        Assert.AreEqual(+5, box.High.AsScalar());
    }

    [Test]
    public void Discrete() {
        using var _ = Py.GIL();

        var pyDiscrete = PyGymSetup.discrete.Invoke(new PyInt(10));

        var discrete = pyDiscrete.As<Discrete>();

        Assert.AreEqual(0, discrete.Base);
        Assert.AreEqual(10, discrete.StateCount);
    }
}
