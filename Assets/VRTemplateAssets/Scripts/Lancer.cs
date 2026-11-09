using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Lancer : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int scoreBIG = 25;
    private int scoreMEDIUM = 10;
    private int scoreSMALL = 5;

    private static int score = 0;

    [SerializeField] private string tier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (scoreText != null) scoreText.text = "Score : " + score;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Faire apparaitre particules et jouer le son
        AudioSource audio = this.GetComponent<AudioSource>();
        if (audio != null) audio.Play();

        GameObject particles = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);

        score += tier switch
        {
            "BIG" => scoreBIG,
            "MEDIUM" => scoreMEDIUM,
            "SMALL" => scoreSMALL,
            _ => 0
        };

        if (scoreText != null) scoreText.text = "" + score;

    }
}
