using System;
using System.Linq;
using NUnit.Framework;

	[TestFixture]
	public class ClassTests : MyBaseClass
	{
		[Test]
		public void EnsureModuleInitIsCalled()
		{
			Assert.AreEqual("true", Environment.GetEnvironmentVariable("ModuleInitializer"));
		}
		[Test]
		public void EnsureAssemblyIsLoaded()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assert.IsTrue(assemblies.Any(x => x.GetName().Name == "AssemblyWithClassAndModuleInit"));
		}
	}