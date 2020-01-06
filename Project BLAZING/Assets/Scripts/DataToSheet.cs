using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSheet : MonoBehaviour
{
    public bool sendingEnabled;

    CoreFinder finder;

    string gameTime;

    public CoreFinder player1Finder;
    public CoreFinder player2Finder;
    string p1Type;
    string p2Type;

    string winner;

    string url;

    // Start is called before the first frame update
    void Start()
    {
        url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeVxUDneKy6QEl0wQcsVVU9HctLzuAU72xdSdLTQZB9RlfhEw/formResponse";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveWinner(string w)
    {
        if (sendingEnabled)
        {
            winner = w;
            RetrieveData();
        }
            }
    

    void RetrieveData()
    {
        gameTime = GameFunctions.ReturnGameTime().ToString("F2");
        p1Type = player1Finder.playerMovement.type.ToString();
        p2Type = player2Finder.playerMovement.type.ToString();
        StartCoroutine("SendData");
    }

    IEnumerator SendData()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.613408419", gameTime);
        form.AddField("entry.1449784429", p1Type);
        form.AddField("entry.2022664408", p2Type);
        form.AddField("entry.115296188", winner);
        byte[] rawData = form.data;
        WWW www = new WWW(url, rawData);
        yield return www;
    }
}
