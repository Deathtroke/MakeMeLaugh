using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot.Collections;

public partial class CardUI : Control
{
	[Signal]
	public delegate void ReparentEventHandler(CardUI ui);

	public Label state;
	public TextureRect icon;
	public Panel panel;
	private CardStateMachine stateMachine;
	public Area2D drop_point;
	public bool hovered;
	public Array<Node> targets = new Array<Node>();
	private Tween tween;
	public Control parent;

	[Export] public StyleBox default_style;
	[Export] public StyleBox hover_style;
	[Export] public StyleBox drag_style;
	
	[Export] public Card card;

	[Export] public CharacterStats Char_stats;

	public override void _Ready()
	{
		state = GetNode<Label>("Label");
		drop_point = GetNode<Area2D>("DropPoint");
		icon = GetNode<TextureRect>("Icon");
		panel = GetNode<Panel>("Panel");

		icon.Texture = card.icon;
		
		stateMachine = GetNode<CardStateMachine>("CardState");
		stateMachine.init(this);
		GuiInput += on_gui_input;
		MouseEntered += on_mouse_enter;
		MouseExited += on_mouse_exit;

		drop_point.MouseEntered += on_mouse_enter;
		drop_point.MouseExited += on_mouse_exit;

		hovered = false;

		parent = GetParent<BoxContainer>();
	}

	public override void _Input(InputEvent e)
	{
		stateMachine.on_input(e);
	}

	void on_gui_input(InputEvent e)
	{
		stateMachine.on_gui_input(e);
	}

	public void animate_to_position(Vector2 new_position, float duration)
	{
		tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "global_position", new_position, duration);
	}

	void on_mouse_enter()
	{
		hovered = true;
		stateMachine.on_mouse_enter();
		panel.Set("theme_override_styles/panel", hover_style);

	}

	void on_mouse_exit()
	{
		hovered = false;
		stateMachine.on_mouse_exit();
		panel.Set("theme_override_styles/panel", default_style);
	}

	public void play()
	{
		var hand = GetTree().GetFirstNodeInGroup("hand");
		targets.Add(hand);
		card.play(targets, Char_stats);
		QueueFree();
	}
}
