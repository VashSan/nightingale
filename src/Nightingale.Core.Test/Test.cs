using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Nightingale.Core.Test
{
	[TestFixture]
	class Test
	{
		[Test]
		public void FirstTest()
		{
			Assert.That( true, Is.True );
		}
	}
}
