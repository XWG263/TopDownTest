using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
	public static void Create(Vector3 position, int damageAmount, bool isCriticalHit)
	{
		PopupText popupText = Instantiate(ResourceMananger.Instance.popupText, position, Quaternion.identity);
		popupText.Setup(damageAmount, isCriticalHit);
	}

	private TextMeshPro _textMeshPro;
	private Color _textColor;

	[Header("Move Up")]
	public Vector3 moveUpVector = new Vector3(0, 1, 0);
	public float moveUpSpeed = 2.0f;
	[Header("Move Down")]
	public Vector3 moveDownVector = new Vector3(-0.7f, 1, 0);
	[Header("Disappear ")]
	public float DisappearSpeed = 3.0f;
	private const float DisappearTimeMax = 0.2f;
	private float _disappearTimer;

	public Color normalColor;
	public Color criticalHitColor;


	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshPro>();
	}

	void Setup(int demageAmount, bool isCriticalHit)
	{
		_textMeshPro.SetText(demageAmount.ToString());

		if (isCriticalHit)
		{
			_textMeshPro.fontSize = 5;
			_textColor = criticalHitColor;
		}
		else
		{
			_textMeshPro.fontSize = 3;
			_textColor = normalColor;
		}
		_textMeshPro.color = _textColor;
		_disappearTimer = DisappearTimeMax;
	}

	private void Update()
	{
		//move up
		if (_disappearTimer > DisappearTimeMax * 0.5f)
		{
			transform.position += moveUpVector * Time.deltaTime;
			moveUpVector += moveUpVector * (Time.deltaTime * moveUpSpeed);
			transform.localScale += Vector3.one * (Time.deltaTime * 1);
		}
		else
		{
			//move donw
			transform.position += moveDownVector * Time.deltaTime;
			transform.localScale -= Vector3.one * (Time.deltaTime * 1);
		}

		//disappear
		_disappearTimer -= Time.deltaTime;
		if (_disappearTimer < 0)
		{
			//alpha
			_textColor.a -= DisappearSpeed * Time.deltaTime;
			_textMeshPro.color = _textColor;
			if (_textColor.a < 0)
			{
				Destroy(gameObject);
			}
		}
	}
}

