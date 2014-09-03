using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;			 

namespace StyleADVISOR.SelectionManager
{
	public class SelectionManager : ISelectionManager
	{

		private readonly Dictionary<string, EManagerStatus> mManagers = new Dictionary<string, EManagerStatus>();
		private readonly object mLockObj = new object();

		#region Implementation of ISelectionManager

		/// <summary>
		/// Get the current estatus of the mananger
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		/// <returns>Status of the current Manager</returns>
		public EManagerStatus GetManagerStatus(string managerId)
		{
			EManagerStatus managerStatus;

			lock (mLockObj)
			{
				if (mManagers.TryGetValue(managerId, out managerStatus) == false)
				{
					Debug.WriteLine("The manager with the id {0} not exist", managerId);
					return EManagerStatus.None;
				}
			}

			return managerStatus;
		}

		/// <summary>
		/// Set the current status of the mananger
		/// </summary>
		/// <param name="managerId">Mananger Id</param>
		/// <param name="newStatus">New manager status</param>
		public void SetManagerStatus(string managerId, EManagerStatus newStatus)
		{
			EManagerStatus currentStatus = GetManagerStatus(managerId);
			if (currentStatus == EManagerStatus.None) return;
			if (currentStatus == newStatus) return;
			lock (mLockObj)
			{
				mManagers[managerId] = newStatus;
			}
			OnManagerStatusChanged(managerId, newStatus);
		}

		/// <summary>
		/// Togle the mananger status. It could be usefull for ui
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		public void TogleManagerStatus(string managerId)
		{
			EManagerStatus managerStatus = GetManagerStatus(managerId);
			EManagerStatus newManagerStatus;

			switch (managerStatus)
			{
				case EManagerStatus.Selected:
					newManagerStatus = EManagerStatus.Unselected;
					break;
				case EManagerStatus.Unselected:
					newManagerStatus = EManagerStatus.Selected;
					break;
				default:
					return;
			}
			SetManagerStatus(managerId, newManagerStatus);
		}

		/// <summary>
		/// Get the list of selected managers
		/// </summary>
		/// <returns>List of managers id</returns>
		public IEnumerable<string> GetSelectedManagers()
		{
			return mManagers.Where(i => i.Value == EManagerStatus.Selected).Select(i => i.Key);
		}

		/// <summary>
		/// Get the list of managers
		/// </summary>
		/// <returns>List of manager id</returns>
		public IEnumerable<string> GetManagers()
		{
			return mManagers.Keys;
		}

		/// <summary>
		/// Add a manager to the selection manager 
		/// By default the manager is added with the status "None"
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		public void AddManager(string managerId)
		{
			lock (mLockObj)
			{
				EManagerStatus managerStatus;
				if (mManagers.TryGetValue(managerId, out managerStatus))
				{
					Debug.WriteLine("The manager {0} already exist with the status {1}", managerId, managerStatus);
					return;
				}
				mManagers.Add(managerId, managerStatus);
			}
		}

		/// <summary>
		/// Add a manager to the selection manager and mark it as selected
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		public void AddSelectedManager(string managerId)
		{
			lock (mLockObj)
			{
				EManagerStatus managerStatus;
				if (mManagers.TryGetValue(managerId, out managerStatus))
				{
					Debug.WriteLine("The manager {0} already exist with the status {1}", managerId, managerStatus);
					if (managerStatus != EManagerStatus.Selected)
					{
						Debug.WriteLine("Changing the manager {0} status from {1} to {2}", managerId, managerStatus, EManagerStatus.Selected);
						SetManagerStatus(managerId, EManagerStatus.Selected);
					}
					return;
				}
				mManagers.Add(managerId, EManagerStatus.Selected);
			}
		}

		/// <summary>
		/// Add a list of managers to the selection manager
		/// By default the manager is added with the status "None"
		/// </summary>
		/// <param name="managerIds">List of managers id</param>
		public void AddManagers(IEnumerable<string> managerIds)
		{
			foreach (var manager in managerIds)
			{
				AddManager(manager);
			}
		}

		/// <summary>
		/// Add a list of managers to the selection manager and mark as selected
		/// </summary>
		/// <param name="managerIds">List of managers id</param>
		public void AddSelectedManagers(IEnumerable<string> managerIds)
		{
			foreach (var manager in managerIds)
			{
				AddSelectedManager(manager);
			}
		}

		/// <summary>
		/// Remove a manager from the selection manager.
		/// If the manager is selected unselect the manager and then remove it.
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		public void RemoveManager(string managerId)
		{
			EManagerStatus managerStatus;
			if (mManagers.TryGetValue(managerId, out managerStatus) == false)
			{
				Debug.WriteLine("The manager with the id {0} not exist", managerId);
			}
			lock (mLockObj)
			{
				if (managerStatus == EManagerStatus.Selected)
				{
					TogleManagerStatus(managerId);
				}
				mManagers.Remove(managerId);
			}
		}

		/// <summary>
		/// Manager status changed event
		/// </summary>
		public event EventHandler<ManagerStatusChangeEventArgs> ManagerStatusChanged;

		#endregion

		/// <summary>
		/// Raise the mananger status changed event
		/// </summary>
		/// <param name="managerId">Mananger Id</param>
		/// <param name="newStatus">New manager status</param>
		internal virtual void OnManagerStatusChanged(string managerId, EManagerStatus newStatus)
		{
			EventHandler<ManagerStatusChangeEventArgs> handler = ManagerStatusChanged;
			if (handler != null)
			{
				handler(this, new ManagerStatusChangeEventArgs(managerId, newStatus));
			}
		}
	}
}