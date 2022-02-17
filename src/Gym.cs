namespace PyGym {
    using static LostTech.Gradient.Interop;

    using ModuleClass = LostTech.Gradient.Modules.PyGym.ModuleClass;

    public static class Gym {
        public static PyEnvironment Make(string id, IDictionary<string, object>? parameters = null)
            => GInvoke<PyEnvironment>(ModuleClass.PythonObject, null, null, parameters, "make", id);
    }
}

namespace LostTech.Gradient.Modules.PyGym {
    using Python.Runtime;

    public class ModuleClass {
        internal static readonly PyObject PythonObject;

        static ModuleClass() {
            global::PyGym.PyGymSetup.EnsureInitialized();
            using (Py.GIL())
                PythonObject = Py.Import("gym");
        }
    }
}