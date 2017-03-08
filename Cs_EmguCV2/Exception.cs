using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cs_EmguCV2
{
	class TimerStackException : ApplicationException
	{
		public TimerStackException()
		{
			Message = "Cannot add more timers any more";
		}

		public override string Message { get; }
	}

	class TimerNotFindException : ApplicationException
	{
		public TimerNotFindException()
		{
			Message = "Cannot find the timer corresponds to GUID";
		}

		public override string Message { get; }
	}
}
