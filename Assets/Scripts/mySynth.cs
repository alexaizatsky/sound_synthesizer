using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class mySynth : MonoBehaviour
{
    private bool isPlaying;
    
    private  soundGenerator osc1, osc2, noiseOsc;
    private MoogFilter moogFilterL, moogFilterR;
    private float[] notes;
    private int timeIndex = 0;
    
    private float osc1Amp = 1;
    private float osc2Amp = 1;
    private float noiseAmp = 1;
    
    private float mainAmp = 1;
    private float releaseAmp = 1;
    private float stopAmp;
    
    private float attackParam =.01f;
    private float holdParam = 0.3f;
    private float releaseParam = 1f;
    private float sustaneParam = 0.0001f;
    
    private float myFmFreq;
    private  float hold;
    private  bool endPlayB;
    private float signal1;
    private float signal2;
    private float signalNoise;

    float[] myBuffer;
    
    public void Init()
    {
        UpdateOSC();
        UpdateAdsr();
        
        moogFilterL = new MoogFilter(48000);
        moogFilterR = new MoogFilter(48000);
        UpdateFilter();
        notes = new float[128];
        for (int i = 0; i < 128; i++)
        {
            notes[i] = Midi2freq(i % 12, i / 12 - 2);
        }
       
    }

    public void UpdateOSC()
    {
        osc1 = new soundGenerator(dependencyManager.Instance._synthSettings.osc1Type);
        osc2 = new soundGenerator(dependencyManager.Instance._synthSettings.osc2Type);
        noiseOsc = new soundGenerator(soundGenerator.Type.noise);
        osc1Amp = dependencyManager.Instance._synthSettings.osc1Amp;
        osc2Amp = dependencyManager.Instance._synthSettings.osc2Amp;
        noiseAmp = dependencyManager.Instance._synthSettings.noiseAmp;
        ResetSound();
    }

    public void UpdateAdsr()
    {
        attackParam = dependencyManager.Instance._synthSettings.atackParam;
        sustaneParam = dependencyManager.Instance._synthSettings.sustainParam;
        holdParam = 0.1f;
        releaseParam = dependencyManager.Instance._synthSettings.releaseParam;
    }

    public void UpdateFilter()
    {
        moogFilterL.SetCutoff(dependencyManager.Instance._synthSettings.cutFilter);
        moogFilterL.SetResonance(dependencyManager.Instance._synthSettings.resFilter);
        moogFilterL.SetOversampling(1);
        moogFilterR.SetCutoff(dependencyManager.Instance._synthSettings.cutFilter);
        moogFilterR.SetResonance(dependencyManager.Instance._synthSettings.resFilter);
        moogFilterR.SetOversampling(1);
    }

    private int index;
    public void StartPlay(int note, int octave)
    {
        index++;
        print("START PLAY "+index);
     //   if (isPlaying == false)
            ResetSound();
        myFmFreq = Midi2freq(note, octave);
        osc1.SetFrequency(Midi2freq(note, octave));
        osc2.SetFrequency(Midi2freq(note, octave));
        noiseOsc.SetFrequency(Midi2freq(note, octave));
        isPlaying = true;
        
    }
    
    public void StopPlay()
    {
      //  ResetSound();
        if (isPlaying)
        {
            endPlayB = true;
        }
    }

 
    public void ResetSound()
    {
       
        endPlayB = false;
        timeIndex = 0;
        mainAmp = 0;
        hold = 0;
        releaseAmp = 1;
        stopAmp = 1;
        isPlaying = false;
    }
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (isPlaying)
        {


            myBuffer = new float[data.Length];
            for (int i = 0; i < data.Length; i += channels)
            {

                if (endPlayB)
                {
                    if (stopAmp > 0)
                    {
                        stopAmp -= releaseParam;
                    }
                    else
                    {
                        isPlaying = false;
                    }
                }
                
                    if (mainAmp < 1)
                    {
                        mainAmp += attackParam;
                    }
                    else
                    {
                        if (hold < 1)
                        {
                            hold += holdParam;
                        }
                        else
                        {
                            if (releaseAmp > 0)
                            {
                                releaseAmp -= sustaneParam;
                            }
                        }
                    }
                

                signal1 = osc1.SoundOscillator() * osc1Amp;

                signal2 = osc2.SoundOscillator() * osc2Amp;

                signalNoise = noiseOsc.SoundOscillator() * noiseAmp;

                osc1.SoundUpdate();
                osc2.SoundUpdate();
                noiseOsc.SoundUpdate();

                mainAmp = Mathf.Clamp(mainAmp, 0, 1);
                releaseAmp = Mathf.Clamp(releaseAmp, 0, 1);
                stopAmp = Mathf.Clamp(stopAmp, 0, 1);

                myBuffer[i] = (signal1 * 0.33f + signal2 * 0.33f + signalNoise * 0.33f) * mainAmp * releaseAmp*stopAmp ;
                
                if (channels == 2)
                    myBuffer[i + 1] = myBuffer[i];

                timeIndex++;
            }

            int sampleCount = data.Length / channels;
            moogFilterL.process_mono_stride(myBuffer, sampleCount,0,2);
            moogFilterR.process_mono_stride(myBuffer, sampleCount, 1, 2);
            Array.Copy(myBuffer, data, data.Length);
        }
        else
        {
            myBuffer = new float[data.Length];
            Array.Copy(myBuffer, data, data.Length);
        }
    }
    
    private float Midi2freq(int note, int octave)
    {
        return 32.70319566257483f * Mathf.Pow(2.0f, note / 12.0f + octave);
    }
}
