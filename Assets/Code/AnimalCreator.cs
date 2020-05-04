using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCreator : MonoBehaviour
{
    public int AnimalsCount = 10;
    public GameObject BasicAnimal;
    private Text _ui;
    void Start()
    {
        _ui =  GetComponentInChildren<Text>();
    }

    private void Awake()
    {
        
        System.Random rnd = new System.Random();
        for (int i = 0; i < AnimalsCount; i++)
        {
            var x = (float) rnd.NextDouble() * 100f;
            var z = (float) rnd.NextDouble() * -100f;
            bool needNew = true;
            while 
            (Physics.Raycast(new Vector3(x, 1, z), 
                new Vector3(x, -1, z), out var hit) && needNew)
            {
                
                if (hit.transform.tag != "Ground")
                {
                    Debug.Log("No Ground");
                    x = (float) rnd.NextDouble() * 100f;
                    z = (float) rnd.NextDouble() * -100f;
                }
                else
                {
                    needNew = false;
                }
            }

            Instantiate(BasicAnimal, new Vector3(x, 1, z), Quaternion.identity, transform);
        }
    }

    private void Update()
    {
        _ui.text = GetComponentsInChildren<Animal>().Length.ToString();
    }
}