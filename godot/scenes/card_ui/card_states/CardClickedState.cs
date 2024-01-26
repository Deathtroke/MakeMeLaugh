using Godot;
using System;

public partial class CardClickedState : CardState
{
    void enter()
    {
        EmitSignal(CardUI.SignalName.Reparent, c_ui);
        c_ui.color.Color = Godot.Color.FromHtml("ORANGE");
        c_ui.state.Text = "clicked";
        
    }

    void on_gui_input(InputEvent e)
    {
        if (e is InputEventMouseMotion)
        {
            EmitSignal(SignalName.Transition, this, (int)State.Drag);
        }
    }
}
