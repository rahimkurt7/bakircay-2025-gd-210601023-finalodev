using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;


public class DestructionTable : MonoBehaviour
{
    private GameObject fruit1 = null;
    private GameObject fruit2 = null;

    public GameObject destroyEffectPrefab;
    public GameObject[] fruitPrefabs;
    public Transform[] spawnPoints;

    public float ejectForce = 15f;
    public Transform tableCenter;
    private int score = 0;

    private int remainingFruits;
    private float remainingTime = 60f;
    private bool isGameOver = false;
    private bool isExtraPointsActive = false;

    private int matchCounter = 0;

    private bool isExtraPointsLocked = false;
    private float extraPointsUnlockTime = 0f;

    private bool isMultiplierActive = false;
    private float multiplierEndTime = 0f;

    private GUIStyle scoreStyle = new GUIStyle();
    private GUIStyle buttonStyle = new GUIStyle();
    private GUIStyle timerStyle = new GUIStyle();
    private GUIStyle gameOverStyle = new GUIStyle();

    void Start()
    {
        // Skor yazı tipi ayarları
        scoreStyle.fontSize = 50;
        scoreStyle.normal.textColor = Color.cyan;
        scoreStyle.alignment = TextAnchor.MiddleLeft;

        // Buton stili ayarları
        buttonStyle.fontSize = 20;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        // Zaman yazı tipi stili
        timerStyle.fontSize = 50;
        timerStyle.normal.textColor = Color.green;
        timerStyle.alignment = TextAnchor.MiddleCenter;

        // Oyun bitiş yazısı stili
        gameOverStyle.fontSize = 80;
        gameOverStyle.normal.textColor = Color.red;
        gameOverStyle.alignment = TextAnchor.MiddleCenter;

        SpawnFruits();
    }

    void Update()
    {
        if (!isGameOver)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOver();
            }
        }

        if (isMultiplierActive && Time.time >= multiplierEndTime)
        {
            isMultiplierActive = false;
        }

        if (isExtraPointsLocked && Time.time >= extraPointsUnlockTime)
            isExtraPointsLocked = false;

        if (matchCounter >= 7 && !isGameOver)
        {
            ResetFruits();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (fruit1 == null)
        {
            fruit1 = other.gameObject;
            Rigidbody rb = fruit1.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        else if (fruit2 == null && fruit1 != other.gameObject)
        {
            fruit2 = other.gameObject;
            Rigidbody rb = fruit2.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            CheckFruits();
        }
    }

    void CheckFruits()
    {
        if (fruit1 != null && fruit2 != null)
        {
            if (fruit1.tag == fruit2.tag)
            {
                Debug.Log("Eşleşen meyveler yok ediliyor!");
                PlayDestroyEffect(fruit1.transform.position);
                PlayDestroyEffect(fruit2.transform.position);

                int points = 100;
                if (isMultiplierActive)
                {
                    points *= 2;
                }

                score += points;

                Destroy(fruit1, 0.5f);
                Destroy(fruit2, 0.5f);
                fruit1 = null;
                fruit2 = null;

                remainingFruits -= 2;

                matchCounter++;
            }
            else
            {
                Debug.Log("Meyveler farklı! İkinci meyve ittiriliyor.");
                EjectObject(fruit2);
                fruit2 = null;
            }
        }
    }

    void PlayDestroyEffect(Vector3 position)
    {
        if (destroyEffectPrefab != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1.5f);
        }
    }

    void EjectObject(GameObject fruit)
    {
        Vector3 ejectDirection = (fruit.transform.position - tableCenter.position).normalized;
        Rigidbody rb = fruit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(ejectDirection * ejectForce, ForceMode.Impulse);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Oyun bitti! Toplam skor: " + score);
    }

    void SpawnFruits()
    {
        remainingFruits = spawnPoints.Length;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject fruit = Instantiate(fruitPrefabs[i % fruitPrefabs.Length], spawnPoints[i].position, Quaternion.identity);
            fruit.tag = fruitPrefabs[i % fruitPrefabs.Length].tag;
        }
    }

    void ResetFruits()
    {
        foreach (GameObject fruit in GameObject.FindGameObjectsWithTag("Fruit"))
        {
            Destroy(fruit);
        }

        SpawnFruits();
        matchCounter = 0;
    }

    void OnGUI()
    {
        // Skor
        GUI.Label(new Rect(20, 20, 200, 50), "Score: " + score, scoreStyle);

        // Sayaç
        GUI.Label(new Rect(20, 80, 200, 50), "Sayaç: " + matchCounter, scoreStyle);

        // Süre
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 80, 200, 50), "Süre: " + Mathf.Ceil(remainingTime).ToString() + "s", timerStyle);

        // RESETLE butonu
        buttonStyle.normal.background = MakeTexture(150, 50, new Color(1f, 0.8f, 0f));
        if (GUI.Button(new Rect(20, 150, 150, 50), "RESETLE", buttonStyle))
        {
            RestartGame();
        }

        // EKSTRA PUAN butonu
        buttonStyle.normal.background = MakeTexture(150, 50, isExtraPointsLocked ? Color.gray : new Color(0.5f, 0f, 1f));
        GUI.enabled = !isExtraPointsLocked;
        if (GUI.Button(new Rect(20, 220, 150, 50), "EKSTRA PUAN", buttonStyle))
        {
            ActivateExtraPoints();
            isExtraPointsLocked = true;
            extraPointsUnlockTime = Time.time + 5f;
        }
        GUI.enabled = true;

        // ÇARPAN PUAN butonu
        buttonStyle.normal.background = MakeTexture(150, 50, new Color(1f, 0.5f, 0f));
        if (GUI.Button(new Rect(20, 290, 150, 50), "ÇARPAN PUAN", buttonStyle))
        {
            ActivateMultiplier();
        }

        // SÜRE EKLE butonu
        buttonStyle.normal.background = MakeTexture(150, 50, new Color(0f, 0.8f, 0.2f));
        if (GUI.Button(new Rect(20, 360, 150, 50), "SÜRE EKLE", buttonStyle))
        {
            AddTime();
        }

        if (isGameOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100), "Oyun Bitti!\nSkor: " + score, gameOverStyle);
        }
    }

    Texture2D MakeTexture(int width, int height, Color col)
    {
        Texture2D result = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = col;
        result.SetPixels(pixels);
        result.Apply();
        return result;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void AddTime()
    {
        if (!isGameOver)
        {
            remainingTime += 10f;
            Debug.Log("Süre 10 saniye artırıldı!");
        }
    }

    void ActivateExtraPoints()
    {
        if (!isGameOver)
        {
            isExtraPointsActive = true;
        }
    }

    void ActivateMultiplier()
    {
        if (!isGameOver)
        {
            isMultiplierActive = true;
            multiplierEndTime = Time.time + 10f;
            Debug.Log("Çarpan Puan aktif! 10 saniye boyunca skorlar iki katına çıkacak.");
        }
    }
}
