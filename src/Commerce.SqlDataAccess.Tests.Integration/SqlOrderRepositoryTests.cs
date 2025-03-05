using Commerce.SqlDataAccess;


namespace Commerce.SqlDataAccess.Tests.Integration
{
    public class SqlOrderRepositoryTests
    {
        [Fact]
        public void CreateWithNullContextWillThrow()
        {
            Action action = () => new SqlOrderRepository(context: null!);

            Assert.Throws<ArgumentNullException>(action);
        }
    }
}