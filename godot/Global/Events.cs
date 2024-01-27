using Godot;
using System;

public partial class Events : Node
{
	[Signal]
	public delegate void AimStartEventHandler(CardUI ui);
	
	[Signal]
	public delegate void AimEndEventHandler(CardUI ui);
}