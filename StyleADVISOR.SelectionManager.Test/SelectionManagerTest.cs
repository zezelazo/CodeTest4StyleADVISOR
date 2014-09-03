 
using System.Collections.Generic;				  
using NUnit.Framework;

namespace StyleADVISOR.SelectionManager.Test
{
	[TestFixture]
	public class SelectionManagerTest
	{
		private readonly SelectionManager mSelectionManager;
		private readonly List<string> mManagers;
		private readonly List<string> mSelectedManagers;

		public SelectionManagerTest()
		{
			mSelectionManager = new SelectionManager();
			mManagers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
			mSelectedManagers = new List<string> { "1", "2"};
		}

		[Test]
		public void AddAndGetSelectedManagers()
		{
			mSelectionManager.AddManagers(mManagers);
			var result = mSelectionManager.GetManagers();
			Assert.AreEqual(mManagers, result);
		}

		[Test]
		public void AddAndGetManagers()
		{
			mSelectionManager.AddSelectedManagers(mSelectedManagers);
			var result = mSelectionManager.GetSelectedManagers();
			Assert.AreEqual(mSelectedManagers,result);
		}		  

	}
}
