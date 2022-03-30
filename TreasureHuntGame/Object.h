#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include "Sprite.h"

class Object {
public:
	Position position;
	Size size;

	virtual ~Object()  = 0;
};

struct Position {
public:
	int x;
	int y;
};
struct Size {
public:
	int width;
	int height;
};
