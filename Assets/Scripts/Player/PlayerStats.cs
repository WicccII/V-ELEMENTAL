using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PlayerStats : MonoBehaviour
{
    //player
    [Header("Player Stats")]
    public PlayerScriptableObject playerData;
    float currentHealth;
    float currentSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentRecovery;
    float currentMagnet;
    public GameObject currentCharacter;

    #region Player current stats

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                //update realtime stats
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentHealthDisplay.text = "Health :" + currentHealth;
                }
            }
        }
    }
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set
        {
            if (currentSpeed != value)
            {
                currentSpeed = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentSpeedDisplay.text = "Speed :" + currentSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMightDisplay.text = "Might :" + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentProjectileDisplay.text = "Projectile Speed :" + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentRecoverDisplay.text = "Recover :" + currentRecovery;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMagnetDisplay.text = "Magnet :" + currentMagnet;
                }
            }
        }
    }
    #endregion

    public Image healthBar;
    //inventory manager
    InventoryManager inventory;
    int skillIndex;
    int itemIndex;
    UIManager ui;

    //spawn StartingWeapon
    public List<GameObject> spawnWeapons;

    [Header("Level/Experience")]
    public int level = 1;
    public int experience = 0;
    public int expierienCap; // Initial experience cap for level 1

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCap;
    }

    public List<LevelRange> levelRanges;

    //I-frame
    [Header("I-frame")]
    public float invisibilityDuration;
    float invisibilityTimer;
    bool isInvincible;

    // Start is called before the first frame update
    void Awake()
    {
        //Get charater choosing in menu
        if (playerData == null)
        {
            playerData = CharacterSelecter.GetCharacter();
        }

        inventory = FindObjectOfType<InventoryManager>();
        ui = FindObjectOfType<UIManager>();

        CurrentHealth = playerData.MaxHealth;
        CurrentSpeed = playerData.MoveSpeed;
        CurrentMight = playerData.Might;
        CurrentProjectileSpeed = playerData.ProjectileSpeed;
        CurrentRecovery = playerData.Recovery;
        CurrentMagnet = playerData.Magnet;
        currentCharacter = playerData.Character;

        //spawn StartingWeapon
        SpawnWeapon(playerData.StartingWeapon);

        //experience
        expierienCap = levelRanges[0].experienceCap;
    }

    void Start()
    {
        //set stats
        GameManager.Instance.currentHealthDisplay.text = "Health :" + currentHealth;
        GameManager.Instance.currentSpeedDisplay.text = "Speed :" + currentSpeed;
        GameManager.Instance.currentRecoverDisplay.text = "Recover :" + currentRecovery;
        GameManager.Instance.currentMightDisplay.text = "Might :" + currentMight;
        GameManager.Instance.currentProjectileDisplay.text = "Projectile Speed :" + currentProjectileSpeed;
        GameManager.Instance.currentMagnetDisplay.text = "Magnet :" + currentMagnet;

        //AssignChoosenCharacter
        GameManager.Instance.AssignChoosenCharacter(playerData);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (invisibilityTimer > 0)
            {
                invisibilityTimer -= Time.deltaTime;
            }
            else
            {
                isInvincible = false;
            }
        }
        //recover health by time
        recover();

        UpdateHealthBar();
        //AssignChoseSkillUI
        GameManager.Instance.ChooseSkillAssign(inventory.skillUI);
        GameManager.Instance.ChooseItemAssign(inventory.itemUI);


    }


    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        while (experience >= expierienCap) // Check for level up
        {
            GameManager.Instance.StartLevelUp();
            experience -= expierienCap;
            level++; // Level up

            int experienceIncrease = 0;
            foreach (var levelRange in levelRanges)
            {
                if (level >= levelRange.startLevel && level <= levelRange.endLevel)
                {
                    experienceIncrease = levelRange.experienceCap;
                    break;
                }
            }
            expierienCap += experienceIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            invisibilityTimer = invisibilityDuration;
            isInvincible = true;
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = CurrentHealth / playerData.MaxHealth;
    }

    void Kill()
    {
        if (!GameManager.Instance.isGameOver)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void IncreaseHealth(int amount)
    {
        if (CurrentHealth < playerData.MaxHealth)
        {
            CurrentHealth += amount;
        }
        else
        {
            CurrentHealth = playerData.MaxHealth;
        }
    }

    void recover()
    {
        if (CurrentHealth < playerData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
        }
        else
        {
            CurrentHealth = playerData.MaxHealth;
        }
    }

    public void SpawnWeapon(GameObject Skill)
    {
        //startingWeapon
        GameObject spawnedWeapon = Instantiate(Skill, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); //set child of player
        inventory.AddSkill(skillIndex, spawnedWeapon.GetComponent<SkillController>());
        skillIndex++;
    }

    public void SpawnItem(GameObject item)
    {
        GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
        spawnedItem.transform.SetParent(transform); //set child of player
        inventory.AddItem(itemIndex, spawnedItem.GetComponent<PassiveItem>());
        itemIndex++;
    }
}
