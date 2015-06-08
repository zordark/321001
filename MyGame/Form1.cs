using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyGame.GameClasses;

namespace MyGame
{
    public partial class Form1 : Form
    {
        private GameController _gameContr;
        private Graphics _gr;
        BufferedGraphics _bufferedGraphics;
        BufferedGraphicsContext _bufferedGraphicsContext;

        public Form1()
        {
            InitializeComponent();
            _gr = DrawArea.CreateGraphics();
            _bufferedGraphicsContext = new BufferedGraphicsContext();
            _bufferedGraphics = _bufferedGraphicsContext.Allocate(_gr, new Rectangle(0, 0, DrawArea.Width, DrawArea.Height));
            _gameContr = new GameController(DrawArea, _bufferedGraphics, (int)(DrawArea.Size.Width / 50), (int)(DrawArea.Size.Height / 50));
			WeaponDescription.Text = _gameContr.Player.WeaponProp.Description;
        }

		// При нажатии на DrawArea проверяется не окончена ли игра. 
		// Если нет то пытаемся переместить игрока
        private void DrawArea_Click(object sender, EventArgs e)
        {
            if (!_gameContr.GameIsEnded)
            {          
                var point = (e as MouseEventArgs).Location;
                _gameContr.ClickOnDrawArea(point);
				// Обновляем значение кнопки взятия объекта(На случай, если у игрока есть камень скорости)
				WorkWithPickUpButton();
                DrawArea.Invalidate();
            }
        }

		// Перерисовка DrawArea
        private void DrawArea_Paint(object sender, PaintEventArgs e)
        {
            _gameContr.Draw();
            _bufferedGraphics.Render();
        }

		// При удалении контрола из DrawArea, пытается закончить игру
		private void DrawArea_ControlRemoved(object sender, ControlEventArgs e)
		{
			_gameContr.GameEnd();
		}

		// При нахатии на кнопку EndTurnButton проверяется состояние игрока.
		//  Если игрок в допустимом состоянии то делается ход
        private void EndTurnButton_Click(object sender, EventArgs e)
        {
            if (_gameContr.Player.InOneOfCorrectStates())
            {
                _gameContr.EndTurn();
				//Если ход для игры был завершающим, то кнопки управления деактивируются
                if (_gameContr.GameIsEnded)
                {
                    EndTurnButton.Enabled = false;
                    PickUpItemButton.Enabled = false;
                }
				// Обновляем изображения камня в инвентаре(На случай, если у игрока был камень защиты и его уничтожили)
                pictureBoxStone.Invalidate();
                DrawArea.Invalidate();
            }
            else
            {
                MessageBox.Show("Необходимо сделать ход.");
            }
			if(!_gameContr.GameIsEnded) WorkWithPickUpButton();
        }

		// При нажатии на кнопку поднятия предмета, говорит игроку поднять игровой предмет(GameObject).
		// Обновляет клетки игрока, которые можно атаковать(На случай если было поднято оружие или камень атаки)
		private void PickUpItemButton_Click(object sender, EventArgs e)
		{
			_gameContr.Player.PickUpGameObj();
			_gameContr.GetStrikeCells(_gameContr.Player);
			// Деактивирует кнопку поднятия предмета и обновляет изображения инвентаря
			PickUpItemButton.Enabled = false;
			pictureBoxStone.Invalidate();
			pictureBoxSword.Invalidate();

			WeaponDescription.Text = _gameContr.Player.WeaponProp.Description;
			if(_gameContr.Player.StoneProp != null) MagicStoneDescription.Text = _gameContr.Player.StoneProp.Description;
		}

		// Начинает новую игру. Создает новый GameController и обновляет значения управляющих кнопок
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            DrawArea.Controls.Clear();
            _gameContr = new GameController(DrawArea, _bufferedGraphics, (int)(DrawArea.Size.Width / 50), (int)(DrawArea.Size.Height / 50));
            _gr.Clear(DrawArea.BackColor);
            EndTurnButton.Enabled = true;
            PickUpItemButton.Text = "Подобрать предмет";
            DrawArea.Invalidate();
        }

		// Проверяет стоит ли игрок на клетке с GameObject-ом и изменяет текст кнопки PickUpItemButton в зависимости от ситуации на поле
		private void WorkWithPickUpButton()
		{
			PickUpItemButton.Enabled = _gameContr.Player.CheckGameObjOnCell();
			if (PickUpItemButton.Enabled == true)
			{
				if (_gameContr.Player.Position.GameObj is Weapon)
				{
					PickUpItemButton.Text = "Сменить оружие";
				}
				else
				{
					if (_gameContr.Player.Position.GameObj is MagicStone && _gameContr.Player.StoneProp != null)
					{
						PickUpItemButton.Text = "Сменить камень";
					}
					else
					{
						PickUpItemButton.Text = "Подобрать камень";
					}
				}
			}
			else
			{
				PickUpItemButton.Text = "Подобрать предмет";
			}

		}

		// Отрисовывает оружие игрока в зоне инвентаря
        private void pictureBoxWeapon_Paint(object sender, PaintEventArgs e)
        {
            if (!_gameContr.GameIsEnded)
            {
                var tempImage = new Bitmap(_gameContr.Player.WeaponProp.WeaponView, pictureBoxSword.Size);
                e.Graphics.DrawImage(tempImage, new Point(0, 0));
            }
        }

		// Отрисовывает магический камень игрока в зоне инвентаря
        private void pictureBoxStone_Paint(object sender, PaintEventArgs e)
        {
            if (!_gameContr.GameIsEnded && _gameContr.Player.StoneProp != null)
            {
                var tempImage = new Bitmap(_gameContr.Player.StoneProp.StoneView, pictureBoxStone.Size);
                e.Graphics.DrawImage(tempImage, new Point(0, 0));
            }
            else
            {
                e.Graphics.Clear(Color.White);
            }
        }
    }
}
