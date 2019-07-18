using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIinGame : MonoBehaviour {

    public  GMananger gm; 
    public GameObject LeftPlane;
    public GameObject RightPlane;

    private Animator leftp;
    private Animator rightp;

    public Text leftTitle;
    public Text leftDesc;

    public Text rightLevelDisplay;


    private void Start()
    {

        gm = gm.GetComponent<GMananger>();
        leftp = LeftPlane.GetComponent<Animator>();
        rightp = RightPlane.GetComponent<Animator>();


        leftTitle.text = gm.FirstScene.NameOfScene.ToString() ;

        leftDesc.text = gm.FirstScene.DescriptionOfScene.ToString();




    }


    private void Update()
    {
        if(Input.GetButton("Overview"))
        {
            leftp.SetBool("in", true);
            rightp.SetBool("in", true);


        }
        else
        {
            leftp.SetBool("in", false);
            rightp.SetBool("in", false);

        }
    }

}
