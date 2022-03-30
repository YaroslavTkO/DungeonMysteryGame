#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <iostream>

class Sprite
{
	sf::Texture texture;
	sf::Sprite sprite;


	Sprite(std::string pathToImage, sf::IntRect rectOnImageToUse) {
		if (!texture.loadFromFile(pathToImage)) {
			std::cout << "Not loaded" << std::endl;
		}
		sprite.setTextureRect(rectOnImageToUse);
	}

}
