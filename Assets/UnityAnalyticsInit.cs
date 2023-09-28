using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class UnityAnalyticsInit : MonoBehaviour
{
    async void Start()
    {
	    await UnityServices.InitializeAsync();
	    AnalyticsService.Instance.StartDataCollection();
	    await Task.Delay(1000);
	    SendBuyContestEvent();
    }

    public void SendBuyContestEvent()
    {
	    Dictionary<string, object> parameters = new Dictionary<string, object>()
	    {
		    { "NickName", PlayerPrefs.GetInt("PlayerName")}
	    };
	    
	    AnalyticsService.Instance.CustomData("BuyContest", parameters);
	    AnalyticsService.Instance.Flush();
    }
}
