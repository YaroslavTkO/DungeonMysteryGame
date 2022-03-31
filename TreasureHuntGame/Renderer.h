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
    Window(int xOnScene, int yOnScene) {
        this->xOnScene = xOnScene;
        this->yOnScene = yOnScene;
        SetFullscreenWindow();
    }
    Window() {
        xOnScene = 0;
        yOnScene = 0;
        SetFullscreenWindow();
    }
    void SetFullscreenWindow() {
        auto x = sf::VideoMode::getDesktopMode().width;
        auto y = sf::VideoMode::getDesktopMode().height;
        Renderer.clear();
        Renderer.create(sf::VideoMode(x, y), "New Title", sf::Style::Fullscreen);
    }
    void MoveWindowOnScene(int xPixelsToMove, int yPixelsToMove) {

    }
  /*  void SetRenderWindow(sf::VideoMode videoMode, std::string NameOfWindow) {
        Renderer.clear();
        Renderer.create(videoMode, NameOfWindow);
    }*/
    void SetCoordsOnScene(int x, int y) {
        xOnScene = x;
        yOnScene = y;
    }
    int X() {
        return xOnScene;
    }
    int Y() {
        return yOnScene;
    }
};

