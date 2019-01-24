using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InputJSON : MonoBehaviour {

    private string JSONData;
    private bool showData;

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.LittleSquare;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] LittleSquare;
        }
    }


    [System.Serializable]
    public class DrupalSelectedCubes
    {
        public string littleSquareLetter;
        public int bigSquareNumber;
        public int depth;
    }

    public DrupalSelectedCubes[] theseCubes;
 
	// Use this for initialization
	void Start () {
        //showData = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TestJsonWithButtonInUnity()
    {
        string testJsonString = GetComponent<Text>().text;
        ReceiveData(testJsonString);

    }

    void ReceiveData(string thisData)
    {
        CubeBuffer.ClearBuffer();
        LayerNavigator.ShowAllCubes();//This shows the hidden cubes, which are hidden to increase performance in WebGL
        JSONData = thisData;
        theseCubes = JsonHelper.FromJson<DrupalSelectedCubes>(thisData);
        //showData = true;
        foreach (DrupalSelectedCubes thisCube in theseCubes)
        {
            int adjustedLevel = Int32.Parse(thisCube.littleSquareLetter[1].ToString());
            adjustedLevel = adjustedLevel + 1;
            string nameOfCube = "littleSquare_" + thisCube.littleSquareLetter[0] + "_" + thisCube.bigSquareNumber + "_" + adjustedLevel.ToString();
            GameObject thisSelectedCube = GameObject.Find(nameOfCube);
            CubeBuffer.SelectedCubes.Add(thisSelectedCube);
        }
        if (CubeBuffer.SelectedCubes.Count > 0)
        {
            CubeBuffer.HideAllCubesExceptSelected();
            CubeBuffer.HighLightCubes(MasterScript.SelectedCubeColor);
        }
    }

    //private void OnGUI()
    //{
    //    if (showData)
    //    {

    //        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), JSONData);
    //        //theseCubes = JsonHelper.FromJson<DrupalSelectedCubes>(JSONData);
    //        //GUI.Label(new Rect(0, 0, Screen.width, Screen.height), theseCubes[0].ToString());
    //        //    for (int i = 0; i < theseCubes.Length; i++)
    //        //    {
    //        //        string thisCubeString = "littleSquare_" + theseCubes[i].littleSquareLetter
    //        //    }
    //    }
    //}

    
}
