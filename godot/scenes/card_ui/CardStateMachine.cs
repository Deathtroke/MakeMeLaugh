using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class CardStateMachine : Node
{
	[Export] 
	private CardState? initial_state;

	private CardState current_state;
	private Dictionary<CardState.State, CardState> states;

	public void init(CardUI card)
	{
		Debug.Print(GetPath());
		
		var idle = GetNode<CardIdleState>("CardIdleState");
		Debug.Print("a" + (int)idle.state);
		hande_init_state(idle, card);
		
		var clicked = GetNode<CardState>("CardClickedState");
		Debug.Print("b" + (int)clicked.state);
		hande_init_state(clicked, card);
		
		var drag = GetNode<CardState>("CardDragState");
		Debug.Print("c" + (int)drag.state);
		hande_init_state(drag, card);
		
		var release = GetNode<CardState>("CardReleaseState");
		Debug.Print("d" + (int)release.state);
		hande_init_state(release, card);
		


		if (initial_state != null)
		{
			initial_state.enter();
			current_state = initial_state;
		}

	}

	private void hande_init_state(CardState state, CardUI card)
	{
		try
		{
			states.Add(state.state, state);
		}
		catch (ArgumentException)
		{
			Console.WriteLine("An element with Key = \"txt\" already exists.");
		}
		state.Transition += on_transition_req;
		state.c_ui = card;
	}
	
	public void on_input(InputEvent e)
	{

		current_state?.on_input(e);
	}
	
	public void on_gui_input(InputEvent e)
	{
		current_state?.on_gui_input(e);
	}
	
	public void on_mouse_enter()
	{
		current_state?.on_mouse_enter();
	}

	public void on_mouse_exit()
	{
		current_state?.on_mouse_exit();
	}

	void on_transition_req(CardState from, CardState.State to)
	{
		if (from != current_state)
		{
			return;
		}

		CardState new_state = states[to];
		
		current_state?.exit();
		current_state = new_state;
	}
}
