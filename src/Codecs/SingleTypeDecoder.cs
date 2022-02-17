namespace PyGym.Codecs;

using Python.Runtime;

abstract class SingleTypeDecoder: IPyObjectDecoder {
    readonly PyType pyType;

    public SingleTypeDecoder(PyType pyType) {
        this.pyType = pyType ?? throw new ArgumentNullException(nameof(pyType));
    }

    public bool CanDecode(PyType objectType, Type targetType) {
        if (objectType is null) throw new ArgumentNullException(nameof(objectType));
        if (targetType is null) throw new ArgumentNullException(nameof(targetType));

        if (!this.CanDecodeInto(targetType)) return false;

        
        if (!PythonReferenceComparer.Instance.Equals(objectType, this.pyType)) return false;

        return true;
    }

    protected abstract bool CanDecodeInto(Type targetType);

    public abstract bool TryDecode<TResult>(PyObject pyObj, out TResult value);
}
