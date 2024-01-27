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
            c_ui.GetParent().RemoveChild(c_ui);
            ui_layer.AddChild(c_ui);
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
            var hand = GetTree().GetFirstNodeInGroup("hand");
            if (hand is BoxContainer box)
            {
                c_ui.GetParent().RemoveChild(c_ui);
                box.AddChild(c_ui);
                c_ui.PivotOffset = Vector2.Zero;
                box.Alignment = BoxContainer.AlignmentMode.Center;
            }
            EmitSignal(SignalName.Transition, this, (int)State.Idle);
        }

        if (confirm)
        {
            c_ui.hovered = false;
            EmitSignal(SignalName.Transition, this, (int)State.Released);
        }
    }
}
