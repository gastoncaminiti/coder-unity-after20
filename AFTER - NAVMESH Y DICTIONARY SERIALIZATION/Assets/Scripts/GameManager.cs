using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //SCORE
    [SerializeField] private int scoreInstanciado;
    [SerializeField] private int powerInstanciado;

    //ITEM DICTIONARY
    private Dictionary<string, string> itemDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreInstanciado = 0;
            powerInstanciado = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        itemDictionary = new Dictionary<string, string>();
        //itemDictionary.Add("lifes", "0"); 
        instance.LoadJSON();
        Debug.Log(itemDictionary.ContainsKey("lifes"));
        Debug.Log(itemDictionary.ContainsKey("pepe"));
        scoreInstanciado = int.Parse(itemDictionary["lifes"]);
        powerInstanciado = int.Parse(itemDictionary["powers"]);

    }

    private void GameOver()
    {
        Debug.Log("EL JUEGO TERMINO");
        scoreInstanciado = 0;
    }

    public static void addScore()
    {
        instance.scoreInstanciado += 1;
        instance.itemDictionary["lifes"] = instance.scoreInstanciado.ToString();
        Debug.Log(instance.itemDictionary["lifes"]);
        instance.SaveJSON();
    }

    public static void addPower()
    {
        instance.powerInstanciado += 1;
        instance.itemDictionary["powers"] = instance.powerInstanciado.ToString();
        Debug.Log(instance.itemDictionary["powers"]);
        instance.SaveJSON();
    }

    public static int GetScore()
    {
        return instance.scoreInstanciado;
    }

    [System.Serializable]
    public class StringStringDictionary
    {
        public string key;
        public string value;
    }

    [System.Serializable]
    private class StringStringDictionaryArray
    {
        public StringStringDictionary[] items;
    }
 
    public void SaveJSON()
    {
        List<StringStringDictionary> dictionaryItemsList = new List<StringStringDictionary>();
        foreach (KeyValuePair<string, string> kvp in itemDictionary)
        {
            dictionaryItemsList.Add(new StringStringDictionary() { key = kvp.Key, value = kvp.Value });
        }

        StringStringDictionaryArray dictionaryArray = new StringStringDictionaryArray() { items = dictionaryItemsList.ToArray() };
        string json = JsonUtility.ToJson(dictionaryArray);
        //ESCRIBIR UN ARCHIVO DONDE EL CONTENIDO ES EL JSON
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/item.json", json);
    }

    public void LoadJSON()
    {
        //DEFINIMOS LA RUTA DEL JSON 
        string path = Application.persistentDataPath + "/item.json";
        //SI EXISTE UN ARCHIVO EN ESTA RUTA
        if (File.Exists(path))
        {
            //OBTENEMOS EL TEXTO DEL ARCHIVO (EL TEXTO ESTA EN FORMATO JSON)
            string json = File.ReadAllText(path);
            Debug.Log(json);
            StringStringDictionaryArray loadedData = JsonUtility.FromJson<StringStringDictionaryArray>(json);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                itemDictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        }
    }
}
