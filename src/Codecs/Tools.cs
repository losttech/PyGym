namespace PyGym.Codecs; 

static class Tools {
    public static bool IsGenericConstructedFrom(this Type type, Type genericTypeDefinition) {
        if (type.IsConstructedGenericType) {
            return type.GetGenericTypeDefinition() == genericTypeDefinition;
        }

        return false;
    }
}
