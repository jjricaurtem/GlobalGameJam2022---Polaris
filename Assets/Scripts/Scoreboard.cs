using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Scoreboard : MonoBehaviour
{
    private const string ScoreUrl = "https://us-central1-jemidevco.cloudfunctions.net/score";

    public static IEnumerator UpdateScore(int finalScore, string playerName, Action onComplete)
    {
        var requestBody = " { \"score\": " + finalScore + ", \"name\": \"" + playerName + "\"}";
        var www = new UnityWebRequest(ScoreUrl, UnityWebRequest.kHttpVerbPOST);
        var utf8 = new UTF8Encoding();
        var uploadHandler = new UploadHandlerRaw(utf8.GetBytes(requestBody));
        www.uploadHandler = uploadHandler;
        www.downloadHandler = new DownloadHandlerBuffer();
        SetDefaultHeaders(www);

        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonResponse = www.downloadHandler.text;
            Debug.Log(jsonResponse);
            onComplete?.Invoke();
        }
    }

    private static void SetDefaultHeaders(UnityWebRequest www)
    {
        www.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        www.SetRequestHeader("Access-Control-Allow-Origin", "*");
        www.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        www.SetRequestHeader("Access-Control-Allow-Headers",
            "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        www.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
    }

    public static IEnumerator RetrieveScores(Action<Root> onComplete)
    {
        var www = new UnityWebRequest(ScoreUrl, UnityWebRequest.kHttpVerbGET);
        www.downloadHandler = new DownloadHandlerBuffer();
        SetDefaultHeaders(www);

        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var jsonResponse = www.downloadHandler.text;
            Debug.Log(jsonResponse);
            var myDeserializedClass = JsonUtility.FromJson<Root>("{\"answer\": " + jsonResponse + "}");
            onComplete?.Invoke(myDeserializedClass);
        }
    }
}