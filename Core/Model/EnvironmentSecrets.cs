namespace Core.Model;

// This is code that would be outside the codebase and in a secure location such as Azure or Environment Variables
public static class EnvironmentSecrets
{
    public static string SecretKey => "7f532771-de9b-4185-af30-ba8b3d4a48d5-3f1735da-92e5-4408-87e3-b65c9d39b794";
    
    public static byte[] SecretKeyByte()
    {
        return "7f532771-de9b-4185-af30-ba8b3d4a48d5-3f1735da-92e5-4408-87e3-b65c9d39b794"u8.ToArray();
    }
}