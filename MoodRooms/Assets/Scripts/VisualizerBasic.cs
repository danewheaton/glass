using UnityEngine;
using System.Collections;

public class VisualizerBasic: MonoBehaviour
{

	protected GameObject[] visualizerObjects;
	public int numberOfObjects;

    //scale to reset to after each rescale
	float defaultScaleR;
	float defaultScaleG;
	float defaultScaleB;

    //visualizer growth amount
    float CrankItR;
    float CrankItG;
    float CrankItB;

    Player_Corridor player;

	
	void Start() 
	{
		visualizerObjects = GameObject.FindGameObjectsWithTag ("Visualizer");
		numberOfObjects = visualizerObjects.Length;

        player = FindObjectOfType<Player_Corridor>();
        defaultScaleR = player.materialToChange.color.r;
        defaultScaleG = player.materialToChange.color.g;
        defaultScaleB = player.materialToChange.color.b;
        CrankItR = Color.red.r;
        CrankItG = Color.red.g;
        CrankItB = Color.red.b;
    }
	
	// Update is called once per frame
	void Update ()
	{
        Visualize1();	
	}

    void Visualize1()
    //standard visualizer system
    {
        float[] spectrum1 = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < numberOfObjects; i++)
        {
            Color previousColor = visualizerObjects[i].GetComponent<Renderer>().material.color;
            previousColor.r = (spectrum1[i] * CrankItR) + defaultScaleR; //additive, non-continuous x growth
            previousColor.g = (spectrum1[i] * CrankItR) + defaultScaleG; //additive, non-continuous x growth
            previousColor.b = (spectrum1[i] * CrankItR) + defaultScaleB; //additive, non-continuous x growth
            visualizerObjects[i].GetComponent<Renderer>().material.color = previousColor;

            //display number of objects in visualizer set:
            //Debug.Log (visualizerObjects.Length + " Objects");
        }
    }

    //void Visualize2()
    ////constant growth system, tree-style
    //{
    //    float[] spectrum1 = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
    //    for (int i = 0; i < numberOfObjects; i++)
    //    {
    //        Vector3 previousScale = visualizerObjects[i].transform.localScale;
    //        previousScale.x += (spectrum1[i] * CrankItX); //additive, continuous x growth
    //        previousScale.y += (spectrum1[i] * CrankItY); //additive, continuous y growth
    //        previousScale.z += (spectrum1[i] * CrankItZ); //additive, continuous z growth
    //        visualizerObjects[i].transform.localScale = previousScale;

    //        //display number of objects in visualizer set:
    //        //Debug.Log (visualizerObjects.Length + " Objects");
    //    }
    //}

}


