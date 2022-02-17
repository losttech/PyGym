namespace PyGym;

using LostTech.Gradient;
using static LostTech.Gradient.Interop;

using Python.Runtime;

using RL;

[PublicAPI]
[PythonFullName("gym.Env")]
public class PyEnvironment : PythonObjectContainer,
    IEnvironment<object, object, object, DynamicSpace, DynamicSpace>,
    IPseudorandom<object>,
    IDisposable {

    public virtual dynamic ActionSpace => GAttr<dynamic>(this.pyObject, "action_space");
    public virtual dynamic ObservationSpace => GAttr<dynamic>(this.pyObject, "observation_space");

    public virtual dynamic Step(dynamic action) => GInvoke<dynamic>(this.pyObject, null, null, null, "step", (object)action);
    public virtual dynamic Reset() => GInvoke<dynamic>(this.pyObject, null, null, null, "reset");
    public virtual dynamic Render(string mode = "human") => GInvoke<dynamic>(this.pyObject, null, null, null, "render", mode);
    public dynamic Render(EnvRenderMode mode) => this.Render(mode.Str());
    public virtual dynamic Seed(dynamic seed) => GInvoke<dynamic>(this.pyObject, null, null, null, "seed", seed);

    public virtual dynamic Unwrapped => GAttr<dynamic>(this.pyObject, "unwrapped");

    DynamicSpace IEnvironment<object, object, object, DynamicSpace, DynamicSpace>.ActionSpace => GAttr<DynamicSpace>(this.pyObject, "action_space");

    DynamicSpace IEnvironment<object, object, object, DynamicSpace, DynamicSpace>.ObservationSpace => GAttr<DynamicSpace>(this.pyObject, "observation_space");

    object IEnvironment<object, object, object, DynamicSpace, DynamicSpace>.Reset() => this.Reset();

    StepResult<object, object> IEnvironment<object, object, object, DynamicSpace, DynamicSpace>.Step(object action) {
        throw new System.NotImplementedException();
    }

    bool IPseudorandom<object>.IsSystemIndependent => true;

    #region IDisposable
    protected virtual void Dispose(bool disposing) {
        if (disposing) {
            GInvoke(this.pyObject, null, null, null, "close");
        }
    }

    public void Dispose() {
        this.Dispose(true);

        GC.SuppressFinalize(this);
    }

    void IPseudorandom<object>.Seed(object seed) {
        throw new NotImplementedException();
    }

    ~PyEnvironment() {
        this.Dispose(false);
    }
    #endregion

    internal PyEnvironment(PyObject pythonObject) : base(pythonObject) { }
}
