using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ConcentricVisualizer : MonoBehaviour
{
    public MusicVisualizer musicVisualizer;
    //public GameObject Prefab;
    //public GameObject[] prefabs;
    public GameObject Strip;
    public GameObject[] strips;
    public GameObject Speaker;
    public GameObject[] speakers;
    public GameObject Trunk;
    public GameObject[] trunks;
    public GameObject External;
    public GameObject[] externals;
    public float scaleMultiplier = 0.01f;
    private float highestDecibelLevel;
    private float amplitude;

    // Start is called before the first frame update
    void Start()
    {
        //prefabs = new GameObject[378];
        //createSpiralLattice();
        //createConcentricLattice();
        strips = new GameObject[108];
        createHangingStrips();
        trunks = new GameObject[9];
        createTreeTrunk();
        speakers = new GameObject[9];
        createSpiralSpeakers();
        externals = new GameObject[960];
        createExternals();
    }

    // Update is called once per frame
    void Update()
    {
        animateHangingStrips();
        animateTreeTrunk();
    }

    private void createHangingStrips()
    {
        int k = 0;
        for (int i = 0; i < 9; i++)
        {
            Material newMat = Resources.Load("Materials/Octave" + (i + 1), typeof(Material)) as Material;
            this.transform.position = new Vector3(0f, (-4f - ((i * 2) / 10f)), 0f);
            for (int j = 0; j < 12; j++)
            {
                this.transform.eulerAngles = new Vector3(0, (20f * k) + (i * 2f), 0);
                strips[k] = (GameObject)Instantiate(Strip);
                //strips[k].GetComponent<Renderer>().material = newMat;
                strips[k].GetComponentInChildren<SkinnedMeshRenderer>().material = newMat;
                strips[k].name = "Strip" + k;
                strips[k].transform.parent = this.transform;
                strips[k].transform.localScale = new Vector3(1f, 1f, (1f - (0.08f * j)));
                strips[k].transform.position = strips[k].transform.position + Vector3.forward * (10f - ((i * 6) / 10f));
                k++;
            }
        }
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, 0f);
    }

    private void animateHangingStrips()
    {
        int l = 3;
        for(int i = 107; i >= 0; i--)
        {
            Material stripMat = Resources.Load("Materials/Octave" + ((int)(i/12) + 1), typeof(Material)) as Material;
            UnityEngine.Color stripColor = stripMat.GetColor("_EmissionColor");
            if (musicVisualizer.MusicalDecibels[l] > musicVisualizer.Threshold)
            {
                strips[i].GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", stripColor * Mathf.Pow(2.0F, 0.5f));
            }
            else
            {
                strips[i].GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", stripColor * Mathf.Pow(2.0F, -0.5f));
            }
            l++;
        }
    }

    private void createTreeTrunk()
    {
        for(int i = 0; i < 9; i++)
        {
            Material newMat = Resources.Load("Materials/Octave" + (i + 1), typeof(Material)) as Material;
            this.transform.position = new Vector3(0f, 0f, 0f);
            this.transform.eulerAngles = new Vector3(0, 40f * i, 0);
            trunks[i] = (GameObject)Instantiate(Trunk);
            trunks[i].GetComponent<Renderer>().material = newMat;
            trunks[i].name = "Trunk" + i;
            trunks[i].transform.parent = this.transform;
        }
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, 0f);
    }

    private void animateTreeTrunk()
    {
        if (musicVisualizer.OverallDecibelLevel > musicVisualizer.Threshold)
        {
            amplitude = musicVisualizer.OverallDecibelLevel/musicVisualizer.Threshold;
            Debug.Log(amplitude);
            for (int i = 0; i < 9; i++)
            {
                Material mat = Resources.Load("Materials/Octave" + (i + 1), typeof(Material)) as Material;
                UnityEngine.Color trunkColor = mat.GetColor("_EmissionColor");
                trunks[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", trunkColor * amplitude);
            }
        }       
    }

    private void createSpiralSpeakers()
    {
        for (int i = 0; i < 9; i++)
        {
            Material newMat = Resources.Load("Materials/Octave" + (i + 1), typeof(Material)) as Material;
            this.transform.position = new Vector3(0f, -6f, 0f);
            this.transform.eulerAngles = new Vector3(0, -90f + (40f * i), 0);
            speakers[i] = (GameObject)Instantiate(Speaker);
            speakers[i].GetComponent<Renderer>().material = newMat;
            speakers[i].name = "Speaker" + i;
            speakers[i].transform.parent = this.transform;
            speakers[i].transform.position = speakers[i].transform.position + Vector3.forward * 2.8f;
        }
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, 0f);
    }

    private void createExternals()
    {
        int k = 0;
        for (int i = 0; i < 40; i++)
        {
            Material newMat = Resources.Load("Materials/ExternalMat", typeof(Material)) as Material;
            this.transform.position = new Vector3(0f, -8f, 0f);
            for (int j = 0; j < 24; j++)
            {
                this.transform.eulerAngles = new Vector3(0, (15f * k) + (i * 6f), 0);
                externals[k] = (GameObject)Instantiate(Strip);
                externals[k].GetComponentInChildren<SkinnedMeshRenderer>().material = newMat;
                externals[k].name = "External" + k;
                externals[k].transform.parent = this.transform;
                externals[k].transform.localScale = new Vector3(1f, 1f, 0.6f);
                externals[k].transform.position = externals[k].transform.position + Vector3.forward * (45f - ((i * 9f) / 10f));
                k++;
            }
        }
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, 0f);
    }
}
