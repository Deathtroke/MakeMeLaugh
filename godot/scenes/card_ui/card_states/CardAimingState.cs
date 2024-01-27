using Godot;
using System;
using System.Collections.Generic;

public partial class CardAimingState : CardState
{
    private const int mouse_y_cancel = 128;
    
	const float drag_min_threshold = 0.05f;
    private bool drag_time_passed = false;
    public override void Enter()
    {
        c_ui.color.Color = Godot.Color.Color8(0, 0, 155);
        c_ui.state.Text = "aiming";

        c_ui.targets = new List<Node>();
        
        var offset = Vector2(c_ui.GetParent().)

        var timer = GetTree().CreateTimer(drag_min_threshold, false);
        timer.Timeout += () => drag_time_passed = true;
    }

    public override void on_gui_input(InputEvent e)
    {
        var mouse_motion = e is InputEventMouseMotion;
        var cancel = e.IsActionPressed("right_mouse");
        var confirm = e.IsActionPressed("left_mouse") || e.IsActionReleased("left_mouse");
        
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
            }
            EmitSignal(SignalName.Transition, this, (int)State.Idle);
        }
        else if (confirm && drag_time_passed)
        {
            if (c_ui.GetGlobalMousePosition().Y < 700)
            {
                GetViewport().SetInputAsHandled();
                c_ui.hovered = false;
                EmitSignal(SignalName.Transition, this, (int)State.Released);
            }
            else
            {
                var hand = GetTree().GetFirstNodeInGroup("hand");
                if (hand is BoxContainer box)
                {
                    c_ui.GetParent().RemoveChild(c_ui);
                    box.AddChild(c_ui);
                    c_ui.PivotOffset = Vector2.Zero;
                }
                EmitSignal(SignalName.Transition, this, (int)State.Idle);
            }
        }
    }
}
