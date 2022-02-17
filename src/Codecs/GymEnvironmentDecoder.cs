namespace PyGym.Codecs;

using Python.Runtime;

class GymEnvironmentDecoder : IPyObjectDecoder {
    readonly PyType abstractEnv;

    public GymEnvironmentDecoder(PyType abstractEnv) {
        this.abstractEnv = abstractEnv ?? throw new ArgumentNullException(nameof(abstractEnv));
    }

    public bool CanDecode(PyType objectType, Type targetType) {
        if (objectType is null) throw new ArgumentNullException(nameof(objectType));
        if (targetType is null) throw new ArgumentNullException(nameof(targetType));

        if (!objectType.IsSubclass(this.abstractEnv)) return false;

        return targetType.IsGenericConstructedFrom(typeof(NumPyEnvironment<,>))
            || targetType.IsAssignableFrom(typeof(PyEnvironment));
    }

    public bool TryDecode<T>(PyObject pyObj, out T? value) {
        if (typeof(T) == typeof(PyEnvironment) || typeof(T) == typeof(object)) {
            value = (T)(object)new PyEnvironment(pyObj);
            return true;
        }

        value = default;

        return false;
    }
}
