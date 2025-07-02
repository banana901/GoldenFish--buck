using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishManager : MonoBehaviour
{
    public List<GameObject> fishPrefabs = new List<GameObject>();
    public List<GameObject> currentFishList = new List<GameObject>();

    private int number;




    void Start()
    {
        // 初期生成
        for (int i = 0; i < 5; i++)
        {
            SpawnFish();
        }





    }

    void Update()
    {



        for (int i = 0; i < currentFishList.Count; i++)
        {
            if (currentFishList[i] == null)
            {
                currentFishList.RemoveAt(i); // nullになった魚をリストから除外
                SpawnFish(); // 新しく1匹生成
                break; // 一度に1匹だけ補充
            }
        }

        ReplenishmentDGoldenFish();
        LimitDatu();


    }

    void SpawnFish()
    {
        int rnd = Random.Range(0, 100);      // 毎回独立した乱数を取る
        Vector3 pos = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);

        if (rnd < 1)                        // 1%
        {
            AddFish(fishPrefabs[2], pos);
        }
        else if (rnd < 11)
        {
            AddFish(fishPrefabs[3], pos);

        }
        else if (rnd < 16)
        {
            AddFish(fishPrefabs[4], pos);

        }
        else if (rnd < 31)
        {
            AddFish(fishPrefabs[5], pos);

        }


        //int randomIndex = Random.Range(0, fishPrefabs.Count);
        //GameObject newFish = Instantiate(fishPrefabs[randomIndex], spawnPos, Quaternion.identity);
        //currentFishList.Add(newFish);
    }

    void AddFish(GameObject prefab, Vector3 pos)
    {
        GameObject fish = Instantiate(prefab, pos, Quaternion.identity);
        currentFishList.Add(fish);
    }

    void ReplenishmentDGoldenFish()
    {

        int fishCount = currentFishList.Count(fishObj => fishObj != null && fishObj.CompareTag("Fish"));//ラムダ式

        if (fishCount < 4)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);
            GameObject additionFish = Instantiate(fishPrefabs[0], spawnPos, Quaternion.identity);
            currentFishList.Add(additionFish);

        }
    }

    void LimitDatu()
    {
        int count = currentFishList.Count(fishObj => fishObj != null && fishObj.CompareTag("Datu"));

        if (count < 2)
        {
            Vector3 spawnDatu = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);
            GameObject newDatu = Instantiate(fishPrefabs[1], spawnDatu, Quaternion.identity);
            currentFishList.Add(newDatu);

        }

    }






}
