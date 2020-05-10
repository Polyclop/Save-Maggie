using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.PostProcessing;

public class FutureGlassesEffect : MonoBehaviour
{
    PostProcessVolume volume;
    PostProcessProfile profile;

    ChromaticAberration chromaLayer;
    Bloom bloomLayer;
    DepthOfField dofLayer;
    ColorGrading colorLayer;

    public Animator maggieAnimator;

    public float refTime;
    float startTime, currentTime, deltaTime;

    public bool inFuture = false;
    public bool inFutureColor = false;
    public bool deloreaning = false;

    public float maxFocalLength = 60f;
    public float maxBloomIntensity = 5f;
    public float maxChromaticAberration = 0.7f;
    public float maxColorTemperature = 80f;
    public float maxColorTint = 80f;
    //Vector4 maxLift = new Vector4(0.12f, 0f, -0.20f, 0f);
    //Vector4 maxGamma = new Vector4(1f, 1f, 1.25f, 0f);
    //Vector4 maxGain = new Vector4(1.1f, 1f, 0.8f, 0f);
    float percentToAdd;
    bool goChangeTime = false;


    public GameObject present, future;
    SpriteRenderer[] presentRend, futureRend;
    public Tilemap[] presentTilemap, futureTilemap;
    float maxAlpha = 1f;
    float objectAlpha = 0;

    public AudioSource[] presentAmbientSound, futureAmbientSound;
    public float[] presentMaxVolume, futureMaxVolume;

    AudioSource futureSoundEffect;
    public AudioClip presentToFuture, futureToPresent;

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        chromaLayer = volume.profile.GetSetting<ChromaticAberration>();
        bloomLayer = volume.profile.GetSetting<Bloom>();
        dofLayer = volume.profile.GetSetting<DepthOfField>();
        colorLayer = volume.profile.GetSetting<ColorGrading>();

        presentRend = present.GetComponentsInChildren<SpriteRenderer>();
        futureRend = future.GetComponentsInChildren<SpriteRenderer>();

