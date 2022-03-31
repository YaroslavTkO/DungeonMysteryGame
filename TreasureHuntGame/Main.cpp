#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <iostream>

#include "Renderer.h"
#include "Scene.h"
#include "Textures.h"
#include "Sprite.h"
//#include "Object.h"


sf::CircleShape MouseRound(sf::Vector2i mousePos);
int main()
{
	TexturesDatabase texture;
	sf::Texture texture1;
	texture1.loadFromFile("Assets/Art/plus.png");
	texture.AddTextureToDatabase(texture1);
	Window window(0, 0);
	SpriteClass sprite(texture1);
	sf::CircleShape circle(100);
	circle.setOrigin(sf::Vector2f(100, 100));
	circle.setFillColor(sf::Color::Black);
	circle.setPosition(window.Renderer.getSize().x / 2, window.Renderer.getSize().y / 2);
	std::cout << circle.getOrigin().x;
	while (window.Renderer.isOpen()) {
		sf::Event event;
		while (window.Renderer.pollEvent(event)) {
			if (event.type == sf::Event::EventType::Closed)
				window.Renderer.close();
			else if (event.type == sf::Event::Resized) {
				//window.SetRenderWindow(sf::VideoMode(window.Renderer.getSize().x, window.Renderer.getSize().y), "new Name");
			}
		}
		window.Renderer.clear(sf::Color::White);
		window.Renderer.draw(circle);
		window.Renderer.draw(sprite.GetSprite());
		window.Renderer.draw(MouseRound(sf::Mouse::getPosition(window.Renderer)));
		window.Renderer.display();

	}
	std::cout << "Hello world";
}

sf::CircleShape MouseRound(sf::Vector2i mousePos) {

	sf::CircleShape colo(20);
	colo.setFillColor(sf::Color::Red);
	auto screen = sf::VideoMode::getDesktopMode();
	colo.setPosition(sf::Vector2f(mousePos.x - 20, mousePos.y - 20));
	return colo;
}