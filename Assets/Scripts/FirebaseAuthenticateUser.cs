using System;

[Serializable]
public class FirebaseAuthenticateUser
{
    public string kind;
    public string idToken;
    public string refreshToken;
    public string expiresIn;
    public string localId;
}