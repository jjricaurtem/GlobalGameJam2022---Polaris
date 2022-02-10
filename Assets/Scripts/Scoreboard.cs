using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private const string APIKey = "AIzaSyDGadhnpC8Ed0v58UAtsQXwmc49aylCs4I";
    private static readonly HttpClient Client = new();
    private FirebaseAuthenticateUser authenticateUser;

    private FirebaseAuthenticateUser GetAuthenticateUser()
    {
        authenticateUser ??= Authenticate();
        return authenticateUser;
    }

    public void UpdateScore(int finalScore)
    {
        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Post,
                "https://firestore.googleapis.com/v1beta1/projects/jemidevco/databases/(default)/documents/polaris/?key=" +
                APIKey);
        requestMessage.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", GetAuthenticateUser().idToken);
        requestMessage.Content = new StringContent(
            "{ \"fields\": { \"score\": { \"stringValue\": \"" + finalScore +
            "\"},\"name\": {\"stringValue\": \"Jugador 1\"}}}",
            Encoding.UTF8, "application/json");

        Client.SendAsync(requestMessage).Wait();
    }

    private static FirebaseAuthenticateUser Authenticate()
    {
        var data = new StringContent(
            "{\"email\":\"jjricaurtem@gmail.com\", \"password\":\"Mont1013599\", \"returnSecureToken\":true}",
            Encoding.UTF8, "application/json");
        var authenticateTask = Client.PostAsync(
            "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + APIKey,
            data).Result.Content.ReadAsStringAsync();
        authenticateTask.Wait();
        var jsonResponse = authenticateTask.Result;
        Debug.Log(jsonResponse);
        return JsonUtility.FromJson<FirebaseAuthenticateUser>(jsonResponse);
    }

    public List<Root> RetrieveScores()
    {
        using var requestMessage =
            new HttpRequestMessage(HttpMethod.Post,
                "https://firestore.googleapis.com/v1beta1/projects/jemidevco/databases/(default)/documents:runQuery?key=" +
                APIKey);
        requestMessage.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", GetAuthenticateUser().idToken);

        requestMessage.Content = new StringContent(
            "{" +
            "  \"structuredQuery\": {" +
            "    \"from\": [" +
            "      {" +
            "        \"collectionId\": \"polaris\"" +
            "      }" +
            "    ]," +
            "    \"orderBy\": [" +
            "      {" +
            "        \"field\": {" +
            "          \"fieldPath\": \"score\"" +
            "        }," +
            "        \"direction\": \"DESCENDING\"" +
            "      }" +
            "    ]," +
            "    \"limit\": 10" +
            "  }" +
            "}",
            Encoding.UTF8, "application/json");

        var stringTask = Client.SendAsync(requestMessage).Result.Content.ReadAsStringAsync();
        stringTask.Wait();
        var jsonResponse = stringTask.Result;
        Debug.Log(jsonResponse);
        var myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(jsonResponse);
        return myDeserializedClass;
    }
}