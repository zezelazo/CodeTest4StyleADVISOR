using System;
using System.Collections.Generic;

namespace StyleADVISOR.SelectionManager
{
	public interface ISelectionManager
	{
		/// <summary>
		/// Get the current estatus of the mananger
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		/// <returns>Status of the current Manager</returns>
		EManagerStatus GetManagerStatus(string managerId);

		/// <summary>
		/// Set the current status of the mananger
		/// </summary>
		/// <param name="managerId">Mananger Id</param>
		/// <param name="newStatus">New manager status</param>
		void SetManagerStatus(string managerId, EManagerStatus newStatus);

		/// <summary>
		/// Togle the mananger status. It could be usefull for ui
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		void TogleManagerStatus(string managerId);

		/// <summary>
		/// Get the list of selected managers
		/// </summary>
		/// <returns>List of managers id</returns>
		IEnumerable<String> GetSelectedManagers();

		/// <summary>
		/// Get the list of managers
		/// </summary>
		/// <returns>List of manager id</returns>
		IEnumerable<String> GetManagers();

		/// <summary>
		/// Add a manager to the selection manager 
		/// By default the manager is added with the status "None"
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		void AddManager(string managerId);

		/// <summary>
		/// Add a manager to the selection manager and mark it as selected
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		void AddSelectedManager(string managerId);

		/// <summary>
		/// Add a list of managers to the selection manager
		/// By default the manager is added with the status "None"
		/// </summary>
		/// <param name="managerIds">List of managers id</param>
		void AddManagers(IEnumerable<string> managerIds);

		/// <summary>
		/// Add a list of managers to the selection manager and mark as selected
		/// </summary>
		/// <param name="managerIds">List of managers id</param>
		void AddSelectedManagers(IEnumerable<string> managerIds);

		/// <summary>
		/// Remove a manager from the selection manager.
		/// If the manager is selected unselect the manager and then remove it.
		/// </summary>
		/// <param name="managerId">Manager Id</param>
		void RemoveManager(string managerId);


		/// <summary>
		/// Manager status changed event
		/// </summary>
		event EventHandler<ManagerStatusChangeEventArgs> ManagerStatusChanged;
	}
}
