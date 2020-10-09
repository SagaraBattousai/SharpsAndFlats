using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public const int SPECTRUM_SIZE = 512;
    public const int NUMBER_OF_BANDS = 8;

    [SerializeField]
    private AudioSource source;

    //Poor SE TODO:
    public static float[] spectrum = new float[SPECTRUM_SIZE];
    public static float[] freqBands = new float[NUMBER_OF_BANDS];
    public static float[] bufferBands = new float[NUMBER_OF_BANDS];
    public static float[] bufferDecrease = new float[NUMBER_OF_BANDS];


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumData();
        GetFrequencyBands();
        GetBufferBands();
    }

    private void GetSpectrumData()
    {
        source.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
    }

    private void GetFrequencyBands()
    {
        /*
         * 
         * AudioSettings.outputSampleRate currently = 48000 (by default) therefore sampleRate = 48000/2 = 24000
         * 
         * 24000 / 512 = 46.875 Hz per sample
         * 
         * Sub-bass	      20 to 60 Hz
         * Bass	          60 to 250 Hz
         * Low midrange	  250 to 500 Hz
         * Midrange	      500 Hz to 2 kHz
         * Upper midrange 2 to 4 kHz
         * Presence	      4 to 6 kHz
         * Brilliance	  6 to 20 kHz
         * 
         * 0 => 2   samples = 93.75
         * 1 => 4   samples = 281.25
         * 2 => 8   samples = 656.25
         * 3 => 16  samples = 1406.25
         * 4 => 32  samples = 2906.25
         * 5 => 64  samples = 5906.25
         * 6 => 128 samples = 11906.25
         * 7 => 256 samples = 23906.25
         * 
         */
        int spectrumIndex = 0;

        for ( int i = 0; i < NUMBER_OF_BANDS; i++)
        {
            float average = 0;
            int numberOfSamples = 1 << (i + 1);

            if (i == 7)
            {
                numberOfSamples += 2;
            }

            for (int j = 0; j < numberOfSamples; j ++)
            {
                average += spectrum[spectrumIndex]; //In some code you'll see it includes + (count + 1) WHY???? Bad HACKY code Grr
                spectrumIndex++;
            }

            average /= numberOfSamples;

            freqBands[i] = average; //Dont scale here imo

        }
    }

    private void GetBufferBands()
    {
        for (int i = 0; i < NUMBER_OF_BANDS; i++)
        {
            if (freqBands[i] > bufferBands[i])
            {
                bufferBands[i] = freqBands[i];
                bufferDecrease[i] = 0.005f;
            }

            if (freqBands[i] < bufferBands[i])
            {
                bufferBands[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }
}
