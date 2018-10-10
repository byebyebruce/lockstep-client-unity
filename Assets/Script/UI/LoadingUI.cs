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
	    foreach (var p in Game.Instance.Logic.Data.Players)
	    {
	        Progress[i].gameObject.SetActive(true);
	        Progress[i].value = 0.01f * p.Value.Progress;

	        i++;
	    }
    }

    IEnumerator ProgressCor()
    {
        yield return new WaitForSeconds(0.5f);
        var b = pb.C2S_ProgressMsg.CreateBuilder();
        b.SetPro(25);
        Game.Instance.Logic.GetMyData().Progress = 25;
        Network.Instance.Send(pb.ID.MSG_Progress,b.Build());

        yield return new WaitForSeconds(0.5f);
        b.SetPro(50);
        Game.Instance.Logic.GetMyData().Progress = 50;
        Network.Instance.Send(pb.ID.MSG_Progress, b.Build());

        yield return new WaitForSeconds(0.5f);
        b.SetPro(75);
        Game.Instance.Logic.GetMyData().Progress = 75;
        Network.Instance.Send(pb.ID.MSG_Progress, b.Build());

        yield return new WaitForSeconds(0.5f);
        b.SetPro(100);
        Game.Instance.Logic.GetMyData().Progress = 100;
        Network.Instance.Send(pb.ID.MSG_Progress, b.Build());

        yield return new WaitForSeconds(0.5f);
        Network.Instance.Send(pb.ID.MSG_Ready);
    }
}
