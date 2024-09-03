using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] List<float> spawnTimes = new List<float>() { 1.5f, 1f, 0.05f };

    public static WaveManager instance;
    bool waveRunning = false;
    int currentWave = 0;
    float currentWaveTime = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {   
        waveText.text = "Wave: 1";
        timeText.text = "30";
        StartNewWave();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !waveRunning)
        {
            StartNewWave();
        }

    }

    public bool WaveRuning()
    {
        return waveRunning;
    }

    private void StartNewWave()
    {
        StopAllCoroutines();
        Player.instance.ResetPlayer();
        timeText.color = Color.white;
        timeText.text = "30";
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30;
        waveText.text = "Wave: " + currentWave;
        
        if (currentWave - 1 < spawnTimes.Count)
        {
            EnemyManager.instance.SetSpawnTime(spawnTimes[currentWave - 1]);
        }
        
        StartCoroutine(WaveTimer());
    } 

    private IEnumerator WaveTimer()
    {
        while(waveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;
            timeText.text = currentWaveTime.ToString();
            if (currentWaveTime <= 10)
            {
                timeText.color = Color.red;
            }
            if(currentWaveTime <= 0)
            {
                WaveCompleted();
            }
        }
        yield return null;
    }

    private void WaveCompleted()
    {
        StopCoroutine(WaveTimer());
        waveRunning = false;
        timeText.text = "Wave Completed!";
        timeText.color = Color.green;
        currentWaveTime = 0;
        EnemyManager.instance.RemoveEnemies();
    }
}
