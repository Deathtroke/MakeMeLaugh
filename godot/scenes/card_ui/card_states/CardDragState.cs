using Godot;
using System;

public partial class CardDragState : CardState
{
    void enter()
    {
        var ui_layer = GetTree().GetFirstNodeInGroup("ui_layer");
        if (ui_layer != null)
        {
            this.Reparent(ui_layer);
        }
        

        c_ui.color.Color = Godot.Color.FromHtml("NAVY_BLUE");
        c_ui.state.Text = "drag";
    }

    void on_gui_input(InputEvent e)
    {
        var mouse_motion = e is InputEventMouseMotion;
        var cancel = e.IsActionPressed("right_mouse");
        var confirm = e.IsActionReleased("left_mouse") || e.IsActionPressed("left_mouse");
        
        if (mouse_motion)
            c_ui.GlobalPosition = c_ui.GetGlobalMousePosition() - c_ui.PivotOffset;

        if (cancel)
        {
            EmitSignal(SignalName.Transition, this, (int)State.Idle);
        }

        if (confirm)
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(SignalName.Transition, this, (int)State.Released);
        }
    }
}
