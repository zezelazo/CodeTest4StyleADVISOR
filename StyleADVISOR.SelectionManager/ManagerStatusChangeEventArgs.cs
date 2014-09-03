using System;

namespace StyleADVISOR.SelectionManager
{
	public class ManagerStatusChangeEventArgs : EventArgs
	{
		public ManagerStatusChangeEventArgs(string manager, EManagerStatus status)
		{
			Status = status;
			Manager = manager;
		}

		public string Manager { get; private set; }
		public EManagerStatus Status { get; private set; }
	}
}