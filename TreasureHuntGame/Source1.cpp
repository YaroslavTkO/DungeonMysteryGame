#include <SFML/Graphics.hpp>


using namespace sf;


float offsetX = 0, offsetY = 0;


const int H = 25;
const int W = 54;


String TileMap[H] = {
"000000000000000000000000000000000000000000000000000000",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"k                                                    r",
"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP",
"                                                      ",
"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP",
 
};




class PLAYER {

public:

	float dx, dy;
	FloatRect rect;
	bool onGround;
	Sprite sprite;
	float currentFrame;

	PLAYER(Texture& image)
	{
		sprite.setTexture(image);
		rect = FloatRect(100, 180, 16, 16);

		dx = dy = 0.1;
		currentFrame = 0;
	}


	void update(float time)
	{

		rect.left += dx * time;
		Collision(0);


		if (!onGround) dy = dy + 0.0005 * time;
		rect.top += dy * time;
		onGround = false;
		Collision(1);


		currentFrame += time * 0.005;
		if (currentFrame > 3) currentFrame -= 3;


		if (dx > 0) sprite.setTextureRect(IntRect(112 + 31 * int(currentFrame), 144, 16, 16));
		if (dx < 0) sprite.setTextureRect(IntRect(112 + 31 * int(currentFrame) + 16, 144, -16, 16));


		sprite.setPosition(rect.left - offsetX, rect.top - offsetY);

		dx = 0;
	}


	void Collision(int num)
	{

		for (int i = rect.top / 16; i < (rect.top + rect.height) / 16; i++)
			for (int j = rect.left / 16; j < (rect.left + rect.width) / 16; j++)
			{
				if ((TileMap[i][j] == 'P') || (TileMap[i][j] == 'k') || (TileMap[i][j] == '0') || (TileMap[i][j] == 'r') )
				{
					if (dy > 0 && num == 1)
					{
						rect.top = i * 16 - rect.height;  dy = 0;   onGround = true;
					}
					if (dy < 0 && num == 1)
					{
						rect.top = i * 16 + 16;   dy = 0;
					}
					if (dx > 0 && num == 0)
					{
						rect.left = j * 16 - rect.width;
					}
					if (dx < 0 && num == 0)
					{
						rect.left = j * 16 + 16;
					}
				}

				
			}

	}

};



class ENEMY
{

public:
	float dx, dy;
	FloatRect rect;
	Sprite sprite;
	float currentFrame;
	


	void set(Texture& image, int x, int y)
	{
		sprite.setTexture(image);
		rect = FloatRect(x, y, 16, 16);

		dx = 0.05;
		currentFrame = 0;
		
	}

	void update(float time)
	{
		rect.left += dx * time;

		Collision();


		currentFrame += time * 0.005;
		if (currentFrame > 2) currentFrame -= 2;

		sprite.setTextureRect(IntRect(18 * int(currentFrame), 0, 16, 16));
		


		sprite.setPosition(rect.left - offsetX, rect.top - offsetY);

	}


	void Collision()//колайдер
	{

		for (int i = rect.top / 16; i < (rect.top + rect.height) / 16; i++)
			for (int j = rect.left / 16; j < (rect.left + rect.width) / 16; j++)
				if ((TileMap[i][j] == 'P') || (TileMap[i][j] == '0')|| (TileMap[i][j] == 'r') || (TileMap[i][j] == 'k'))
				{
					if (dx > 0)
					{
						rect.left = j * 16 - rect.width; dx *= -1;
					}
					else if (dx < 0)
					{
						rect.left = j * 16 + 16;  dx *= -1;
					}

				}
	}

};



int main()
{

	RenderWindow window(VideoMode(865, 375), "test!");

	Texture tileSet;
	tileSet.loadFromFile("454.png");


	PLAYER player(tileSet);
	
	Sprite tile(tileSet);


	

	Clock clock;

	while (window.isOpen())
	{

		float time = clock.getElapsedTime().asMicroseconds();
		clock.restart();

		time = time / 500; 

		if (time > 20) time = 20;

		Event event;
		while (window.pollEvent(event))
		{
			if (event.type == Event::Closed)
				window.close();
		}


		if (Keyboard::isKeyPressed(Keyboard::Left))    player.dx = -0.1;

		if (Keyboard::isKeyPressed(Keyboard::Right))    player.dx = 0.1;

		if (Keyboard::isKeyPressed(Keyboard::Up))	if (player.onGround) { player.dy = -0.27; player.onGround = false;  }



		player.update(time);
	

	


		//if (Mario.rect.left > 100) offsetX = Mario.rect.left -100; // нестирати!!





		window.clear(Color(Color::Black));

		for (int i = 0; i < H; i++)
			for (int j = 0; j < W; j++)
			{
				if (TileMap[i][j] == 'P')  tile.setTextureRect(IntRect(240, 720, 16*2, 16*2));

				if (TileMap[i][j] == 'k')  tile.setTextureRect(IntRect(175, 672, 16, 16));

				if (TileMap[i][j] == 'r')  tile.setTextureRect(IntRect(512 , 672, 16, 16));

				if ((TileMap[i][j] == ' ') || (TileMap[i][j] == '0')) continue;

				tile.setPosition(j * 16 - offsetX, i * 16 - offsetY);
				window.draw(tile);
			}



		window.draw(player.sprite);
		

		window.display();
	}

	return 0;
}



