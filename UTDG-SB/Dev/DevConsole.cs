using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB.Dev
{
    class DevConsole
    {
        private Main game;
        public enum ConsoleState
        {
            Open,
            Closed
        };
        private ConsoleState currentState = ConsoleState.Closed;
        public bool IsOpen() { return currentState == ConsoleState.Open ? true : false; }
        private ConsoleState prevState;

        private int scrollSpeed = 25;

        private Rectangle bounds;
        private Texture2D backgroundTexture;
        private Texture2D selectedBackgroundTexture;
        private SpriteFont consoleFont;

        private KeyboardState prevKeyboard = Keyboard.GetState();

        public string currentLine;
        public int currentIndex = 0;
        private int maxLineLength = 48;

        private int currentBlinkTime = 0;
        private int cursorBlinkRate = 30;
        private bool displayBlink = true;
        private string cursorString = "";

        private int selectedIndex = 0;
        private int selectedLength = 0;

        List<string> ExcecutedCommands = new List<string>();

        List<string> BaseCommands = new List<string>{
                "player",
                "dummy"
            };
        List<string> playerCommands = new List<string>
        {
            "position",
            "speed"
        };
        List<string> dummyCommands = new List<string>
        {
            "position"
        };
        public DevConsole(Main game)
        {
            this.game = game;
            prevState = currentState;
            backgroundTexture = game.textureHandler.consoleBG;
            selectedBackgroundTexture = game.textureHandler.consoleSelectedBG;
            consoleFont = game.fontHandler.devFont;
            bounds = new Rectangle(0, -300, 500, 300);
            currentLine = "";
        }

        public void Open()
        {
            currentState = ConsoleState.Open;
        }   
        
        public void Close()
        {
            currentState = ConsoleState.Closed;
        }
        
        public void ExcecuteCommand(string command)
        {
            if (command.Contains('='))
            {
                string leftHand = command.Split('=')[0].Trim();
                string rightHand = command.Split('=')[1].Trim();
                float rightHandVal;
                try
                {
                    rightHandVal = (float)Convert.ToDouble(rightHand);
                    string baseCmd = leftHand.Split('.')[0];
                    string actionCmd = leftHand.Split('.')[1];

                    if (leftHand.ToLower() == "camera.scale")
                    {
                        game.camera.scale = rightHandVal;
                    }
                    if (leftHand.ToLower() == "dev.mode")
                    {
                        if (rightHandVal == 1)
                        {
                            game.devMode = true;
                            ExcecutedCommands.Add("Devmode turned on.");
                        }
                        else if (rightHandVal == 0)
                        {
                            game.devMode = false;
                            ExcecutedCommands.Add("Devmode turned off.");
                        }
                        else
                            ExcecutedCommands.Add("invalid devmode value. try 1 or 0.");
                    }
                    if (leftHand.ToLower() == "player.maxvel")
                    {
                        game.player.physicsHandler.maxVelocity = rightHandVal;
                        ExcecutedCommands.Add("Player Max Velocity set to " + rightHandVal + ".");
                    }
                    else if (leftHand.ToLower() == "player.acceleration")
                    {
                        game.player.physicsHandler.accelerationValue = rightHandVal;
                        ExcecutedCommands.Add("Player Acceleration Value set to " + rightHandVal + ".");
                    }
                }
                catch
                {
                    ExcecutedCommands.Add("This command was invalid.");
                }
            }
            else
            {
                if (command.ToLower().Contains("content.reload"))
                {
                    game.ReloadContent();
                }
            }
        }

        public void HandleInput(TextInputEventArgs e)
        {
            displayBlink = true;
            currentBlinkTime = 0;
            switch (e.Key)
            {
                case (Keys.Tab):
                    if (currentLine.Length < maxLineLength)
                    {
                        if (selectedLength > 0)
                        {
                            currentLine = currentLine.Remove(selectedIndex, selectedLength);
                            selectedLength = 0;
                            currentIndex = selectedIndex;
                        }
                        currentLine = currentLine.Insert(currentIndex, "   ");
                    }
                    currentIndex += 3;
                    break;
                case (Keys.Back):
                    //backspace;
                    if(selectedLength > 0)
                    {
                        currentLine = currentLine.Remove(selectedIndex, selectedLength);
                        selectedLength = 0;
                        currentIndex = selectedIndex;
                    }
                    else if (currentIndex > 0) {
                        currentLine = currentLine.Remove(currentIndex - 1, 1);
                        currentIndex--;
                    }
                    break;
                default:
                    if (consoleFont.Characters.Contains(e.Character) && currentLine.Length < maxLineLength)
                    {
                        if (selectedLength > 0)
                        {
                            currentLine = currentLine.Remove(selectedIndex, selectedLength);
                            selectedLength = 0;
                            currentIndex = selectedIndex;
                        }
                        currentLine = currentLine.Insert(currentIndex, e.Character.ToString());
                        currentIndex++;
                    }
                    break;
            }
        }

        public void Clear()
        {

        }

        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (currentState == ConsoleState.Open)
            {                
                if(bounds.Y < 0)                
                    bounds.Y+=scrollSpeed;
                if (bounds.Y > 0)
                    bounds.Y = 0;

                //keyboard inputs
                if(keyboard.IsKeyDown(Keys.Enter) && prevKeyboard.IsKeyDown(Keys.Enter) && currentLine.Length > 0)
                {
                    ExcecutedCommands.Add(currentLine);
                    ExcecuteCommand(currentLine);
                    currentLine = "";
                    currentIndex = 0;
                    selectedIndex = 0;
                    selectedLength = 0;
                }
                if (keyboard.IsKeyDown(Keys.Delete) && prevKeyboard.IsKeyUp(Keys.Delete))                
                    if (currentIndex < currentLine.Length)
                        currentLine = currentLine.Remove(currentIndex, 1);

                if (keyboard.IsKeyDown(Keys.End) && prevKeyboard.IsKeyUp(Keys.End))
                {
                    currentIndex = currentLine.Length;
                    if (selectedLength > 0)
                    {
                        selectedIndex = currentIndex;
                        selectedLength = 0;
                    }                    
                }
                if (keyboard.IsKeyDown(Keys.Home) && prevKeyboard.IsKeyUp(Keys.Home))
                {
                    currentIndex = 0;
                    if (selectedLength > 0)
                    {
                        selectedIndex = currentIndex;
                        selectedLength = 0;
                    }
                }

                if (keyboard.IsKeyDown(Keys.Left) && prevKeyboard.IsKeyUp(Keys.Left))
                    if (currentIndex > 0)
                    {
                        if (keyboard.IsKeyDown(Keys.LeftControl))
                        {
                            if (selectedIndex + selectedLength != currentIndex)
                            {
                                selectedIndex = currentIndex - 1;
                                selectedLength = 1;
                            }else if(selectedIndex > 0)
                            {
                                selectedIndex--;
                                selectedLength++;
                            }
                        }
                        else
                        {
                            if (selectedLength > 0)
                            {
                                currentIndex = selectedIndex;
                                selectedIndex = 0;
                                selectedLength = 0;
                            }
                            currentIndex--;
                            displayBlink = true;
                            currentBlinkTime = 0;
                        }
                    }

                if (keyboard.IsKeyDown(Keys.Right) && prevKeyboard.IsKeyUp(Keys.Right))
                    if (currentIndex < currentLine.Length)
                    {
                        if (keyboard.IsKeyDown(Keys.LeftControl))
                        {
                            if (selectedIndex != currentIndex)
                            {
                                selectedIndex = currentIndex;
                                selectedLength = 1;
                            }
                            else
                            {                                
                                selectedLength++;
                            }
                        }
                        else
                        {
                            if (selectedLength > 0)
                            {
                                currentIndex = selectedIndex + selectedLength;
                                selectedIndex = 0;
                                selectedLength = 0;
                            }
                            currentIndex++;
                            displayBlink = true;
                            currentBlinkTime = 0;
                        }
                    }

                if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape))
                    currentState = ConsoleState.Closed;

                if(keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.A))
                {
                    selectedIndex = 0;
                    selectedLength = currentLine.Length;
                }
            }

            if(currentState == ConsoleState.Closed)
            {
                if (bounds.Y > -bounds.Height)
                    bounds.Y -= scrollSpeed;                               
            }

            //cursor
            if (currentBlinkTime < cursorBlinkRate) currentBlinkTime++;
            else
            {
                currentBlinkTime = 0;
                if (displayBlink == true) displayBlink = false;
                else displayBlink = true;
            }
            cursorString = "";
            for (int i = 0; i < currentIndex; i++)
                cursorString += ' ';
            cursorString += '|';

            prevState = currentState;
            prevKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bounds.Y > -bounds.Height)
            {   
                //background
                spriteBatch.Draw(backgroundTexture, bounds, Color.White);
                spriteBatch.Draw(backgroundTexture, new Rectangle(bounds.X, bounds.Y + bounds.Height - 30, bounds.Width, 30), Color.DarkGray);

                //excecuted
                if (ExcecutedCommands.Count > 0)
                {
                    int e_offSet = 0;
                    for (int i = 0; i < ExcecutedCommands.Count; i++)
                    {
                        spriteBatch.DrawString(consoleFont, ExcecutedCommands[i], new Vector2(bounds.X + 5, bounds.Y + 5 + e_offSet), Color.LightGray);
                        e_offSet += 20;
                    }
                }

                //type area
                int selectedBoxLength = selectedLength * 10;
                char[] splitLine = currentLine.ToCharArray();
                currentLine = "";
                int offset = 10;
                if (splitLine.Length > 48) offset -= (splitLine.Length - 48) * 10;
                spriteBatch.Draw(selectedBackgroundTexture, new Rectangle(bounds.X + 10 + (selectedIndex * 10), bounds.Y + bounds.Height - 25, selectedBoxLength, 20), Color.White);
                for (int i = 0; i < splitLine.Length; i++)
                {
                    if (consoleFont.Characters.Contains(splitLine[i]) && splitLine[i] != '\r' && splitLine[i] != '\n')
                    {
                        if (selectedLength > 0)
                        {
                            if (i >= selectedIndex && i < selectedIndex + selectedLength)
                                spriteBatch.DrawString(consoleFont, splitLine[i].ToString(), new Vector2(bounds.X + 10 + (i * offset), bounds.Y + bounds.Height - 23), Color.Black);
                            else spriteBatch.DrawString(consoleFont, splitLine[i].ToString(), new Vector2(bounds.X + 10 + (i * offset), bounds.Y + bounds.Height - 23), Color.White);
                        }
                        else spriteBatch.DrawString(consoleFont, splitLine[i].ToString(), new Vector2(bounds.X + 10 + (i * offset), bounds.Y + bounds.Height - 23), Color.White);
                        currentLine += splitLine[i];
                    }
                }                
                if (displayBlink && selectedLength == 0)spriteBatch.DrawString(consoleFont, cursorString, new Vector2(bounds.X + 5, bounds.Y + bounds.Height - 23), Color.White);
            }
        }
    }
}
