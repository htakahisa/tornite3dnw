using UnityEngine;
using UnityEngine.UI;

public class ReticleCustomizer : MonoBehaviour {
    public Image ReticleImage;
    public Slider ReticleSizeSlider;
    public FlexibleColorPicker ReticleColorPicker;

    public Image DotImage;
    public Slider DotSizeSlider;
    public FlexibleColorPicker DotColorPicker;

    public Image BackDotImage;
    public Slider BackDotSizeSlider;
    public FlexibleColorPicker BackDotColorPicker;


    private static Color reticlecolor = new Color(1,1,1,1);
    private static float reticlesize = 1;

    private static Color dotcolor = new Color(1, 1, 1, 1);
    private static float dotsize = 1;

    private static Color backdotcolor = new Color(1, 1, 1, 1);
    private static float backdotsize = 1;


    



    void Start() {

        // èâä˙ê›íË
        ReticleSizeSlider.onValueChanged.AddListener(UpdateReticleSize);
        ReticleColorPicker.onColorChange.AddListener(UpdateReticleColor);

        DotSizeSlider.onValueChanged.AddListener(UpdateDotSize);
        DotColorPicker.onColorChange.AddListener(UpdateDotColor);

        BackDotSizeSlider.onValueChanged.AddListener(UpdateBackDotSize);
        BackDotColorPicker.onColorChange.AddListener(UpdateBackDotColor);
       
        
    }

    private void Update() {

        ReticleImage.color = reticlecolor;
        ReticleImage.rectTransform.sizeDelta = new Vector2(reticlesize, reticlesize);
        DotImage.color = dotcolor;
        DotImage.rectTransform.sizeDelta = new Vector2(dotsize, dotsize);
        BackDotImage.color = backdotcolor;
        BackDotImage.rectTransform.sizeDelta = new Vector2(backdotsize, backdotsize);

    }

    void UpdateReticleColor(Color newColor) {
        reticlecolor = newColor;
        ReticleImage.color = reticlecolor;
    }

    void UpdateReticleSize(float newSize) {
        reticlesize = newSize;
        ReticleImage.rectTransform.sizeDelta = new Vector2(reticlesize, reticlesize);
    }

    void UpdateDotColor(Color newColor) {
        dotcolor = newColor;
        DotImage.color = dotcolor;

    }

    void UpdateDotSize(float newSize) {

        dotsize = newSize;
        DotImage.rectTransform.sizeDelta = new Vector2(dotsize, dotsize);
    }


    void UpdateBackDotColor(Color newColor) {

        backdotcolor = newColor;
        BackDotImage.color = backdotcolor;
    }


    void UpdateBackDotSize(float newSize) {

        backdotsize = newSize;
        BackDotImage.rectTransform.sizeDelta = new Vector2(backdotsize, backdotsize);
    }



   
}
