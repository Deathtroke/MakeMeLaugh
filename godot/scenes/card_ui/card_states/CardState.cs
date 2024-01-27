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

	public virtual void Enter()
	{
		
	}

	public virtual void Exit()
	{
		
	}

	public virtual void on_input(InputEvent e)
	{
		
	}
	
	public virtual void on_gui_input(InputEvent e)
	{
		
	}

	public virtual void on_mouse_enter()
	{
		
	}

	public virtual void on_mouse_exit()
	{
		
	}
}
