namespace PyGym.Codecs;

using System;

using Python.Runtime;

using RL.Spaces;

class PyBoxDecoder : SingleTypeDecoder {
    public PyBoxDecoder(PyType box) : base(box) { }

    protected override bool CanDecodeInto(Type targetType) {
        if (targetType is null) throw new ArgumentNullException(nameof(targetType));

        return
            // TODO: uncomment when all properties are supported
            //targetType == typeof(object) ||
            //targetType == typeof(ValueType) || 
            targetType.IsGenericConstructedFrom(typeof(Box<>));
    }

    public override bool TryDecode<TResult>(PyObject pyObj, out TResult value) {
        var low = pyObj.GetAttr("low");
        var high = pyObj.GetAttr("high");

        var boxType = typeof(TResult);

        var spaceType = boxType.GetGenericArguments()[0];

        value = (TResult)Activator.CreateInstance(boxType, low.AsManagedObject(spaceType), high.AsManagedObject(spaceType));

        return true;
    }
}
