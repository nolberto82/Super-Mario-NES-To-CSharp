using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarioBTXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public partial class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        SpriteFont font;
        Texture2D screen;
        Texture2D pixel;
        Texture2D sprites;
        Texture2D background;
        KeyboardState ks;
        KeyboardState oks;
        SoundEffectInstance song;
        Matrix matrix;
        Dictionary<int, SoundEffect> sfx1;
        Dictionary<int, SoundEffect> sfx2;
        Dictionary<int, SoundEffect> mus1;
        Dictionary<int, SoundEffect> mus2;
        string msg = "";
        int[] buffer;
        const int Width = 960;
        const int Height = 544;
        const int GameWidth = 256;
        const int GameHeight = 240;
        static bool[] btnState;
        byte[] file;
        byte[] palette;
        char[] hexchars;
        int WRBP;
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;

        int[,] nametableMirrorLookup = {
    {0, 0, 1, 1}, // Vertical
    {0, 1, 0, 1}  // Horizontal
};

        int[] paletteRGB =
        {
        0x7c7c7c,
        0x0000fc,
        0x0000bc,
        0x4428bc,
        0x940084,
        0xa80020,
        0xa81000,
        0x881400,
        0x503000,
        0x007800,
        0x006800,
        0x005800,
        0x004058,
        0x000000,
        0x000000,
        0x000000,
        0xbcbcbc,
        0x0078f8,
        0x0058f8,
        0x6844fc,
        0xd800cc,
        0xe40058,
        0xf83800,
        0xe45c10,
        0xac7c00,
        0x00b800,
        0x00a800,
        0x00a844,
        0x008888,
        0x000000,
        0x000000,
        0x000000,
        0xf8f8f8,
        0x3cbcfc,
        0x6888fc,
        0x9878f8,
        0xf878f8,
        0xf85898,
        0xf87858,
        0xfca044,
        0xf8b800,
        0xb8f818,
        0x58d854,
        0x58f898,
        0x00e8d8,
        0x787878,
        0x000000,
        0x000000,
        0xfcfcfc,
        0xa4e4fc,
        0xb8b8f8,
        0xd8b8f8,
        0xf8b8f8,
        0xf8a4c0,
        0xf0d0b0,
        0xfce0a8,
        0xf8d878,
        0xd8f878,
        0xb8f8b8,
        0xb8f8d8,
        0x00fcfc,
        0xf8d8f8,
        0x000000,
        0x000000
    };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            matrix = Matrix.CreateScale(2f, 2.26f, 1f);
            buffer = new int[256 * 240];
            screen = new Texture2D(GraphicsDevice, GameWidth, GameHeight);
            btnState = new bool[8];
            palette = new byte[32];
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            SwapArray();
            sfx1 = new Dictionary<int, SoundEffect>();
            sfx2 = new Dictionary<int, SoundEffect>();
            mus1 = new Dictionary<int, SoundEffect>();
            mus2 = new Dictionary<int, SoundEffect>();
            //LoadMusic();
            hexchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            file = File.ReadAllBytes("smb.nes");
            ResetGame();
            base.Initialize();
        }

        void ResetGame()
        {
            Init(file);
            Label8000();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sb = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("PressStart");
            sprites = Content.Load<Texture2D>("sprites");
            background = Content.Load<Texture2D>("background");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // 1 Second has passed
            if (_elapsed_time >= 1000.0f)
            {
                _fps = _total_frames;
                _total_frames = 0;
                _elapsed_time = 0;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ks = Keyboard.GetState();

            GetInput(ks);

            bool f1 = oks.IsKeyUp(Keys.F1) && ks.IsKeyDown(Keys.F1);
            bool f2 = oks.IsKeyUp(Keys.F2) && ks.IsKeyDown(Keys.F2);
            bool f3 = oks.IsKeyUp(Keys.F3) && ks.IsKeyDown(Keys.F3);
            bool f5 = oks.IsKeyUp(Keys.F5) && ks.IsKeyDown(Keys.F5);
            bool f11 = oks.IsKeyUp(Keys.F11) && ks.IsKeyDown(Keys.F11);

            if (f1)
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream("state.bin", FileMode.Create, FileAccess.Write)))
                {
                    for (int i = 0; i < ram.Length; i++)
                        bw.Write(ram[i]);

                    for (int i = 0; i < vram.Length; i++)
                        bw.Write(vram[i]);
                }
            }

            if (f2)
            {
                using (BinaryReader br = new BinaryReader(new FileStream("state.bin", FileMode.Open, FileAccess.Read)))
                {
                    ram = br.ReadBytes(0x10000);
                    vram = br.ReadBytes(0x4000);
                }
            }

            //if (ram[0x201] == 0x86)
            //    System.Diagnostics.Debugger.Break();

            //if (msg != "")
            //    WRBP = int.Parse(msg, System.Globalization.NumberStyles.HexNumber);

            if (f3)
            {
                ResetGame();
                return;
            }

            if (f5)
            {
               // using (BinaryWriter bw = new BinaryWriter(new FileStream("ram.bin", FileMode.Create, FileAccess.Write)))
               // {
               //     for (int i = 0; i < ram.Length; i++)
                //        bw.Write(ram[i]);
                //}
            }

            Label8082();

            //ram[0x79e] = 0x08;
            //ram[0x75a] = 0x09;
            //ram[0x754] = 0x00;
            ram[0x787] = 0x18;

            //Copy vram 0x2000 to 0x2800 for scrolling
            Buffer.BlockCopy(vram, 0x2000, vram, 0x2800, 0x800);

            oks = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _total_frames++;

            GraphicsDevice.Clear(Color.Black);
            //GraphicsDevice.SetRenderTarget(renderTarget);

            //GraphicsDevice.Textures[0] = null;
            //GraphicsDevice.SetRenderTarget(renderTarget);
            sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);


            Render();
            //DrawSprites();
            //DrawBackground();

            screen.SetData<int>(buffer);
            sb.Draw(screen, new Rectangle(0, 0, GameWidth, GameHeight), Color.White);
            //DrawRegs(700, 30);
            //DrawBreakpoint();
            //DrawVariable();

            sb.End();
            //GraphicsDevice.SetRenderTarget(null);


            sb.Begin();
            //DrawVariable();
            //DrawFPS();
            //Rectangle titleSafeRectangle = new Rectangle(0, 0, screen.Width*2, screen.Height*2);
            //DrawBorder(titleSafeRectangle, 1, Color.White, sb);
            sb.End();

            base.Draw(gameTime);
        }

        void DrawBackground()
        {
            int address = 0x2000;
            for (int ypos = 0; ypos < 30; ypos++)
            {
                for (int xpos = 0; xpos < 32; xpos++)
                {
                    int scrollX = ppuScrollX + ((ram[0x2000] & 1) == 1 ? 256 : 0);
                    int xOffset = scrollX / 8;
                    int yOffset = (scrollX + 256) / 8;

                    int xPixel = (int)xpos + xOffset;
                    int yPixel = (int)ypos + yOffset;

                    byte tile = vram[address++];
                    sb.Draw(background, new Rectangle(xpos * 8, ypos * 8, 8, 8), new Rectangle((tile % 16) * 8, (tile / 16) * 8, 8, 8), Color.White);
                }
            }
        }

        void DrawSprites()
        {
            for (int i = 0; i < 64; i++)
            {
                SpriteEffects effects = SpriteEffects.None;
                byte ypos = (byte)ram[0x200 + (i * 4)];
                byte index = (byte)ram[0x200 + (i * 4) + 1];
                byte attrib = (byte)ram[0x200 + (i * 4) + 2];
                byte xpos = (byte)ram[0x200 + (i * 4) + 3];

                if ((attrib & 0x40) > 0)
                    effects = SpriteEffects.FlipHorizontally;
                if ((attrib & 0x80) > 0)
                    effects |= SpriteEffects.FlipVertically;

                sb.Draw(sprites, new Rectangle(xpos, ypos, 8, 8), new Rectangle((index % 16) * 8, (index / 16) * 8, 8, 8), Color.White, 0f, Vector2.Zero, effects, 0f);
            }
        }

        void GetInput(KeyboardState ks)
        {
            btnState[0] = ks.IsKeyDown(Keys.Right);
            btnState[1] = ks.IsKeyDown(Keys.Left);
            btnState[2] = ks.IsKeyDown(Keys.Down);
            btnState[3] = ks.IsKeyDown(Keys.Up);
            btnState[4] = ks.IsKeyDown(Keys.Enter);
            btnState[5] = ks.IsKeyDown(Keys.Space);
            btnState[6] = ks.IsKeyDown(Keys.Z);
            btnState[7] = ks.IsKeyDown(Keys.X);
        }

        void DrawFPS()
        {
            sb.DrawString(font, string.Format("FPS={0}", _fps), new Vector2(50, 5), Color.White);
        }

        void DrawVariable()
        {
            sb.DrawString(font, String.Format("{0:X2}", ram[0x79e]), new Vector2(5, 5), Color.White);
        }

        void DrawBreakpoint()
        {
            if (msg != "")
                sb.DrawString(font, string.Format("{0:S}", msg), new Vector2(542, 20), Color.White);
        }

        void DrawRegs(int xx, int yy)
        {
            sb.DrawString(font, String.Format("BP:"), new Vector2(xx, yy - 10), Color.White);
            sb.DrawString(font, String.Format(" A: {0:X2}", a), new Vector2(xx, yy + 10), Color.White);
            sb.DrawString(font, String.Format(" X: {0:X2}", x), new Vector2(xx, yy + 30), Color.White);
            sb.DrawString(font, String.Format(" Y: {0:X2}", y), new Vector2(xx, yy + 50), Color.White);
            sb.DrawString(font, String.Format("PA: {0:X4}", ppuAddr), new Vector2(xx, yy + 70), Color.White);
            sb.DrawString(font, String.Format("{0:S}{1:S}{2:S}", c ? "C" : "", z ? "Z" : "", n ? "N" : ""), new Vector2(xx, yy + 90), Color.White);
        }

        public static bool[] GetJoyState()
        {
            return btnState;
        }

        void Render()
        {
            for (int index = 0; index < GameWidth * GameHeight; index++)
            {
                buffer[index] = paletteRGB[vram[0x3f00]];
            }
            //Draw sprites behind the background
            if ((ram[0x2001] & 0x10) > 0)
            {
                for (int i = 0; i < 64; i++)
                {
                    byte yy = (byte)ram[0x200 + (i * 4)];
                    byte index = (byte)ram[0x200 + (i * 4) + 1];
                    byte attributes = (byte)ram[0x200 + (i * 4) + 2];
                    byte xx = (byte)ram[0x200 + (i * 4) + 3];

                    if ((attributes & 0x20) == 0)
                        continue;

                    if (yy >= 0xef || xx >= 0xf9)
                        continue;

                    byte tile = (byte)(index + (ram[0x2000] & ((1 << 3) > 0 ? 256 : 0)));
                    bool flipX = (attributes & 0x40) > 0;
                    bool flipY = (attributes & 0x80) > 0;

                    for (int row = 0; row < 8; row++)
                    {
                        int plane1 = vram[tile * 16 + row];
                        int plane2 = vram[tile * 16 + row + 8];

                        for (int column = 0; column < 8; column++)
                        {
                            byte bit = (byte)(1 << column);
                            int paletteIndex = ((((plane1 & bit) > 0) ? 1 : 0) + ((plane2 & bit) > 0 ? 2 : 0));
                            int colorIndex = vram[0x3f00 + 0x10 + (attributes & 3) * 4 + paletteIndex];

                            if (paletteIndex == 0)
                                continue;
                            long pixel = 0x000000 | paletteRGB[colorIndex];

                            int xOffset = 7 - column;
                            if (flipX)
                            {
                                xOffset = column;
                            }
                            int yOffset = row;
                            if (flipY)
                            {
                                yOffset = 7 - row;
                            }

                            int xPixel = (int)xx + xOffset;
                            int yPixel = (int)yy + yOffset;
                            if (xPixel < 0 || xPixel >= 256 || yPixel < 0 || yPixel >= 240)
                                continue;
                            buffer[yPixel * 256 + xPixel] = (int)pixel;
                        }
                    }
                }
            }
            //Draw the background (nametable)
            if ((ram[0x2001] & (1 << 3)) > 0)
            {
                int scrollX = ppuScrollX + ((ram[0x2000] & 1) == 1 ? 256 : 0);
                int xMin = scrollX / 8;
                int xMax = (scrollX + 256) / 8;

                //WriteText(xMin.ToString("X3"), 2, 0, Color.White);

                //Console.WriteLine(scrollX);


                for (int xx = 0; xx < 32; xx++)
                {
                    for (int yy = 0; yy < 4; yy++)
                    {
                        renderTile(buffer, 0x2000 + 32 * yy + xx, xx * 8, yy * 8);
                    }
                }
                for (int xx = xMin; xx <= xMax; xx++)
                {
                    for (int yy = 4; yy < 30; yy++)
                    {
                        // Determine the index of the tile to render
                        int index = 0;
                        if (xx < 32)
                        {
                            index = 0x2000 + 32 * yy + xx;
                        }
                        else if (xx < 64)
                        {
                            index = 0x2400 + 32 * yy + (xx - 32);
                        }
                        else
                        {
                            index = 0x2800 + 32 * yy + (xx - 64);
                        }

                        // Render the tile
                        renderTile(buffer, index, (xx * 8) - (int)scrollX, (yy * 8));
                    }
                }
            }

            //Draw sprites in front of the background
            if ((ram[0x2001] & 0x10) > 0)
            {
                for (int i = 0; i < 64; i++)
                {
                    byte yy = (byte)ram[0x200 + (i * 4)];
                    byte index = (byte)ram[0x200 + (i * 4) + 1];
                    byte attributes = (byte)ram[0x200 + (i * 4) + 2];
                    byte xx = (byte)ram[0x200 + (i * 4) + 3];

                    if ((attributes & 0x20) > 0)
                        continue;

                    if (yy >= 0xef || xx >= 0xf9)
                        continue;

                    int tile = index + (ram[0x2000] & ((1 << 3) > 0 ? 256 : 0));
                    bool flipX = (attributes & 0x40) > 0;
                    bool flipY = (attributes & 0x80) > 0;

                    for (int row = 0; row < 8; row++)
                    {
                        int plane1 = vram[tile * 16 + row];
                        int plane2 = vram[tile * 16 + row + 8];

                        for (int column = 0; column < 8; column++)
                        {
                            byte bit = (byte)(1 << column);
                            int paletteIndex = ((((plane1 & bit) > 0) ? 1 : 0) + ((plane2 & bit) > 0 ? 2 : 0));
                            int colorIndex = vram[0x3f10 + (attributes & 3) * 4 + paletteIndex];

                            if (paletteIndex == 0)
                                continue;
                            long pixel = 0x000000 | paletteRGB[colorIndex];

                            int xOffset = 7 - column;
                            if (flipX)
                            {
                                xOffset = column;
                            }
                            int yOffset = row;
                            if (flipY)
                            {
                                yOffset = 7 - row;
                            }

                            int xPixel = (int)xx + xOffset;
                            int yPixel = (int)yy + yOffset;
                            if (xPixel < 0 || xPixel >= 256 || yPixel < 0 || yPixel >= 240)
                                continue;
                            buffer[yPixel * 256 + xPixel] = (int)pixel;
                        }
                    }
                }
            }
        }

        void renderTile(int[] buffer, int index, int xOffset, int yOffset)
        {
            //if (index == 0x2085) System.Diagnostics.Debugger.Break();

            int tile = vram[index] | ((ram[0x2000] & (1 << 4)) > 0 ? 256 : 0);
            int attribute = getAttributeTableValue(index);

            for (int row = 0; row < 8; row++)
            {
                int plane1 = vram[tile * 16 + row];
                int plane2 = vram[tile * 16 + row + 8];
                for (int column = 0; column < 8; column++)
                {
                    int paletteIndex = (((plane1 & (1 << column)) > 0 ? 1 : 0) + ((plane2 & (1 << column)) > 0 ? 2 : 0));
                    int colorIndex = vram[0x3f00 + (attribute * 4 + paletteIndex)];

                    if (paletteIndex == 0)
                        continue;
                    long pixel = 0x000000 | paletteRGB[colorIndex];

                    int xx = (xOffset + (7 - column));
                    int yy = (yOffset + row);
                    if (xx < 0 || xx >= 256 || yy < 0 || yy >= 240)
                    {
                        continue;
                    }
                    buffer[yy * 256 + xx] = (int)pixel;
                }

            }
        }

        int getAttributeTableValue(int nametableAddress)
        {
            nametableAddress = getNametableIndex(nametableAddress);

            int row = ((nametableAddress & 0x3e0) >> 5) / 4;
            int col = (nametableAddress & 0x1f) / 4;

            // Determine the 16x16 metatile for the 8x8 tile addressed
            int shift = ((nametableAddress & (1 << 6)) > 0 ? 4 : 0) + ((nametableAddress & (1 << 1)) > 0 ? 2 : 0);

            // Determine the offset into the attribute table
            int offset = (nametableAddress & 0xc00) + 0x400 - 64 + (row * 8 + col);

            // Determine the attribute table value
            return (vram[0x2000 + offset] & (0x3 << shift)) >> shift;
        }

        int getNametableIndex(int address)
        {
            address = (address - 0x2000) % 0x1000;
            int table = address / 0x400;
            int offset = address % 0x400;
            int mode = 1; // Mirroring mode for Super Mario Bros.
            return (nametableMirrorLookup[mode, table] * 0x400 + offset) % 2048;
        }

        void WriteText(string text, int xx, int yy, Color c)
        {
            sb.DrawString(font, text, new Vector2(xx, yy), c);
        }

        void SwapArray()
        {
            for (int i = 0; i < paletteRGB.Length; i++)
            {
                paletteRGB[i] = ReverseBytes((int)paletteRGB[i]);
            }
        }

        int ReverseBytes(int value)
        {
            return (value & 0x0000FF) << 16 | (value & 0x00FF00) |
            (value & 0xFF0000) >> 16;
        }
    }
}
