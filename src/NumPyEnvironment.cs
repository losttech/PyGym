using numpy;

using RL;
using RL.Spaces;

namespace PyGym;
class NumPyEnvironment<TAction, TObservation> : IEnvironment<ndarray<TObservation>, ndarray<TAction>, float, Box<ndarray<TObservation>>, Box<ndarray<TAction>>> {
    public Box<ndarray<TAction>> ActionSpace => throw new System.NotImplementedException();

    public Box<ndarray<TObservation>> ObservationSpace => throw new System.NotImplementedException();

    public ndarray<TObservation> Reset() {
        throw new System.NotImplementedException();
    }

    public StepResult<ndarray<TObservation>, float> Step(ndarray<TAction> action) {
        throw new System.NotImplementedException();
    }
}
