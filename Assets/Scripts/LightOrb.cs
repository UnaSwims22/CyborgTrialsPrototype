using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightOrb : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Light orbFrontLight;
    [SerializeField] private Light orbBackLight;
    [SerializeField] private TMP_Text orbLifePercentText; // battery percent
    [SerializeField] private TMP_Text orbLifeCountText; //battery count text

    [Header("Battery Settings")]
    [SerializeField] private float maxLifetime = 100f;
    [SerializeField] private float currentLifetime = 100f;
    [SerializeField] private int powerUps = 0;
    public float rechargeAmount = 50f;

    [Header("Audio")]
    [SerializeField] private AudioSource flashON;
    [SerializeField] private AudioSource flashOFF;

    private bool isOn = false;
    public bool IsLightOn => isOn;

    public bool batteryWasDepleted = false;

    public static LightOrb Instance;
    

    void Start()
    {
        if (orbBackLight == null)
            orbBackLight = GetComponent<Light>();

        if (orbFrontLight == null)
            orbFrontLight = GetComponent<Light>();

        orbBackLight.enabled = false;
        orbFrontLight.enabled = false;

        isOn = false;

        Instance = this;
    }

    void Update()
    {
        //Track light usage
        if (isOn)
        {
            //ScoreManager.Instance.totalLightUsed += Time.deltaTime;
           // ScoreManager.Instance.usefulLightUsed += Time.deltaTime;
            
        }

       // ScoreManager.Instance.timesBatteryDepleted++;
        
        UpdateUI();
        HandleDrain();
        HandleInput();

    }

    void HandleInput()
    {
        //  Toggle with F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
                TurnOff();
            else if (currentLifetime > 0)
                TurnOn();
        }

        // Reload with R
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (powerUps > 0)
            {
                powerUps--;
                GameSessionTracker.Instance.UsedBatteryPowerUp();
                currentLifetime += rechargeAmount;
                batteryWasDepleted = false;

                if (currentLifetime > maxLifetime)
                    currentLifetime = maxLifetime;
                UIManager.Instance?.UpdateBolts(powerUps);
                //when player uses one power up, but  has multiple, ui of all bolts disappears (all power ups used up in one setting) 
            }
        }
    }

    void UpdateUI()
    {
        orbLifePercentText.text = currentLifetime.ToString("0") + "%";
        UIManager.Instance?.UpdateBolts(powerUps);
        //orbLifeCountText.text = powerUps.ToString();
    }

    public void AddPowerUp(int amount)
    {
        powerUps += amount;
        UIManager.Instance?.UpdateBolts(powerUps);
    }

    void HandleDrain()
    {
        if (!isOn) return;

        currentLifetime -= Time.deltaTime;

        
        //IF BATTERY HITS ZER0
        if (currentLifetime <= 0f)
        {
            currentLifetime = 0f;

            // ONLY COUNT ONCE
            if (!batteryWasDepleted)
            {
                batteryWasDepleted = true;

                if (GameSessionTracker.Instance != null)
                {
                    GameSessionTracker.Instance.BatteryDrained();
                }
            }
            //GameSessionTracker.Instance.BatteryDrained();
            //if (GameSessionTracker.Instance != null)
            //{
            //    GameSessionTracker.Instance.BatteryDrained();
            //}


            TurnOff();
        }

       
    }

    //  INPUT SYSTEM METHODS (called by PlayerInput)
    public void OnToggleLight(InputValue value)
    {
        if (!value.isPressed) return;

        if (isOn)
            TurnOff();
        else if (currentLifetime > 0)
            TurnOn();
    }

    public void OnReload(InputValue value)
    {
        if (!value.isPressed) return;

        if (powerUps <= 0) return;

        powerUps--;
        GameSessionTracker.Instance.UsedBatteryPowerUp();
        currentLifetime += rechargeAmount;
        batteryWasDepleted = false;

        if (currentLifetime > maxLifetime)
            currentLifetime = maxLifetime;
        UIManager.Instance?.UpdateBolts(powerUps);
    }

    void TurnOn()
    {
        flashON?.Play();
        orbBackLight.enabled = true;
        orbFrontLight.enabled = true;
       //orbLight.enabled = true;
        isOn = true;
    }

    void TurnOff()
    {
        flashOFF?.Play();
        orbBackLight.enabled = false;
        orbFrontLight.enabled = false;
        //orbLight.enabled = false;
        isOn = false;
    }
}
