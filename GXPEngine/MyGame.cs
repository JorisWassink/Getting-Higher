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

	public RotatingSpaceship ():base (800,600, false, false)
	{
		_spaceship = new Sprite ("spaceship.png");
		_spaceship.SetOrigin (_spaceship.width / 2, _spaceship.height / 2);
		_spaceship.SetXY (width / 2, height / 2);
		AddChild (_spaceship);

		_spaceship.rotation = 270;

		_text = new EasyDraw (200,20);
		_text.TextAlign (CenterMode.Min, CenterMode.Min);
		_text.Text ("Rotation: 0",0,0);
		AddChild (_text);
	}

	void Update ()
	{
		if (Input.GetKey(Key.A))
		{
			_autoRotateLeft = true;
			_move = true;
			if (_spaceship.rotation <= 190)
			{
				_autoRotateLeft = false;
			}
		}
		else
		{
			_autoRotateLeft = false;
            _move = false;
		}
		if (Input.GetKey(Key.D))
		{
			_autoRotateRight = true;
			_move = true;
            if (_spaceship.rotation >= 350)
            {
                _autoRotateRight = false;
            }
        }
		else if (Input.GetKey(Key.A))
		{
            _autoRotateRight = false;
        }
		else
		{
			_autoRotateRight = false;
			_move = false;
		}

		if (_autoRotateLeft)
		{
			_spaceship.rotation -= 1.5f;
		}
        if (_autoRotateRight)
        {
            _spaceship.rotation += 1.5f;
        }

        // move in current direction:
        if (_move) {
            if (Input.GetKey(Key.A) && Input.GetKey(Key.D)){
                _spaceship.Move(4, 0);
            }
			else _spaceship.Move (2, 0);
		}

		_text.Clear (Color.Transparent);
		_text.Text("Rotation:" + _spaceship.rotation,0,0);
	}

	static void Main() {
		new RotatingSpaceship ().Start ();
	}
}