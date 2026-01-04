using Godot;
using System;

public partial class Enemy : Area2D
{
	// Ezeket az értékeket a Godot felületén is tudod majd állítani
	[Export] public Vector2 MoveDirection = new Vector2(200, 0); // Mennyit mozogjon el
	[Export] public float Speed = 2.0f; // Sebesség (mennyi idő alatt érjen oda)

	private Vector2 _startPos;
	private Vector2 _targetPos;
	private float _time = 0;

	public override void _Ready()
	{
		// Elmentjük, honnan indul
		_startPos = Position;
		// Kiszámoljuk, hová érkezzen meg
		_targetPos = _startPos + MoveDirection;

		// Összekötjük az eseményt: "Ha valami hozzáér az őrhöz"
		BodyEntered += OnEnemyBodyEntered;
	}

	public override void _Process(double delta)
	{
		// Ez a rész mozgatja oda-vissza (szinusz hullám segítségével)
		_time += (float)delta * Speed;
		float oscillation = (MathF.Sin(_time) + 1.0f) / 2.0f;
		
		// Kiszámolja a két pont közötti aktuális helyzetet
		Position = _startPos.Lerp(_targetPos, oscillation);
	}

	private void OnEnemyBodyEntered(Node2D body)
	{
		// Megnézzük, hogy a "Player" nevű objektum ért-e hozzá
		if (body.Name == "player")
		{
			GD.Print("Bumm! Elkapott az őr!");
			// Újraindítja az aktuális pályát
			GetTree().ReloadCurrentScene();
		}
	}
}
