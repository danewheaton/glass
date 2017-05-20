using UnityEngine;
using System.Collections;

public class Visualizer_Size : MonoBehaviour
{
    public float defaultScaleX = .02f, defaultScaleY = .02f, defaultScaleZ = .02f,
        CrankItX = .05f, CrankItY = .05f, CrankItZ = .05f;
    
    void Update()
    {
        Visualize1();
    }

    void Visualize1()
    {
        float[] spectrum1 = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        Vector3 previousScale = transform.localScale;
        previousScale.x = (spectrum1[0] * CrankItX) + defaultScaleX; //additive, non-continuous x growth
        previousScale.y = (spectrum1[0] * CrankItY) + defaultScaleY; //additive, non-continuous x growth
        previousScale.z = (spectrum1[0] * CrankItZ) + defaultScaleZ; //additive, non-continuous x growth
        transform.localScale = previousScale;
    }
}
