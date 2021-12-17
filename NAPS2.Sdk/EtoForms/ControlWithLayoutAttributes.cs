using Eto.Drawing;
using Eto.Forms;

namespace NAPS2.EtoForms;

public class ControlWithLayoutAttributes : LayoutElement
{
    public ControlWithLayoutAttributes(Control? control)
    {
        Control = control;
    }
        
    public ControlWithLayoutAttributes(
        ControlWithLayoutAttributes control, bool? center = null, bool? xScale = null, bool? yScale = null,
        bool? autoSize = null, Padding? padding = null, Size? spacing = null)
    {
        Control = control.Control;
        Center = center ?? control.Center;
        XScale = xScale ?? control.XScale;
        YScale = yScale ?? control.YScale;
        AutoSize = autoSize ?? control.AutoSize;
        Padding = padding ?? control.Padding;
        Spacing = spacing ?? control.Spacing;
    }
        
    public static implicit operator ControlWithLayoutAttributes(Control control) =>
        new ControlWithLayoutAttributes(control);

    private Control? Control { get; }
    private bool Center { get; }
    private bool? XScale { get; }
    private bool? YScale { get; }
    private bool AutoSize { get; }
    private Padding? Padding { get; }
    private Size? Spacing { get; }

    public override void AddTo(DynamicLayout layout)
    {
        if (AutoSize)
        {
            layout.AddAutoSized(Control, xscale: XScale, yscale: YScale, centered: Center, padding: Padding, spacing: Spacing);
        }
        else if (Center)
        {
            layout.AddCentered(Control, xscale: XScale, yscale: YScale, padding: Padding, spacing: Spacing);
        }
        else
        {
            if (Padding != null || Spacing != null)
            {
                throw new InvalidOperationException("Padding and Spacing aren't supported on controls except with AutoSize and/or Center.");
            }
            layout.Add(Control, xscale: XScale, yscale: YScale);
        }
    }
}