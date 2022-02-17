namespace PyGym.Codecs;

using System;

using Python.Runtime;

using RL.Spaces;

class PyDiscreteDecoder : SingleTypeDecoder {
    public PyDiscreteDecoder(PyType discrete) : base(discrete) { }

    protected override bool CanDecodeInto(Type targetType) {
        if (targetType is null) throw new ArgumentNullException(nameof(targetType));

        return targetType == typeof(Discrete)
            // TODO: uncomment when all properties are supported
            //|| targetType == typeof(ValueType)
            //|| targetType == typeof(object)
            ;
    }

    public override bool TryDecode<TResult>(PyObject pyObj, out TResult value) {
        int stateCount = pyObj.GetAttr("n").As<int>();
        int @base = pyObj.GetAttr("start", _default: new PyInt(0)).As<int>();

        value = (TResult)(object)new Discrete(stateCount, @base: @base);

        return true;
    }
}
