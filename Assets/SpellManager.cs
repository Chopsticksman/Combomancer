using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] TMP_Text spellText;
    [SerializeField] SpriteRenderer fireballSprite;
    [SerializeField] SpriteRenderer windSprite;
    [SerializeField] SpriteRenderer killSprite;
    public List<Tuple<SpriteRenderer, int>> activeEffects;

    private void Awake()
    {
        activeEffects = new List<Tuple<SpriteRenderer, int>>();
        GameTick.OnTick += DecreaseQueue;
    }
    public void Cast(List<ButtonType> list)
    {
        string spellString = "";
        foreach (ButtonType bt in list)
        {
            if (bt == ButtonType.Left)
            {
                spellString += "F";
            }
            else
            {
                spellString += "J";
            }
        }
        switch (spellString)
        {
            case "FJFF":
                spellText.text = "Fireball";
                spellText.color = fireballSprite.color;
                CastFireball(EnemyManager.instance.ClosestEnemy());
                break;
            case "JFJ":
                spellText.text = "Gust of Wind";
                spellText.color = windSprite.color;
                spellText.alpha = 1;
                CastGustOfWind(EnemyManager.instance.ClosestEnemy());
                break;
            case "FFFJF":
                spellText.text = "Cast Power Word: Kill";
                CastPWKill(EnemyManager.instance.ClosestEnemy());
                break;
            case "JJF":
                spellText.text = "Cast Shield!";
                break;
            default:
                spellText.text = "No Spell Cast.";
                break;
        }
    }

    void CastFireball(Enemy e)
    {
        SpriteRenderer fireball = Instantiate(fireballSprite, transform);
        fireball.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(fireball, 50));
    }

    void CastGustOfWind(Enemy e)
    {
        SpriteRenderer wind = Instantiate(windSprite, transform);
        wind.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(wind, 50));
    }
    void CastPWKill(Enemy e)
    {
        SpriteRenderer kill = Instantiate(killSprite, transform);
        kill.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(kill, 50));
    }

    void DecreaseQueue()
    {
        if (activeEffects.Count == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < activeEffects.Count; i++)
            {
                if (activeEffects[i].Item2 <= 0)
                {
                    Destroy(activeEffects[i].Item1.gameObject);
                    activeEffects.RemoveAt(i);
                }
                else
                {
                    // if (activeEffects[i].Item1.Equals(fireballSprite))
                    // {
                        
                    // }
                    activeEffects[i] = new Tuple<SpriteRenderer, int>(activeEffects[i].Item1, activeEffects[i].Item2 - 1);
                }
            }
        }
    }
}
