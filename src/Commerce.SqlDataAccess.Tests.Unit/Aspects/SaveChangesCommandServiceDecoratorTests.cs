using Commerce.SqlDataAccess.Aspects;
using Commerce.SqlDataAccess.Tests.Unit.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess.Tests.Unit.Aspects
{
    public class SaveChangesCommandServiceDecoratorTests
    {
        [Fact]
        public void CreateWithNullContextWillThrow()
        {
            // Act
            Action action = () => new SaveChangesCommandServiceDecorator<object>(
                context: null!,
                decoratee: new StubCommandService<object>());

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void CreateWithNullDecorateeWillThrow()
        {
            Action action = () => new SaveChangesCommandServiceDecorator<object>(
                context: new SpyCommerceContext(),
                decoratee: null!
             );

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ExecuteWillSaveChangesAfterInvokingTheDecoratee()
        {
            var context = new SpyCommerceContext();
            var decoratee = new SpyCommandService<object>();
            var sut = new SaveChangesCommandServiceDecorator<object>(
                context: context,
                decoratee: decoratee
                );

            decoratee.Executed += () =>
            {
                Assert.False(context.HasChanged);
            };  

            await sut.ExecuteAsync(command: new object());

            Assert.True(decoratee.ExecutedOnce);
            Assert.True(context.HasChanged);
        }
    }
}
