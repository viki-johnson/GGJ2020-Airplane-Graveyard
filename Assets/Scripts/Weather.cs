using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weather : MonoBehaviour
{
    public windDirection currentWindDirection = windDirection.North;
    public Radio radioScript;

    [SerializeField]
    private int weatherSwitchSeconds = 120;
    [SerializeField]
    private List<string> regions = new List<string>();
    [SerializeField]
    private float seaLevel = 0f;
    [SerializeField]
    private float windSpeed = 0f;
    [SerializeField]
    private float windDegrees = 0f;
    private const string API_KEY = "c38e88af26cd8c3a7352d889e2808ce6";
    private string url = "https://api.openweathermap.org/data/2.5/weather?q=";
    private int index = 0;
    private bool isWeatherRotating = true;

    private windDirection nextWindDirection = windDirection.North;
    private float nextWindSpeed = 0f;
    private float nextWindDegrees = 0f;


    public enum windDirection
    {
        North,
        East,
        South,
        West,
        NorthEast,
        SouthEast,
        SouthWest,
        NorthWest
    }

    void Start()
    {
        //FetchWeatherData();
        InvokeRepeating("FetchWeatherData", 180, weatherSwitchSeconds);
    }

    void Update()
    {
        
    }

    public void FetchWeatherData()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        while (isWeatherRotating)
        {
            string nextRegion = GetNextRegion();
            UnityWebRequest www = UnityWebRequest.Get(url + nextRegion + "&APPID=" + API_KEY);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                
            }
            else
            {
                JSONObject result = new JSONObject(www.downloadHandler.text);

                if (result.HasField("main") )
                {
                    JSONObject main = result["main"];
                    if (main.HasField("sea_level"))
                    {
                        SetSeaLevel(result["main"]["sea_level"].n);

                    }
                }
                if (result.HasField("wind"))
                {
                    JSONObject wind = result["wind"];
                    if (wind.HasField("speed"))
                    {
                        //SetNextWindSpeed(result["wind"]["speed"].n);
                        SetNextWindSpeed(3f);
                    }
                    if (wind.HasField("deg"))
                    {
                        SetNextWindDegrees(result["wind"]["deg"].n);
                    }
                }
            }
            Debug.Log("Fetched weather data, now play radio message");
            CalculateNextWindDirection();
            //yield return new WaitForSeconds(weatherSwitchSeconds);
            radioScript.PlayRadioMessage(nextWindDirection);
            yield break;
        }
    }

    private string GetNextRegion()
    {
        //if (index > regions.Count - 1)
        //{
        //    index = 0;
        //}
        //string nextRegion = regions[index];
        //index += 1;
        string nextRegion = regions[Random.Range(0, regions.Count)];
        return nextRegion;
    }

    private void SetSeaLevel(float seaLevel)
    {
        this.seaLevel = seaLevel;
    }

    public float GetSeaLevel()
    {
        return seaLevel;
    }

    private void SetWindSpeed(float windSpeed)
    {
        this.windSpeed = windSpeed;
    }

    public float GetWindSpeed()
    {
        return windSpeed;
    }

    private void SetWindDegrees(float windDegrees)
    {
        this.windDegrees = windDegrees;
    }

    public float GetWindDegrees()
    {
        return windDegrees;
    }

    private void CalculateNextWindDirection()
    {
        float nextWindDegrees = GetNextWindDegrees();
        if (nextWindDegrees >= 337.5f && nextWindDegrees < 22.5f)
        {
            nextWindDirection = windDirection.North;
            this.nextWindDegrees = 0f;
        }
        else if (nextWindDegrees >= 22.5f && nextWindDegrees < 67.5f)
        {
            nextWindDirection = windDirection.NorthEast;
            this.nextWindDegrees = 45f;
        }
        else if (nextWindDegrees >= 67.5f && nextWindDegrees < 112.5f)
        {
            nextWindDirection = windDirection.East;
            this.nextWindDegrees = 90f;
        }
        else if (nextWindDegrees >= 112.5f && nextWindDegrees < 157.5f)
        {
            nextWindDirection = windDirection.SouthEast;
            this.nextWindDegrees = 135f;
        }
        else if (nextWindDegrees >= 157.5f && nextWindDegrees < 202.5f)
        {
            nextWindDirection = windDirection.South;
            this.nextWindDegrees = 180f;
        }
        else if (nextWindDegrees >= 202.5f && nextWindDegrees < 247.5f)
        {
            nextWindDirection = windDirection.SouthWest;
            this.nextWindDegrees = 225f;
        }
        else if (nextWindDegrees >= 247.5f && nextWindDegrees < 292.5f)
        {
            nextWindDirection = windDirection.West;
            this.nextWindDegrees = 270f;
        }
        else if (nextWindDegrees >= 292.5f && nextWindDegrees < 337.5f)
        {
            nextWindDirection = windDirection.NorthWest;
            this.nextWindDegrees = 315f;
        }

        //SetWindDegrees(windDegrees);
    }

    public windDirection getWindDirection()
    {
        return currentWindDirection;
    }

    private void SetNextWindSpeed(float nextWindSpeed)
    {
        this.nextWindSpeed = nextWindSpeed;
    }

    public float GetNextWindDegrees()
    {
        return nextWindDegrees;
    }

    private void SetNextWindDegrees(float nextWindDegrees)
    {
        this.nextWindDegrees = nextWindDegrees;
    }

    public void SetNextWeatherToCurrent()
    {
        windSpeed = nextWindSpeed;
        windDegrees = nextWindDegrees;
        currentWindDirection = nextWindDirection;
    }
}
