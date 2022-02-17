namespace PyGym;
public enum EnvRenderMode {
    Human,
    RgbNumpyArray,
    Ansi,
}

public static class EnvRenderModeExtensions {
    public static string Str(this EnvRenderMode mode) => mode switch {
        EnvRenderMode.Human => "human",
        EnvRenderMode.RgbNumpyArray => "rgb_array",
        EnvRenderMode.Ansi => "ansi",
        _ => throw new ArgumentOutOfRangeException(),
    };
}
