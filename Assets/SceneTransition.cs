using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public GameObject Image;
    public GameObject Title;
    public GameObject ListView;
    public GameObject MainCanvas;
    public GameObject Status;

    public bool SkipTransitions = false;

	void Start ()
	{
        Image im = Image.GetComponent<Image>();
        Text titleText = Title.GetComponent<Text>();
        RectTransform titleTransform = Title.GetComponent<RectTransform>();
        RectTransform listTransform = ListView.GetComponent<RectTransform>();
        RectTransform statusTransform = Status.GetComponent<RectTransform>();

	    if (!SkipTransitions)
	    {
	        im.color = new Color(0, 0, 0);
	        titleText.fontSize = 128;
	        titleTransform.sizeDelta = new Vector2(1280, 720);

            listTransform.position = new Vector3(listTransform.position.x - listTransform.rect.width, listTransform.position.y, listTransform.position.z);
            statusTransform.position = new Vector3(statusTransform.position.x, statusTransform.position.y - 2500, statusTransform.position.z);

            MainCanvas.transform.position = new Vector3(0, 400);
	        MainCanvas.transform.rotation = Quaternion.Euler(45, 0, 0);

            MainCanvas.transform.DOMove(new Vector3(0, 0, 753), 2f).SetDelay(1);
	        MainCanvas.transform.DORotate(new Vector3(0, 0, 0), 2f).SetDelay(1).SetEase(Ease.OutBack, 10, 0);

            DOTween.To(() => im.color, x => im.color = x, new Color(1, 1, 1), 2f)
                .SetDelay(1)
                .OnComplete(delegate
                {
                    titleTransform.DOSizeDelta(new Vector2(1280, 65), 2f).SetEase(Ease.InQuad).SetDelay(2);

                    DOTween.To(() => listTransform.position, x => listTransform.position = x,
                        new Vector3(-555, listTransform.position.y, listTransform.position.z), 1f)
                        .SetEase(Ease.OutBack).SetDelay(4f);

                    DOTween.To(() => statusTransform.position, x => statusTransform.position = x,
                        new Vector3(statusTransform.position.x, -310, statusTransform.position.z), 2f)
                        .SetEase(Ease.OutBack, 1.05f).SetDelay(3.5f);
                });


	    }
	}
}
