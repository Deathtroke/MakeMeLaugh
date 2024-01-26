using Godot;
using System;

public partial class CardUI : Control
{
	[Signal]
	public delegate void ReparentEventHandler(CardUI ui);
	
	public ColorRect color;
	public Label state;
	private CardStateMachine stateMachine;
	
	public override void _Ready()
	{
		color = GetNode<ColorRect>("ColorRect");
		state = GetNode<Label>("Label");
		
		
		stateMachine = GetNode<CardStateMachine>("CardState");
		stateMachine.init(this);
		GuiInput += on_gui_input;
		MouseEntered += on_mouse_enter;
		MouseExited += on_mouse_exit;
	}

	void _input(InputEvent e)
	{

		stateMachine.on_input(e);
	}
	
	void on_gui_input(InputEvent e)
	{
		stateMachine.on_gui_input(e);
	}
	
	void on_mouse_enter()
	{
		stateMachine.on_mouse_enter();
	}

	void on_mouse_exit()
	{
		stateMachine.on_mouse_exit();
	}

}
