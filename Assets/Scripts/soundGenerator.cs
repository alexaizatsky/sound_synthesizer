using UnityEngine;
using System;

public delegate float SoundOsc();

public class soundGenerator
{
    public SoundOsc SoundOscillator;
    
    public enum Type
    {
        sin,
        square,
        saw,
        triangle,
        noise,
    }

    public Type myType;
    public UInt32 phase = 0u;
    public  float tempSample = 0;
    
    const float PHASE_MAX = 4294967296;
   
    private UInt32 freqPhase = 0u;
    private float amp = 0.5f;

    private int samplesPerWavelength;
    private float myAmpStep;
    private float myTempSample;
    private float myStep;
    System.Random rand = new System.Random();

    public soundGenerator(Type _type)
    {
        myType = _type;
        switch(_type){

            case Type.sin:
                SoundOscillator = Sin;
                break;
            case Type.square:
                SoundOscillator = Square;
                break;
            case Type.saw:
                SoundOscillator = Saw;
                break;
            case Type.triangle:
                SoundOscillator = Triang;
                break;
            case Type.noise:
                SoundOscillator = Noise;
                break;
        }
    }

    public void SetFrequency(float freq__hz, int sample_rate = 48000)
    {
        float freq__ppsmp = freq__hz / sample_rate; // periods per sample
        freqPhase = (uint)(freq__ppsmp * PHASE_MAX);
        samplesPerWavelength = Convert.ToInt32(sample_rate / (freq__hz ));
        myAmpStep = (float)( 1f / samplesPerWavelength);
        tempSample = 0;
        myTempSample = -1;
        myStep = myAmpStep;
    }
    
    public float Sin()
    {
        float ph01 = phase / PHASE_MAX;
        return Mathf.Sin(ph01 * 6.28318530717959f) ;
    }
    
    public float Noise()
    {
        float ph01 = phase / PHASE_MAX;
        float result = (float)(rand.NextDouble() * 2.0 - 1.0 )*0.5f;
        return result;
    }
    public float Saw()
    {
        myTempSample += myAmpStep;
        if (myTempSample>=1)
        {
            myTempSample = -1;
        }

        return myTempSample;
    }
    public float Square()
    {
        
        float result = Math.Sign(Sin());
        return result;
    }
    public float Triang()
    {
        if (Math.Abs( tempSample) >= 1)
        {
            myStep = -myStep;
        }
       
        tempSample += myStep;
        return tempSample;
    }
    
    public void SoundUpdate()
    {
        phase += freqPhase;
    }
}
