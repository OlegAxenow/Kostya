using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace LifeGame;

public partial class MainForm : Form
{
	/// <summary>
	/// Таймер для обновления, если хочется побыстрее - 40мс ≈ 25 кадров в секунду. </summary>
	private const int TimerIntervalMs = 1000;

	/// <summary>
	/// Размер поля в ячейках (поле квадратное). </summary>
	private const int BoardSize = 200;

	/// <summary>
	/// Высота ячейки в пикселях. </summary>
	private const int CellHeight = 4;

	/// <summary>
	/// Ширина ячейки в пикселях. </summary>
	private const int CellWidth = 4;

	private readonly Random _random = new();
	private readonly Timer _timer = new();

	public MainForm()
	{
		InitializeComponent();
		// пока по центру, потом можно запоминать и восстанавливать
		StartPosition = FormStartPosition.CenterScreen;

		Size = new Size(CellWidth * BoardSize, CellHeight * BoardSize);

		_timer.Interval = TimerIntervalMs;
		_timer.Tick += TimerOnTick;
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		_timer.Start();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		_timer.Stop();
	}

	private void TimerOnTick(object? sender, EventArgs e)
	{
		/* Invalidate заставляет форму перерисоваться и получаем белый прямоугольник за счёт того, что BackColor = WhiteSmoked;
		 * остальная отрисовка поверх чёрного фона будет в OnPaint */
		Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		var g = e.Graphics;
		using var cellBrush = new SolidBrush(Color.Black);

		for (var i = 0; i < 100; i++)
		{
			var x = _random.Next(0, BoardSize - 1);
			var y = _random.Next(0, BoardSize - 1);
			g.FillRectangle(cellBrush, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
		}
	}
}