namespace PyGym;

using System.Reflection;

using LostTech.Gradient;
using LostTech.NumPy;

using PyGym.Codecs;

using Python.Runtime;

/// <summary>
/// Controls initialization of PyGym library
/// </summary>
public static class PyGymSetup {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // initialized in EnsureInitialized
    internal static PyType env, box, discrete;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Checks if PyGym integration has been initialized, and initializes it if necessary.
    /// </summary>
    public static bool EnsureInitialized() {
        lock (initLock) {
            if (initialized)
                return false;

            NumPySetup.EnsureInitialized();

            using (Py.GIL()) {
                using var gym = Py.Import("gym");
                using var spaces = Py.Import("gym.spaces");
                env = new PyType(gym.GetAttr("Env"));
                box = new PyType(spaces.GetAttr("Box"));
                discrete = new PyType(spaces.GetAttr("Discrete"));
            }

            Interop.CustomSharpeners.Add(new GymEnvironmentDecoder(env));
            Interop.CustomSharpeners.Add(new PyBoxDecoder(box));
            Interop.CustomSharpeners.Add(new PyDiscreteDecoder(discrete));
            Interop.RegisterWrappersFrom(Assembly.GetExecutingAssembly());
            initialized = true;
            return true;
        }
    }

    static bool initialized;
    static readonly object initLock = new object();
}
