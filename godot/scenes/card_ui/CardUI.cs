using Godot;
using System;
using System.Collections.Generic;
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
	public List<Node> targets = new List<Node>();
	private Tween tween;
	public Control parent;
	
	[Export] public Card card;

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
	}

	void on_mouse_exit()
	{
		hovered = false;
		stateMachine.on_mouse_exit();
	}
}
