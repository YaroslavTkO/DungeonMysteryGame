#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <iostream>

#include "Object.h"

class SpriteClass
{
	sf::Sprite sprite;

public:
	SpriteClass(sf::Texture &texture, sf::IntRect rectOnImageToUse) {
		sprite.setTexture(texture);
		sprite.setTextureRect(rectOnImageToUse);
	}
	SpriteClass(sf::Texture &texture) {
		sprite.setTexture(texture);
	}
	void ChangeSprite(sf::Texture &texture, sf::IntRect rectOnImageToUse) {
		sprite.setTexture(texture);
		sprite.setTextureRect(rectOnImageToUse);
	}
	void ChangeSprite(sf::Texture &texture) {
		sprite.setTexture(texture);
	}
	void MoveOnImage(sf::IntRect rectOnImageToUse) {
		sprite.setTextureRect(rectOnImageToUse);
	}
	sf::Sprite GetSprite() {
		return sprite;
	}
	/*	void SetSpriteSize(Size ObjectSize) {
			auto size = sprite.getLocalBounds();
			sprite.setScale(sf::Vector2f(ObjectSize.width / size.width, ObjectSize.height / size.height));
		}*/
};
