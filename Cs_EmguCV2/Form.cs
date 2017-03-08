using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Cs_EmguCV2
{
	public partial class FormMain : Form
	{
		#region Singleton

		protected FormMain()
		{
			InitializeComponent();
		}

		private static FormMain _instance;

		public static FormMain GetInstance()
		{
			return _instance ?? (_instance = new FormMain());
		}

		#endregion

		#region Func

		private void WriteNum(object argNum)
		{
			Console.WriteLine((int)argNum);
		}


		private void CaptureToImage(object arg)
		{
			Mat imageMat = _captureTarget.QueryFrame();
			Image<Bgr, byte> image = imageMat.ToImage<Bgr, byte>();
			Size pictureBoxSize = pictureBox.Size;
			pictureBox.Image = image.Resize(pictureBoxSize.Width, pictureBoxSize.Height, Inter.Linear).Bitmap;
			imageMat.Dispose();
			image.Dispose();
		}

		#endregion

		private Capture _captureTarget;
		private void FormMain_Load(object sender, EventArgs e)
		{
			TimerManager tempTimerManager = TimerManager.GetInstance();
			tempTimerManager.Init();
			_captureTarget = new Capture();
			_captureTarget.Start();
			try
			{
				Guid tempGuid1 = tempTimerManager.AddTimer(CaptureToImage, null, 50);
			}
			catch (TimerStackException exception)
			{
				Console.WriteLine(exception);
			}
			catch (TimerNotFindException exception)
			{
				Console.WriteLine(exception);
			}

		}
	}
}
