using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour {

    public List<UnityEngine.UI.Slider> Progress;

	// Use this for initialization
	void Start () {
		StartCoroutine(ProgressCor());

	    foreach (var slider in Progress)
	    {
	        slider.gameObject.SetActive(false);
	    }


    }
	
	// Update is called once per frame
	void Update ()
	{
	    int i = 0;
	    foreach (var p in GameLogic.Instance.Data.Players)
	    {
	        Progress[i].gameObject.SetActive(true);
	        Progress[i].value = 0.01f * p.Value.Progress;

	        i++;
	    }
    }

    IEnumerator ProgressCor()
    {
        yield return new WaitForSeconds(0.5f);
        var msg = new message.C2S_ProgressMsg();
        msg.Pro = 25;
        Network.Instance.Send(message.C2S_Progress,msg);

        yield return new WaitForSeconds(0.5f);
        msg.Pro = 50;
        Network.Instance.Send(message.C2S_Progress, msg);

        yield return new WaitForSeconds(0.5f);
        msg.Pro = 75;
        Network.Instance.Send(message.C2S_Progress, msg);

        yield return new WaitForSeconds(0.5f);
        msg.Pro = 1;
        Network.Instance.Send(message.C2S_Progress, msg);

        yield return new WaitForSeconds(0.5f);
        Network.Instance.Send(message.C2S_Ready);
    }
}