        inFuture = (PlayerPrefs.GetInt("inFuture") == 1) ? true : false;
        inFutureColor = inFuture;
        colorLayer.temperature.value = inFuture ? maxColorTemperature : 0;
        colorLayer.tint.value = inFuture ? maxColorTint : 0;
        maggieAnimator.SetBool("hasGlasses", inFuture);
        if (inFuture)
        {
            for (int i = 0; i < presentRend.Length; i++)
            {
                presentRend[i].color = new Color(1, 1, 1, 1 - maxAlpha);
            }
            for (int i = 0; i < futureRend.Length; i++)
            {
                futureRend[i].color = new Color(1, 1, 1, maxAlpha);
            }

            for (int i = 0; i < presentTilemap.Length; i++)
            {
                presentTilemap[i].color = new Color(1, 1, 1, 1 - maxAlpha);
            }
            for (int i = 0; i < futureTilemap.Length; i++)
            {
                futureTilemap[i].color = new Color(1, 1, 1, maxAlpha);
            }

            for (int i = 0; i < presentAmbientSound.Length; i++)
            {
                presentAmbientSound[i].volume = 0;
            }
            for (int i = 0; i < futureAmbientSound.Length; i++)
            {
                futureAmbientSound[i].volume = futureMaxVolume[i];
            }
            objectAlpha = maxAlpha;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        futureSoundEffect = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !deloreaning)
        {
            deloreaning = true;
            startTime = Time.time;
            futureSoundEffect.clip = inFuture ? futureToPresent : presentToFuture;
            futureSoundEffect.Play();
        }
        if (deloreaning)
        {
            // Effet du futur

            currentTime = Time.time;
            deltaTime = currentTime - startTime;

            percentToAdd = Time.deltaTime / refTime * 2;

            if (inFutureColor)
            {
                colorLayer.temperature.value -= (maxColorTemperature * percentToAdd / 2);
                colorLayer.tint.value -= (maxColorTint * percentToAdd / 2);

                objectAlpha -= (maxAlpha * percentToAdd / 2);

                for (int i = 0; i < presentRend.Length; i++)
                {
                    presentRend[i].color = new Color(1, 1, 1, 1 - objectAlpha);
                }
                for (int i = 0; i < futureRend.Length; i++)
                {
                    futureRend[i].color = new Color(1, 1, 1, objectAlpha);
                }
                for (int i = 0; i < presentTilemap.Length; i++)
                {
                    presentTilemap[i].color = new Color(1, 1, 1, 1 - objectAlpha);
                }
                for (int i = 0; i < futureTilemap.Length; i++)
                {
                    futureTilemap[i].color = new Color(1, 1, 1, objectAlpha);
                }



                /*
                colorLayer.lift.value = new Vector4((colorLayer.lift.value.w + (maxLift.w * percentToAdd)),
                    (colorLayer.lift.value.x + (maxLift.x * percentToAdd)),
                    (colorLayer.lift.value.y + (maxLift.y * percentToAdd)),
                    (colorLayer.lift.value.z + (maxLift.z * percentToAdd)));
                colorLayer.gamma.value = new Vector4((colorLayer.gamma.value.w + (maxGamma.w * percentToAdd)),
                    (colorLayer.gamma.value.x + (maxGamma.x * percentToAdd)),
                    (colorLayer.gamma.value.y + (maxGamma.y * percentToAdd)),
                    (colorLayer.gamma.value.z + (maxGamma.z * percentToAdd)));
                colorLayer.gain.value = new Vector4((colorLayer.gain.value.w + (maxGain.w * percentToAdd)),
                    (colorLayer.lift.value.x + (maxGain.x * percentToAdd)),
                    (colorLayer.lift.value.y + (maxGain.y * percentToAdd)),
                    (colorLayer.lift.value.z + (maxGain.z * percentToAdd)));
                    */

                for (int i = 0; i < presentAmbientSound.Length; i++)
                {
                    presentAmbientSound[i].volume += (presentMaxVolume[i] * percentToAdd / 2);
                }
                for (int i = 0; i < futureAmbientSound.Length; i++)
                {
                    futureAmbientSound[i].volume -= (futureMaxVolume[i] * percentToAdd / 2);
                }

            }
        
            else
            {
                colorLayer.temperature.value += (maxColorTemperature * percentToAdd /2);
                colorLayer.tint.value += (maxColorTint * percentToAdd / 2);

                objectAlpha += (maxAlpha * percentToAdd / 2);

                for (int i = 0; i < presentRend.Length; i++)
                {
                    presentRend[i].color = new Color(1, 1, 1, 1 - objectAlpha);
                }
                for (int i = 0; i < futureRend.Length; i++)
                {
                    futureRend[i].color = new Color(1, 1, 1, objectAlpha);
                }
                for (int i = 0; i < presentTilemap.Length; i++)
                {
                    presentTilemap[i].color = new Color(1, 1, 1, 1 - objectAlpha);
                }
                for (int i = 0; i < futureTilemap.Length; i++)
                {
                    futureTilemap[i].color = new Color(1, 1, 1, objectAlpha);
                }

                /*
                colorLayer.lift.value = new Vector4((colorLayer.lift.value.w - (maxLift.w * percentToAdd)),
                    (colorLayer.lift.value.x - (maxLift.x * percentToAdd)),
                    (colorLayer.lift.value.y - (maxLift.y * percentToAdd)),
                    (colorLayer.lift.value.z - (maxLift.z * percentToAdd)));
                colorLayer.gamma.value = new Vector4((colorLayer.gamma.value.w - (maxGamma.w * percentToAdd)),
                    (colorLayer.gamma.value.x - (maxGamma.x * percentToAdd)),
                    (colorLayer.gamma.value.y - (maxGamma.y * percentToAdd)),
                    (colorLayer.gamma.value.z - (maxGamma.z * percentToAdd)));
                colorLayer.gain.value = new Vector4((colorLayer.gain.value.w - (maxGain.w * percentToAdd)),
                    (colorLayer.lift.value.x - (maxGain.x * percentToAdd)),
                    (colorLayer.lift.value.y - (maxGain.y * percentToAdd)),
                    (colorLayer.lift.value.z - (maxGain.z * percentToAdd)));
                */

                for (int i = 0; i < presentAmbientSound.Length; i++)
                {
                    presentAmbientSound[i].volume -= (presentMaxVolume[i] * percentToAdd / 2);
                }
                for (int i = 0; i < futureAmbientSound.Length; i++)
                {
                    futureAmbientSound[i].volume += (futureMaxVolume[i] * percentToAdd / 2);
                }

            }

            if (deltaTime <= refTime/2)
            {
                bloomLayer.intensity.value += (maxBloomIntensity * percentToAdd);
                chromaLayer.intensity.value += (maxChromaticAberration * percentToAdd);
                dofLayer.focalLength.value += (maxFocalLength * percentToAdd);

            }
            else if(deltaTime <= refTime)
            {
                if(!goChangeTime)
                {
                    goChangeTime = true;
                    inFuture = !inFuture;
                    PlayerPrefs.SetInt("inFuture", (inFuture ? 1 : 0));
                }
                bloomLayer.intensity.value -= (maxBloomIntensity * percentToAdd);
                chromaLayer.intensity.value -= (maxChromaticAberration * percentToAdd);
                dofLayer.focalLength.value -= (maxFocalLength * percentToAdd);
            }
            else
            {
                goChangeTime = false;

                bloomLayer.intensity.value = 0;
                chromaLayer.intensity.value = 0;
                dofLayer.focalLength.value = 0;
                deloreaning = false;

                inFutureColor = inFuture;


                colorLayer.temperature.value = inFuture? maxColorTemperature : 0;
                colorLayer.tint.value = inFuture ? maxColorTint : 0;
                /*
                colorLayer.lift.value = new Vector4(0, 0, 0, 0);
                colorLayer.gamma.value = new Vector4(1, 1, 1, 0);
                colorLayer.gain.value = new Vector4(1, 1, 1, 0);
                */
            }
        }

        maggieAnimator.SetBool("hasGlasses", inFuture);

    }
}
