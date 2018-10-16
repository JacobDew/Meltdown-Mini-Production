using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    private Animator animator;
    private string m_PLevelName;

	// Use this for initialization
	void Start() {
        
        animator = gameObject.GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    public void FadeToLevel(string Levelname)
    {
        m_PLevelName = Levelname;
        animator.SetTrigger("FadeOut");

    }

    public void OnFadeCmplete()
    {
        SceneManager.LoadScene(m_PLevelName);
    }
}
