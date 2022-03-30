#pragma once
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <iostream>

class Window
{
    int xOnScene;
    int yOnScene;
public:
    sf::RenderWindow Renderer;
    Window(sf::VideoMode videoMode, std::string NameOfWindow) {
        Renderer.create(videoMode, NameOfWindow);
        xOnScene = 0;
        yOnScene = 0;
    }
    Window(sf::VideoMode videoMode, std::string NameOfWindow, int xOnScene, int yOnScene) {
        Renderer.create(videoMode, NameOfWindow);
        this->xOnScene = xOnScene;
        this->yOnScene = yOnScene;
    }
    void SetRenderWindow(sf::VideoMode videoMode, std::string NameOfWindow) {
        Renderer.clear();
        Renderer.create(videoMode, NameOfWindow);
    }
    void SetCoordsOnScene(int x, int y) {
        xOnScene = x;
        yOnScene = y;
    }
};

