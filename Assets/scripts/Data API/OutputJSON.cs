using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutputJSON : MonoBehaviour {

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.LittleSquare;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.LittleSquare = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.LittleSquare = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] LittleSquare;
        }
    }

    [Serializable]
	public class LittleSquare
    {
        public string bigSquareNumber;
        public string littleSquareLetter;
        public string depth;
    }

    public LittleSquare[] selectedSquares;
    public LittleSquare thisSquare;

    private string originalText;
    private Color originalColor;

    private void Start()
    {
        originalText = transform.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text;
        originalColor = transform.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color;
    }
    public void SaveDataToJSON()
    {
        int i = 0;
        if (CubeBuffer.SelectedCubes == null || CubeBuffer.SelectedCubes.Count == 0)
        {
            transform.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "No Data Selected!";
            transform.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color = new Color(1, 0, 0);
            Invoke("SwitchTextBack", 1f);
        } else
        {
            selectedSquares = new LittleSquare[CubeBuffer.SelectedCubes.Count];
            foreach (GameObject littleSquare in CubeBuffer.SelectedCubes)
            {
                selectedSquares[i] = new LittleSquare();
                selectedSquares[i].bigSquareNumber = littleSquare.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text;
                selectedSquares[i].littleSquareLetter = littleSquare.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text;
                selectedSquares[i].depth = littleSquare.transform.GetChild(2).gameObject.GetComponent<TextMesh>().text;
                i++;
            }
            
            MasterScript.SendCubesToExternalApplication(JsonHelper.ToJson(selectedSquares));
            System.IO.File.WriteAllText(Application.persistentDataPath + "/squares.json", JsonHelper.ToJson(selectedSquares));

        }
        
    }
    

    public void SwitchTextBack()
    {
        this.transform.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = originalText;
        this.transform.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().color = originalColor;
    }

}
