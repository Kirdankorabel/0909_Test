using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class WinPanel : MonoBehaviour
{
    public event System.Action OnGameRestarted;

    [SerializeField] private Image panel;
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button playAganButton;
    [SerializeField] private TMP_Text massageText;
    [SerializeField] private string massageVin;
    [SerializeField] private string massageLose;

    [SerializeField] private float panelScaleDuration = 0.5f;
    [SerializeField] private float fillDuration = 1f;
    [SerializeField] private float buttonPulseDuration = 0.5f;
    [SerializeField] private float buttonPulseScale = 1.1f;

    private Coroutine fillRoutine;

    private void Awake()
    {
        panel.transform.localScale = Vector3.zero;
        fillImage.fillAmount = 0f;
        text.text = "0%";
        playAganButton.gameObject.SetActive(false);
        playAganButton.transform.localScale = Vector3.one;

        playAganButton.onClick.AddListener(OnPlayAgainClicked);
    }

    public void ShowResult(float result)
    {
        gameObject.SetActive(true);
        massageText.text = result > 0.05f ? massageVin : massageLose;

        ResetVisuals();

        panel.transform.DOScale(Vector3.one, panelScaleDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                if (result <= 0f)
                    ShowButtonInstant();
                else
                    fillRoutine = StartCoroutine(FillProgressRoutine(result));
            });
    }

    private void ResetVisuals()
    {
        if (fillRoutine != null)
        {
            StopCoroutine(fillRoutine);
            fillRoutine = null;
        }

        panel.transform.localScale = Vector3.zero;
        fillImage.fillAmount = 0f;
        text.text = "0%";

        playAganButton.gameObject.SetActive(false);
        playAganButton.transform.localScale = Vector3.one;

        playAganButton.transform.DOKill();
        panel.transform.DOKill();
    }

    private IEnumerator FillProgressRoutine(float targetValue)
    {
        float time = 0f;
        float startFill = 0f;
        float startPercent = 0f;
        float endPercent = targetValue * 100f;

        while (time < fillDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / fillDuration);

            fillImage.fillAmount = Mathf.Lerp(startFill, targetValue, t);
            float percent = Mathf.Lerp(startPercent, endPercent, t);
            text.text = Mathf.RoundToInt(percent) + "%";

            yield return null;
        }

        fillImage.fillAmount = targetValue;
        text.text = Mathf.RoundToInt(endPercent) + "%";

        StartButtonPulse();
        fillRoutine = null;
    }

    private void StartButtonPulse()
    {
        playAganButton.gameObject.SetActive(true);
        playAganButton.transform.DOKill();
        playAganButton.transform.DOScale(buttonPulseScale * Vector3.one, buttonPulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void ShowButtonInstant()
    {
        playAganButton.gameObject.SetActive(true);
        playAganButton.transform.localScale = Vector3.one;
    }

    private void OnPlayAgainClicked()
    {
        if (fillRoutine != null)
        {
            StopCoroutine(fillRoutine);
            fillRoutine = null;
        }

        playAganButton.transform.DOKill();
        playAganButton.transform.DOScale(Vector3.zero, panelScaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            panel.transform.DOKill();
            panel.transform.DOScale(Vector3.zero, panelScaleDuration).SetEase(Ease.InBack).OnComplete(() =>
            {
                OnGameRestarted?.Invoke();
                gameObject.SetActive(false);
            });
        });
    }
}
