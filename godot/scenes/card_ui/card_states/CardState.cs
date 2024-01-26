using Godot;
using System;

public partial class CardState : Node
{
	[Signal]
	public delegate void TransitionEventHandler(CardState from, State to);
	
	public enum State
	{
		Idle,
		Clicked,
		Drag,
		Released
	}

	[Export]
	public State state;
	
	public CardUI c_ui;

	public void enter()
	{
		
	}

	public void exit()
	{
		
	}

	public void on_input(InputEvent e)
	{
		
	}
	
	public void on_gui_input(InputEvent e)
	{
		
	}

	public void on_mouse_enter()
	{
		
	}

	public void on_mouse_exit()
	{
		
	}
}
