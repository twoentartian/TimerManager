using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cs_EmguCV2
{
	class TimerManager
	{
		#region Singleton

		protected TimerManager()
		{
			
		}

		private static TimerManager _instance;

		public static TimerManager GetInstance()
		{
			return _instance ?? (_instance = new TimerManager());
		}

		#endregion

		private class SingleTimer
		{
			public bool SignOccupy = false;
			public Timer TimerTarget;
			public Guid TimerGuid;
		}

		private SingleTimer[] TimerList = new SingleTimer[3];

		public void Init()
		{
			for (int i = 0; i < TimerList.Length; i++)
			{
				TimerList[i] = new SingleTimer();
			}
		}

		public Guid AddTimer(TimerCallback argMethod, object arg, int period)
		{
			IEnumerable<SingleTimer> freeTimers = from singleTimer in TimerList where singleTimer.SignOccupy == false select singleTimer;
			foreach (var tempTimer in freeTimers)
			{
				SingleTimer freeTimer = tempTimer;
				freeTimer.TimerTarget = new Timer(argMethod, arg, 0, period);
				freeTimer.SignOccupy = true;
				freeTimer.TimerGuid = Guid.NewGuid();
				return freeTimer.TimerGuid;
			}
			throw new TimerStackException();
		}

		public void DelAllTimer()
		{
			foreach (var timer in TimerList)
			{
				if (timer.SignOccupy)
				{
					timer.TimerGuid = Guid.Empty;
					timer.TimerTarget.Dispose();
					timer.TimerTarget = null;
					timer.SignOccupy = false;
				}
			}
		}

		public void DelTimer(Guid argGuid)
		{
			foreach (var timer in TimerList)
			{
				if (timer.TimerGuid == argGuid)
				{
					timer.TimerGuid = Guid.Empty;
					timer.TimerTarget.Dispose();
					timer.TimerTarget = null;
					timer.SignOccupy = false;
					return;
				}
			}
			throw new TimerNotFindException();
		}
	}
}
