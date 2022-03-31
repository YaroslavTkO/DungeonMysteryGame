#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include "Sprite.h"

struct Position {
	int x = 0;
	int y = 0;
} point;
struct Size {
	int width = 0;
	int height = 0;
};

class Object {
	Position position;
	Size size;
	SpriteClass objectSprite;
public:

	Object(int x, int y, int width, int height, sf::Texture texture, sf::IntRect  textureRect) {
		SetPosition(x, y);
		SetSize(width, height);

	}
	Object(Position _position, Size _size, SpriteClass _objectSprite) {
		position = _position;
		size = _size;
		objectSprite = _objectSprite;
	}

	Position GetPosition() {
		return position;
	}
	Size GetSize() {
		return size;
	}
	SpriteClass ObjSprite() {
		return objectSprite;
	}
	void SetSprite(SpriteClass spriteClass) {
		objectSprite = spriteClass;
	}
	void SetSprite(sf::Texture& texture, sf::IntRect rectOnImageToUse) {
		objectSprite.ChangeSprite(texture, rectOnImageToUse);
	}
	void SetSprite(sf::Texture& texture) {
		objectSprite.ChangeSprite(texture);
	}
	void SetSprite(sf::IntRect rectOnImageToUse) {
		objectSprite.MoveOnImage(rectOnImageToUse);
	}
	void SetPosition(Position _position) {
		position = _position;
	}
	void SetPosition(int x, int y) {
		position.x = x;
		position.y = y;
	}
	void SetSize(Size _size) {
		size = _size;
	}
	void SetSize(int width, int height) {
		size.width = width;
		size.height = height;
	}
};


