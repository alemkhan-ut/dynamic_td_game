using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public const string BEST_WAVE_TIME = "BestWaveTime";
    public const string CURRENT_WAVE_TIME = "CurrentWaveTime";

    public static float GetBestWaveTime()
    {
        if (PlayerPrefs.HasKey(BEST_WAVE_TIME))
        {
            return PlayerPrefs.GetFloat(BEST_WAVE_TIME);
        }
        else
        {
            PlayerPrefs.SetFloat(BEST_WAVE_TIME, 0.0f);
            return GetBestWaveTime();
        }
    }

    public static void SetBestWaveTime(float time)
    {
        PlayerPrefs.SetFloat(BEST_WAVE_TIME, time);
    }

    public static float GetCurrentWaveTime()
    {
        if (PlayerPrefs.HasKey(CURRENT_WAVE_TIME))
        {
            return PlayerPrefs.GetFloat(CURRENT_WAVE_TIME);
        }
        else
        {
            PlayerPrefs.SetFloat(CURRENT_WAVE_TIME, 0.0f);
            return GetCurrentWaveTime();
        }
    }

    public static void SetCurrentWaveTime(float time)
    {
        PlayerPrefs.SetFloat(CURRENT_WAVE_TIME, time);
    }
}
