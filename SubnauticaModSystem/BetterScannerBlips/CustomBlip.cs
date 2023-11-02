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
        if (Settings.NoText)
        {
            text.gameObject.SetActive(false);
        }
        else
        {
            if (Settings.ShowDistance)
            {
                string meters = (distance < 5 ? Math.Round(distance, 1) : Mathf.RoundToInt(distance)).ToString();
                text.text = resourceName + " " + meters + "m";
            }
            else
            {
                text.text = resourceName;
            }
            text.gameObject.SetActive(distance < Settings.TextRange);
        }
    }

    private void RefreshScale(float distance)
    {
        if (distance < Settings.MinRange)
        {
            SetScale(Settings.MinRangeScale);
        }
        else if (distance >= Settings.MinRange && distance < Settings.CloseRange)
        {
            var t = Mathf.InverseLerp(Settings.MinRange, Settings.CloseRange, distance);
            SetScale(Mathf.Lerp(Settings.MinRangeScale, Settings.CloseRangeScale, t));
        }
        else if (distance >= Settings.CloseRange && distance < Settings.MaxRange)
        {
            var t = Mathf.InverseLerp(Settings.CloseRange, Settings.MaxRange, distance);
            SetScale(Mathf.Lerp(Settings.CloseRangeScale, Settings.MaxRangeScale, t));
        }
        else if (distance >= Settings.MaxRange)
        {
            SetScale(Settings.MaxRangeScale);
        }
    }

    private void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1);
    }

    private void RefreshColor(float distance)
    {
        if (Settings.CustomColors)
        {
            image.color = Settings.CircleColor;
            text.color = Settings.TextColor;
        }
        else
        {
            image.color = defaultCircleColour;
            text.color = defaultTextColour;
        }

        if (distance < Settings.AlphaOutRange)
        {
            SetAlpha(Settings.MaxAlpha);
        }
        else
        {
            var t = Mathf.InverseLerp(Settings.AlphaOutRange, Settings.MaxRange, distance);
            SetAlpha(Mathf.Lerp(Settings.MaxAlpha, Settings.MinAlpha, t));
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
