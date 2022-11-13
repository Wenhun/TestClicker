using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int enemyHealth = 5;
    [SerializeField] Image healthBar;
    [Header("Damage Materials")]
    [SerializeField] Material enemyNormalMaterial;
    [SerializeField] Material enemyDamageMaterial;
    [SerializeField] Material enemyFreezeMaterial;
    [Header("Particle System")]
    [SerializeField] ParticleSystem deathVFX;
    [Header("Sounds")]
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip birthSound;
    [SerializeField] GameObject deathSound;
    [Header("Tools")]
    [SerializeField] Canvas canvas;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject poolFX;
 
    GameProcess gameProcess;
    AudioSource audioSource;
    GameObject parentGameObject;

    int maxHealth;

    bool isGodMode = false;

    void Awake()
    {
        maxHealth = enemyHealth;
        gameProcess = GetComponentInParent<GameProcess>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        enemyHealth = maxHealth; //reset health
        //reset health bar
        healthBar.color = Color.green; 
        healthBar.fillAmount = 1f;

        audioSource.PlayOneShot(birthSound);
        GetComponentInChildren<SkinnedMeshRenderer>().material = enemyNormalMaterial;
        if(GetComponent<EnemyMover>().IsFreeze()) GetComponentInChildren<SkinnedMeshRenderer>().material = enemyFreezeMaterial;
    }

    void OnDisable()
    {
        try
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Instantiate(deathSound, transform.position, Quaternion.identity);
            GetComponentInParent<SpawnEnemy>().DecreaseCountEnemy();
        }
        catch
        {
            return;
        }
    }

    IEnumerator ChangeColor()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = enemyDamageMaterial;
        yield return new WaitForSeconds(0.1f);
        if(GetComponent<EnemyMover>().IsFreeze())
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = enemyFreezeMaterial;
        }
        else
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = enemyNormalMaterial;
        }
    }

    public IEnumerator GodMode(float godModeTime)
    {
        isGodMode = true;
        yield return new WaitForSeconds(godModeTime);
        isGodMode = false;
    }

    void OnMouseDown()
    {
        audioSource.PlayOneShot(damageSound);
        StartCoroutine(ChangeColor());

        if (isGodMode) KillEnemy();
        
        enemyHealth--;
        DecreaseHealthBar(); 

        if (enemyHealth <= 0) KillEnemy();
    }

    private void DecreaseHealthBar()
    {
        healthBar.fillAmount -= 1f / maxHealth;
        if (enemyHealth <= maxHealth * .6f)
            healthBar.color = Color.yellow;
        if (enemyHealth <= maxHealth * .2f)
            healthBar.color = Color.red;
    }

    private void KillEnemy()
    {
        gameProcess.IncreaseScore();
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        canvas.transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
    }

    public void IncreaseHealth(int healthUp)
    {
        enemyHealth += healthUp;
        maxHealth += healthUp;
    }
}
