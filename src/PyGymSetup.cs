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
    /// <summary>
    /// Checks if PyGym integration has been initialized, and initializes it if necessary.
    /// </summary>
    public static bool EnsureInitialized() {
        lock (initLock) {
            if (initialized)
                return false;

            NumPySetup.EnsureInitialized();

            PyType env;
            using (Py.GIL()) {
                using var gym = Py.Import("gym");
                env = new PyType(gym.GetAttr("Env"));
            }

            Interop.CustomSharpeners.Add(new GymEnvironmentDecoder(env));
            Interop.RegisterWrappersFrom(Assembly.GetExecutingAssembly());
            initialized = true;
            return true;
        }
    }

    static bool initialized;
    static readonly object initLock = new object();
}
