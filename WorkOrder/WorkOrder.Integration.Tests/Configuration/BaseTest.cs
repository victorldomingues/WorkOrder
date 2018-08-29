namespace WorkOrder.Integration.Tests.Configuration
{
    public abstract class BaseTest
    {
        protected TestContext Context;
        protected BaseTest()
        {
                Context = new TestContext();
        }
    }
}
