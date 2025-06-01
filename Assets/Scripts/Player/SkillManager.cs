using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SkillLevelStats
{
    public int damage;
    public float cooldown;
    public float duration;
    public int quantity;
    public int charge; // ← NOVO
}

[System.Serializable]
public class SkillUI
{
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI qtyText;
    public TextMeshProUGUI chargeText; // ← NOVO
    public TextMeshProUGUI levelText;
}

public class SkillManager : MonoBehaviour
{
    private longAttack longAttackScript;
    private defaultAttack defaultAttackScript;
    private collect collectScript;
    private p_Shield shieldScript;

    private bool activatedSkill;

    // Níveis atuais
    public int levelLong = 1;
    public int levelDefault = 1;
    public int levelCollect = 1;
    public int levelShield = 1;

    // Listas de níveis
    public List<SkillLevelStats> longAttackLevels = new List<SkillLevelStats>();
    public List<SkillLevelStats> defaultAttackLevels = new List<SkillLevelStats>();
    public List<SkillLevelStats> collectLevels = new List<SkillLevelStats>();
    public List<SkillLevelStats> shieldLevels = new List<SkillLevelStats>();

    // UI das habilidades
    public List<SkillUI> skillUISlots = new List<SkillUI>();

    // Referência ao objeto player
    public GameObject playerObject;

    // Singleton (opcional)
    public static SkillManager Instance;

    private readonly string[] skillNames = { "long", "default", "collect", "shield" };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        if (playerObject != null)
        {
            defaultAttackScript = playerObject.GetComponentInChildren<defaultAttack>();
            longAttackScript = playerObject.GetComponentInChildren<longAttack>();
            collectScript = playerObject.GetComponentInChildren<collect>();
            shieldScript = playerObject.GetComponentInChildren<p_Shield>();
        }
        else
        {
            Debug.LogError("Player não encontrado! Defina manualmente no SkillManager ou use a tag 'Player'.");
        }

        activatedSkill = false;
        UpdateAllSkillUIs();
    }

    void Update()
    {
        if (!activatedSkill)
        {
            if (Input.GetMouseButton(0))
            {
                defaultAttackScript?.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                longAttackScript?.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                shieldScript?.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (collectScript == null)
                {
                    Debug.LogWarning("Script de COLETA não encontrado");
                }
                else
                {
                    collectScript?.Activate();
                }
            }
        }
    }

    public void skillStatus(bool status)
    {
        activatedSkill = status;
        Debug.Log("Executando skill: " + activatedSkill);
    }

    public SkillLevelStats GetSkillStats(string skillName)
    {
        switch (skillName)
        {
            case "long":
                return longAttackLevels[Mathf.Clamp(levelLong - 1, 0, longAttackLevels.Count - 1)];
            case "default":
                return defaultAttackLevels[Mathf.Clamp(levelDefault - 1, 0, defaultAttackLevels.Count - 1)];
            case "collect":
                return collectLevels[Mathf.Clamp(levelCollect - 1, 0, collectLevels.Count - 1)];
            case "shield":
                return shieldLevels[Mathf.Clamp(levelShield - 1, 0, shieldLevels.Count - 1)];
            default:
                Debug.LogWarning("Skill não encontrada: " + skillName);
                return null;
        }
    }

    private int GetSkillLevel(string skillName)
    {
        return skillName switch
        {
            "long" => levelLong,
            "default" => levelDefault,
            "collect" => levelCollect,
            "shield" => levelShield,
            _ => 1,
        };
    }

    public void UpgradeSkill(string skillName)
    {
        switch (skillName)
        {
            case "long":
                if (levelLong < longAttackLevels.Count)
                    levelLong++;
                break;
            case "default":
                if (levelDefault < defaultAttackLevels.Count)
                    levelDefault++;
                break;
            case "collect":
                if (levelCollect < collectLevels.Count)
                    levelCollect++;
                break;
            case "shield":
                if (levelShield < shieldLevels.Count)
                    levelShield++;
                break;
            default:
                Debug.LogWarning("Skill inválida para upgrade: " + skillName);
                break;
        }

        UpdateAllSkillUIs();
    }

    public void UpdateAllSkillUIs()
    {
        for (int i = 0; i < skillUISlots.Count && i < skillNames.Length; i++)
        {
            var skillStats = GetSkillStats(skillNames[i]);
            var ui = skillUISlots[i];

            if (skillStats != null && ui != null)
            {
                ui.dmgText.text = "DMG: " + skillStats.damage;
                ui.cooldownText.text = "COOLDOWN: " + skillStats.cooldown + "s";
                ui.durationText.text = "DURATION: " + skillStats.duration + "s";
                ui.qtyText.text = "QTD: " + skillStats.quantity + "x";
                ui.chargeText.text = "CHARGE: " + skillStats.charge; // ← NOVO
                ui.levelText.text = "" + GetSkillLevel(skillNames[i]);
            }
        }
    }
}
