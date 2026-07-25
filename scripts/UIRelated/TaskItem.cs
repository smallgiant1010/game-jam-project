using Godot;
using System;

public partial class TaskItem : PanelContainer
{
	public Label label_;
	public TextureRect textureRect_;
	public override void _Ready()
	{
		label_ = GetNode<Label>("TaskName");
		textureRect_ = GetNode<TextureRect>("TaskIcon");
	}
}
