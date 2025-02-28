using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;

namespace LifeGame;

public partial class MainForm : Form
{
	private const int BoardSize = 200;
	private readonly Random _random = new();
	private readonly Size _cellSize = new(4, 4);

	private readonly Timer _timer = new();

	public MainForm()
	{
		InitializeComponent();
		// пока по центру, потом можно запоминать и восстанавливать
		StartPosition = FormStartPosition.CenterScreen;
		// запрещаем менять размер окна (так проще)
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Size = new Size(_cellSize.Width * BoardSize, _cellSize.Height * BoardSize);

		// таймер для обновления, если хочется побыстрее - 40мс ≈ 25 кадров в секунду
		_timer.Interval = 1000;
		_timer.Tick += TimerOnTick;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		var g = e.Graphics;
		using var cellBrush = new SolidBrush(Color.Red);

		for (int i = 0; i < 100; i++)
		{
			var x = _random.Next(0, BoardSize - 1);
			var y = _random.Next(0, BoardSize - 1);
			g.FillRectangle(cellBrush, x * _cellSize.Width, y * _cellSize.Height, _cellSize.Width, _cellSize.Height);
		}
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
		/* заставляет форму перерисовывать, то есть получаем чёрный прямоугольник за счёт того, что BackColor = Black,
		 * а остальную отрисовку делаем в OnPaint
		 */
		Invalidate();
	}
}