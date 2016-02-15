using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static int JSONParseInt(float jsonstring)
    {
        return System.Convert.ToInt32(jsonstring);
    }

    public static string BetweenOfFixed(string ActualStr, string StrFirst, string StrLast)
    {
        int startIndex = ActualStr.IndexOf(StrFirst) + StrFirst.Length;
        int endIndex = ActualStr.IndexOf(StrLast, startIndex);
        return ActualStr.Substring(startIndex, endIndex - startIndex);
    }

}
