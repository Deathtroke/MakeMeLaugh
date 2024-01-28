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
	private Card _last_card;

	public void init(CardUI card)
	{
		states = new Dictionary<CardState.State, CardState>();
		
		var idle = GetNode<CardState>("CardIdleState");
		hande_init_state(idle, card);
		
		var clicked = GetNode<CardState>("CardClickedState");
		hande_init_state(clicked, card);
		
		var drag = GetNode<CardState>("CardDragState");
		hande_init_state(drag, card);
		
		var release = GetNode<CardState>("CardReleaseState");
		hande_init_state(release, card);
		
		var aiming = GetNode<CardState>("CardAimingState");
		hande_init_state(aiming, card);
		


		if (initial_state != null)
		{
			initial_state.Enter();
			current_state = initial_state;
		}

	}

	private void hande_init_state(CardState c_state, CardUI card)
	{
		try
		{
			states.Add(c_state.state, c_state);
		}
		catch (ArgumentException)
		{
			Console.WriteLine("An element with Key = \"txt\" already exists.");
		}
		_last_card = card.card;
		c_state.Transition += on_transition_req;
		c_state.c_ui = card;
	}
	
	public void on_input(InputEvent e)
	{

		current_state?.on_gui_input(e);
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
			Debug.Print("This is not supposed to happen");
			return;
		}

		if (to == CardState.State.Released)
		{
			PlayerHandler playerHandler = GetTree().GetFirstNodeInGroup("playerhandler") as PlayerHandler;
			playerHandler.OnCardReleased(_last_card);
			Speech bubble = GetTree().GetFirstNodeInGroup("speechbubble") as Speech;
			bubble.display();
			SpriteChanger spriteChanger = GetTree().GetFirstNodeInGroup("spritechanger") as SpriteChanger;
			spriteChanger.show_attack();
		}
		
		CardState new_state = states[to];
		
		current_state?.Exit();
		current_state = new_state;
		current_state.Enter();
	}
}
