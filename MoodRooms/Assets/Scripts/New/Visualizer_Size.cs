using UnityEngine;
using System.Collections;

public class Visualizer_Size : MonoBehaviour
{
    protected GameObject[] visualizerObjects;
    public int numberOfObjects;

    //scale to reset to after each rescale
    float defaultScaleX;
    float defaultScaleY;
    float defaultScaleZ;

    //visualizer growth amount
    float CrankItX;
    float CrankItY;
    float CrankItZ;

    Player_Corridor player;


    void Start()
    {
        visualizerObjects = GameObject.FindGameObjectsWithTag("Visualizer");
        numberOfObjects = visualizerObjects.Length;

        player = FindObjectOfType<Player_Corridor>();
        defaultScaleX = player.materialToChange.color.r;
        defaultScaleY = player.materialToChange.color.g;
        defaultScaleZ = player.materialToChange.color.b;
        CrankItX = Color.red.r;
        CrankItY = Color.red.g;
        CrankItZ = Color.red.b;
    }

    // Update is called once per frame
    void Update()
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
            previousColor.r = (spectrum1[i] * CrankItX) + defaultScaleX; //additive, non-continuous x growth
            previousColor.g = (spectrum1[i] * CrankItY) + defaultScaleY; //additive, non-continuous x growth
            previousColor.b = (spectrum1[i] * CrankItZ) + defaultScaleZ; //additive, non-continuous x growth
            visualizerObjects[i].GetComponent<Renderer>().material.color = previousColor;

            //display number of objects in visualizer set:
            //Debug.Log (visualizerObjects.Length + " Objects");
        }
    }
}
