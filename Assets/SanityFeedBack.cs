using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityFeedBack : MonoBehaviour
{
    public VolumeProfile sanityVol;
    DepthOfField Dof;
    FilmGrain FG;
    LensDistortion LD;

    private void Start()
    {
        DepthOfField tmp;
        FilmGrain tmpFG;
        LensDistortion tmpLD;

        if (sanityVol.TryGet(out tmp)) Dof = tmp;
        if (sanityVol.TryGet(out tmpFG)) FG = tmpFG;
        if (sanityVol.TryGet(out tmpLD)) LD = tmpLD;
    }

    private void Update()
    {
        Dof.focalLength.value = Player.INSANITY * 3;
        FG.intensity.value = Player.INSANITY / 100;
        LD.intensity.value = Player.INSANITY / -100;
    }
}
