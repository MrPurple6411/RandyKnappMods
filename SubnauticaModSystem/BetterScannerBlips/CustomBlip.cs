namespace BetterScannerBlips;

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class CustomBlip : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private TechType techType;
    private string resourceName;

    private Color defaultCircleColour;
    private Color defaultTextColour;

    public void Awake()
    {
        image = gameObject.GetComponent<Image>();
        if (defaultCircleColour == default)
        {
            defaultCircleColour = image.color;
        }

        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        if (defaultTextColour == default)
        {
            defaultTextColour = text.color;
        }
    }

    public void Refresh(ResourceTrackerDatabase.ResourceInfo target)
    {
        if (target != null)
        {
            var vectorToPlayer = Player.main.transform.position - target.position;
            var distance = vectorToPlayer.magnitude;

            if (resourceName == string.Empty || techType != target.techType)
            {
                techType = target.techType;
                resourceName = Language.main.Get(techType);
            }

            RefreshColor(distance);
            RefreshText(distance);
            RefreshScale(distance);
        }
    }

    private void RefreshText(float distance)
    {
        if (Config.NoText)
        {
            text.gameObject.SetActive(false);
        }
        else
        {
            if (Config.ShowDistance)
            {
                string meters = (distance < 5 ? Math.Round(distance, 1) : Mathf.RoundToInt(distance)).ToString();
                text.text = resourceName + " " + meters + "m";
            }
            else
            {
                text.text = resourceName;
            }
            text.gameObject.SetActive(distance < Config.TextRange);
        }
    }

    private void RefreshScale(float distance)
    {
        if (distance < Config.MinRange)
        {
            SetScale(Config.MinRangeScale);
        }
        else if (distance >= Config.MinRange && distance < Config.CloseRange)
        {
            var t = Mathf.InverseLerp(Config.MinRange, Config.CloseRange, distance);
            SetScale(Mathf.Lerp(Config.MinRangeScale, Config.CloseRangeScale, t));
        }
        else if (distance >= Config.CloseRange && distance < Config.MaxRange)
        {
            var t = Mathf.InverseLerp(Config.CloseRange, Config.MaxRange, distance);
            SetScale(Mathf.Lerp(Config.CloseRangeScale, Config.MaxRangeScale, t));
        }
        else if (distance >= Config.MaxRange)
        {
            SetScale(Config.MaxRangeScale);
        }
    }

    private void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1);
    }

    private void RefreshColor(float distance)
    {
        if (Config.CustomColors)
        {
            image.color = Config.CircleColor;
            text.color = Config.TextColor;
        }
        else
        {
            image.color = defaultCircleColour;
            text.color = defaultTextColour;
        }

        if (distance < Config.AlphaOutRange)
        {
            SetAlpha(Config.MaxAlpha);
        }
        else
        {
            var t = Mathf.InverseLerp(Config.AlphaOutRange, Config.MaxRange, distance);
            SetAlpha(Mathf.Lerp(Config.MaxAlpha, Config.MinAlpha, t));
        }
    }

    private void SetAlpha(float alpha)
    {
        var iColor = image.color;
        iColor.a = alpha;
        image.color = iColor;

        var tColor = text.color;
        tColor.a = alpha;
        text.color = tColor;
    }
}
