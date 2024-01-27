using Godot;
using System;
using System.Diagnostics;

public partial class CardDragState : CardState
{
    public override void Enter()
    {
        var ui_layer = GetTree().GetFirstNodeInGroup("ui_layer");
        if (ui_layer != null)
        {
            Reparent(ui_layer);
        }
        

        c_ui.color.Color = Godot.Color.Color8(0, 0, 255);
        c_ui.state.Text = "drag";
    }

    public override void on_gui_input(InputEvent e)
    {
        var mouse_motion = e is InputEventMouseMotion;
        var cancel = e.IsActionPressed("right_mouse");
        var confirm = e.IsActionPressed("left_mouse");
        
        if (mouse_motion)
            c_ui.GlobalPosition = c_ui.GetGlobalMousePosition() - c_ui.PivotOffset;

        if (cancel)
        {
            EmitSignal(SignalName.Transition, this, (int)State.Idle);
        }

        if (confirm)
        {
            c_ui.hovered = false;
            EmitSignal(SignalName.Transition, this, (int)State.Released);
        }
    }
}
