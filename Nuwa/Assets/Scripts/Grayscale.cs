using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable] //The Serializable attribute allows Unity to serialize this class and extend PostProcessEffectSettings.
/* The [PostProcess()] attribute tells Unity that this class holds post-processing data. 
 * The first parameter links the settings to a renderer. 
 * The second parameter creates the injection point for the effect. 
 * The third parameter is the menu entry for the effect. 
 * You can use a forward slash (/) to create sub-menu categories. */
[PostProcess(typeof(GrayscaleRenderer), PostProcessEvent.AfterStack, "Custom/Grayscale")] 
public sealed class Grayscale : PostProcessEffectSettings {
    // You can create boxed fields to override or blend parameters. This uses a FloatParameter with a fixed range from 0 to 1.
  [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
   public FloatParameter blend = new FloatParameter { value = 0.5f };
}
public sealed class GrayscaleRenderer : PostProcessEffectRenderer<Grayscale> {
   public override void Render(PostProcessRenderContext context) {
        // Request a PropertySheet for our shader and set the uniform within it.
       var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Grayscale"));
        // Send the blend parameter value to the shader.
       sheet.properties.SetFloat("_Blend", settings.blend);
        // This context provides a command buffer which you can use to blit a fullscreen pass using a source image as an input with a destination for the shader, sheet and pass number.
       context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
  }
}
