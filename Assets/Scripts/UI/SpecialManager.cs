using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpecialManager : MonoBehaviour
{
    public Image specialBar;
    [SerializeField]private Audiomanager audiomanager;

    #region
    public float chargeTime = 15f;
    private float currentCharge = 0f;
    private bool isCharging = true;
    #endregion


    //evento para gerir a UI de especial
    public delegate void SpecialReadyHandler();
    public SpecialReadyHandler onSpecialReady;

    private void Start()
    {
        specialBar.fillAmount = 0f;
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
                    audiomanager.PlaySfx(audiomanager.SpecialUIAudio);
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
            isCharging = true;
        }
    }



}
