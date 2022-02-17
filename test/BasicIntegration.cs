using numpy;

namespace PyGym;
public class BasicIntegration {
    [Test]
    public void WebSiteFirstSample() {
        var random = new Random();
        int Policy(ndarray observation) => random.Next(2);

        using var env = Gym.Make("CartPole-v1");
        var observation = env.Reset();
        for (int step = 0; step < 1000; step++) {
            // needs screen
            // ndarray<byte> img = env.Render(EnvRenderMode.RgbNumpyArray);
            int action = Policy(observation);
            var stepResult = env.Step(action);
            if (stepResult[2])
                observation = env.Reset();
        }
    }
}
