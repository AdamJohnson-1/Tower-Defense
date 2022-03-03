using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public GameObject MobScriptParentObject;
    private float TotalHealth;

    private float CurrentHealth;
    private float CurrentFlashIndicatorHealth;
    private float CurrentDownwardFlashMomentum = 0f;

    public Image HealthGreen;
    public Image HealthFlash;

    // Start is called before the first frame update
    void Start()
    {
        TotalHealth = MobScriptParentObject.GetComponent<MobScript>().GetStartingHealth();
        CurrentHealth = TotalHealth;
        CurrentFlashIndicatorHealth = TotalHealth;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //reorient the
        transform.eulerAngles = new Vector3(20, 0, 0);

        if (CurrentHealth != CurrentFlashIndicatorHealth)
        {
            CurrentDownwardFlashMomentum += Time.deltaTime * TotalHealth;
            CurrentFlashIndicatorHealth -= Time.deltaTime * CurrentDownwardFlashMomentum;

            if(CurrentFlashIndicatorHealth < CurrentHealth)
            {
                CurrentDownwardFlashMomentum = 0f;
                CurrentFlashIndicatorHealth = CurrentHealth;
            }

            HealthFlash.fillAmount = CurrentFlashIndicatorHealth / TotalHealth;
        }
    }

    public void SetCurrentHealth(float amount)
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        CurrentHealth = amount;
        HealthGreen.fillAmount = CurrentHealth / TotalHealth;
    }
}
