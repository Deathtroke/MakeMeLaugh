using Godot;
using System;

public partial class CardClickedState : CardState
{
    public override void Enter()
    {
        EmitSignal(CardUI.SignalName.Reparent, c_ui);
        c_ui.color.Color = Godot.Color.Color8(150, 150, 0);
        c_ui.state.Text = "clicked";
        
    }

    public override void on_gui_input(InputEvent e)
    {
        if (e is InputEventMouseMotion)
        {
            EmitSignal(SignalName.Transition, this, (int)State.Drag);
        }
    }
}
