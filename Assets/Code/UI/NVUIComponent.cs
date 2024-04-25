using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class NVUIComponent : MonoBehaviour {
    RectTransform _tform;
    UIDataSource _dataSource;
    TextMeshProUGUI _text;
    public TextMeshProUGUI text {get{if(!_text) _text = GetComponent<TextMeshProUGUI>(); return _text;}}

    Image _image;
    public Image image {get{if(!_image) _image = GetComponent<UnityEngine.UI.Image>(); return _image;}}
    AlphaModulator _alpha;
    public AlphaModulator alpha {get{if(!_alpha) _alpha = GetComponent<AlphaModulator>(); return _alpha;}}

    public RectTransform tform {get{if(!_tform) _tform = GetComponent<RectTransform>(); return _tform;}}
    public UIDataSource dataSource {get{if(!_dataSource) _dataSource = GetComponent<UIDataSource>(); return _dataSource;}}
    protected void Sound(String clip){
        GameController.gameController.aud.PlayOneShot(AudioManager.GetClip(clip));
    }
    public void SetActive(bool active){
        if(text)
            text.enabled=active;
        if(image)
            image.enabled=active;
    }
}