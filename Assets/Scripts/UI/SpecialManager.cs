using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpecialManager : MonoBehaviour
{
    public Image specialBar;
    public Image readyIndicator;
    public float chargeTime = 15f;
    private float currentCharge = 0f;
    private bool isCharging = true;


    public delegate void SpecialReadyHandler();
    public SpecialReadyHandler onSpecialReady;

    private void Start()
    {
        specialBar.fillAmount = 0f;
        readyIndicator.gameObject.SetActive(false);
        StartCoroutine(ChargingSpecial());
    }

    IEnumerator ChargingSpecial()
    {
        while (true)
        {
            if (isCharging)
            {
                currentCharge += Time.deltaTime / chargeTime;
                //limitar para a barra ( 0 a 1)
                currentCharge = Mathf.Clamp(currentCharge, 0, 1);

                specialBar.fillAmount = currentCharge;
                if (currentCharge >= 1f)
                {
                    isCharging = false;
                    readyIndicator.gameObject.SetActive(true);
                    onSpecialReady?.Invoke();
                }
            }
            yield return null;
        }
    }
    public void UseSpecial()
    {
        if (!isCharging && currentCharge >= 1f)
        {
            currentCharge = 0f;
            specialBar.fillAmount = 0f;
            readyIndicator.gameObject.SetActive(false);
            isCharging = true;
        }
    }



}
