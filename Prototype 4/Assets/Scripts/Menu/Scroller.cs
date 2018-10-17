using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    private GameObject m_pFadecanvas;

    // Use this for initialization
    void Start () {
        m_pFadecanvas = GameObject.Find("Fade Canvas");

    }
	
	// Update is called once per frame
	void Update () {

        

    }

    public void Mainmneu()
    {
        m_pFadecanvas.GetComponent<LevelChanger>().FadeToLevel("Main");
    }
}
