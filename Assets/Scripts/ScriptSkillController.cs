using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptSkillController : MonoBehaviour
{

    public ScriptPlayerController playerController;
    

    [SerializeField] private Image cooldownImage;

    public TextMeshProUGUI cooldownText;

    private bool isCooldown = false;

    public float cooldownTime;
    public float cooldownTimer;

    void Start()
    {
        cooldownText.gameObject.SetActive(false);
        cooldownImage.fillAmount = 0f;
    }

    public void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            playerController.GetComponent<ScriptAtkSlash>().canleftATK = true;
            isCooldown = false;
            cooldownText.gameObject.SetActive(false);
            cooldownImage.fillAmount = 0f;
            cooldownTimer = 0f;
        }
        else
        {
            cooldownText.text = Mathf.RoundToInt(cooldownTimer).ToString();
        }
    }

    public bool SkillCooldown()
    {

        if (isCooldown)
        {
            return false;
        }
        else
        {
            playerController.GetComponent<ScriptAtkSlash>().canleftATK = false;
            isCooldown = true;
            cooldownText.gameObject.SetActive(true);
            return true;
        }
    }
}
