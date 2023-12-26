using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingPoint : MonoBehaviour
{
    [Header("Assign Components")]
    [SerializeField] private Sprite clickedSprite;
    [SerializeField] private TextMeshProUGUI indexNumberText;
    [SerializeField] private RopeRenderer ropeRenderer;

    // Set On Awake
    private Button buttonInteractable;
    private Image visualImage;
    private CanvasGroup canvasGroup;

    // Set In GameManager
    int indexNumber;
    private ConnectingPoint pointToConnectRope;

    private void Awake()
    {
        buttonInteractable = GetComponent<Button>();
        visualImage = GetComponent<Image>();
        canvasGroup = indexNumberText.GetComponent<CanvasGroup>();

        buttonInteractable.onClick.AddListener(() =>
        {
            ButtonClicked();            
        });
    }

    /// <summary>
    /// Changes the button visual if its currect button pressed and asks to start rope animation
    /// </summary>
    private void ButtonClicked()
    {
        if(GameManager.instance.CheckIfThisIsNextIndex(indexNumber))
        {
            visualImage.sprite = clickedSprite;
            StartCoroutine(FadeOutText());
            if (indexNumber > 0)
                GameManager.instance.StartRopeAnimation(this);
        }         
    }

    /// <summary>
    /// Plays the rope animation
    /// </summary>
    public void PlayRopeAnimation()
    {
        ropeRenderer.SetRopePoints(pointToConnectRope.transform.position, transform.position);
        ropeRenderer.PlayAnimation();         
    }

    /// <summary>
    /// Sets the index to button
    /// </summary>
    /// <param name="index">index to set to</param>
    public void SetIndexNumber(int index)
    {
        indexNumber = index;
        indexNumberText.text = (index+1).ToString();
    }

    /// <summary>
    /// Sets the point to connect the rope to
    /// </summary>
    /// <param name="point">point to connect to</param>
    public void SetConnectingPoint(ConnectingPoint point)
    {
        pointToConnectRope = point;
    }

    /// <summary>
    /// Plays fade-out animation on text component
    /// </summary>
    private IEnumerator FadeOutText()
    {
        float fadeOutDuration = 1;
        float elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// Gets the button index
    /// </summary>
    /// <returns>button index</returns>
    public int GetIndex()
    {
        return indexNumber;
    }
}
