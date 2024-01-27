using Godot;
using System;
using System.Diagnostics;

public partial class CardUI : Control
{
	[Signal]
	public delegate void ReparentEventHandler(CardUI ui);
	
	public ColorRect color;
	public Label state;
	private CardStateMachine stateMachine;
	public Area2D drop_point;
	public bool hovered;

	[Export] private Card card;
	
	public override void _Ready()
	{
		color = GetNode<ColorRect>("ColorRect");
		state = GetNode<Label>("Label");
		drop_point = GetNode<Area2D>("DropPoint");
		
		stateMachine = GetNode<CardStateMachine>("CardState");
		stateMachine.init(this);
		GuiInput += on_gui_input;
		MouseEntered += on_mouse_enter;
		MouseExited += on_mouse_exit;

		drop_point.MouseEntered += on_mouse_enter;
		drop_point.MouseExited += on_mouse_exit;

		hovered = false;
	}

	public override void _Input(InputEvent e)
	{
		stateMachine.on_input(e);
	}
	
	void on_gui_input(InputEvent e)
	{
		stateMachine.on_gui_input(e);
	}
	
	void on_mouse_enter()
	{
		hovered = true;
		stateMachine.on_mouse_enter();
	}

	void on_mouse_exit()
	{
		hovered = false;
		stateMachine.on_mouse_exit();
	}
}
