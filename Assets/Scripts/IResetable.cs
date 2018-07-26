using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
	public interface IResetable
	{
		void Reset();

		void SelfRegisterAsResetable();
	}
}
