using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    #region Variables
    public GameObject personPrefab;
    public int populationSize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;
	GUIStyle guiStyle = new GUIStyle();
    #endregion
    

    void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trail Time: " + (int)elapsed, guiStyle);
    }
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f),0);
            GameObject person = Instantiate(personPrefab, position, Quaternion.identity);
            person.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            person.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            person.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            person.GetComponent<DNA>().s = Random.Range(0.1f, 0.3f);
            population.Add(person);
        }
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed  = 0 ;
            Debug.Log("regeneration population");   
        }
    }
    void BreedNewPopulation()
    {
        List<GameObject>  NewPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
            Debug.Log("working");
        }

        // destroy all parents and previous population.
        for (int i = 0; i < sortedList.Count ; i++)
        {
            Destroy(sortedList[i]);
        }
        generation ++; 

    }
    GameObject Breed (GameObject parent1, GameObject parent2) 
    {
        Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, position, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        //swap parent dna
        if(Random.Range(0,1000) > 5)
        {
            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;
        }
        else
        {
            offspring.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().s = Random.Range(0.1f, 0.3f);
        }

        return offspring;

    }
    
}
