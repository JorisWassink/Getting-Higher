using System;
using System.Drawing;
using GXPEngine;

public class RotatingSpaceship : Game
{
	Sprite _spaceship;
	EasyDraw _text;
	bool _autoRotateLeft = false;
    bool _autoRotateRight = false;
    bool _move =false;

	public RotatingSpaceship ():base (1920,1080, false, false)
	{
		Player player = new Player(width/2, height/2);
		AddChild (player);
    }


        
    

	static void Main() {
		new RotatingSpaceship ().Start ();
	}
}