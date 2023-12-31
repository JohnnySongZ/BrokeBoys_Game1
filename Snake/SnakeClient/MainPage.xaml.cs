﻿using NetworkUtil;

namespace SnakeGame;
using GameController;
using System.Net;

public partial class MainPage : ContentPage
{
    GameController controller = new GameController();
    private SocketState? server;

    public MainPage()
    {
        InitializeComponent();
        graphicsView.Invalidate();

      //  controller.OnMessageArrived +=
    }

    void OnTapped(object sender, EventArgs args)
    {
        keyboardHack.Focus();
    }

    void OnTextChanged(object sender, TextChangedEventArgs args)
    {
        Entry entry = (Entry)sender;
        String text = entry.Text.ToLower();
        if (text == "w")
        {

        }
        else if (text == "a")
        {
            // Move left
        }
        else if (text == "s")
        {
            // Move down
        }
        else if (text == "d")
        {
            // Move right
        }
        entry.Text = "";
    }

    private void NetworkErrorHandler()
    {
        DisplayAlert("Error", "Disconnected from server", "OK");
    }

    private void HandleConnected()
    {
        //Dispatcher.Dispatch(() => 
    }


    /// <summary>
    /// Event handler for the connect button
    /// We will put the connection attempt interface here in the view.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void ConnectClick(object sender, EventArgs args)
    {
        if (serverText.Text == "")
        {
            DisplayAlert("Error", "Please enter a server address", "OK");
            return;
        }
        if (nameText.Text == "")
        {
            DisplayAlert("Error", "Please enter a name", "OK");
            return;
        }
        if (nameText.Text.Length > 16)
        {
            DisplayAlert("Error", "Name must be less than 16 characters", "OK");
            return;
        }
        keyboardHack.Focus();

        connectButton.IsEnabled = false;
        serverText.IsEnabled = false;
        controller.GetName(nameText.Text);
        Networking.ConnectToServer(controller.OnConnect, serverText.Text, 11000);
    }


    /// <summary>
    /// Use this method as an event handler for when the controller has updated the world
    /// </summary>
    public void OnFrame()
    {
        Dispatcher.Dispatch(() => graphicsView.Invalidate());
    }

    private void ControlsButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Controls",
                     "W:\t\t Move up\n" +
                     "A:\t\t Move left\n" +
                     "S:\t\t Move down\n" +
                     "D:\t\t Move right\n",
                     "OK");
    }

    private void AboutButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("About",
      "SnakeGame solution\nArtwork by Jolie Uk and Alex Smith\nGame design by Daniel Kopta and Travis Martin\n" +
      "Implementation by ...\n" +
        "CS 3500 Fall 2022, University of Utah", "OK");
    }

    private void ContentPage_Focused(object sender, FocusEventArgs e)
    {
        if (!connectButton.IsEnabled)
            keyboardHack.Focus();
    }
}