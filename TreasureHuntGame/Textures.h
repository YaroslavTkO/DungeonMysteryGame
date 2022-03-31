#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <vector>

class TexturesDatabase {

	std::vector<sf::Texture> textures;
public:

	void AddTextureToDatabase(sf::Texture texture) {
		textures.push_back(texture);
	}
	sf::Texture GetTextureById(int id) {
		return textures.at(id);
	}
};